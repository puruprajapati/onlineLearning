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
  public class SectionRepository : Repository<SectionDetail>, ISectionRepository
  {
    public ApplicationDatabaseContext ApplicationDatabaseContext
    {
      get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
    }

    public SectionRepository(ApplicationDatabaseContext context) : base(context) { }

    public async Task<SectionDetail> FindBySection(string section)
    {
      return await context.Set<SectionDetail>().FirstOrDefaultAsync(x => x.SectionName == section);
    }

    public async Task<List<ListViewModel>> getList(Guid schoolId)
    {
      var list = await context.Set<SectionDetail>().Where(sectionData => sectionData.Active == ActiveStatus.Active.ToString() && sectionData.SchoolId == schoolId).ToListAsync();

      var result = (from data in list
                    select new ListViewModel
                    {
                      Id = data.Id,
                      Name = data.SectionName
                    }).ToList();
      return result;
    }
  }
}