using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO.ViewModel;
using DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Model;
using OnlineLearning.Service.Interface;
using DTO.Queries;
using Newtonsoft.Json;

namespace OnlineLearning.Api.Controllers
{
    //[Authorize]
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
      var user = _mapper.Map<UserViewModel, User>(newUser);
      user.Id = Guid.NewGuid();

      var response = await _userService.CreateUserAsync(user);
      if (!response.Success)
      {
        return BadRequest(new ErrorResource(response.Message));
      }

      var userResource = _mapper.Map<User, UserViewModel>(response.User);
      return Ok(userResource);
    }


  }
}