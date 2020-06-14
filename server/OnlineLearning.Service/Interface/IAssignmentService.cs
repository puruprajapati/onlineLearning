using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
    public interface IAssignmentService
    {
        Task<PagedList<Assignment>> ListAsync(BaseParameter baseParameter);
        Task<AssignmentResponse> CreateAsync(Assignment newData, UserContextInfo userContext);
        Task<AssignmentResponse> FindByIdAsync(Guid id);
        Task<AssignmentResponse> UpdateAsync(Guid id, Assignment data, UserContextInfo userContext);
        Task<AssignmentResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<AssignmentResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}