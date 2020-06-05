using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
    public class Subject
		: BaseEntity
	{
		public Guid SchoolId { get; set; }
		[ForeignKey("SchoolId")]
		public School School { get; set; }
		public String SubjectName { get; set; }
		public Guid ClassId { get; set; }
		[ForeignKey("ClassId")]
		public ClassDetail ClassDetail { get; set; }
		//public Boolean Active { get; set; }
	}
}