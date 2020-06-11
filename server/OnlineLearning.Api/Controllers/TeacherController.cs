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

namespace OnlineLearning.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] BaseParameter baseParameter)
        {
            var results = await _teacherService.ListAsync(baseParameter);
            var resultViewModel = _mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherViewModel>>(results);
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
        public async Task<IActionResult> CreateTeacherAsync([FromBody] TeacherViewModel newTeacher)
        {
            var userContext = HttpContext.GetUserContext();
            var teacher = _mapper.Map<TeacherViewModel, Teacher>(newTeacher);

            User user = new User()
            {
                FullName = newTeacher.Name,
                UserName = newTeacher.UserName,
                Email = newTeacher.EmailAddress,
                IsVerified = true,
                CreatedByUserId = userContext.Id,
                SchoolId = userContext.SchoolId,
                UserRole = "Teacher"
            };

            teacher.CreatedByUserId = userContext.Id;
            teacher.SchoolId = userContext.SchoolId.Value;

            var response = await _teacherService.CreateTeacher(teacher, user);
            if (!response.Success)
            {
                return BadRequest(new ErrorResource(response.Message));
            }

            var teacherResource = _mapper.Map<Teacher, TeacherViewModel>(response.Teacher);
            return Ok(teacherResource);
        }

    }
}



//        private IRepository<Teacher> teacherRepository;
//        public TeacherController(IRepository<Teacher> teacherRepository)
//        { this.teacherRepository = teacherRepository; }

//        [HttpGet]
//        [Route("")]
//        public async Task<IEnumerable<Teacher>> GetAllTeacher() => await teacherRepository.GetAll();

//        [HttpGet]
//        [Route("{teacherId}")]
//        public async Task<Teacher> GetTeacherById(Guid teacherId) => await teacherRepository.GetById(teacherId);

//        [HttpPost]
//        [Route("")]
//        //[AllowAnonymous]
//        public void AddTeacher([FromBody] Teacher teacher) => teacherRepository.Insert(teacher);

//        [HttpDelete]
//        [Route("{teacherId}")]
//        //[AllowAnonymous]
//        public void DeleteTeacher(Guid teacherId) => teacherRepository.Delete(teacherId);
//    }
//}