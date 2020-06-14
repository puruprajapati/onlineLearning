using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class ReferenceTypeResponse : BaseResponse
	{
		public ReferenceType ReferenceType { get; private set; }

		public ReferenceTypeResponse(bool success, string message, ReferenceType classObject) : base(success, message)
		{
			ReferenceType = classObject;
		}

		// success response
		public ReferenceTypeResponse(ReferenceType classObject) : this(true, string.Empty, classObject)
		{ }

		// error response
		public ReferenceTypeResponse(string message) : this(false, message, null)
		{ }
	}
}