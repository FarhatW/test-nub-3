using System;
using System.Linq;
using System.Reflection;
using FluentAssertions.Common;
using jce.Common.Core.EnumClasses;
using jce.Common.Entites;
using jce.Common.Entites.IdentityServerDbContext;
using jce.Common.Entites.JceDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace jce.DataAccess.Core.dbContext
{
    public class JceDbContext : DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<PintelSheet> PintelSheets { get; set; }
        public DbSet<Ce> CEs { get; set; }
        public DbSet<CeSetup> CeSetups { get; set; }
        public DbSet<Event> Events { get; set; }
//        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Command> Command { get; set; }
        public DbSet<HistoryAction> HistoryActions { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<PersonJceProfile> PersonJceProfile { get; set; }
        public DbSet<AdminJceProfile> AdminJceProfile { get; set; }
        //public DbSet<AgeGroup> AgeGroups { get; set; }

        public JceDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PintelSheet>()
                .OwnsOne(ag => ag.AgeGroup)
                .Property(ag => ag.Id);

            modelBuilder.Entity<CatalogGood>(entity =>
            {
                entity.HasKey(cp => new {cp.CatalogId, cp.GoodId});
                entity.Property(cg => cg.IsAddedManually)
                    .HasDefaultValue(false);
                entity.Property(cg => cg.EmployeeParticipationMessage)
                    .HasDefaultValue("");
            });

            modelBuilder.Entity<Batch>()
                .HasMany(v => v.Products);
            modelBuilder.Entity<Product>();
               
            modelBuilder.Entity<ScheduleEmployee>().HasKey(ese => new {ese.ScheduleId, ese.EmployeeId });
            modelBuilder.Entity<CommandChildProduct>().HasKey(ccp => new { ccp.CommandId, ccp.ChildId, ccp.ProductId });

            modelBuilder.Entity<Good>()
                .ToTable("Goods");

            modelBuilder.Entity<Good>()
                .HasDiscriminator<int>("Discriminator")
                .HasValue<Product>(1)
                .HasValue<Batch>(2);

            modelBuilder.Entity<JceProfile>()
                .ToTable("JceProfiles");

            modelBuilder.Entity<JceProfile>()
                .HasDiscriminator<int>("userType")
                .HasValue<AdminJceProfile>(1)
                .HasValue<PersonJceProfile>(2);

            
        }
    }
}
