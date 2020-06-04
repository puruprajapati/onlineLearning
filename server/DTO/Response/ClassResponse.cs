using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class ClassResponse : BaseResponse
	{
		public ClassDetail ClassDetail { get; private set; }

		public ClassResponse(bool success, string message, ClassDetail classDetail) : base(success, message)
		{
			ClassDetail = classDetail;
		}

		// success response
		public ClassResponse(ClassDetail classDetail) : this(true, string.Empty, classDetail)
		{ }

		// error response
		public ClassResponse(string message) : this(false, message, null)
		{ }
	}
}