using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
    public class SubmitAssignmentViewModel
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid StudentId { get; set; }
        public Guid SessionId { get; set; }
        public String AssignmentContent { get; set; }
        public int DoCount { get; set; }
    }
}