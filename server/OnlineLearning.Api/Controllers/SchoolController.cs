using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineLearning.DTO.ViewModel;
using OnlineLearning.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Model;
using OnlineLearning.Service.Interface;
using OnlineLearning.DTO.Queries;
using Newtonsoft.Json;
using OnlineLearning.Api.Extensions;
using OnlineLearning.Repository;

namespace OnlineLearning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private IRepository<School> schoolRepository;
        public SchoolController(IRepository<School> schoolRepository)
        { this.schoolRepository = schoolRepository; }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<School>> GetAllSchool() => await schoolRepository.GetAll();

        [HttpGet]
        [Route("{schoolId}")]
        public async Task<School> GetSchoolById(Guid schoolId) => await schoolRepository.GetById(schoolId);

        [HttpPost]
        [Route("")]
        //[AllowAnonymous]
        public void AddSchool([FromBody] School school) => schoolRepository.Insert(school);

        [HttpDelete]
        [Route("{schoolId}")]
        //[AllowAnonymous]
        public void DeleteSchool(Guid schoolId) => schoolRepository.Delete(schoolId);
    }
}