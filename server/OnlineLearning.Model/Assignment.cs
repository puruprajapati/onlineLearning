using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
	public class Assignment : BaseEntity
	{
		public Guid SchoolId { get; set; }
		public Guid TeacherId { get; set; }
		public Guid SessionId { get; set; }
		public String Title { get; set; }
		public String Description { get; set; }
		public String AssignmentContent { get; set; }
		public DateTime Deadline { get; set; }
		public Boolean Active { get; set; }
	}
}