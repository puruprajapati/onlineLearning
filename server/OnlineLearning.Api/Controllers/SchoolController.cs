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
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class SchoolController : ControllerBase
  {

    private readonly IMapper _mapper;
    private readonly ISchoolService _schoolService;
    public SchoolController(ISchoolService schoolService, IMapper mapper)
    {
      _mapper = mapper;
      _schoolService = schoolService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllSchool([FromQuery] BaseParameter baseParameter)
    {
      var results = await _schoolService.ListAsync(baseParameter);
      var resultViewModel = _mapper.Map<IEnumerable<School>, IEnumerable<SchoolViewModel>>(results);
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
    public async Task<IActionResult> CreateSchoolAsync([FromBody] SchoolViewModel newSchool)
    {
      var userContext = HttpContext.GetUserContext();
      var school = _mapper.Map<SchoolViewModel, School>(newSchool);

      var response = await _schoolService.CreateSchoolAsync(school, userContext);
      if (!response.Success)
      {
        return BadRequest(new ErrorResource(response.Message));
      }

      var schoolResource = _mapper.Map<School, SchoolViewModel>(response.School);
      return Ok(schoolResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] SchoolViewModel request)
    {
      var userContext = HttpContext.GetUserContext();
      var school = _mapper.Map<SchoolViewModel, School>(request);
      var result = await _schoolService.UpdateAsync(id, school, userContext);

      if (!result.Success)
        return BadRequest(new ErrorResource(result.Message));

      var resultViewModel = _mapper.Map<School, SchoolViewModel>(result.School);
      return Ok(resultViewModel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
      var userContext = HttpContext.GetUserContext();
      var result = await _schoolService.DeleteAsync(id, userContext);

      if (!result.Success)
        return BadRequest(new ErrorResource(result.Message));
      var resultViewModel = _mapper.Map<School, SchoolViewModel>(result.School);
      return Ok(resultViewModel);
    }

    [HttpPost]
    [Route("deletemultiple")]
    public async Task<IActionResult> MultipleDelete([FromBody] List<Guid> ids)
    {
      var userContext = HttpContext.GetUserContext();
      var result = await _schoolService.MultipleDeleteAsync(ids, userContext);
      if (!result.Success)
        return BadRequest(new ErrorResource(result.Message));
      return Ok(result);

    }
  }
}