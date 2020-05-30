﻿using System;

namespace OnlineLearning.Shared.Interface.Security
{
	public interface IPasswordHasher
	{
		string HashPassword(string password);
		bool PasswordMatches(string providedPassword, string passwordHash);
	}
}
