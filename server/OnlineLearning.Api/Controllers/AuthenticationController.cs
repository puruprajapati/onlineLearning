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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IMapper mapper, IAuthenticationService authenticationService)
    {
      _authenticationService = authenticationService;
      _mapper = mapper;
    }

    [AllowAnonymous]
    [Route("/api/login")]
    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel userCredentials)
    {
      var response = await _authenticationService.CreateAccessTokenAsync(userCredentials.UserName, userCredentials.Password);
      if (!response.Success)
      {
        return BadRequest(new ErrorResource(response.Message));
      }

      //TODO: why automapper is not working
      //var accessTokenResource = _mapper.Map<AccessToken, AccessTokenViewModel>(response.Token);
      var accessTokenResource = new AccessTokenViewModel()
      {
        AccessToken = response.Token.Token,
        RefreshToken = response.Token.RefreshToken.Token,
        Expiration = response.Token.Expiration
      };
      return Ok(accessTokenResource);
    }
  }
}