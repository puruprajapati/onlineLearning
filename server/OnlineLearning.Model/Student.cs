using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
	public class Student
			: BaseEntity
	{
		public Guid SchoolId { get; set; }
		[ForeignKey("SchoolId")]
		public School School { get; set; }
		public String Name { get; set; }
		public Guid ClassId { get; set; }
		[ForeignKey("ClassId")]
		public ClassDetail ClassDetail { get; set; }
		public Guid SectionId { get; set; }
		[ForeignKey("SectionId")]
		public SectionDetail SectionDetail { get; set; }
		public String RollNumber { get; set; }
		public Guid ParentId { set; get; }
		[ForeignKey("ParentId")]
		public Parent Parent { get; set; }
		public Boolean Active { get; set; }
	}
}