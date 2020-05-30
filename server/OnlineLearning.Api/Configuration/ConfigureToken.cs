
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Shared.Security.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using OnlineLearning.Model;

namespace OnlineLearning.Api.Configuration
{
	public class ConfigureToken : IConfigure
	{
		public void ConfigureServices(IServiceCollection services, IConfiguration config)
		{

      services.Configure<TokenOptions>(config.GetSection("TokenOptions"));
      var tokenOptions = config.GetSection("TokenOptions").Get<TokenOptions>();

      var signingConfigurations = new SigningConfigurations();
      services.AddSingleton(signingConfigurations);

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(jwtBearerOptions =>
        {
          jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            IssuerSigningKey = signingConfigurations.Key,
            ClockSkew = TimeSpan.Zero
          };
        });
    }
	}
}
