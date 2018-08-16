using System.IO;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace jce.DataAccess.Core.FactoryDb_Migration
{
    internal class ConfigurationDbContextFactory :  IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();


        ConfigurationDbContext IDesignTimeDbContextFactory<ConfigurationDbContext>.CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            builder.UseSqlServer(_configuration.GetConnectionString("IdentityServer"), b => b.MigrationsAssembly("jce.DataAccess"));

            return new ConfigurationDbContext(builder.Options, new ConfigurationStoreOptions());
        }
    }
}
