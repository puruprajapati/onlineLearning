using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class SubjectResponse : BaseResponse
	{
		public Subject Subject { get; private set; }

		public SubjectResponse(bool success, string message, Subject classObject) : base(success, message)
		{
			Subject = classObject;
		}

		// success response
		public SubjectResponse(Subject classObject) : this(true, string.Empty, classObject)
		{ }

		// error response
		public SubjectResponse(string message) : this(false, message, null)
		{ }
	}
}