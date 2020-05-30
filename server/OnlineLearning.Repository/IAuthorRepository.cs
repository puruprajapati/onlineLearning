using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
	public interface IAuthorRepository: IRepository<Author>
	{
		Task<Author> GetByName(string firstName);
	}
}
