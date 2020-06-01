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
    public class ReferenceTypeController : ControllerBase
    {
        private IRepository<ReferenceType> referenceTypeRepository;
        public ReferenceTypeController(IRepository<ReferenceType> referenceTypeRepository)
        { this.referenceTypeRepository = referenceTypeRepository; }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ReferenceType>> GetAllReferenceType() => await referenceTypeRepository.GetAll();
    }
}