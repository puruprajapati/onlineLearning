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
    public class StudentController : ControllerBase
    {
        private IRepository<Student> studentRepository;
        public StudentController(IRepository<Student> studentRepository)
        { this.studentRepository = studentRepository; }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Student>> GetAllStudent() => await studentRepository.GetAll();

        [HttpGet]
        [Route("{studentId}")]
        public async Task<Student> GetStudentById(Guid studentId) => await studentRepository.GetById(studentId);

        [HttpPost]
        [Route("")]
        //[AllowAnonymous]
        public void AddStudent([FromBody] Student student) => studentRepository.Insert(student);

        [HttpDelete]
        [Route("{studentId}")]
        //[AllowAnonymous]
        public void DeleteStudent(Guid studentId) => studentRepository.Delete(studentId);
    }
}