using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.EntityFramework.Migrations
{
    public partial class Relationship2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentUpload",
                table: "SubmitAssignments");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Teachers",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Parents",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SessionDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    SessionTitle = table.Column<string>(nullable: true),
                    SessionDesc = table.Column<string>(nullable: true),
                    ClassId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false),
                    ScheduledDate = table.Column<DateTime>(nullable: false),
                    StartingTime = table.Column<TimeSpan>(nullable: false),
                    EndingTime = table.Column<TimeSpan>(nullable: false),
                    SessionStatusId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    SubjectName = table.Column<string>(nullable: true),
                    ClassId = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_ClassDetails_ClassId",
                        column: x => x.ClassId,
                        principalTable: "ClassDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subject_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmitAssignmentAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SubmitAssignmentId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    AttachmentFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmitAssignmentAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmitAssignmentAttachments_SessionDetail_SessionId",
                        column: x => x.SessionId,
                        principalTable: "SessionDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmitAssignmentAttachments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmitAssignmentAttachments_SubmitAssignments_SubmitAssignmentId",
                        column: x => x.SubmitAssignmentId,
                        principalTable: "SubmitAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SchoolId",
                table: "Users",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_ClassId",
                table: "TeacherSubjects",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_SchoolId",
                table: "TeacherSubjects",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_SubjectId",
                table: "TeacherSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_TeacherId",
                table: "TeacherSubjects",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SchoolId",
                table: "Teachers",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitAssignments_SchoolId",
                table: "SubmitAssignments",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitAssignments_SessionId",
                table: "SubmitAssignments",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitAssignments_StudentId",
                table: "SubmitAssignments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ParentId",
                table: "Students",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolId",
                table: "Students",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SectionId",
                table: "Students",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionReferences_ReferenceTypeId",
                table: "SessionReferences",
                column: "ReferenceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionReferences_SchoolId",
                table: "SessionReferences",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionReferences_SessionId",
                table: "SessionReferences",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionReferences_TeacherId",
                table: "SessionReferences",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionDetails_SchoolId",
                table: "SectionDetails",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_SchoolId",
                table: "Parents",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReplies_FromUserId",
                table: "MessageReplies",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReplies_SchoolId",
                table: "MessageReplies",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReplies_ToUserId",
                table: "MessageReplies",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageMains_FromUserId",
                table: "MessageMains",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageMains_SchoolId",
                table: "MessageMains",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageMains_ToUserId",
                table: "MessageMains",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassDetails_SchoolId",
                table: "ClassDetails",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendences_SchoolId",
                table: "Attendences",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendences_SessionId",
                table: "Attendences",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendences_StudentId",
                table: "Attendences",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendences_TeacherId",
                table: "Attendences",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_AssignmentId",
                table: "AssignmentSubmissions",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_CheckbyIdTeacherId",
                table: "AssignmentSubmissions",
                column: "CheckbyIdTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_SchoolId",
                table: "AssignmentSubmissions",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_SubmissionStatusId",
                table: "AssignmentSubmissions",
                column: "SubmissionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_SubmittedByStudentId",
                table: "AssignmentSubmissions",
                column: "SubmittedByStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_SchoolId",
                table: "Assignments",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_SessionId",
                table: "Assignments",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_TeacherId",
                table: "Assignments",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_ClassId",
                table: "Subject",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SchoolId",
                table: "Subject",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitAssignmentAttachments_SessionId",
                table: "SubmitAssignmentAttachments",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitAssignmentAttachments_StudentId",
                table: "SubmitAssignmentAttachments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitAssignmentAttachments_SubmitAssignmentId",
                table: "SubmitAssignmentAttachments",
                column: "SubmitAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Schools_SchoolId",
                table: "Assignments",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_SessionDetail_SessionId",
                table: "Assignments",
                column: "SessionId",
                principalTable: "SessionDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Teachers_TeacherId",
                table: "Assignments",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentSubmissions_Assignments_AssignmentId",
                table: "AssignmentSubmissions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentSubmissions_Users_CheckbyIdTeacherId",
                table: "AssignmentSubmissions",
                column: "CheckbyIdTeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentSubmissions_Schools_SchoolId",
                table: "AssignmentSubmissions",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentSubmissions_SubmissionStatus_SubmissionStatusId",
                table: "AssignmentSubmissions",
                column: "SubmissionStatusId",
                principalTable: "SubmissionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentSubmissions_Users_SubmittedByStudentId",
                table: "AssignmentSubmissions",
                column: "SubmittedByStudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendences_Schools_SchoolId",
                table: "Attendences",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendences_SessionDetail_SessionId",
                table: "Attendences",
                column: "SessionId",
                principalTable: "SessionDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ClassDetails_Schools_SchoolId",
                table: "ClassDetails",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageMains_Users_FromUserId",
                table: "MessageMains",
                column: "FromUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageMains_Schools_SchoolId",
                table: "MessageMains",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageMains_Users_ToUserId",
                table: "MessageMains",
                column: "ToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReplies_Users_FromUserId",
                table: "MessageReplies",
                column: "FromUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReplies_Schools_SchoolId",
                table: "MessageReplies",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageReplies_Users_ToUserId",
                table: "MessageReplies",
                column: "ToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Schools_SchoolId",
                table: "Parents",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionDetails_Schools_SchoolId",
                table: "SectionDetails",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionReferences_ReferenceTypes_ReferenceTypeId",
                table: "SessionReferences",
                column: "ReferenceTypeId",
                principalTable: "ReferenceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionReferences_Schools_SchoolId",
                table: "SessionReferences",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionReferences_SessionDetail_SessionId",
                table: "SessionReferences",
                column: "SessionId",
                principalTable: "SessionDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionReferences_Teachers_TeacherId",
                table: "SessionReferences",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassDetails_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "ClassDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Parents_ParentId",
                table: "Students",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SectionDetails_SectionId",
                table: "Students",
                column: "SectionId",
                principalTable: "SectionDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmitAssignments_Schools_SchoolId",
                table: "SubmitAssignments",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmitAssignments_SessionDetail_SessionId",
                table: "SubmitAssignments",
                column: "SessionId",
                principalTable: "SessionDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmitAssignments_Students_StudentId",
                table: "SubmitAssignments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Schools_SchoolId",
                table: "Teachers",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_ClassDetails_ClassId",
                table: "TeacherSubjects",
                column: "ClassId",
                principalTable: "ClassDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Schools_SchoolId",
                table: "TeacherSubjects",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Subject_SubjectId",
                table: "TeacherSubjects",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Teachers_TeacherId",
                table: "TeacherSubjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Schools_SchoolId",
                table: "Users",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Schools_SchoolId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_SessionDetail_SessionId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Teachers_TeacherId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentSubmissions_Assignments_AssignmentId",
                table: "AssignmentSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentSubmissions_Users_CheckbyIdTeacherId",
                table: "AssignmentSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentSubmissions_Schools_SchoolId",
                table: "AssignmentSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentSubmissions_SubmissionStatus_SubmissionStatusId",
                table: "AssignmentSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentSubmissions_Users_SubmittedByStudentId",
                table: "AssignmentSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendences_Schools_SchoolId",
                table: "Attendences");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendences_SessionDetail_SessionId",
                table: "Attendences");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendences_Users_StudentId",
                table: "Attendences");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendences_Users_TeacherId",
                table: "Attendences");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassDetails_Schools_SchoolId",
                table: "ClassDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageMains_Users_FromUserId",
                table: "MessageMains");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageMains_Schools_SchoolId",
                table: "MessageMains");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageMains_Users_ToUserId",
                table: "MessageMains");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageReplies_Users_FromUserId",
                table: "MessageReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageReplies_Schools_SchoolId",
                table: "MessageReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageReplies_Users_ToUserId",
                table: "MessageReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Schools_SchoolId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionDetails_Schools_SchoolId",
                table: "SectionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionReferences_ReferenceTypes_ReferenceTypeId",
                table: "SessionReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionReferences_Schools_SchoolId",
                table: "SessionReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionReferences_SessionDetail_SessionId",
                table: "SessionReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionReferences_Teachers_TeacherId",
                table: "SessionReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassDetails_ClassId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Parents_ParentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_SectionDetails_SectionId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmitAssignments_Schools_SchoolId",
                table: "SubmitAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmitAssignments_SessionDetail_SessionId",
                table: "SubmitAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmitAssignments_Students_StudentId",
                table: "SubmitAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Schools_SchoolId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_ClassDetails_ClassId",
                table: "TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Schools_SchoolId",
                table: "TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Subject_SubjectId",
                table: "TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Teachers_TeacherId",
                table: "TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schools_SchoolId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "SubmitAssignmentAttachments");

            migrationBuilder.DropTable(
                name: "SessionDetail");

            migrationBuilder.DropIndex(
                name: "IX_Users_SchoolId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubjects_ClassId",
                table: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubjects_SchoolId",
                table: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubjects_SubjectId",
                table: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeacherSubjects_TeacherId",
                table: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SchoolId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_SubmitAssignments_SchoolId",
                table: "SubmitAssignments");

            migrationBuilder.DropIndex(
                name: "IX_SubmitAssignments_SessionId",
                table: "SubmitAssignments");

            migrationBuilder.DropIndex(
                name: "IX_SubmitAssignments_StudentId",
                table: "SubmitAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ParentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SectionId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_SessionReferences_ReferenceTypeId",
                table: "SessionReferences");

            migrationBuilder.DropIndex(
                name: "IX_SessionReferences_SchoolId",
                table: "SessionReferences");

            migrationBuilder.DropIndex(
                name: "IX_SessionReferences_SessionId",
                table: "SessionReferences");

            migrationBuilder.DropIndex(
                name: "IX_SessionReferences_TeacherId",
                table: "SessionReferences");

            migrationBuilder.DropIndex(
                name: "IX_SectionDetails_SchoolId",
                table: "SectionDetails");

            migrationBuilder.DropIndex(
                name: "IX_Parents_SchoolId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_MessageReplies_FromUserId",
                table: "MessageReplies");

            migrationBuilder.DropIndex(
                name: "IX_MessageReplies_SchoolId",
                table: "MessageReplies");

            migrationBuilder.DropIndex(
                name: "IX_MessageReplies_ToUserId",
                table: "MessageReplies");

            migrationBuilder.DropIndex(
                name: "IX_MessageMains_FromUserId",
                table: "MessageMains");

            migrationBuilder.DropIndex(
                name: "IX_MessageMains_SchoolId",
                table: "MessageMains");

            migrationBuilder.DropIndex(
                name: "IX_MessageMains_ToUserId",
                table: "MessageMains");

            migrationBuilder.DropIndex(
                name: "IX_ClassDetails_SchoolId",
                table: "ClassDetails");

            migrationBuilder.DropIndex(
                name: "IX_Attendences_SchoolId",
                table: "Attendences");

            migrationBuilder.DropIndex(
                name: "IX_Attendences_SessionId",
                table: "Attendences");

            migrationBuilder.DropIndex(
                name: "IX_Attendences_StudentId",
                table: "Attendences");

            migrationBuilder.DropIndex(
                name: "IX_Attendences_TeacherId",
                table: "Attendences");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentSubmissions_AssignmentId",
                table: "AssignmentSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentSubmissions_CheckbyIdTeacherId",
                table: "AssignmentSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentSubmissions_SchoolId",
                table: "AssignmentSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentSubmissions_SubmissionStatusId",
                table: "AssignmentSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentSubmissions_SubmittedByStudentId",
                table: "AssignmentSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_SchoolId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_SessionId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_TeacherId",
                table: "Assignments");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignmentUpload",
                table: "SubmitAssignments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
