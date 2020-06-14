//ISubmissionStatusService
using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OnlineLearning.Service.Interface
{
    public interface ISubmissionStatusService
    {
        Task<PagedList<SubmissionStatus>> ListAsync(BaseParameter baseParameter);
        Task<SubmissionStatusResponse> CreateAsync(SubmissionStatus newData, UserContextInfo userContext);
        Task<SubmissionStatusResponse> FindByIdAsync(Guid id);
        Task<SubmissionStatusResponse> UpdateAsync(Guid id, SubmissionStatus data, UserContextInfo userContext);
        Task<SubmissionStatusResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<SubmissionStatusResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}