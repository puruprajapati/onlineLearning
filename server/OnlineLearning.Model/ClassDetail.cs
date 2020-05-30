using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
    public class ClassDetail
        : BaseEntity
    {
        public Guid?  SchoolId { get; set; }
        public String ClassName { get; set; }
        public String Description { get; set; }
    }
}