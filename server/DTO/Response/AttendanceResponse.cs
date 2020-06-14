using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class AttendanceResponse : BaseResponse
	{

		public Attendence Attendence { get; private set; }

		public AttendanceResponse(bool success, string message, Attendence attendence) : base(success, message)
		{
			Attendence = attendence;
		}

		// success response
		public AttendanceResponse(Attendence attendence) : this(true, string.Empty, attendence)
		{ }

		// error response
		public AttendanceResponse(string message) : this(false, message, null)
		{ }
	}
}