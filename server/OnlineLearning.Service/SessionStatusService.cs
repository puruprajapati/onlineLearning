//SessionStatusService
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
    public class SessionStatusService : ISessionStatusService
    {
        private readonly ISessionStatusRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SessionStatusService(ISessionStatusRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<SessionStatus>> ListAsync(BaseParameter baseParameter)
        {
            return await _repository.GetPaginatedList(baseParameter);
        }
        public async Task<SessionStatusResponse> CreateAsync(SessionStatus newData, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(newData.Id);
            if (oldData != null)
            {
                return new SessionStatusResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                //newData.SchoolId = userContext.SchoolId.Value;
                await _repository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new SessionStatusResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new SessionStatusResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<SessionStatusResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SessionStatusResponse("Data not found.");

            try
            {
                _repository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new SessionStatusResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SessionStatusResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<SessionStatusResponse> FindByIdAsync(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null)
                return new SessionStatusResponse("SessionDetail not found.");

            return new SessionStatusResponse(data);
        }

        public async Task<SessionStatusResponse> UpdateAsync(Guid id, SessionStatus data, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SessionStatusResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.SessionStatusDesc = data.SessionStatusDesc;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _repository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new SessionStatusResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SessionStatusResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<SessionStatusResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _repository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new SessionStatusResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new SessionStatusResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}