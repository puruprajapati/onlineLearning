using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
    public class Attendence : BaseEntity
    {
        public Guid? SchoolId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? SessionId { get; set; }
        public Boolean IsPresent { get; set; }
    }
}