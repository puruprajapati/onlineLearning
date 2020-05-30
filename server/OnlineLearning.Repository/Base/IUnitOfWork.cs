using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
	public interface IUnitOfWork
	{
		Task CompleteAsync();
	}
}
