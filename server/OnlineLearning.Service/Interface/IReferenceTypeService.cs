using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace OnlineLearning.Service.Interface
{
    public interface IReferenceTypeService
    {
        Task<PagedList<ReferenceType>> ListAsync(BaseParameter baseParameter);
        Task<ReferenceTypeResponse> CreateAsync(ReferenceType newData, UserContextInfo userContext);
        Task<ReferenceTypeResponse> FindByIdAsync(Guid id);
        Task<ReferenceTypeResponse> UpdateAsync(Guid id, ReferenceType data, UserContextInfo userContext);
        Task<ReferenceTypeResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<ReferenceTypeResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}