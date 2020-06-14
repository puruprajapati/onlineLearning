using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class ParentResponse : BaseResponse
	{
		public Parent Parent { get; private set; }

		public ParentResponse(bool success, string message, Parent parent) : base(success, message)
		{
			Parent = parent;
		}

		// success response
		public ParentResponse(Parent parent) : this(true, string.Empty, parent)
		{ }

		// error response
		public ParentResponse(string message) : this(false, message, null)
		{ }
	}
}