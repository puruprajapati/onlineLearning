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

namespace OnlineLearning.Api.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UsersController(IUserService userService, IMapper mapper)
    {
      _userService = userService;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] BaseParameter baseParameter)
    {
      //var userContext = HttpContext.GetUserContext(); //pprajapati: reference for user context
      var results = await _userService.ListAsync(baseParameter);
      var resultViewModel = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(results);
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
    public async Task<IActionResult> CreateUserAsync([FromBody] UserViewModel newUser)
    {
      var userContext = HttpContext.GetUserContext();
      var user = _mapper.Map<UserViewModel, User>(newUser);

      var response = await _userService.CreateUserAsync(user);
      if (!response.Success)
      {
        return BadRequest(new ErrorResource(response.Message));
      }

      var userResource = _mapper.Map<User, UserViewModel>(response.User);
      return Ok(userResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
      var userContext = HttpContext.GetUserContext();
      var result = await _userService.DeleteAsync(id, userContext);

      if (!result.Success)
        return BadRequest(new ErrorResource(result.Message));
      var resultViewModel = _mapper.Map<User, UserViewModel>(result.User);
      return Ok(resultViewModel);
    }

    [HttpPost]
    [Route("deletemultiple")]
    public async Task<IActionResult> MultipleDelete([FromBody] List<Guid> ids)
    {
      var userContext = HttpContext.GetUserContext();
      var result = await _userService.MultipleDeleteAsync(ids, userContext);
      if (!result.Success)
        return BadRequest(new ErrorResource(result.Message));
      return Ok(result);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, [FromBody] UserViewModel request)
    {
      var userContext = HttpContext.GetUserContext();
      var user = _mapper.Map<UserViewModel, User>(request);
      var result = await _userService.UpdateAsync(id, user, userContext);

      if (!result.Success)
        return BadRequest(new ErrorResource(result.Message));

      var resultViewModel = _mapper.Map<User, UserViewModel>(result.User);
      return Ok(resultViewModel);
    }


  }
}