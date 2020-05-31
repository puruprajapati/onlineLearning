using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
    public class SessionReference : BaseEntity
    {
        public Guid SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public School School { get; set; }
        public Guid TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
        public Guid SessionId { get; set; }
        [ForeignKey("SessionId")]
        public SessionDetail SessionDetail { get; set; }
        public Guid ReferenceTypeId { get; set; }
        [ForeignKey("ReferenceTypeId")]
        public ReferenceType ReferenceType { get; set; }
        public String ReferenceDetail { get; set; }
    }
}