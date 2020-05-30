using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.EntityFramework
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDatabaseContext _context;

		public UnitOfWork(ApplicationDatabaseContext context)
		{
			_context = context;
		}

		public async Task CompleteAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
