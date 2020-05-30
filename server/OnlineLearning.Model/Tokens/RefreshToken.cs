using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
	public class RefreshToken : JsonWebToken
	{
		public RefreshToken(string token, long expiration) : base(token, expiration)
		{
		}
	}
}
