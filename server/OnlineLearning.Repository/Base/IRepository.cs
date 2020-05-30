using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Repository
{
	public interface IRepository<TModel> where TModel : class
	{
		IEnumerable<TModel> GetAll();
		TModel GetById(Guid id);
		void Insert(TModel entity);
		void Update(TModel entity);
		void Delete(Guid id);
	}
}
