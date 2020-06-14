//AssignmentSubmissionResponse
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class AssignmentSubmissionResponse : BaseResponse
	{
		public AssignmentSubmission AssignmentSubmission { get; private set; }

		public AssignmentSubmissionResponse(bool success, string message, AssignmentSubmission classObject) : base(success, message)
		{
			AssignmentSubmission = classObject;
		}

		// success response
		public AssignmentSubmissionResponse(AssignmentSubmission classObject) : this(true, string.Empty, classObject)
		{ }

		// error response
		public AssignmentSubmissionResponse(string message) : this(false, message, null)
		{ }
	}
}