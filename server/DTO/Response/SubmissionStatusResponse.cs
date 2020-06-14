using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class SubmissionStatusResponse : BaseResponse
	{
		public SubmissionStatus SubmissionStatus { get; private set; }

		public SubmissionStatusResponse(bool success, string message, SubmissionStatus classObject) : base(success, message)
		{
			SubmissionStatus = classObject;
		}

		// success response
		public SubmissionStatusResponse(SubmissionStatus classObject) : this(true, string.Empty, classObject)
		{ }

		// error response
		public SubmissionStatusResponse(string message) : this(false, message, null)
		{ }
	}
}