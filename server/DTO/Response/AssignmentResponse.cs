using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class AssignmentResponse : BaseResponse
	{
		public Assignment Assignment { get; private set; }

		public AssignmentResponse(bool success, string message, Assignment assignment) : base(success, message)
		{
			Assignment = assignment;
		}

		// success response
		public AssignmentResponse(Assignment assignment) : this(true, string.Empty, assignment)
		{ }

		// error response
		public AssignmentResponse(string message) : this(false, message, null)
		{ }
	}
}