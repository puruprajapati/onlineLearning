using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
    public class SubmitAssignmentAttachmentsViewModel
    {
        public Guid Id { get; set; }
        public Guid SubmitAssignmentId { get; set; }
        public Guid StudentId { get; set; }
        public Guid SessionId { get; set; }
        public String AttachmentFileName { get; set; }
    }
}