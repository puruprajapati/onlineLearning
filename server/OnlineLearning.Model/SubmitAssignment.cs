using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
    public class SubmitAssignment : BaseEntity
    {
        public Guid SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public School School { get; set; }

        public Guid StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public Guid SessionId { get; set; }
        [ForeignKey("SessionId")]
        public SessionDetail SessionDetail { get; set; }
        public String AssignmentContent { get; set; }
        public String AssignmentUpload { get; set; }
        public int DoCount { get; set; }
    }
}