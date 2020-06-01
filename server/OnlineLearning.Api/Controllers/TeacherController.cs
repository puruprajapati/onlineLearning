using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Model;
using OnlineLearning.Repository;

namespace OnlineLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private IRepository<Teacher> teacherRepository;
        public TeacherController(IRepository<Teacher> teacherRepository)
        { this.teacherRepository = teacherRepository; }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Teacher>> GetAllTeacher() => await teacherRepository.GetAll();

        [HttpGet]
        [Route("{teacherId}")]
        public async Task<Teacher> GetTeacherById(Guid teacherId) => await teacherRepository.GetById(teacherId);

        [HttpPost]
        [Route("")]
        //[AllowAnonymous]
        public void AddTeacher([FromBody] Teacher teacher) => teacherRepository.Insert(teacher);

        [HttpDelete]
        [Route("{teacherId}")]
        //[AllowAnonymous]
        public void DeleteTeacher(Guid teacherId) => teacherRepository.Delete(teacherId);
    }
}