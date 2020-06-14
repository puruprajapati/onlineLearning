//ISubjectService
using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OnlineLearning.Service.Interface
{
    public interface ISubjectService
    {
        Task<PagedList<Subject>> ListAsync(BaseParameter baseParameter);
        Task<SubjectResponse> CreateAsync(Subject newData, UserContextInfo userContext);
        Task<SubjectResponse> FindByIdAsync(Guid id);
        Task<SubjectResponse> UpdateAsync(Guid id, Subject data, UserContextInfo userContext);
        Task<SubjectResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<SubjectResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}