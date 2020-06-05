using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
	public class Assignment : BaseEntity
	{
		public Guid SchoolId { get; set; }
		[ForeignKey("SchoolId")]
		public School School { get; set; }
		public Guid TeacherId { get; set; }
		[ForeignKey("TeacherId")]
		public Teacher Teacher { get; set; }
		public Guid SessionId { get; set; }
		[ForeignKey("SessionId")]
		public SessionDetail Session { get; set; }
		public String Title { get; set; }
		public String Description { get; set; }
		public String AssignmentContent { get; set; }
		public DateTime Deadline { get; set; }
		//public Boolean Active { get; set; }
	}
}