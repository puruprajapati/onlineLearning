using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.ViewModel
{
	public class LoginViewModel
	{
		[Required]
		[StringLength(255)]
		public string UserName { get; set; }

		[Required]
		[StringLength(32)]
		public string Password { get; set; }
	}
}
