using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
	public class Attendence : BaseEntity
	{
		public Guid SchoolId { get; set; }
		[ForeignKey("SchoolId")]
		public School School { get; set; }
		public Guid StudentId { get; set; }
		[ForeignKey("StudentId")]
		public Student Student { get; set; }
		public Guid TeacherId { get; set; }
		[ForeignKey("TeacherId")]
		public Teacher Teacher { get; set; }
		public Guid SessionId { get; set; }
		[ForeignKey("SessionId")]
		public SessionDetail Session { get; set; }
		public Boolean IsPresent { get; set; }
		public String Remarks { get; set; }

	}
}