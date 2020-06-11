using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
	public class AssignmentViewModel
	{
		public Guid TeacherId { get; set; }
		public Guid SessionId { get; set; }
		public String Title { get; set; }
		public String Description { get; set; }
		public String AssignmentContent { get; set; }
		public DateTime Deadline { get; set; }
	}
}
