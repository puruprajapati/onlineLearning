using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class SectionResponse : BaseResponse
	{
		public SectionDetail SectionDetail { get; private set; }

		public SectionResponse(bool success, string message, SectionDetail sectionDetail) : base(success, message)
		{
			SectionDetail = sectionDetail;
		}

		// success response
		public SectionResponse(SectionDetail sectionDetail) : this(true, string.Empty, sectionDetail)
		{ }

		// error response
		public SectionResponse(string message) : this(false, message, null)
		{ }
	}
}