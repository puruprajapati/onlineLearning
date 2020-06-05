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
  public class SchoolRepostiory : Repository<School>, ISchoolRepository
  {
    public ApplicationDatabaseContext ApplicationDatabaseContext
    {
      get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
    }

    public SchoolRepostiory(ApplicationDatabaseContext context) : base(context) { }

    public async Task<School> FindBySchoolCode(string code)
    {
      return await context.Set<School>().FirstOrDefaultAsync(school => school.SchoolCode == code);
    }
  }
}
