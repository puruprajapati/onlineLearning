using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
    public class SessionReference : BaseEntity
    {
        public Guid? SchoolId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? SessionId { get; set; }
        public Guid? ReferenceTypeId { get; set; }
        public String ReferenceDetail { get; set; }
    }
}