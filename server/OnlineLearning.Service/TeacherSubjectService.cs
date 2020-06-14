//TeacherSubjectService

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
    public class TeacherSubjectService : ITeacherSubjectService
    {
        private readonly ITeacherSubjectRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TeacherSubjectService(ITeacherSubjectRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<TeacherSubject>> ListAsync(BaseParameter baseParameter)
        {
            return await _repository.GetPaginatedList(baseParameter);
        }
        public async Task<TeacherSubjectResponse> CreateAsync(TeacherSubject newData, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(newData.Id);
            if (oldData != null)
            {
                return new TeacherSubjectResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                newData.SchoolId = userContext.SchoolId.Value;
                await _repository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new TeacherSubjectResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new TeacherSubjectResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<TeacherSubjectResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new TeacherSubjectResponse("Data not found.");

            try
            {
                _repository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new TeacherSubjectResponse(oldData);
            }
            catch (Exception ex)
            {
                return new TeacherSubjectResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<TeacherSubjectResponse> FindByIdAsync(Guid id)
        {
            var data = await _repository.GetById(id);
            if (data == null)
                return new TeacherSubjectResponse("SessionDetail not found.");

            return new TeacherSubjectResponse(data);
        }

        public async Task<TeacherSubjectResponse> UpdateAsync(Guid id, TeacherSubject data, UserContextInfo userContext)
        {
            var oldData = await _repository.GetById(id);

            if (oldData == null)
                return new TeacherSubjectResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.ClassId = data.ClassId;
            oldData.SubjectId = data.SubjectId;
            oldData.TeacherId = data.TeacherId;

            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _repository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new TeacherSubjectResponse(oldData);
            }
            catch (Exception ex)
            {
                return new TeacherSubjectResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<TeacherSubjectResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _repository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new TeacherSubjectResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new TeacherSubjectResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}