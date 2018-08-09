using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace My.CoachManager.Infrastructure.Data.Migrations
{
    public partial class AddPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Adresses",
                columns: new[] { "Id", "City", "CountryId", "CreatedBy", "CreatedDate", "Latitude", "Longitude", "ModifiedBy", "ModifiedDate", "PostalCode", "Row1", "Row2" },
                values: new object[] { 1, "Vic le comte", null, null, null, 0.0, 0.0, null, null, "63270", "Impasse du Babory", null });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "AddressId", "Birthdate", "CountryId", "CreatedBy", "CreatedDate", "Description", "Discriminator", "FirstName", "Gender", "LastName", "LicenseNumber", "ModifiedBy", "ModifiedDate", "Photo", "PlaceOfBirth", "Size", "CategoryId", "Height", "Laterality", "ShoesSize", "Weight" },
                values: new object[] { 1, null, new DateTime(1989, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 76, null, null, null, "Player", "Stéphane", 1, "André", null, null, null, null, "Nevers", "L", 13, 175, 2, 44, 75 });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Default", "Label", "ModifiedBy", "ModifiedDate", "PersonId", "PersonId1", "Type", "Value" },
                values: new object[,]
                {
                    { 1, null, null, true, "Test", null, null, 1, null, 2, "andre.cs2i@gmail.com" },
                    { 2, null, null, false, "Test2", null, null, 1, null, 2, "vincentsourdeix@gmail.com" },
                    { 3, null, null, true, "Test", null, null, 1, null, 1, "0664411391" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "AddressId", "Birthdate", "CountryId", "CreatedBy", "CreatedDate", "Description", "Discriminator", "FirstName", "Gender", "LastName", "LicenseNumber", "ModifiedBy", "ModifiedDate", "Photo", "PlaceOfBirth", "Size", "CategoryId", "Height", "Laterality", "ShoesSize", "Weight" },
                values: new object[] { 2, 1, new DateTime(1986, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 76, null, null, null, "Player", "Vincent", 0, "Sourdeix", "123456789", null, null, null, "Tulle", "L", 3, null, 1, 42, null });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Default", "Label", "ModifiedBy", "ModifiedDate", "PersonId", "PersonId1", "Type", "Value" },
                values: new object[] { 4, null, null, true, "Principale", null, null, 2, null, 2, "visourdeix@gmail.com" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Default", "Label", "ModifiedBy", "ModifiedDate", "PersonId", "PersonId1", "Type", "Value" },
                values: new object[] { 5, null, null, false, "Pub", null, null, 2, null, 2, "vincentsourdeix@gmail.com" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Default", "Label", "ModifiedBy", "ModifiedDate", "PersonId", "PersonId1", "Type", "Value" },
                values: new object[] { 6, null, null, true, "Portable", null, null, 2, null, 1, "0679189256" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Adresses",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
