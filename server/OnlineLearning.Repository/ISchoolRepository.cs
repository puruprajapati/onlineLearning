using OnlineLearning.DTO.ViewModel;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
  public interface ISchoolRepository : IRepository<School>
  {
    Task<School> FindBySchoolCode(string code);
    Task<List<ListViewModel>> getList();
  }
}
