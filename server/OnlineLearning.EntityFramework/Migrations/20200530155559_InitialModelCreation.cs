using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.EntityFramework.Migrations
{
    public partial class InitialModelCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AssignmentContent = table.Column<string>(nullable: true),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentSubmissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    AssignmentId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    SubmittedByStudentId = table.Column<Guid>(nullable: false),
                    CheckbyIdTeacherId = table.Column<Guid>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    SubmissionStatusId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSubmissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    IsPresent = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    ClassName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    GradeDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageMains",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    HighPriority = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    IsSeen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageMains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageReplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    MessageMainId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    IsSeen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReplies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    ParentName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PrimaryContactNumber = table.Column<string>(nullable: true),
                    SecondaryContactNumber = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    ReferenceDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    LogoLocation = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectionDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    SectionName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionReferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    ReferenceTypeId = table.Column<Guid>(nullable: false),
                    ReferenceDetail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SessionStatusDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ClassId = table.Column<Guid>(nullable: false),
                    SectionId = table.Column<Guid>(nullable: false),
                    RollNumber = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SubmissionStatusDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubmitAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    AssignmentContent = table.Column<string>(nullable: true),
                    AssignmentUpload = table.Column<string>(nullable: true),
                    DoCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmitAssignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false),
                    SubjectId = table.Column<Guid>(nullable: false),
                    ClassId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    RoleDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<Guid>(nullable: false),
                    ModifiedByUserId = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserRole = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "AssignmentSubmissions");

            migrationBuilder.DropTable(
                name: "Attendences");

            migrationBuilder.DropTable(
                name: "ClassDetails");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "MessageMains");

            migrationBuilder.DropTable(
                name: "MessageReplies");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "ReferenceTypes");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "SectionDetails");

            migrationBuilder.DropTable(
                name: "SessionReferences");

            migrationBuilder.DropTable(
                name: "SessionStatus");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "SubmissionStatus");

            migrationBuilder.DropTable(
                name: "SubmitAssignments");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
