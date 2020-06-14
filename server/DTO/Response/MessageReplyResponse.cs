using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class MessageReplyResponse : BaseResponse
	{
		public MessageReply MessageReply { get; private set; }

		public MessageReplyResponse(bool success, string message, MessageReply messageReply) : base(success, message)
		{
			MessageReply = messageReply;
		}

		// success response
		public MessageReplyResponse(MessageReply messageReply) : this(true, string.Empty, messageReply)
		{ }

		// error response
		public MessageReplyResponse(string message) : this(false, message, null)
		{ }
	}
}