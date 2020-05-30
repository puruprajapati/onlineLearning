using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
    public class Teacher : BaseEntity
    {
        public Guid? SchoolId { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String ContactNumber { get; set; }
        public String EmailAddress { get; set; }
    }
}
