﻿using Microsoft.Extensions.Options;
using OnlineLearning.Model;
using OnlineLearning.Shared.Interface.Security;
using OnlineLearning.Shared.Interface.Security.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OnlineLearning.Shared.Security.Tokens
{
	public class TokenHandler : ITokenHandler
	{
		private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();

		private readonly TokenOptions _tokenOptions;
		private readonly SigningConfigurations _signingConfigurations;
		private readonly IPasswordHasher _passwordHaser;

		public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot, SigningConfigurations signingConfigurations, IPasswordHasher passwordHaser)
		{
			_passwordHaser = passwordHaser;
			_tokenOptions = tokenOptionsSnapshot.Value;
			_signingConfigurations = signingConfigurations;
		}

		public AccessToken CreateAccessToken(User user)
		{
			var refreshToken = BuildRefreshToken(user);
			var accessToken = BuildAccessToken(user, refreshToken);
			_refreshTokens.Add(refreshToken);

			return accessToken;
		}

		public RefreshToken TakeRefreshToken(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
				return null;

			var refreshToken = _refreshTokens.SingleOrDefault(t => t.Token == token);
			if (refreshToken != null)
				_refreshTokens.Remove(refreshToken);

			return refreshToken;
		}

		public void RevokeRefreshToken(string token)
		{
			TakeRefreshToken(token);
		}

		private RefreshToken BuildRefreshToken(User user)
		{
			var refreshToken = new RefreshToken
			(
				token: _passwordHaser.HashPassword(Guid.NewGuid().ToString()),
				expiration: DateTime.UtcNow.AddSeconds(_tokenOptions.RefreshTokenExpiration).Ticks
			);

			return refreshToken;
		}

		private AccessToken BuildAccessToken(User user, RefreshToken refreshToken)
		{
			var accessTokenExpiration = DateTime.UtcNow.AddHours(_tokenOptions.AccessTokenExpiration);

			var securityToken = new JwtSecurityToken
			(
				issuer: _tokenOptions.Issuer,
				audience: _tokenOptions.Audience,
				claims: GetClaims(user),
				expires: accessTokenExpiration,
				notBefore: DateTime.UtcNow,
				signingCredentials: _signingConfigurations.SigningCredentials
			);

			var handler = new JwtSecurityTokenHandler();
			var accessToken = handler.WriteToken(securityToken);

			return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
		}

		private IEnumerable<Claim> GetClaims(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim("UserId", user.Id.ToString()),
				new Claim("UserName", user.UserName.ToString()),
				new Claim("FullName", user.FullName.ToString()),
				new Claim("UserRole", user.UserRole.ToString()),
				new Claim("SchoolId", user.SchoolId.ToString())
			};

			return claims;
		}
	}
}
