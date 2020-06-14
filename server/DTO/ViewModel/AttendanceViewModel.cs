
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
	public class AttendanceViewModel
	{
		public Guid StudentId { get; set; }
		public Guid TeacherId { get; set; }
		public Guid SessionId { get; set; }
		public Boolean IsPresent { get; set; }
		public String Remarks { get; set; }
	}
}