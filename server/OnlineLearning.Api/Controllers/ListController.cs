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
  // [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ListController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IListService _service;
    public ListController(IListService service, IMapper mapper)
    {
      _mapper = mapper;
      _service = service;
    }

    [HttpGet]
    [Route("")]
    public async Task<List<List<ListViewModel>>> GetAllList([FromQuery] BaseParameter baseParameter)
    {
      var userContext = HttpContext.GetUserContext();

      return await _service.GetAllList(userContext);
    }


  }
}