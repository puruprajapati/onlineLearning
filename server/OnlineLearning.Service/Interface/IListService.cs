//ITeacherSubjectService
using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.DTO.ViewModel;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OnlineLearning.Service.Interface
{
  public interface IListService
  {
    Task<List<List<ListViewModel>>> GetAllList(UserContextInfo userContext);
  }
}