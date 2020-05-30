using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
	public class UserContextInfo
	{
		public Guid Id { get; set; }
		public String FullName { get; set; }

		public String UserName { get; set; }

		public String UserRole { get; set; }

		public Guid? SchoolId { get; set; }
	}
}
