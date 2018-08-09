using Microsoft.EntityFrameworkCore.Migrations;

namespace My.CoachManager.Infrastructure.Data.Migrations
{
    public partial class AddUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Login", "Mail", "ModifiedBy", "ModifiedDate", "Name", "Password" },
                values: new object[,]
                {
                    { 1, null, null, "andre", "andre.cs2i@gmail.com", null, null, "Stéphane ANDRE (Home)", "qRBfE9MoPFs=" },
                    { 2, null, null, "E0214719", "stephane.andre@merial.com", null, null, "Stéphane ANDRE (Merial)", "qRBfE9MoPFs=" },
                    { 3, null, null, "E0268620", "vincentsourdeix@test.fr", null, null, "Vincent SOURDEIX (BI)", "qRBfE9MoPFs=" },
                    { 4, null, null, "stephane.andre", "stephane.andre@modis.com", null, null, "Stéphane ANDRE (Modis)", "qRBfE9MoPFs=" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
