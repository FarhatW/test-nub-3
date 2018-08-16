using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace jce.DataAccess.Data.Migrations
{
    public partial class createDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Command",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    OvertakeCommand = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Command", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActionName = table.Column<string>(maxLength: 255, nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    TableName = table.Column<string>(maxLength: 255, nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PintelSheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    Season = table.Column<string>(nullable: true),
                    SheetId = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    AgeGroup_DateMax = table.Column<DateTime>(nullable: false),
                    AgeGroup_DateMin = table.Column<DateTime>(nullable: false),
                    AgeGroup_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PintelSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    SupplierRef = table.Column<string>(maxLength: 20, nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    Discriminator = table.Column<int>(nullable: false),
                    GoodDepartmentId = table.Column<int>(nullable: false),
                    IndexId = table.Column<string>(nullable: true),
                    IsDiscountable = table.Column<bool>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false),
                    RefPintel = table.Column<string>(nullable: false),
                    Season = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    BatchId = table.Column<int>(nullable: true),
                    File = table.Column<string>(nullable: true),
                    IsBasicProduct = table.Column<bool>(nullable: true),
                    IsDisplayedOnJCE = table.Column<bool>(nullable: true),
                    OriginId = table.Column<int>(nullable: true),
                    PintelSheetId = table.Column<int>(nullable: true),
                    SupplierId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goods_Goods_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Goods_PintelSheets_PintelSheetId",
                        column: x => x.PintelSheetId,
                        principalTable: "PintelSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Goods_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogGoods",
                columns: table => new
                {
                    CatalogId = table.Column<int>(nullable: false),
                    GoodId = table.Column<int>(nullable: false),
                    ClientProductAlias = table.Column<string>(nullable: true),
                    DateMax = table.Column<DateTime>(nullable: false),
                    DateMin = table.Column<DateTime>(nullable: false),
                    EmployeeParticipationMessage = table.Column<string>(nullable: true, defaultValue: ""),
                    IsAddedManually = table.Column<bool>(nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogGoods", x => new { x.CatalogId, x.GoodId });
                    table.ForeignKey(
                        name: "FK_CatalogGoods_Goods_GoodId",
                        column: x => x.GoodId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BooksQuantity = table.Column<int>(nullable: true),
                    CatalogChoiceTypeId = table.Column<int>(nullable: false),
                    CatalogType = table.Column<int>(nullable: false),
                    CeId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DisplayPrice = table.Column<bool>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    IndexId = table.Column<string>(nullable: true),
                    IsActif = table.Column<bool>(nullable: true),
                    ProductChoiceQuantity = table.Column<int>(nullable: true),
                    SubscriptionQuantity = table.Column<int>(nullable: true),
                    ToysQuantity = table.Column<int>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CeSetups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CeCalculation = table.Column<bool>(nullable: true),
                    CeId = table.Column<int>(nullable: false),
                    ChildCalculation = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsAgeGroupChoiceLimitation = table.Column<bool>(nullable: true),
                    IsCeParticipation = table.Column<bool>(nullable: true),
                    IsEmployeeParticipation = table.Column<bool>(nullable: true),
                    IsExceeding = table.Column<bool>(nullable: true),
                    IsGroupingAllowed = table.Column<bool>(nullable: true),
                    IsHomeDelivery = table.Column<bool>(nullable: true),
                    IsOrderConfirmationMail = table.Column<bool>(nullable: true),
                    IsOrderConfirmationMail4CeAdmin = table.Column<bool>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    WelcomeMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CeSetups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CeSetupId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    MailBody = table.Column<string>(nullable: false),
                    MailObject = table.Column<string>(nullable: false),
                    MailType = table.Column<string>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mail_CeSetups_CeSetupId",
                        column: x => x.CeSetupId,
                        principalTable: "CeSetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdultNumber = table.Column<int>(nullable: false),
                    CeId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsActif = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    MaxAge = table.Column<int>(nullable: false),
                    MinAge = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    IsActif = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    NbParticipant = table.Column<int>(nullable: false),
                    ScheduleMax = table.Column<DateTime>(nullable: false),
                    ScheduleMin = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JceProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 255, nullable: false),
                    Address2 = table.Column<string>(nullable: true),
                    AddressExtra = table.Column<string>(nullable: true),
                    Agency = table.Column<string>(maxLength: 255, nullable: false),
                    City = table.Column<string>(maxLength: 255, nullable: false),
                    Company = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(maxLength: 255, nullable: false),
                    Service = table.Column<string>(maxLength: 255, nullable: false),
                    StreetNumber = table.Column<string>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    userType = table.Column<int>(nullable: false),
                    CeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JceProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Actif = table.Column<bool>(nullable: true),
                    Address1 = table.Column<string>(nullable: false),
                    Address2 = table.Column<string>(nullable: true),
                    AddressExtra = table.Column<string>(nullable: true),
                    AdminJceProfileId = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Company = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Fax = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Logo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    StreetNumber = table.Column<string>(nullable: false),
                    Telephone = table.Column<string>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ces_JceProfiles_AdminJceProfileId",
                        column: x => x.AdminJceProfileId,
                        principalTable: "JceProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountParticipationCe = table.Column<decimal>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    IsActif = table.Column<bool>(nullable: true),
                    IsRegrouper = table.Column<bool>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    PersonJceProfileId = table.Column<int>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UserProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Children_JceProfiles_PersonJceProfileId",
                        column: x => x.PersonJceProfileId,
                        principalTable: "JceProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchedulesEmployee",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    NbChildren = table.Column<int>(nullable: false),
                    NbParticipantsEvent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulesEmployee", x => new { x.ScheduleId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_SchedulesEmployee_JceProfiles_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "JceProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchedulesEmployee_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandChildProduct",
                columns: table => new
                {
                    CommandId = table.Column<int>(nullable: false),
                    ChildId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    OvertakeCommandChild = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandChildProduct", x => new { x.CommandId, x.ChildId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CommandChildProduct_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandChildProduct_Command_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Command",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandChildProduct_Goods_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogGoods_GoodId",
                table: "CatalogGoods",
                column: "GoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_CeId",
                table: "Catalogs",
                column: "CeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ces_AdminJceProfileId",
                table: "Ces",
                column: "AdminJceProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CeSetups_CeId",
                table: "CeSetups",
                column: "CeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Children_PersonJceProfileId",
                table: "Children",
                column: "PersonJceProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandChildProduct_ChildId",
                table: "CommandChildProduct",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandChildProduct_ProductId",
                table: "CommandChildProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CeId",
                table: "Events",
                column: "CeId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_BatchId",
                table: "Goods",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_PintelSheetId",
                table: "Goods",
                column: "PintelSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_SupplierId",
                table: "Goods",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_JceProfiles_CeId",
                table: "JceProfiles",
                column: "CeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mail_CeSetupId",
                table: "Mail",
                column: "CeSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EventId",
                table: "Schedules",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulesEmployee_EmployeeId",
                table: "SchedulesEmployee",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogGoods_Catalogs_CatalogId",
                table: "CatalogGoods",
                column: "CatalogId",
                principalTable: "Catalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Catalogs_Ces_CeId",
                table: "Catalogs",
                column: "CeId",
                principalTable: "Ces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CeSetups_Ces_CeId",
                table: "CeSetups",
                column: "CeId",
                principalTable: "Ces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Ces_CeId",
                table: "Events",
                column: "CeId",
                principalTable: "Ces",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JceProfiles_Ces_CeId",
                table: "JceProfiles",
                column: "CeId",
                principalTable: "Ces",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JceProfiles_Ces_CeId",
                table: "JceProfiles");

            migrationBuilder.DropTable(
                name: "CatalogGoods");

            migrationBuilder.DropTable(
                name: "CommandChildProduct");

            migrationBuilder.DropTable(
                name: "HistoryActions");

            migrationBuilder.DropTable(
                name: "Mail");

            migrationBuilder.DropTable(
                name: "SchedulesEmployee");

            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "Command");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "CeSetups");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "PintelSheets");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Ces");

            migrationBuilder.DropTable(
                name: "JceProfiles");
        }
    }
}
