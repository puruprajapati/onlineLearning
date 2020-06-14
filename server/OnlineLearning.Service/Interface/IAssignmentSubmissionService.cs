//IAssignmentSubmissionService

using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OnlineLearning.Service.Interface
{
    public interface IAssignmentSubmissionService
    {
        Task<PagedList<AssignmentSubmission>> ListAsync(BaseParameter baseParameter);
        Task<AssignmentSubmissionResponse> CreateAsync(AssignmentSubmission newData, UserContextInfo userContext);
        Task<AssignmentSubmissionResponse> FindByIdAsync(Guid id);
        Task<AssignmentSubmissionResponse> UpdateAsync(Guid id, AssignmentSubmission data, UserContextInfo userContext);
        Task<AssignmentSubmissionResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<AssignmentSubmissionResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}