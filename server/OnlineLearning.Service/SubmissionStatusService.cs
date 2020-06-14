//SubmissionStatusService
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
    public class SubmissionStatusService : ISubmissionStatusService
    {
        private readonly ISubmissionStatusRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SubmissionStatusService(ISubmissionStatusRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<SubmissionStatus>> ListAsync(BaseParameter baseParameter)
        {
            return await _repository.GetPaginatedList(baseParameter);
        }
        public async Task<SubmissionStatusResponse> CreateAsync(SubmissionStatus newData, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(newData.Id);
            if (oldData != null)
            {
                return new SubmissionStatusResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                //newData.SchoolId = userContext.SchoolId.Value;
                await _repository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new SubmissionStatusResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new SubmissionStatusResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<SubmissionStatusResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SubmissionStatusResponse("Data not found.");

            try
            {
                _repository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new SubmissionStatusResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SubmissionStatusResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<SubmissionStatusResponse> FindByIdAsync(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null)
                return new SubmissionStatusResponse("SessionDetail not found.");

            return new SubmissionStatusResponse(data);
        }

        public async Task<SubmissionStatusResponse> UpdateAsync(Guid id, SubmissionStatus data, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SubmissionStatusResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.SubmissionStatusDesc = data.SubmissionStatusDesc;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _repository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new SubmissionStatusResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SubmissionStatusResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<SubmissionStatusResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _repository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new SubmissionStatusResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new SubmissionStatusResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}