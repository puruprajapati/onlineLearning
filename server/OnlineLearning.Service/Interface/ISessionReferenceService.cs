using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OnlineLearning.Service.Interface
{
    public interface ISessionReferenceService
    {
        Task<PagedList<SessionReference>> ListAsync(BaseParameter baseParameter);
        Task<SessionReferenceResponse> CreateAsync(SessionReference newData, UserContextInfo userContext);
        Task<SessionReferenceResponse> FindByIdAsync(Guid id);
        Task<SessionReferenceResponse> UpdateAsync(Guid id, SessionReference data, UserContextInfo userContext);
        Task<SessionReferenceResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<SessionReferenceResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}