using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.ViewModel
{
	public class AccessTokenViewModel
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public long Expiration { get; set; }
	}
}
