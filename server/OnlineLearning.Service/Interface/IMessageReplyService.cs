using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
    public interface IMessageReplyService
    {
        Task<PagedList<MessageReply>> ListAsync(BaseParameter baseParameter);
        Task<MessageReplyResponse> CreateAsync(MessageReply newData, UserContextInfo userContext);
        Task<MessageReplyResponse> FindByIdAsync(Guid id);
        Task<MessageReplyResponse> UpdateAsync(Guid id, MessageReply data, UserContextInfo userContext);
        Task<MessageReplyResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<MessageReplyResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}