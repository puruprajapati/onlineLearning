using Microsoft.EntityFrameworkCore;
using OnlineLearning.DTO.ViewModel;
using OnlineLearning.EntityFramework;
using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using OnlineLearning.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<List<ListViewModel>> getList()
    {
      var schoolList = await context.Set<School>().Where(school => school.Active == ActiveStatus.Active.ToString()).ToListAsync();
      var data = (from school in schoolList
                  select new ListViewModel
                  {
                    Id = school.Id,
                    Name = school.Name
                  }).ToList();
      return data;
    }
  }
}
