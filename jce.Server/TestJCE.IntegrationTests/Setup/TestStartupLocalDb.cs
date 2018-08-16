using jce.BackOffice;
using jce.DataAccess.Core.dbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;

namespace TestJCE.IntegrationTests.Tests
{
    public class TestStartupLocalDb : Startup, IDisposable
    {
        public TestStartupLocalDb(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {
        }

        public override void SetUpDataBase(IServiceCollection services)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "TestDb",

            };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqlConnection(connectionString);
            services
              .AddEntityFrameworkSqlServer()
              .AddDbContext<JceDbContext>(
                options => options.UseSqlServer(connection)
              );
        }

        public override void EnsureDatabaseCreated(JceDbContext dbContext)
        {
            DestroyDatabase();
            CreateDatabase();
        }

        public void Dispose()
        {
            DestroyDatabase();
        }

        private static void CreateDatabase()
        {
            ExecuteSqlCommand(Master, $@"
			  IF(db_id(N'VS2017Db_TokenAuthWebApiCore.Server.Local') IS NULL)
			  BEGIN
                CREATE DATABASE [VS2017Db_TokenAuthWebApiCore.Server.Local]
                ON (NAME = 'VS2017Db_TokenAuthWebApiCore.Server.Local',
                FILENAME = '{Filename}')
              END");

            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "TestDb",

                IntegratedSecurity = true,
            };
            var connectionString = connectionStringBuilder.ToString();

            var optionsBuilder = new DbContextOptionsBuilder<JceDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new JceDbContext(optionsBuilder.Options))
            {
                context.Database.Migrate();
                context.SaveChanges();
            }
        }

        private static void DestroyDatabase()
        {
            var fileNames = ExecuteSqlQuery(Master, @"
                SELECT [physical_name] FROM [sys].[master_files]
                WHERE [database_id] = DB_ID('VS2017Db_TokenAuthWebApiCore.Server.Local')",
                row => (string)row["physical_name"]);

            if (fileNames.Any())
            {
                ExecuteSqlCommand(Master, @"
                    ALTER DATABASE [VS2017Db_TokenAuthWebApiCore.Server.Local] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    EXEC sp_detach_db 'VS2017Db_TokenAuthWebApiCore.Server.Local', 'true'");

                fileNames.ForEach(File.Delete);
            }
            if (File.Exists(Filename))
            {
                File.Delete(Filename);
            }
            if (File.Exists(LogFilename))
            {
                File.Delete(LogFilename);
            }
        }

        private static void ExecuteSqlCommand(
            SqlConnectionStringBuilder connectionStringBuilder,
            string commandText)
        {
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static List<T> ExecuteSqlQuery<T>(
            SqlConnectionStringBuilder connectionStringBuilder,
            string queryText,
            Func<SqlDataReader, T> read)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = queryText;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(read(reader));
                        }
                    }
                }
            }
            return result;
        }

        private static SqlConnectionStringBuilder Master =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "master",
                IntegratedSecurity = true
            };

        private static string Filename => Path.Combine(
            Path.GetDirectoryName(
                typeof(TestStartupLocalDb).GetTypeInfo().Assembly.Location),
            "VS2017Db_TokenAuthWebApiCore.Server.Local.mdf");

        private static string LogFilename => Path.Combine(
            Path.GetDirectoryName(
                typeof(TestStartupLocalDb).GetTypeInfo().Assembly.Location),
            "VS2017Db_TokenAuthWebApiCore.Server.Local_log.ldf");
    }
}
