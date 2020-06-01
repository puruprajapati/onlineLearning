using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.EntityFramework.Migrations
{
    public partial class AddedActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Active",
                table: "Users",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "UserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "TeacherSubjects",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Active",
                table: "Teachers",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "SubmitAssignments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "SubmitAssignmentAttachments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "SubmissionStatus",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Active",
                table: "Subject",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Active",
                table: "Students",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "SessionStatus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "SessionReferences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "SessionDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "SectionDetails",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Active",
                table: "Schools",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "ReferenceTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "Parents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "MessageReplies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "MessageMains",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "Grades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "ClassDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "Attendences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "AssignmentSubmissions",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Active",
                table: "Assignments",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "SubmitAssignments");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "SubmitAssignmentAttachments");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "SubmissionStatus");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "SessionStatus");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "SessionReferences");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "SessionDetail");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "SectionDetails");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ReferenceTypes");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "MessageReplies");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "MessageMains");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ClassDetails");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Attendences");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AssignmentSubmissions");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Teachers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Subject",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Students",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Schools",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Assignments",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
