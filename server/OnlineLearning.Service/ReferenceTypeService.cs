//ReferenceTypeService
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
    public class ReferenceTypeService : IReferenceTypeService
    {
        private readonly IReferenceTypeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ReferenceTypeService(IReferenceTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<ReferenceType>> ListAsync(BaseParameter baseParameter)
        {
            return await _repository.GetPaginatedList(baseParameter);
        }
        public async Task<ReferenceTypeResponse> CreateAsync(ReferenceType newData, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(newData.Id);
            if (oldData != null)
            {
                return new ReferenceTypeResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                //newData.SchoolId = userContext.SchoolId.Value;
                await _repository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new ReferenceTypeResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new ReferenceTypeResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<ReferenceTypeResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new ReferenceTypeResponse("Data not found.");

            try
            {
                _repository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new ReferenceTypeResponse(oldData);
            }
            catch (Exception ex)
            {
                return new ReferenceTypeResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<ReferenceTypeResponse> FindByIdAsync(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null)
                return new ReferenceTypeResponse("SessionDetail not found.");

            return new ReferenceTypeResponse(data);
        }

        public async Task<ReferenceTypeResponse> UpdateAsync(Guid id, ReferenceType data, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new ReferenceTypeResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.ReferenceDescription = data.ReferenceDescription;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _repository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new ReferenceTypeResponse(oldData);
            }
            catch (Exception ex)
            {
                return new ReferenceTypeResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<ReferenceTypeResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _repository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new ReferenceTypeResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new ReferenceTypeResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}