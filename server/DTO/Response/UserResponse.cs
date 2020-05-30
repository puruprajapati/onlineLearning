using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Response
{
	public class UserResponse : BaseResponse
	{
		public User User { get; private set; }

		public UserResponse(bool success, string message, User user) : base(success, message)
		{
			User = user;
		}

		// success response
		public UserResponse(User user) : this(true, string.Empty, user)
		{ }

		// error response
		public UserResponse(string message) : this(false, message, null)
		{ }
	}
}
