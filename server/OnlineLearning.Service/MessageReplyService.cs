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
    public class MessageReplyService : IMessageReplyService
    {
        private readonly IMessageReplyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageReplyService(IMessageReplyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<MessageReply>> ListAsync(BaseParameter baseParameter)
        {
            return await _repository.GetPaginatedList(baseParameter);
        }
        public async Task<MessageReplyResponse> CreateAsync(MessageReply newData, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(newData.Id);
            if (oldData != null)
            {
                return new MessageReplyResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                newData.SchoolId = userContext.SchoolId.Value;
                await _repository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new MessageReplyResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new MessageReplyResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<MessageReplyResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new MessageReplyResponse("Data not found.");

            try
            {
                _repository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new MessageReplyResponse(oldData);
            }
            catch (Exception ex)
            {
                return new MessageReplyResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<MessageReplyResponse> FindByIdAsync(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null)
                return new MessageReplyResponse("SessionDetail not found.");

            return new MessageReplyResponse(data);
        }

        public async Task<MessageReplyResponse> UpdateAsync(Guid id, MessageReply data, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new MessageReplyResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.Message = data.Message;
            oldData.FromUserId = data.FromUserId;
            oldData.ToUserId = data.ToUserId;
            oldData.IsSeen = data.IsSeen;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _repository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new MessageReplyResponse(oldData);
            }
            catch (Exception ex)
            {
                return new MessageReplyResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<MessageReplyResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _repository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new MessageReplyResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new MessageReplyResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}