using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Api.Extensions
{
	public static class ModelStateExtensions
	{
		public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
		{
			return dictionary.SelectMany(m => m.Value.Errors)
							 .Select(m => m.ErrorMessage)
							 .ToList();
		}
	}
}


// all extension methods should be static as well as the classes where they are declared
// this keyword in fornt of the prameter telles to treat it as n extension method
// the result is that we can call it like a normal method 
