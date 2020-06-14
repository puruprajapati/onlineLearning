//SubmitAssignmentAttachmentsService
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
    public class SubmitAssignmentAttachmentsService : ISubmitAssignmentAttachmentsService
    {
        private readonly ISubmitAssignmentAttachmentsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SubmitAssignmentAttachmentsService(ISubmitAssignmentAttachmentsRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<SubmitAssignmentAttachments>> ListAsync(BaseParameter baseParameter)
        {
            return await _repository.GetPaginatedList(baseParameter);
        }
        public async Task<SubmitAssignmentAttachmentsResponse> CreateAsync(SubmitAssignmentAttachments newData, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(newData.Id);
            if (oldData != null)
            {
                return new SubmitAssignmentAttachmentsResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                //newData. = userContext.SchoolId.Value;
                await _repository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new SubmitAssignmentAttachmentsResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new SubmitAssignmentAttachmentsResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<SubmitAssignmentAttachmentsResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SubmitAssignmentAttachmentsResponse("Data not found.");

            try
            {
                _repository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new SubmitAssignmentAttachmentsResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SubmitAssignmentAttachmentsResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<SubmitAssignmentAttachmentsResponse> FindByIdAsync(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null)
                return new SubmitAssignmentAttachmentsResponse("SessionDetail not found.");

            return new SubmitAssignmentAttachmentsResponse(data);
        }

        public async Task<SubmitAssignmentAttachmentsResponse> UpdateAsync(Guid id, SubmitAssignmentAttachments data, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SubmitAssignmentAttachmentsResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.AttachmentFileName = data.AttachmentFileName;
            oldData.SessionId = data.SessionId;
            oldData.StudentId = data.StudentId;
            oldData.SubmitAssignmentId = data.SubmitAssignmentId;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _repository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new SubmitAssignmentAttachmentsResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SubmitAssignmentAttachmentsResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<SubmitAssignmentAttachmentsResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _repository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new SubmitAssignmentAttachmentsResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new SubmitAssignmentAttachmentsResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}