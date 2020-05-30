using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
    public class SectionDetail
     : BaseEntity
    {
        public Guid SchoolId { get; set; }
        public String SectionName { get; set; }
        public String Description { get; set; }
    }
}