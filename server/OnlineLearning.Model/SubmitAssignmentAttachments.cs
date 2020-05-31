using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
    public class SubmitAssignmentAttachments : BaseEntity
    {
        public Guid SubmitAssignmentId { get; set; }
        [ForeignKey("SubmitAssignmentId")]
        public SubmitAssignment SubmitAssignment { get; set; }
        public Guid StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public Guid SessionId { get; set; }
        [ForeignKey("SessionId")]
        public SessionDetail SessionDetail { get; set; }
        public String AttachmentFileName { get; set; }
    }
}