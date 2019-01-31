using Microsoft.EntityFrameworkCore.Migrations;

namespace My.CoachManager.Infrastructure.Data.Migrations
{
    public partial class TrainingAttendance2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingAttendances_Trainings_TraigingId",
                table: "TrainingAttendances");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TrainingAttendances_TraigingId_PlayerId",
                table: "TrainingAttendances");

            migrationBuilder.RenameColumn(
                name: "TraigingId",
                table: "TrainingAttendances",
                newName: "TrainingId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TrainingAttendances_TrainingId_PlayerId",
                table: "TrainingAttendances",
                columns: new[] { "TrainingId", "PlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingAttendances_Trainings_TrainingId",
                table: "TrainingAttendances",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingAttendances_Trainings_TrainingId",
                table: "TrainingAttendances");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TrainingAttendances_TrainingId_PlayerId",
                table: "TrainingAttendances");

            migrationBuilder.RenameColumn(
                name: "TrainingId",
                table: "TrainingAttendances",
                newName: "TraigingId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TrainingAttendances_TraigingId_PlayerId",
                table: "TrainingAttendances",
                columns: new[] { "TraigingId", "PlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingAttendances_Trainings_TraigingId",
                table: "TrainingAttendances",
                column: "TraigingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
