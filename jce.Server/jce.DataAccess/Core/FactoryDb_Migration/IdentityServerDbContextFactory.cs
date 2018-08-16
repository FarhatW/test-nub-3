using System.IO;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace jce.DataAccess.Core.FactoryDb_Migration
{
    public class IdentityServerDbContextFactory : IDesignTimeDbContextFactory<IdentityServerDbContext>
    {
        readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();


        public IdentityServerDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<IdentityServerDbContext>();
            builder.UseSqlServer(_configuration.GetConnectionString("IdentityServer"), b => b.MigrationsAssembly("jce.DataAccess"));

            return new IdentityServerDbContext(builder.Options);

        }
    }
}
