﻿// <auto-generated />
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace jce.DataAccess.Data.Migrations
{
    [DbContext(typeof(JceDbContext))]
    [Migration("20180514123456_createDatabase")]
    partial class createDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("jce.Common.Entites.Catalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BooksQuantity");

                    b.Property<int>("CatalogChoiceTypeId");

                    b.Property<int>("CatalogType");

                    b.Property<int>("CeId");

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("DisplayPrice");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<string>("IndexId");

                    b.Property<bool?>("IsActif");

                    b.Property<int?>("ProductChoiceQuantity");

                    b.Property<int?>("SubscriptionQuantity");

                    b.Property<int?>("ToysQuantity");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("CeId")
                        .IsUnique();

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("jce.Common.Entites.CatalogGood", b =>
                {
                    b.Property<int>("CatalogId");

                    b.Property<int>("GoodId");

                    b.Property<string>("ClientProductAlias");

                    b.Property<DateTime>("DateMax");

                    b.Property<DateTime>("DateMin");

                    b.Property<string>("EmployeeParticipationMessage")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.Property<bool?>("IsAddedManually")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasKey("CatalogId", "GoodId");

                    b.HasIndex("GoodId");

                    b.ToTable("CatalogGoods");
                });

            modelBuilder.Entity("jce.Common.Entites.CeSetup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("CeCalculation");

                    b.Property<int>("CeId");

                    b.Property<bool?>("ChildCalculation");

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool?>("IsAgeGroupChoiceLimitation");

                    b.Property<bool?>("IsCeParticipation");

                    b.Property<bool?>("IsEmployeeParticipation");

                    b.Property<bool?>("IsExceeding");

                    b.Property<bool?>("IsGroupingAllowed");

                    b.Property<bool?>("IsHomeDelivery");

                    b.Property<bool?>("IsOrderConfirmationMail");

                    b.Property<bool?>("IsOrderConfirmationMail4CeAdmin");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<string>("WelcomeMessage");

                    b.HasKey("Id");

                    b.HasIndex("CeId")
                        .IsUnique();

                    b.ToTable("CeSetups");
                });

            modelBuilder.Entity("jce.Common.Entites.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AmountParticipationCe");

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<bool?>("IsActif");

                    b.Property<bool?>("IsRegrouper");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int?>("PersonJceProfileId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("PersonJceProfileId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("jce.Common.Entites.Command", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("OvertakeCommand");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Command");
                });

            modelBuilder.Entity("jce.Common.Entites.CommandChildProduct", b =>
                {
                    b.Property<int>("CommandId");

                    b.Property<int>("ChildId");

                    b.Property<int>("ProductId");

                    b.Property<int>("OvertakeCommandChild");

                    b.HasKey("CommandId", "ChildId", "ProductId");

                    b.HasIndex("ChildId");

                    b.HasIndex("ProductId");

                    b.ToTable("CommandChildProduct");
                });

            modelBuilder.Entity("jce.Common.Entites.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdultNumber");

                    b.Property<int>("CeId");

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IsActif");

                    b.Property<bool>("IsDelete");

                    b.Property<int>("MaxAge");

                    b.Property<int>("MinAge");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("CeId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("jce.Common.Entites.HistoryAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Content");

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("HistoryActions");
                });

            modelBuilder.Entity("jce.Common.Entites.JceDbContext.Ce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Actif");

                    b.Property<string>("Address1")
                        .IsRequired();

                    b.Property<string>("Address2");

                    b.Property<string>("AddressExtra");

                    b.Property<int>("AdminJceProfileId");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Company")
                        .IsRequired();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Fax");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Logo");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PostalCode")
                        .IsRequired();

                    b.Property<string>("StreetNumber")
                        .IsRequired();

                    b.Property<string>("Telephone")
                        .IsRequired();

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("AdminJceProfileId");

                    b.ToTable("Ces");
                });

            modelBuilder.Entity("jce.Common.Entites.JceDbContext.Good", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Details");

                    b.Property<int>("Discriminator");

                    b.Property<int>("GoodDepartmentId");

                    b.Property<string>("IndexId");

                    b.Property<bool>("IsDiscountable");

                    b.Property<bool>("IsEnabled");

                    b.Property<double>("Price");

                    b.Property<int>("ProductTypeId");

                    b.Property<string>("RefPintel")
                        .IsRequired();

                    b.Property<string>("Season");

                    b.Property<string>("Title");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Goods");

                    b.HasDiscriminator<int>("Discriminator");
                });

            modelBuilder.Entity("jce.Common.Entites.JceDbContext.JceProfile", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Address2");

                    b.Property<string>("AddressExtra");

                    b.Property<string>("Agency")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Phone");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("StreetNumber")
                        .IsRequired();

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<int>("userType");

                    b.HasKey("Id");

                    b.ToTable("JceProfiles");

                    b.HasDiscriminator<int>("userType");
                });

            modelBuilder.Entity("jce.Common.Entites.JceDbContext.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<bool?>("IsEnabled");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("SupplierRef")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("jce.Common.Entites.Mail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CeSetupId");

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("MailBody")
                        .IsRequired();

                    b.Property<string>("MailObject")
                        .IsRequired();

                    b.Property<string>("MailType")
                        .IsRequired();

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("CeSetupId");

                    b.ToTable("Mail");
                });

            modelBuilder.Entity("jce.Common.Entites.PintelSheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("FilePath");

                    b.Property<string>("Season");

                    b.Property<string>("SheetId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("PintelSheets");
                });

            modelBuilder.Entity("jce.Common.Entites.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("EventId");

                    b.Property<bool>("IsActif");

                    b.Property<bool>("IsDelete");

                    b.Property<int>("NbParticipant");

                    b.Property<DateTime>("ScheduleMax");

                    b.Property<DateTime>("ScheduleMin");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("jce.Common.Entites.ScheduleEmployee", b =>
                {
                    b.Property<int>("ScheduleId");

                    b.Property<int>("EmployeeId");

                    b.Property<bool>("IsDelete");

                    b.Property<int>("NbChildren");

                    b.Property<int>("NbParticipantsEvent");

                    b.HasKey("ScheduleId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("SchedulesEmployee");
                });

            modelBuilder.Entity("jce.Common.Entites.JceDbContext.Batch", b =>
                {
                    b.HasBaseType("jce.Common.Entites.JceDbContext.Good");


                    b.ToTable("Batches");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("jce.Common.Entites.Product", b =>
                {
                    b.HasBaseType("jce.Common.Entites.JceDbContext.Good");

                    b.Property<int?>("BatchId");

                    b.Property<string>("File");

                    b.Property<bool?>("IsBasicProduct");

                    b.Property<bool?>("IsDisplayedOnJCE");

                    b.Property<int>("OriginId");

                    b.Property<int?>("PintelSheetId");

                    b.Property<int>("SupplierId");

                    b.HasIndex("BatchId");

                    b.HasIndex("PintelSheetId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("jce.Common.Entites.JceDbContext.AdminJceProfile", b =>
                {
                    b.HasBaseType("jce.Common.Entites.JceDbContext.JceProfile");


                    b.ToTable("AdminJceProfiles");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("jce.Common.Entites.JceDbContext.PersonJceProfile", b =>
                {
                    b.HasBaseType("jce.Common.Entites.JceDbContext.JceProfile");

                    b.Property<int>("CeId");

                    b.HasIndex("CeId");

                    b.ToTable("PersonJceProfiles");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("jce.Common.Entites.Catalog", b =>
                {
                    b.HasOne("jce.Common.Entites.JceDbContext.Ce")
                        .WithOne("Catalog")
                        .HasForeignKey("jce.Common.Entites.Catalog", "CeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jce.Common.Entites.CatalogGood", b =>
                {
                    b.HasOne("jce.Common.Entites.Catalog", "Catalog")
                        .WithMany("CatalogGoods")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("jce.Common.Entites.JceDbContext.Good", "Good")
                        .WithMany()
                        .HasForeignKey("GoodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jce.Common.Entites.CeSetup", b =>
                {
                    b.HasOne("jce.Common.Entites.JceDbContext.Ce")
                        .WithOne("CeSetup")
                        .HasForeignKey("jce.Common.Entites.CeSetup", "CeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jce.Common.Entites.Child", b =>
                {
                    b.HasOne("jce.Common.Entites.JceDbContext.PersonJceProfile")
                        .WithMany("Children")
                        .HasForeignKey("PersonJceProfileId");
                });

            modelBuilder.Entity("jce.Common.Entites.CommandChildProduct", b =>
                {
                    b.HasOne("jce.Common.Entites.Child", "Child")
                        .WithMany()
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("jce.Common.Entites.Command", "Command")
                        .WithMany("CommandChildProduct")
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("jce.Common.Entites.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jce.Common.Entites.Event", b =>
                {
                    b.HasOne("jce.Common.Entites.JceDbContext.Ce")
                        .WithMany("Events")
                        .HasForeignKey("CeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jce.Common.Entites.JceDbContext.Ce", b =>
                {
                    b.HasOne("jce.Common.Entites.JceDbContext.AdminJceProfile")
                        .WithMany("Ces")
                        .HasForeignKey("AdminJceProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jce.Common.Entites.Mail", b =>
                {
                    b.HasOne("jce.Common.Entites.CeSetup")
                        .WithMany("Mail")
                        .HasForeignKey("CeSetupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jce.Common.Entites.PintelSheet", b =>
                {
                    b.OwnsOne("jce.Common.Core.EnumClasses.AgeGroup", "AgeGroup", b1 =>
                        {
                            b1.Property<int>("PintelSheetId");

                            b1.Property<DateTime>("DateMax");

                            b1.Property<DateTime>("DateMin");

                            b1.Property<int>("Id");

                            b1.ToTable("PintelSheets");

                            b1.HasOne("jce.Common.Entites.PintelSheet")
                                .WithOne("AgeGroup")
                                .HasForeignKey("jce.Common.Core.EnumClasses.AgeGroup", "PintelSheetId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("jce.Common.Entites.Schedule", b =>
                {
                    b.HasOne("jce.Common.Entites.Event")
                        .WithMany("Schedules")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jce.Common.Entites.ScheduleEmployee", b =>
                {
                    b.HasOne("jce.Common.Entites.JceDbContext.PersonJceProfile", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("jce.Common.Entites.Schedule", "Schedules")
                        .WithMany("EventSchedulesEmployees")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jce.Common.Entites.Product", b =>
                {
                    b.HasOne("jce.Common.Entites.JceDbContext.Batch")
                        .WithMany("Products")
                        .HasForeignKey("BatchId");

                    b.HasOne("jce.Common.Entites.PintelSheet")
                        .WithMany("Products")
                        .HasForeignKey("PintelSheetId");

                    b.HasOne("jce.Common.Entites.JceDbContext.Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("jce.Common.Entites.JceDbContext.PersonJceProfile", b =>
                {
                    b.HasOne("jce.Common.Entites.JceDbContext.Ce")
                        .WithMany("UserProfiles")
                        .HasForeignKey("CeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
