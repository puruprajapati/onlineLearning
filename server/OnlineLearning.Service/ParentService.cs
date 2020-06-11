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
    public class ParentService : IParentService
    {
        private readonly IParentRepository _parentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ParentService(IParentRepository parentRepository, IUnitOfWork unitOfWork)
        {
            _parentRepository = parentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<Parent>> ListAsync(BaseParameter baseParameter)
        {
            return await _parentRepository.GetPaginatedList(baseParameter);
        }
        public async Task<ParentResponse> CreateAsync(Parent newData, UserContextInfo userContext)
        {
            var oldData = await _parentRepository.GetById(newData.Id);
            if (oldData != null)
            {
                return new ParentResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                newData.SchoolId = userContext.SchoolId.Value;
                await _parentRepository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new ParentResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new ParentResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<ParentResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _parentRepository.GetById(id);

            if (oldData == null)
                return new ParentResponse("Data not found.");

            try
            {
                _parentRepository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new ParentResponse(oldData);
            }
            catch (Exception ex)
            {
                return new ParentResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<ParentResponse> FindByIdAsync(Guid id)
        {
            var data = await _parentRepository.GetById(id);
            if (data == null)
                return new ParentResponse("SessionDetail not found.");

            return new ParentResponse(data);
        }

        public async Task<ParentResponse> UpdateAsync(Guid id, Parent data, UserContextInfo userContext)
        {
            var oldData = await _parentRepository.GetById(id);

            if (oldData == null)
                return new ParentResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.ParentName = data.ParentName;
            oldData.PrimaryContactNumber = data.PrimaryContactNumber;
            oldData.SecondaryContactNumber = data.SecondaryContactNumber;
            oldData.EmailAddress = data.EmailAddress;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _parentRepository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new ParentResponse(oldData);
            }
            catch (Exception ex)
            {
                return new ParentResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<ParentResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _parentRepository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new ParentResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new ParentResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}