using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
	public interface IUserService
	{
		Task<PagedList<User>> ListAsync(BaseParameter baseParameter);
		Task<UserResponse> CreateUserAsync(User user);
		Task<UserResponse> FindByIdAsync(Guid id);

		Task<User> FindByEmailAsync(string email);

		Task<User> FindByUsernameAsync(string userName);
		Task<UserResponse> UpdateAsync(Guid id, User user);
		Task<UserResponse> DeleteAsync(Guid id);
	}
}
