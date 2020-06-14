using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
    public interface ISessionService
    {
        Task<PagedList<SessionDetail>> ListAsync(BaseParameter baseParameter);
        Task<SessionDetailResponse> CreateSessionAsync(SessionDetail sessionDetail, UserContextInfo userContext);
        Task<SessionDetailResponse> FindByIdAsync(Guid id);
        Task<SessionDetailResponse> UpdateAsync(Guid id, SessionDetail sessionDetail, UserContextInfo userContext);
        Task<SessionDetailResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<SessionDetailResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}