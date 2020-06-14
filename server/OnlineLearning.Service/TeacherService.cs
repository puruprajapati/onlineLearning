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
  public class TeacherService : ITeacherService
  {
    private IRepository<Teacher> _teacherRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserService _userService;

    public TeacherService(IRepository<Teacher> teacherRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUserService userService)
    {
      _passwordHasher = passwordHasher;
      _unitOfWork = unitOfWork;
      _teacherRepository = teacherRepository;
      _userService = userService;
    }

    public async Task<PagedList<Teacher>> ListAsync(BaseParameter baseParameter)
    {
      return await _teacherRepository.GetPaginatedList(baseParameter);
    }

    public async Task<TeacherResponse> FindByIdAsync(Guid id)
    {
      var teacher = await _teacherRepository.GetById(id);
      return new TeacherResponse(true, null, teacher);
    }
    public async Task<TeacherResponse> CreateTeacher(Teacher teacher, User user)
    {
      try
      {
        user.Password = _passwordHasher.HashPassword(user.UserName);


        await _userService.CreateUserAsync(user);
        teacher.Active = ActiveStatus.Active.ToString();
        await _teacherRepository.Insert(teacher);
        await _unitOfWork.CompleteAsync();

        return new TeacherResponse(true, null, teacher);

      }
      catch (Exception ex)
      {
        return new TeacherResponse($"An error occurred when saving the user: {ex.Message}");
      }

    }

    public async Task<TeacherResponse> DeleteAsync(Guid id)
    {
      var existingData = await _teacherRepository.GetById(id);

      if (existingData == null)
        return new TeacherResponse("Teacher not found.");

      try
      {
        _teacherRepository.Delete(id);
        await _unitOfWork.CompleteAsync();

        return new TeacherResponse(existingData);
      }
      catch (Exception ex)
      {
        return new TeacherResponse($"An error occurred when deleting the user: {ex.Message}");
      }
    }

    public Task<TeacherResponse> UpdateAsync(Guid id, Teacher user)
    {
      throw new NotImplementedException();
    }
  }
}