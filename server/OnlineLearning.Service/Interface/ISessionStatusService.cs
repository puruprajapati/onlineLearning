//ISessionStatusService
using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OnlineLearning.Service.Interface
{
    public interface ISessionStatusService
    {
        Task<PagedList<SessionStatus>> ListAsync(BaseParameter baseParameter);
        Task<SessionStatusResponse> CreateAsync(SessionStatus newData, UserContextInfo userContext);
        Task<SessionStatusResponse> FindByIdAsync(Guid id);
        Task<SessionStatusResponse> UpdateAsync(Guid id, SessionStatus data, UserContextInfo userContext);
        Task<SessionStatusResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<SessionStatusResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}