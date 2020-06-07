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
  public class ClassController : ControllerBase
  {

    private readonly IMapper _mapper;
    private readonly IClassService _classService;
    public ClassController(IClassService classService, IMapper mapper)
    {
      _mapper = mapper;
      _classService = classService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllClasses([FromQuery] BaseParameter baseParameter)
    {
      var results = await _classService.ListAsync(baseParameter);
      var resultViewModel = _mapper.Map<IEnumerable<ClassDetail>, IEnumerable<ClassViewModel>>(results);
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
    public async Task<IActionResult> CreateClassAsync([FromBody] ClassViewModel newClass)
    {
      var userContext = HttpContext.GetUserContext();
      var classData = _mapper.Map<ClassViewModel, ClassDetail>(newClass);

      var response = await _classService.CreateClassDetail(classData, userContext);
      if (!response.Success)
      {
        return BadRequest(new ErrorResource(response.Message));
      }

      var schoolResource = _mapper.Map<ClassDetail, ClassViewModel>(response.ClassDetail);
      return Ok(schoolResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] ClassViewModel request)
    {
      var userContext = HttpContext.GetUserContext();
      var classData = _mapper.Map<ClassViewModel, ClassDetail>(request);
      var result = await _classService.UpdateAsync(id, classData, userContext);

      if (!result.Success)
        return BadRequest(new ErrorResource(result.Message));

      var resultViewModel = _mapper.Map<ClassDetail, ClassViewModel>(result.ClassDetail);
      return Ok(resultViewModel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
      var userContext = HttpContext.GetUserContext();
      var result = await _classService.DeleteAsync(id);

      if (!result.Success)
        return BadRequest(new ErrorResource(result.Message));
      var resultViewModel = _mapper.Map<ClassDetail, ClassViewModel>(result.ClassDetail);
      return Ok(resultViewModel);
    }
  }
}