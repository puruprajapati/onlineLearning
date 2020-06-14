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
    public class MessageMainService : IMessageMainService
    {
        private readonly IMessageMainRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageMainService(IMessageMainRepository repository, IUnitOfWork unitOfWork)
        {
            _Repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<MessageMain>> ListAsync(BaseParameter baseParameter)
        {
            return await _Repository.GetPaginatedList(baseParameter);
        }
        public async Task<MessageMainResponse> CreateAsync(MessageMain newData, UserContextInfo userContext)
        {
            var oldData = await _Repository.GetById(newData.Id);
            if (oldData != null)
            {
                return new MessageMainResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                newData.SchoolId = userContext.SchoolId.Value;
                await _Repository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new MessageMainResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new MessageMainResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<MessageMainResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _Repository.GetById(id);

            if (oldData == null)
                return new MessageMainResponse("Data not found.");

            try
            {
                _Repository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new MessageMainResponse(oldData);
            }
            catch (Exception ex)
            {
                return new MessageMainResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<MessageMainResponse> FindByIdAsync(Guid id)
        {
            var data = await _Repository.GetById(id);
            if (data == null)
                return new MessageMainResponse("SessionDetail not found.");

            return new MessageMainResponse(data);
        }

        public async Task<MessageMainResponse> UpdateAsync(Guid id, MessageMain data, UserContextInfo userContext)
        {
            var oldData = await _Repository.GetById(id);

            if (oldData == null)
                return new MessageMainResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.Message = data.Message;
            oldData.FromUserId = data.FromUserId;
            oldData.ToUserId = data.ToUserId;
            oldData.HighPriority = data.HighPriority;
            oldData.IsSeen = data.IsSeen;
            oldData.Subject = data.Subject;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _Repository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new MessageMainResponse(oldData);
            }
            catch (Exception ex)
            {
                return new MessageMainResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<MessageMainResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _Repository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new MessageMainResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new MessageMainResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}