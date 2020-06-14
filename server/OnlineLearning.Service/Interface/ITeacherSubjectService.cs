//ITeacherSubjectService
using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OnlineLearning.Service.Interface
{
    public interface ITeacherSubjectService
    {
        Task<PagedList<TeacherSubject>> ListAsync(BaseParameter baseParameter);
        Task<TeacherSubjectResponse> CreateAsync(TeacherSubject newData, UserContextInfo userContext);
        Task<TeacherSubjectResponse> FindByIdAsync(Guid id);
        Task<TeacherSubjectResponse> UpdateAsync(Guid id, TeacherSubject data, UserContextInfo userContext);
        Task<TeacherSubjectResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<TeacherSubjectResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}