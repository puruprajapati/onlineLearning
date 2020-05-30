using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Queries
{
	public class BaseParameter: QueryStringParameters
	{
		public BaseParameter()
		{
			OrderBy = "Id";
		}
	}
}
