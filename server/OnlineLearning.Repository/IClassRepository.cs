using OnlineLearning.DTO.ViewModel;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
  public interface IClassRepository : IRepository<ClassDetail>
  {
    Task<ClassDetail> FindByClass(string ClassName);
    Task<List<ListViewModel>> getList(Guid schoolId);
  }
}