using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
    public class AssignmentSubmissionViewModel
    {
        public Guid AssignmentId { get; set; }
        public Guid SubmittedByStudentId { get; set; }
        public Guid CheckbyIdTeacherId { get; set; }
        public String Remarks { get; set; }
        public Guid SubmissionStatusId { get; set; }
    }
}