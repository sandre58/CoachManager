using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace My.CoachManager.Infrastructure.Data.Migrations
{
    public partial class Injuries_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Injuries");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedReturn",
                table: "Injuries",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Login", "Mail", "Name" },
                values: new object[] { "stephane.andre", "stephane.andre@modis.com", "Stéphane ANDRE (Modis)" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedReturn",
                table: "Injuries",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Availability",
                table: "Injuries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Login", "Mail", "Name" },
                values: new object[] { "E0214719", "stephane.andre@merial.com", "Stéphane ANDRE (Merial)" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Login", "Mail", "ModifiedBy", "ModifiedDate", "Name", "Password" },
                values: new object[] { 3, null, null, "E0268620", "vincentsourdeix@test.fr", null, null, "Vincent SOURDEIX (BI)", "qRBfE9MoPFs=" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Login", "Mail", "ModifiedBy", "ModifiedDate", "Name", "Password" },
                values: new object[] { 4, null, null, "stephane.andre", "stephane.andre@modis.com", null, null, "Stéphane ANDRE (Modis)", "qRBfE9MoPFs=" });
        }
    }
}
