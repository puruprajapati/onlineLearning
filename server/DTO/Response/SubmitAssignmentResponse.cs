using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class SubmitAssignmentResponse : BaseResponse
	{
		public SubmitAssignment SubmitAssignment { get; private set; }

		public SubmitAssignmentResponse(bool success, string message, SubmitAssignment classObject) : base(success, message)
		{
			SubmitAssignment = classObject;
		}

		// success response
		public SubmitAssignmentResponse(SubmitAssignment classObject) : this(true, string.Empty, classObject)
		{ }

		// error response
		public SubmitAssignmentResponse(string message) : this(false, message, null)
		{ }
	}
}