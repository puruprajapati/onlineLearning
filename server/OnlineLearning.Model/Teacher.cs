using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
	public class Teacher : BaseEntity
	{
		public Guid SchoolId { get; set; }
		[ForeignKey("SchoolId")]
		public School School { get; set; }
		public String Name { get; set; }
		public String Address { get; set; }
		public String ContactNumber { get; set; }

		[DataType(DataType.EmailAddress)]
		[StringLength(255)]
		public String EmailAddress { get; set; }
		//public Boolean Active { get; set; }
	}
}
