using System;
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
    public class GradeController : ControllerBase
    {
        private IRepository<Grade> gradeRepository;
        public GradeController(IRepository<Grade> gradeRepository)
        { this.gradeRepository = gradeRepository; }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Grade>> GetAllGrade() => await gradeRepository.GetAll();

    }
}