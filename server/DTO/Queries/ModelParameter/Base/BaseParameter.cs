using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Queries
{
	public class BaseParameter: QueryStringParameters
	{
		public BaseParameter()
		{
			OrderBy = "Id";
		}
	}
}
