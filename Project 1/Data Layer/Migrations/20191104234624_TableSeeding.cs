using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Layer.Migrations
{
    public partial class TableSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccountTransactionStates",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 0, "Open Account" },
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTransactionStates",
                keyColumn: "ID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "AccountTransactionStates",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AccountTransactionStates",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AccountTransactionStates",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AccountTransactionStates",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AccountTransactionStates",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AccountTransactionStates",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AccountTransactionStates",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AccountTransactionStates",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "ID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 49);
        }
    }
}
