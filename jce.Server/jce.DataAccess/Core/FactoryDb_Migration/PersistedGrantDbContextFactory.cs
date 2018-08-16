using System.IO;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace jce.DataAccess.Core.FactoryDb_Migration
{
    class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        PersistedGrantDbContext IDesignTimeDbContextFactory<PersistedGrantDbContext>.CreateDbContext(string[] args)
        {
        var builder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
        builder.UseSqlServer(_configuration.GetConnectionString("IdentityServer"), b => b.MigrationsAssembly("jce.DataAccess"));
                     
        
        return new PersistedGrantDbContext(builder.Options, new OperationalStoreOptions());
        }
    }
}
