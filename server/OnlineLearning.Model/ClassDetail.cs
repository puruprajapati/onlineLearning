using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
    public class ClassDetail
        : BaseEntity
    {
        public Guid  SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public School School { get; set; }
        public String ClassName { get; set; }
        public String Description { get; set; }
    }
}