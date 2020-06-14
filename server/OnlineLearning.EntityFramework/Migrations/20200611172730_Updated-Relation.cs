using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.EntityFramework.Migrations
{
    public partial class UpdatedRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendences_Users_StudentId",
                table: "Attendences");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendences_Users_TeacherId",
                table: "Attendences");

            migrationBuilder.CreateIndex(
                name: "IX_SessionDetail_ClassId",
                table: "SessionDetail",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionDetail_SchoolId",
                table: "SessionDetail",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionDetail_TeacherId",
                table: "SessionDetail",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendences_Students_StudentId",
                table: "Attendences",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); // ReferentialAction.Cascade

            migrationBuilder.AddForeignKey(
                name: "FK_Attendences_Teachers_TeacherId",
                table: "Attendences",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionDetail_ClassDetails_ClassId",
                table: "SessionDetail",
                column: "ClassId",
                principalTable: "ClassDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionDetail_Schools_SchoolId",
                table: "SessionDetail",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionDetail_Teachers_TeacherId",
                table: "SessionDetail",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendences_Students_StudentId",
                table: "Attendences");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendences_Teachers_TeacherId",
                table: "Attendences");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionDetail_ClassDetails_ClassId",
                table: "SessionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionDetail_Schools_SchoolId",
                table: "SessionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionDetail_Teachers_TeacherId",
                table: "SessionDetail");

            migrationBuilder.DropIndex(
                name: "IX_SessionDetail_ClassId",
                table: "SessionDetail");

            migrationBuilder.DropIndex(
                name: "IX_SessionDetail_SchoolId",
                table: "SessionDetail");

            migrationBuilder.DropIndex(
                name: "IX_SessionDetail_TeacherId",
                table: "SessionDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendences_Users_StudentId",
                table: "Attendences",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendences_Users_TeacherId",
                table: "Attendences",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
