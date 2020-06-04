using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
    public interface ITeacherService
    {
        Task<PagedList<Teacher>> ListAsync(BaseParameter baseParameter);
        Task<TeacherResponse> CreateTeacher(Teacher teacher, User user);
        Task<TeacherResponse> FindByIdAsync(Guid id);

        Task<TeacherResponse> UpdateAsync(Guid id, Teacher user);
        Task<TeacherResponse> DeleteAsync(Guid id);
    }
}
