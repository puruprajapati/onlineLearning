using DTO.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
	public interface IAuthenticationService
	{
		Task<TokenResponse> CreateAccessTokenAsync(string userName, string password);
	}
}
