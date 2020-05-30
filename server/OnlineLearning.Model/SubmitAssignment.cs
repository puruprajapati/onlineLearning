using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
    public class SubmitAssignment : BaseEntity
    {
        public Guid? SchoolId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? SessionId { get; set; }
        public String AssignmentContent { get; set; }
        public String AssignmentUpload { get; set; }
        public int DoCount { get; set; }
    }
}