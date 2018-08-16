using System.IO;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace jce.DataAccess.Core.FactoryDb_Migration
{
    internal class JceDbContextFactory : IDesignTimeDbContextFactory<JceDbContext>
    {
        readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        JceDbContext IDesignTimeDbContextFactory<JceDbContext>.CreateDbContext(string[] args)
                {
                    
                    var builder = new DbContextOptionsBuilder<JceDbContext>();
                    builder.UseSqlServer(_configuration.GetConnectionString("jce_live"));
                    builder.EnableSensitiveDataLogging();
        
                    return new JceDbContext(builder.Options);
                }
    }
}
