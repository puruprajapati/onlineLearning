using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class MessageMainResponse : BaseResponse
	{
		public MessageMain MessageMain { get; private set; }

		public MessageMainResponse(bool success, string message, MessageMain messageMain) : base(success, message)
		{
			MessageMain = messageMain;
		}

		// success response
		public MessageMainResponse(MessageMain messageMain) : this(true, string.Empty, messageMain)
		{ }

		// error response
		public MessageMainResponse(string message) : this(false, message, null)
		{ }
	}
}