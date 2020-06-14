using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class SessionStatusResponse : BaseResponse
	{
		public SessionStatus SessionStatus { get; private set; }

		public SessionStatusResponse(bool success, string message, SessionStatus classObject) : base(success, message)
		{
			SessionStatus = classObject;
		}

		// success response
		public SessionStatusResponse(SessionStatus classObject) : this(true, string.Empty, classObject)
		{ }

		// error response
		public SessionStatusResponse(string message) : this(false, message, null)
		{ }
	}
}