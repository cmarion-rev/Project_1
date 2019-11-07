using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Layer.Migrations
{
    public partial class SeedInterestRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccountInterestRates",
                columns: new[] { "ID", "AccountTypeID", "Rate" },
                values: new object[,]
                {
                    { 1, 0, 0.0075f },
                    { 2, 1, 0.015f },
                    { 3, 2, 0.0345f },
                    { 4, 3, 0.0425f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountInterestRates",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AccountInterestRates",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AccountInterestRates",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AccountInterestRates",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
