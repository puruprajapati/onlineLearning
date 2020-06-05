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
    public class SessionStatusController : ControllerBase
    {
        private IRepository<SessionStatus> sessionStatusRepository;
        public SessionStatusController(IRepository<SessionStatus> sessionStatusRepository)
        { this.sessionStatusRepository = sessionStatusRepository; }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<SessionStatus>> GetAllSessionStatus() => await sessionStatusRepository.GetAll();

    }
}