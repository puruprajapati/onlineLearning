using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class SessionDetailResponse : BaseResponse
	{

		public SessionDetail SessionDetail { get; private set; }

		public SessionDetailResponse(bool success, string message, SessionDetail sessionDetail) : base(success, message)
		{
			SessionDetail = sessionDetail;
		}

		// success response
		public SessionDetailResponse(SessionDetail sessionDetail) : this(true, string.Empty, sessionDetail)
		{ }

		// error response
		public SessionDetailResponse(string message) : this(false, message, null)
		{ }
	}
}