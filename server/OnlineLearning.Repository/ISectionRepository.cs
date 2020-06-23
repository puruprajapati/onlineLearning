using OnlineLearning.DTO.ViewModel;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
  public interface ISectionRepository : IRepository<SectionDetail>
  {
    Task<SectionDetail> FindBySection(string section);
    Task<List<ListViewModel>> getList(Guid schoolId);
  }
}