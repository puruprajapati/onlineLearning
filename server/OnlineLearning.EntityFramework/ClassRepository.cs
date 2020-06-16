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
  public class ClassRepository : Repository<ClassDetail>, IClassRepository
  {
    public ApplicationDatabaseContext ApplicationDatabaseContext
    {
      get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
    }

    public ClassRepository(ApplicationDatabaseContext context) : base(context) { }

    public async Task<ClassDetail> FindByClass(string ClassName)
    {
      return await context.Set<ClassDetail>().FirstOrDefaultAsync(x => x.ClassName == ClassName);
    }

    public async Task<List<ListViewModel>> getList(Guid schoolId)
    {
      var classList = await context.Set<ClassDetail>().Where(classData => classData.Active == ActiveStatus.Active.ToString() && classData.SchoolId == schoolId).ToListAsync();

      var data = (from classdata in classList
                  select new ListViewModel
                  {
                    Id = classdata.Id,
                    Name = classdata.ClassName
                  }).ToList();
      return data;
    }
  }
}