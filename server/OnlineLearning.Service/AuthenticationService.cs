using OnlineLearning.DTO.Response;
using OnlineLearning.Service.Interface;
using OnlineLearning.Shared.Interface.Security;
using OnlineLearning.Shared.Interface.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IUserService _userService;
		private readonly IPasswordHasher _passwordHasher;
		private readonly ITokenHandler _tokenHandler;

		public AuthenticationService(IUserService userService, IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
		{
			_tokenHandler = tokenHandler;
			_passwordHasher = passwordHasher;
			_userService = userService;
		}
		public async Task<TokenResponse> CreateAccessTokenAsync(string userName, string password)
		{
			var user = await _userService.FindByUsernameAsync(userName);

			if (user == null || !_passwordHasher.PasswordMatches(password, user.Password))
			{
				return new TokenResponse(false, "Invalid credentials.", null);
			}

			var token = _tokenHandler.CreateAccessToken(user);

			return new TokenResponse(true, null, token);

		}
	}
}
