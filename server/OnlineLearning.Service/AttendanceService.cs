using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using OnlineLearning.Service.Interface;
using OnlineLearning.Shared.Enums;
using OnlineLearning.Shared.Interface.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceService(IAttendanceRepository attendanceRepository, IUnitOfWork unitOfWork)
        {
            _attendanceRepository = attendanceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<Attendence>> ListAsync(BaseParameter baseParameter)
        {
            return await _attendanceRepository.GetPaginatedList(baseParameter);
        }
        public async Task<AttendanceResponse> CreateAsync(Attendence newData, UserContextInfo userContext)
        {
            var oldData = await _attendanceRepository.GetById(newData.Id);
            if (oldData != null)
            {
                return new AttendanceResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                newData.SchoolId = userContext.SchoolId.Value;
                await _attendanceRepository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new AttendanceResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new AttendanceResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<AttendanceResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _attendanceRepository.GetById(id);

            if (oldData == null)
                return new AttendanceResponse("Data not found.");

            try
            {
                _attendanceRepository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new AttendanceResponse(oldData);
            }
            catch (Exception ex)
            {
                return new AttendanceResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<AttendanceResponse> FindByIdAsync(Guid id)
        {
            var data = await _attendanceRepository.GetById(id);
            if (data == null)
                return new AttendanceResponse("SessionDetail not found.");

            return new AttendanceResponse(data);
        }

        public async Task<AttendanceResponse> UpdateAsync(Guid id, Attendence data, UserContextInfo userContext)
        {
            var oldData = await _attendanceRepository.GetById(id);

            if (oldData == null)
                return new AttendanceResponse("Attendance not found.");

            oldData.Active = data.Active;
            oldData.IsPresent = data.IsPresent;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _attendanceRepository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new AttendanceResponse(oldData);
            }
            catch (Exception ex)
            {
                return new AttendanceResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<AttendanceResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _attendanceRepository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new AttendanceResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new AttendanceResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}