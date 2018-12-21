using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace My.CoachManager.Infrastructure.Data.Migrations
{
    public partial class Positions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "Persons",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Row1",
                table: "Adresses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Adresses",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Adresses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    Label = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Side = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Row = table.Column<int>(nullable: false),
                    Column = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.UniqueConstraint("AK_Positions_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "PlayerPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    PlayerId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false),
                    Note = table.Column<int>(nullable: false),
                    IsNatural = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPositions", x => x.Id);
                    table.UniqueConstraint("AK_PlayerPositions_PlayerId_PositionId", x => new { x.PlayerId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_PlayerPositions_Positions_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPositions_Persons_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                column: "FromDate",
                value: new DateTime(2007, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Code", "Column", "CreatedBy", "CreatedDate", "Description", "Label", "ModifiedBy", "ModifiedDate", "Order", "Row", "Side", "Type" },
                values: new object[,]
                {
                    { 16, "ATT", 1, null, null, null, "Attaquant", null, null, 16, 6, 1, 9 },
                    { 15, "AG", 0, null, null, null, "Ailier gauche", null, null, 15, 6, 0, 8 },
                    { 14, "MOD", 2, null, null, null, "Milieu offensif Droit", null, null, 14, 5, 2, 7 },
                    { 13, "MOC", 1, null, null, null, "Milieu offensif central", null, null, 13, 5, 1, 7 },
                    { 12, "MOG", 0, null, null, null, "Milieu offensif gauche", null, null, 12, 5, 0, 7 },
                    { 11, "MD", 2, null, null, null, "Milieu Droit", null, null, 11, 4, 2, 6 },
                    { 10, "MC", 1, null, null, null, "Milieu central", null, null, 10, 4, 1, 6 },
                    { 17, "AD", 2, null, null, null, "Ailier Droit", null, null, 17, 6, 2, 8 },
                    { 9, "MG", 0, null, null, null, "Milieu gauche", null, null, 9, 4, 0, 6 },
                    { 7, "LD", 2, null, null, null, "Latéral Droit", null, null, 7, 3, 0, 4 },
                    { 6, "LG", 0, null, null, null, "Latéral Gauche", null, null, 6, 3, 0, 4 },
                    { 5, "DD", 2, null, null, null, "Défenseur Droit", null, null, 5, 2, 2, 2 },
                    { 4, "DC", 1, null, null, null, "Défenseur central", null, null, 4, 2, 1, 3 },
                    { 3, "DG", 0, null, null, null, "Défenseur gauche", null, null, 3, 2, 0, 2 },
                    { 2, "L", 1, null, null, null, "Libéro", null, null, 2, 1, 1, 1 },
                    { 1, "GB", 1, null, null, null, "Gardien", null, null, 1, 0, 1, 0 },
                    { 8, "MDC", 1, null, null, null, "Milieu défensif", null, null, 8, 3, 1, 5 }
                });

            migrationBuilder.UpdateData(
                table: "Seasons",
                keyColumn: "Id",
                keyValue: 1,
                column: "Order",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPositions_PositionId",
                table: "PlayerPositions",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerPositions");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "Row1",
                table: "Adresses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Adresses",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Adresses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Seasons",
                keyColumn: "Id",
                keyValue: 1,
                column: "Order",
                value: 1);
        }
    }
}
