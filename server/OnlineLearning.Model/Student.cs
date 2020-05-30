using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
    public class Student
        : BaseEntity
    {
        public Guid SchoolId { get; set; }
        public String Name { get; set; }
        public Guid ClassId { get; set; }
        public Guid SectionId { get; set; }
        public String RollNumber { get; set; }
        public Guid ParentId { set; get; }
    }
}