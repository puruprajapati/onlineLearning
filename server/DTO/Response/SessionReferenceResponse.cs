using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class SessionReferenceResponse : BaseResponse
	{
		public SessionReference SessionReference { get; private set; }

		public SessionReferenceResponse(bool success, string message, SessionReference classObject) : base(success, message)
		{
			SessionReference = classObject;
		}

		// success response
		public SessionReferenceResponse(SessionReference classObject) : this(true, string.Empty, classObject)
		{ }

		// error response
		public SessionReferenceResponse(string message) : this(false, message, null)
		{ }
	}
}