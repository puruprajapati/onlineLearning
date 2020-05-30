using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
	public interface IUserRepository: IRepository<User>
	{
		Task<User> FindByUserName(string userName);
	}
}
