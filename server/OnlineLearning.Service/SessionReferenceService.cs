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
    public class SessionReferenceService : ISessionReferenceService
    {
        private readonly ISessionReferenceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SessionReferenceService(ISessionReferenceRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<SessionReference>> ListAsync(BaseParameter baseParameter)
        {
            return await _repository.GetPaginatedList(baseParameter);
        }
        public async Task<SessionReferenceResponse> CreateAsync(SessionReference newData, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(newData.Id);
            if (oldData != null)
            {
                return new SessionReferenceResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                newData.SchoolId = userContext.SchoolId.Value;
                await _repository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new SessionReferenceResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new SessionReferenceResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<SessionReferenceResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SessionReferenceResponse("Data not found.");

            try
            {
                _repository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new SessionReferenceResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SessionReferenceResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<SessionReferenceResponse> FindByIdAsync(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null)
                return new SessionReferenceResponse("SessionDetail not found.");

            return new SessionReferenceResponse(data);
        }

        public async Task<SessionReferenceResponse> UpdateAsync(Guid id, SessionReference data, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SessionReferenceResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.ReferenceDetail = data.ReferenceDetail;
            oldData.ReferenceTypeId = data.ReferenceTypeId;
            oldData.SessionId = data.SessionId;
            oldData.TeacherId = data.TeacherId;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _repository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new SessionReferenceResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SessionReferenceResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<SessionReferenceResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _repository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new SessionReferenceResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new SessionReferenceResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}