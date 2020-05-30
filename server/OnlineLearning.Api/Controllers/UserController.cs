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