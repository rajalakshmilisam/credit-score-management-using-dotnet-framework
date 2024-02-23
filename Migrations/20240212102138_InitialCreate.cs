using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditApplicationMVCProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditApplicationStatusMaster",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CreditAp__C0F28966696C6703", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ContactNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A4AE64B8E26857BA", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentStatusMaster",
                columns: table => new
                {
                    EmploymentStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmploymentStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentStatusMaster", x => x.EmploymentStatusID);
                });

            migrationBuilder.CreateTable(
                name: "PurposeMaster",
                columns: table => new
                {
                    PurposeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurposeName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PurposeM__ED352978E14F1057", x => x.PurposeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NewPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "FinancialInformation",
                columns: table => new
                {
                    FinancialInformationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    MonthlyIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Expenses = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EmploymentStatusID = table.Column<int>(type: "int", nullable: true),
                    CreditScore = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Financia__713EF851616ACAC8", x => x.FinancialInformationID);
                    table.ForeignKey(
                        name: "FK__Financial__Custo__4E88ABD4",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Financial__Emplo__4D94879B",
                        column: x => x.EmploymentStatusID,
                        principalTable: "EmploymentStatusMaster",
                        principalColumn: "EmploymentStatusID");
                });

            migrationBuilder.CreateTable(
                name: "CreditApplication",
                columns: table => new
                {
                    ApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RequestedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurposeID = table.Column<int>(type: "int", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CreditAp__C93A4F794DE3C87E", x => x.ApplicationID);
                    table.ForeignKey(
                        name: "FK__CreditApp__Custo__4BAC3F29",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__CreditApp__Purpo__4F7CD00D",
                        column: x => x.PurposeID,
                        principalTable: "PurposeMaster",
                        principalColumn: "PurposeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__CreditApp__Statu__4E88AB92",
                        column: x => x.StatusID,
                        principalTable: "CreditApplicationStatusMaster",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditDecision",
                columns: table => new
                {
                    DecisionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: true),
                    Decision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecisionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DecisionDetails = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CreditDe__C0F289660890CF43", x => x.DecisionID);
                    table.ForeignKey(
                        name: "FK__CreditDec__Appli__5165187F",
                        column: x => x.ApplicationId,
                        principalTable: "CreditApplication",
                        principalColumn: "ApplicationID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditApplication_CustomerId",
                table: "CreditApplication",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditApplication_PurposeID",
                table: "CreditApplication",
                column: "PurposeID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditApplication_StatusID",
                table: "CreditApplication",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_CreditDecision_ApplicationId",
                table: "CreditDecision",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialInformation_CustomerId",
                table: "FinancialInformation",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialInformation_EmploymentStatusID",
                table: "FinancialInformation",
                column: "EmploymentStatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditDecision");

            migrationBuilder.DropTable(
                name: "FinancialInformation");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CreditApplication");

            migrationBuilder.DropTable(
                name: "EmploymentStatusMaster");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "PurposeMaster");

            migrationBuilder.DropTable(
                name: "CreditApplicationStatusMaster");
        }
    }
}
