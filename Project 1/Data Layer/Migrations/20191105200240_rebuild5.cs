using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Layer.Migrations
{
    public partial class rebuild5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTransactionStates",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactionStates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Abbreviation = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    StateID = table.Column<int>(nullable: false),
                    ZipCode = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    UserIdentity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customers_States_StateID",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountTypeID = table.Column<int>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    AccountBalance = table.Column<double>(nullable: false),
                    MaturityDate = table.Column<DateTime>(nullable: false),
                    InterestRate = table.Column<float>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsOpen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountTypes_AccountTypeID",
                        column: x => x.AccountTypeID,
                        principalTable: "AccountTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(nullable: false),
                    AccountTransactionStateID = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_AccountTransactionStates_AccountTransactionStateID",
                        column: x => x.AccountTransactionStateID,
                        principalTable: "AccountTransactionStates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountTransactionStates",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { -1, "Open Account" },
                    { 1, "Close Account" },
                    { 2, "Deposit" },
                    { 3, "Withdrawal" },
                    { 4, "Loan Installment" },
                    { 5, "Overdraft Fee" },
                    { 6, "Interest Accrued" },
                    { 7, "Overdraft Protection" },
                    { 8, "Maturity Not Reached" }
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 3, "Loan" },
                    { 2, "Term CD" },
                    { 0, "Checking" },
                    { 1, "Business" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "ID", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 35, "OK", "Oklahoma" },
                    { 26, "NE", "Nebraska" },
                    { 27, "NV", "Nevada" },
                    { 28, "NH", "New Hampshire" },
                    { 29, "NJ", "New Jersey" },
                    { 30, "NM", "New Mexico" },
                    { 31, "NY", "New York" },
                    { 32, "NC", "North Carolina" },
                    { 33, "ND", "North Dakota" },
                    { 34, "OH", "Ohio" },
                    { 36, "OR", "Oregon" },
                    { 42, "TX", "Texas" },
                    { 38, "RI", "Rhode Island" },
                    { 39, "SC", "South Carolina" },
                    { 40, "SD", "South Dakota" },
                    { 41, "TN", "Tennessee" },
                    { 25, "MT", "Montana" },
                    { 43, "UT", "Utah" },
                    { 44, "VT", "Vermont" },
                    { 45, "VA", "Virginia" },
                    { 46, "WA", "Washington" },
                    { 47, "WV", "West Virginia" },
                    { 37, "PA", "Pennsylvania" },
                    { 24, "MO", "Missouri" },
                    { 18, "ME", "Maine" },
                    { 22, "MN", "Minnesota" },
                    { 0, "AL", "Alabama" },
                    { 1, "AK", "Alaska" },
                    { 2, "AZ", "Arizona" },
                    { 3, "AR", "Arkansas" },
                    { 4, "CA", "California" },
                    { 5, "CO", "Colorado" },
                    { 6, "CT", "Connecticut" },
                    { 7, "DE", "Delaware" },
                    { 8, "FL", "Florida" },
                    { 9, "GA", "Georgia" },
                    { 23, "MS", "Mississippi" },
                    { 10, "HI", "Hawaii" },
                    { 12, "IL", "Illinois" },
                    { 13, "IN", "Indiana" },
                    { 14, "IA", "Iowa" },
                    { 15, "KS", "Kansas" },
                    { 16, "KY", "Kentucky" },
                    { 17, "LA", "Louisiana" },
                    { 48, "WI", "Wisconsin" },
                    { 19, "MD", "Maryland" },
                    { 20, "MA", "Massachusetts" },
                    { 21, "MI", "Michigan" },
                    { 11, "ID", "Idaho" },
                    { 49, "WY", "Wyoming" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeID",
                table: "Accounts",
                column: "AccountTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerID",
                table: "Accounts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_AccountID",
                table: "AccountTransactions",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_AccountTransactionStateID",
                table: "AccountTransactions",
                column: "AccountTransactionStateID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_StateID",
                table: "Customers",
                column: "StateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTransactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountTransactionStates");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
