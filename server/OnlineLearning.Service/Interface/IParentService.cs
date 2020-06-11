using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
    public interface IParentService
    {
        Task<PagedList<Parent>> ListAsync(BaseParameter baseParameter);
        Task<ParentResponse> CreateAsync(Parent newData, UserContextInfo userContext);
        Task<ParentResponse> FindByIdAsync(Guid id);
        Task<ParentResponse> UpdateAsync(Guid id, Parent data, UserContextInfo userContext);
        Task<ParentResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<ParentResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}