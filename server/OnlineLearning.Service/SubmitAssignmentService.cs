//SubmitAssignmentService
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
    public class SubmitAssignmentService : ISubmitAssignmentService
    {
        private readonly ISubmitAssignmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SubmitAssignmentService(ISubmitAssignmentRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<SubmitAssignment>> ListAsync(BaseParameter baseParameter)
        {
            return await _repository.GetPaginatedList(baseParameter);
        }
        public async Task<SubmitAssignmentResponse> CreateAsync(SubmitAssignment newData, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(newData.Id);
            if (oldData != null)
            {
                return new SubmitAssignmentResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                newData.SchoolId = userContext.SchoolId.Value;
                await _repository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new SubmitAssignmentResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new SubmitAssignmentResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<SubmitAssignmentResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SubmitAssignmentResponse("Data not found.");

            try
            {
                _repository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new SubmitAssignmentResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SubmitAssignmentResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<SubmitAssignmentResponse> FindByIdAsync(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null)
                return new SubmitAssignmentResponse("SessionDetail not found.");

            return new SubmitAssignmentResponse(data);
        }

        public async Task<SubmitAssignmentResponse> UpdateAsync(Guid id, SubmitAssignment data, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SubmitAssignmentResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.SessionId = data.SessionId;
            oldData.AssignmentContent = data.AssignmentContent;
            oldData.DoCount = oldData.DoCount;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _repository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new SubmitAssignmentResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SubmitAssignmentResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<SubmitAssignmentResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _repository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new SubmitAssignmentResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new SubmitAssignmentResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}