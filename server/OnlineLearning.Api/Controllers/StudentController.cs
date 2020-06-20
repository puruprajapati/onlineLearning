using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineLearning.Api.Extensions;
using OnlineLearning.DTO.Queries;
using OnlineLearning.DTO.Response;
using OnlineLearning.DTO.ViewModel;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using OnlineLearning.Service.Interface;
using OnlineLearning.Shared.Enums;

namespace OnlineLearning.Api.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class StudentController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService, IMapper mapper)
    {
      _studentService = studentService;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] BaseParameter baseParameter)
    {
      var results = await _studentService.ListAsync(baseParameter);
      var resultViewModel = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentViewModel>>(results);
      var metadata = new
      {
        results.TotalCount,
        results.PageSize,
        results.CurrentPage,
        results.TotalPages,
        results.HasNext,
        results.HasPrevious
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

      return Ok(resultViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudentAsync([FromBody] StudentViewModel newStudent)
    {
      var userContext = HttpContext.GetUserContext();
      var student = _mapper.Map<StudentViewModel, Student>(newStudent);

      User user = new User()
      {
        FullName = newStudent.Name,
        UserName = newStudent.UserName,
        Email = newStudent.Email,
        IsVerified = true,
        CreatedByUserId = userContext.Id,
        SchoolId = userContext.SchoolId.Value,
        UserRole = "User"
      };

      Parent parent = new Parent()
      {
        Active = ActiveStatus.Active.ToString(),
        SchoolId = userContext.SchoolId.Value,
        ParentName = newStudent.ParentName,
        Address = newStudent.Address,
        PrimaryContactNumber = newStudent.PrimaryContactNo,
        SecondaryContactNumber = newStudent.SecondaryContactNo,
        EmailAddress = newStudent.ParentEmailAddress,
        CreatedByUserId = userContext.Id
      };

      student.CreatedByUserId = userContext.Id;
      student.SchoolId = userContext.SchoolId.Value;

      var response = await _studentService.CreateStudent(student, user, parent, userContext);
      if (!response.Success)
      {
        return BadRequest(new ErrorResource(response.Message));
      }

      var studentResource = _mapper.Map<Student, StudentViewModel>(response.Student);
      return Ok(studentResource);
    }







    // private IRepository<Student> studentRepository;
    // public StudentController(IRepository<Student> studentRepository)
    // { this.studentRepository = studentRepository; }

    // [HttpGet]
    // [Route("")]
    // public async Task<IEnumerable<Student>> GetAllStudent() => await studentRepository.GetAll();

    // [HttpGet]
    // [Route("{studentId}")]
    // public async Task<Student> GetStudentById(Guid studentId) => await studentRepository.GetById(studentId);

    // [HttpPost]
    // [Route("")]
    // //[AllowAnonymous]
    // public void AddStudent([FromBody] Student student) => studentRepository.Insert(student);

    // [HttpDelete]
    // [Route("{studentId}")]
    // //[AllowAnonymous]
    // public void DeleteStudent(Guid studentId) => studentRepository.Delete(studentId);
  }
}