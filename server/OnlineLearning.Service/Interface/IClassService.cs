using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
  public interface IClassService
  {
    Task<PagedList<ClassDetail>> ListAsync(BaseParameter baseParameter);
    Task<ClassResponse> CreateClassDetail(ClassDetail classDetail, UserContextInfo userContext);
    Task<ClassResponse> FindByIdAsync(Guid id);

    Task<ClassResponse> UpdateAsync(Guid id, ClassDetail classDetail, UserContextInfo userContext);
    Task<ClassResponse> DeleteAsync(Guid id);
  }
}