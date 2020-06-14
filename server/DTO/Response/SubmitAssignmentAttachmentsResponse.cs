//SubmitAssignmentAttachmentsResponse
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class SubmitAssignmentAttachmentsResponse : BaseResponse
	{
		public SubmitAssignmentAttachments SubmitAssignmentAttachments { get; private set; }

		public SubmitAssignmentAttachmentsResponse(bool success, string message, SubmitAssignmentAttachments classObject) : base(success, message)
		{
			SubmitAssignmentAttachments = classObject;
		}

		// success response
		public SubmitAssignmentAttachmentsResponse(SubmitAssignmentAttachments classObject) : this(true, string.Empty, classObject)
		{ }

		// error response
		public SubmitAssignmentAttachmentsResponse(string message) : this(false, message, null)
		{ }
	}
}
