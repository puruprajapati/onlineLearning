using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
	public class SubjectViewModel
	{
		public Guid Id { get; set; }
		public Guid SchoolId { get; set; }
		public String SubjectName { get; set; }
		public Guid ClassId { get; set; }
	}
}