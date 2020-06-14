//AssignmentSubmissionService
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
    public class AssignmentSubmissionService : IAssignmentSubmissionService
    {
        private readonly IAssignmentSubmissionRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignmentSubmissionService(IAssignmentSubmissionRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<AssignmentSubmission>> ListAsync(BaseParameter baseParameter)
        {
            return await _repository.GetPaginatedList(baseParameter);
        }
        public async Task<AssignmentSubmissionResponse> CreateAsync(AssignmentSubmission newData, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(newData.Id);
            if (oldData != null)
            {
                return new AssignmentSubmissionResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                newData.SchoolId = userContext.SchoolId.Value;
                await _repository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new AssignmentSubmissionResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new AssignmentSubmissionResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<AssignmentSubmissionResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new AssignmentSubmissionResponse("Data not found.");

            try
            {
                _repository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new AssignmentSubmissionResponse(oldData);
            }
            catch (Exception ex)
            {
                return new AssignmentSubmissionResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<AssignmentSubmissionResponse> FindByIdAsync(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null)
                return new AssignmentSubmissionResponse("SessionDetail not found.");

            return new AssignmentSubmissionResponse(data);
        }

        public async Task<AssignmentSubmissionResponse> UpdateAsync(Guid id, AssignmentSubmission data, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new AssignmentSubmissionResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.AssignmentId = data.AssignmentId;
            oldData.CheckbyIdTeacherId = data.CheckbyIdTeacherId;
            oldData.Remarks = data.Remarks;
            oldData.SchoolId = data.SchoolId;
            oldData.SubmissionStatusId = data.SubmissionStatusId;
            oldData.SubmittedByStudentId = data.SubmittedByStudentId;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _repository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new AssignmentSubmissionResponse(oldData);
            }
            catch (Exception ex)
            {
                return new AssignmentSubmissionResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<AssignmentSubmissionResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _repository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new AssignmentSubmissionResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new AssignmentSubmissionResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}