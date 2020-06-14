//SubmitAssignmentAttachmentsService
using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OnlineLearning.Service.Interface
{
    public interface ISubmitAssignmentAttachmentsService
    {
        Task<PagedList<SubmitAssignmentAttachments>> ListAsync(BaseParameter baseParameter);
        Task<SubmitAssignmentAttachmentsResponse> CreateAsync(SubmitAssignmentAttachments newData, UserContextInfo userContext);
        Task<SubmitAssignmentAttachmentsResponse> FindByIdAsync(Guid id);
        Task<SubmitAssignmentAttachmentsResponse> UpdateAsync(Guid id, SubmitAssignmentAttachments data, UserContextInfo userContext);
        Task<SubmitAssignmentAttachmentsResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<SubmitAssignmentAttachmentsResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}