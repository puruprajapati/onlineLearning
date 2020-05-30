using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
	public class TokenOptions
	{
		public string Audience { get; set; }
		public string Issuer { get; set; }
		public long AccessTokenExpiration { get; set; }
		public long RefreshTokenExpiration { get; set; }
	}
}
