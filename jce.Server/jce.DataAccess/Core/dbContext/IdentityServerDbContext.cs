using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Entites.IdentityServerDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jce.DataAccess.Core.dbContext
{
    public class IdentityServerDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.HasMany(u => u.UserRoles)
                    .WithOne(ur => ur.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
         
            modelBuilder.Entity<Role>(role =>
            {
                role.ToTable("Roles");
                role.HasKey(r => r.Id);
                role.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
                role.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
                role.Property(u => u.Enable).HasDefaultValue(true);
                role.Property(u => u.Name).HasMaxLength(256);
                role.Property(u => u.NormalizedName).HasMaxLength(256);

                role.HasMany<UserRole>()
                    .WithOne(ur => ur.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                role.HasMany<IdentityRoleClaim<int>>()
                    .WithOne()
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(roleClaim =>
            {
                roleClaim.HasKey(rc => rc.Id);
                roleClaim.ToTable("RoleClaims");
            });

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.ToTable("UserRoles");
                userRole.HasKey(r => new { r.UserId, r.RoleId });
            });

            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

        }
    }


}
