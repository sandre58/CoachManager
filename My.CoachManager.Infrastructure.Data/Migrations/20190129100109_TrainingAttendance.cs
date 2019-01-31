using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace My.CoachManager.Infrastructure.Data.Migrations
{
    public partial class TrainingAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingAttendances",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    TraigingId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    Attendance = table.Column<int>(nullable: false, defaultValue: 0),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingAttendances", x => x.Id);
                    table.UniqueConstraint("AK_TrainingAttendances_TraigingId_PlayerId", x => new { x.TraigingId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_TrainingAttendances_Persons_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingAttendances_Trainings_TraigingId",
                        column: x => x.TraigingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingAttendances_PlayerId",
                table: "TrainingAttendances",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingAttendances");
        }
    }
}
