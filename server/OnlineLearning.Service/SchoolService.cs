using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using OnlineLearning.Service.Interface;
using OnlineLearning.Shared.Interface.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Service
{
  public class SchoolService : ISchoolService
  {
    private readonly ISchoolRepository _schoolRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SchoolService(ISchoolRepository schoolRepository, IUnitOfWork unitOfWork)
    {
      _schoolRepository = schoolRepository;
      _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<School>> ListAsync(BaseParameter baseParameter)
    {
      return await _schoolRepository.GetPaginatedList(baseParameter);
    }
    public async Task<SchoolResponse> CreateSchoolAsync(School school, UserContextInfo userContext)
    {
      var existingSchool = await _schoolRepository.FindBySchoolCode(school.SchoolCode);
      if (existingSchool != null)
      {
        return new SchoolResponse(false, "School Code already in use.", null);
      }
      try
      {
        school.CreatedByUserId = userContext.Id;
        await _schoolRepository.Insert(school);
        await _unitOfWork.CompleteAsync();

        return new SchoolResponse(true, null, school);

      }
      catch (Exception ex)
      {
        return new SchoolResponse($"An error occurred when saving the school: {ex.Message}");
      }

    }

    public async Task<SchoolResponse> DeleteAsync(Guid id, UserContextInfo userContext)
    {
      var existingSchool = await _schoolRepository.GetById(id);

      if (existingSchool == null)
        return new SchoolResponse("School not found.");

      try
      {
        _schoolRepository.Delete(id);
        await _unitOfWork.CompleteAsync();

        return new SchoolResponse(existingSchool);
      }
      catch (Exception ex)
      {
        return new SchoolResponse($"An error occurred when deleting the school: {ex.Message}");
      }
    }
    public async Task<SchoolResponse> FindByIdAsync(Guid id)
    {
      var school = await _schoolRepository.GetById(id);
      if (school == null)
        return new SchoolResponse("School not found.");

      return new SchoolResponse(school);
    }

    public async Task<SchoolResponse> UpdateAsync(Guid id, School school, UserContextInfo userContext)
    {
      var existingSchool = await _schoolRepository.GetById(id);

      if (existingSchool == null)
        return new SchoolResponse("School not found.");

      school.ModifiedAt = DateTime.Now;
      school.ModifiedByUserId = userContext.Id;

      try
      {
        _schoolRepository.Update(school);
        await _unitOfWork.CompleteAsync();

        return new SchoolResponse(school);
      }
      catch (Exception ex)
      {
        return new SchoolResponse($"An error occurred when updating the role: {ex.Message}");
      }
    }


  }
}
