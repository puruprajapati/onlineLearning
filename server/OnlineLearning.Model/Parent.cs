using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
    public class Parent
                : BaseEntity
    {
        public Guid SchoolId { get; set; }
        public string ParentName { get; set; }
        public String Address { get; set; }
        public String PrimaryContactNumber { get; set; }
        public String SecondaryContactNumber { get; set; }
        public String EmailAddress { get; set; }
    }
}