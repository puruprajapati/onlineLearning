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
  public class TeacherRepository : Repository<Teacher>, ITeacherRepository
  {
    public ApplicationDatabaseContext ApplicationDatabaseContext
    {
      get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
    }

    public TeacherRepository(ApplicationDatabaseContext context) : base(context) { }

    public async Task<List<ListViewModel>> getList(Guid schoolId)
    {
      var list = await context.Set<Teacher>().Where(teacher => teacher.Active == ActiveStatus.Active.ToString() && teacher.SchoolId == schoolId).ToListAsync();

      var result = (from data in list
                    select new ListViewModel
                    {
                      Id = data.Id,
                      Name = data.Name
                    }).ToList();
      return result;
    }
  }
}