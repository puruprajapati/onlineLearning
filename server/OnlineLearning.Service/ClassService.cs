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
  public class ClassService : IClassService
  {
    private IRepository<ClassDetail> _classRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClassService(IRepository<ClassDetail> classRepository, IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
      _classRepository = classRepository;

    }

    public async Task<PagedList<ClassDetail>> ListAsync(BaseParameter baseParameter)
    {
      return await _classRepository.GetPaginatedList(baseParameter);
    }

    public async Task<ClassResponse> FindByIdAsync(Guid id)
    {
      var classdetail = await _classRepository.GetById(id);
      return new ClassResponse(true, null, classdetail);
    }

    public async Task<ClassResponse> CreateClassDetail(ClassDetail classdetail, UserContextInfo userContext)
    {
      try
      {
        classdetail.CreatedByUserId = userContext.Id;
        classdetail.Active = ActiveStatus.Active.ToString();
        await _classRepository.Insert(classdetail);
        await _unitOfWork.CompleteAsync();
        return new ClassResponse(true, null, classdetail);

      }
      catch (Exception ex)
      {
        return new ClassResponse($"An error occurred when saving the data: {ex.Message}");
      }
    }

    public async Task<ClassResponse> DeleteAsync(Guid id)
    {
      var existingData = await _classRepository.GetById(id);

      if (existingData == null)
        return new ClassResponse("Class not found.");

      try
      {
        _classRepository.Delete(id);
        await _unitOfWork.CompleteAsync();

        return new ClassResponse(existingData);
      }
      catch (Exception ex)
      {
        return new ClassResponse($"An error occurred when deleting the user: {ex.Message}");
      }
    }

    public async Task<ClassResponse> UpdateAsync(Guid id, ClassDetail data, UserContextInfo userContext)
    {
      var existingData = await _classRepository.GetById(id);

      if (existingData == null)
        return new ClassResponse("Class not found.");

      existingData.ClassName = data.ClassName;
      existingData.Description = data.Description;
      existingData.ModifiedAt = DateTime.Now;
      existingData.ModifiedByUserId = userContext.Id;
      existingData.Active = data.Active;

      try
      {
        _classRepository.Update(existingData);
        await _unitOfWork.CompleteAsync();

        return new ClassResponse(existingData);
      }
      catch (Exception ex)
      {
        return new ClassResponse($"An error occurred when updating the role: {ex.Message}");
      }
    }
  }
}