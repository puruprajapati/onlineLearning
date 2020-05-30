using Microsoft.EntityFrameworkCore;
using OnlineLearning.Model;

namespace OnlineLearning.EntityFramework.Context
{
  public class ApplicationDatabaseContext : DbContext
  {
    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
    { }

    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }
    public DbSet<Attendence> Attendences { get; set; }
    public DbSet<ClassDetail> ClassDetails { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<MessageMain> MessageMains { get; set; }
    public DbSet<MessageReply> MessageReplies { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<ReferenceType> ReferenceTypes { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<SectionDetail> SectionDetails { get; set; }
    public DbSet<SessionReference> SessionReferences { get; set; }
    public DbSet<SessionStatus> SessionStatus { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<SubmissionStatus> SubmissionStatus { get; set; }
    public DbSet<SubmitAssignment> SubmitAssignments { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //  base.OnConfiguring(optionsBuilder);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}
