using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
    public interface ISectionService
    {
        Task<PagedList<SectionDetail>> ListAsync(BaseParameter baseParameter);
        Task<SectionResponse> CreateAsync(SectionDetail newData, UserContextInfo userContext);
        Task<SectionResponse> FindByIdAsync(Guid id);
        Task<SectionResponse> UpdateAsync(Guid id, SectionDetail data, UserContextInfo userContext);
        Task<SectionResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<SectionResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}