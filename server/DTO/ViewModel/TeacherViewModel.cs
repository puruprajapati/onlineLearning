using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
    public class TeacherViewModel
    {
		public Guid Id { get; set; }
		public Guid SchoolId { get; set; }
		public String Name { get; set; }
		public String Address { get; set; }
		public String ContactNumber { get; set; }
		public String EmailAddress { get; set; }
		public Boolean Active { get; set; }
		public String UserName { get; set; }
	}
}
