using Microsoft.EntityFrameworkCore;
using OnlineLearning.EntityFramework;
using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.EntityFramework
{
  public class UserRepository : Repository<User>, IUserRepository
  {
    public ApplicationDatabaseContext ApplicationDatabaseContext
    {
      get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
    }

    public UserRepository(ApplicationDatabaseContext context) : base(context) { }

    public async Task<User> FindByUserName(string userName)
    {
      return await context.Set<User>().FirstOrDefaultAsync(user => user.UserName == userName);
    }
  }
}
