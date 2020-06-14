//SubjectService
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
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(ISubjectRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<Subject>> ListAsync(BaseParameter baseParameter)
        {
            return await _repository.GetPaginatedList(baseParameter);
        }
        public async Task<SubjectResponse> CreateAsync(Subject newData, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(newData.Id);
            if (oldData != null)
            {
                return new SubjectResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                newData.SchoolId = userContext.SchoolId.Value;
                await _repository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new SubjectResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new SubjectResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<SubjectResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SubjectResponse("Data not found.");

            try
            {
                _repository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new SubjectResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SubjectResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<SubjectResponse> FindByIdAsync(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null)
                return new SubjectResponse("SessionDetail not found.");

            return new SubjectResponse(data);
        }

        public async Task<SubjectResponse> UpdateAsync(Guid id, Subject data, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new SubjectResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.ClassId = data.ClassId;
            oldData.SubjectName = data.SubjectName;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _repository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new SubjectResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SubjectResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<SubjectResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _repository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new SubjectResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new SubjectResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}