using Microsoft.EntityFrameworkCore;
using OnlineLearning.EntityFramework.Abstract;
using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.EntityFramework
{
	public class AuthorRepository: Repository<Author>, IAuthorRepository
	{
    public ApplicationDatabaseContext ApplicationDatabaseContext
    {
      get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
    }

    public AuthorRepository(ApplicationDatabaseContext context) : base(context) { }

    public Task<Author> GetByName(string name)
    {
      return context.Set<Author>().FirstOrDefaultAsync(author => author.Name == name);
      // return FirstOrDefault(author => author.Name == name);
    }
  }
}
