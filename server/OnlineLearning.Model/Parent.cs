using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
    public class Parent
                : BaseEntity
    {
        public Guid SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public School School { get; set; }
        public string ParentName { get; set; }
        public String Address { get; set; }
        public String PrimaryContactNumber { get; set; }
        public String SecondaryContactNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public String EmailAddress { get; set; }
    }
}