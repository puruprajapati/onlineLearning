using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
  public interface IStudentService
  {
    Task<PagedList<Student>> ListAsync(BaseParameter baseParameter);
    Task<StudentResponse> CreateStudent(Student student, User user, Parent parent, UserContextInfo userContext);
    Task<StudentResponse> FindByIdAsync(Guid id);

    Task<StudentResponse> UpdateAsync(Guid id, Student user);
    Task<StudentResponse> DeleteAsync(Guid id);
  }
}
