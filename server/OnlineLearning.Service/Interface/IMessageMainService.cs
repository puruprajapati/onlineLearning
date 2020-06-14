using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
    public interface IMessageMainService
    {
        Task<PagedList<MessageMain>> ListAsync(BaseParameter baseParameter);
        Task<MessageMainResponse> CreateAsync(MessageMain newData, UserContextInfo userContext);
        Task<MessageMainResponse> FindByIdAsync(Guid id);
        Task<MessageMainResponse> UpdateAsync(Guid id, MessageMain data, UserContextInfo userContext);
        Task<MessageMainResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<MessageMainResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}