using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class SchoolResponse : BaseResponse
	{
		public School School { get; private set; }

		public SchoolResponse(bool success, string message, School school) : base(success, message)
		{
			School = school;
		}

		// success response
		public SchoolResponse(School school) : this(true, string.Empty, school)
		{ }

		// error response
		public SchoolResponse(string message) : this(false, message, null)
		{ }
	}
}