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
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignmentService(IAssignmentRepository assignmentRepository, IUnitOfWork unitOfWork)
        {
            _assignmentRepository = assignmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<Assignment>> ListAsync(BaseParameter baseParameter)
        {
            return await _assignmentRepository.GetPaginatedList(baseParameter);
        }
        public async Task<AssignmentResponse> CreateAsync(Assignment newData, UserContextInfo userContext)
        {
            var oldData = await _assignmentRepository.GetById(newData.Id);
            if (oldData != null)
            {
                return new AssignmentResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                newData.SchoolId = userContext.SchoolId.Value;
                await _assignmentRepository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new AssignmentResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new AssignmentResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<AssignmentResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _assignmentRepository.GetById(id);

            if (oldData == null)
                return new AssignmentResponse("Data not found.");

            try
            {
                _assignmentRepository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new AssignmentResponse(oldData);
            }
            catch (Exception ex)
            {
                return new AssignmentResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<AssignmentResponse> FindByIdAsync(Guid id)
        {
            var data = await _assignmentRepository.GetById(id);
            if (data == null)
                return new AssignmentResponse("SessionDetail not found.");

            return new AssignmentResponse(data);
        }

        public async Task<AssignmentResponse> UpdateAsync(Guid id, Assignment data, UserContextInfo userContext)
        {
            var oldData = await _assignmentRepository.GetById(id);

            if (oldData == null)
                return new AssignmentResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.AssignmentContent = data.AssignmentContent;
            oldData.Deadline = data.Deadline;
            oldData.Description = data.Description;
            oldData.Title = data.Title;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _assignmentRepository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new AssignmentResponse(oldData);
            }
            catch (Exception ex)
            {
                return new AssignmentResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<AssignmentResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _assignmentRepository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new AssignmentResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new AssignmentResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}