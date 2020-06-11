
using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service.Interface
{
    public interface IAttendanceService
    {
        Task<PagedList<Attendence>> ListAsync(BaseParameter baseParameter);
        Task<AttendanceResponse> CreateAsync(Attendence newData, UserContextInfo userContext);
        Task<AttendanceResponse> FindByIdAsync(Guid id);
        Task<AttendanceResponse> UpdateAsync(Guid id, Attendence data, UserContextInfo userContext);
        Task<AttendanceResponse> DeleteAsync(Guid id, UserContextInfo userContext);
        Task<AttendanceResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext);
    }
}