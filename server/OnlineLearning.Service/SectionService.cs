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
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SectionService(ISectionRepository sectionRepository, IUnitOfWork unitOfWork)
        {
            _sectionRepository = sectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<SectionDetail>> ListAsync(BaseParameter baseParameter)
        {
            return await _sectionRepository.GetPaginatedList(baseParameter);
        }
        public async Task<SectionResponse> CreateAsync(SectionDetail newData, UserContextInfo userContext)
        {
            var oldData = await _sectionRepository.GetById(newData.Id);
            if (oldData != null)
            {
                return new SectionResponse(false, "Data already created.", null);
            }
            try
            {
                newData.CreatedByUserId = userContext.Id;
                newData.Active = ActiveStatus.Active.ToString();
                newData.SchoolId = userContext.SchoolId.Value;
                await _sectionRepository.Insert(newData);
                await _unitOfWork.CompleteAsync();

                return new SectionResponse(true, null, newData);
            }
            catch (Exception ex)
            {
                return new SectionResponse($"An error occurred when saving the sessionDetail: {ex.Message}");
            }
        }

        public async Task<SectionResponse> DeleteAsync(Guid id, UserContextInfo userContext)
        {
            var oldData = await _sectionRepository.GetById(id);

            if (oldData == null)
                return new SectionResponse("Data not found.");

            try
            {
                _sectionRepository.Delete(id);
                await _unitOfWork.CompleteAsync();

                return new SectionResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SectionResponse($"An error occurred when deleting the sessionDetail: {ex.Message}");
            }
        }
        public async Task<SectionResponse> FindByIdAsync(Guid id)
        {
            var data = await _sectionRepository.GetById(id);
            if (data == null)
                return new SectionResponse("SessionDetail not found.");

            return new SectionResponse(data);
        }

        public async Task<SectionResponse> UpdateAsync(Guid id, SectionDetail data, UserContextInfo userContext)
        {
            var oldData = await _sectionRepository.GetById(id);

            if (oldData == null)
                return new SectionResponse("SessionDetail not found.");

            oldData.Active = data.Active;
            oldData.SectionName = data.SectionName;
            oldData.ModifiedByUserId = userContext.Id;
            oldData.ModifiedAt = System.DateTime.Now;
            try
            {
                _sectionRepository.Update(oldData);
                await _unitOfWork.CompleteAsync();

                return new SectionResponse(oldData);
            }
            catch (Exception ex)
            {
                return new SectionResponse($"An error occurred when updating the session details: {ex.Message}");
            }
        }

        public async Task<SectionResponse> MultipleDeleteAsync(List<Guid> ids, UserContextInfo userContext)
        {
            try
            {
                _sectionRepository.MultipleDelete(ids);
                await _unitOfWork.CompleteAsync();
                return new SectionResponse($"Deleted successfully");
            }
            catch (Exception ex)
            {
                return new SectionResponse($"An error occurred when deleting the record: {ex.Message}");
            }
        }
    }
}