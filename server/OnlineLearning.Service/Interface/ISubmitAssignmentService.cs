//ISubmitAssignmentService
using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OnlineLearning.Service.Interface
{
    public interface ISubmitAssignmentService
    {
        Task<PagedList<SubmitAssignment>> ListAsync(BaseParameter baseParameter);
        Task<SubmitAssignmentResponse> CreateAsync(SubmitAssignment newData, UserContextInfo userContext);
        Task<SubmitAssignmentResponse> FindByIdAsync(Guid id);
        Task<SubmitAssignmentResponse> UpdateAsync(Guid id, SubmitAssignment data, UserContextInfo userContext);
        Task<SubmitAssignmentResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<SubmitAssignmentResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}