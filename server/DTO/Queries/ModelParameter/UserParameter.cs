using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Queries
{
	public class UserParameter : BaseParameter
	{

		public UserParameter()
		{
			OrderBy = "FullName";
		}

		// searchable parameter

		public string FullName { get; set; }

		// sortable format
		// ?orderBy=FirstName,MiddleName desc   ///asc

	}
}
