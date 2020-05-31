using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
    public class AssignmentSubmission : BaseEntity
    {
        public Guid AssignmentId { get; set; }
        [ForeignKey("AssignmentId")]
        public Assignment GetAssignment { get; set; }

        public Guid SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public School School { get; set; }
        
        public Guid SubmittedByStudentId { get; set; }
        [ForeignKey("SubmittedByStudentId")]
        public User UserStudent { get; set; }
        public Guid CheckbyIdTeacherId { get; set; }
        [ForeignKey("CheckbyIdTeacherId")]
        public User UserTeacher { get; set; }
        public String Remarks { get; set; }
        public Guid SubmissionStatusId { get; set; }
        [ForeignKey("SubmissionStatusId")]
        public SubmissionStatus SubmissionStatus { get; set; }
    }
}