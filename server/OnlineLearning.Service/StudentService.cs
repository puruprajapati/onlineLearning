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
  public class StudentService : IStudentService
  {
    private IRepository<Student> _studentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    private readonly IUserService _userService;
    private readonly IParentService _parentService;

    public StudentService(IRepository<Student> studentRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUserService userService, IParentService parentService)
    {
      _passwordHasher = passwordHasher;
      _unitOfWork = unitOfWork;
      _studentRepository = studentRepository;
      _userService = userService;
      _parentService = parentService;
    }

    public async Task<PagedList<Student>> ListAsync(BaseParameter baseParameter)
    {
      return await _studentRepository.GetPaginatedList(baseParameter);
    }

    public async Task<StudentResponse> FindByIdAsync(Guid id)
    {
      var student = await _studentRepository.GetById(id);
      return new StudentResponse(true, null, student);
    }
    public async Task<StudentResponse> CreateStudent(Student student, User user, Parent parent, UserContextInfo userContext)
    {
      try
      {
        user.Password = _passwordHasher.HashPassword(user.UserName);
        var parentData = await _parentService.CreateAsync(parent, userContext);
        student.ParentId = parentData.Parent.Id;
        student.Active = ActiveStatus.Active.ToString();
        await _userService.CreateUserAsync(user);
        await _studentRepository.Insert(student);
        await _unitOfWork.CompleteAsync();
        return new StudentResponse(true, null, student);
      }
      catch (Exception ex)
      {
        return new StudentResponse($"An error occurred when saving the user: {ex.Message}");
      }

    }

    public async Task<StudentResponse> DeleteAsync(Guid id)
    {
      var existingUser = await _studentRepository.GetById(id);

      if (existingUser == null)
        return new StudentResponse("Student not found.");

      try
      {
        _studentRepository.Delete(id);
        await _unitOfWork.CompleteAsync();

        return new StudentResponse(existingUser);
      }
      catch (Exception ex)
      {
        return new StudentResponse($"An error occurred when deleting the user: {ex.Message}");
      }
    }

    public Task<StudentResponse> UpdateAsync(Guid id, Student user)
    {
      throw new NotImplementedException();
    }
  }
}