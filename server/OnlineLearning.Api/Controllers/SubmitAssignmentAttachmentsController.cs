﻿//SubmitAssignmentAttachmentsController
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
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitAssignmentAttachmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISubmitAssignmentAttachmentsService _service;
        public SubmitAssignmentAttachmentsController(ISubmitAssignmentAttachmentsService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll([FromQuery] BaseParameter baseParameter)
        {
            var results = await _service.ListAsync(baseParameter);
            var resultViewModel = _mapper.Map<IEnumerable<SubmitAssignmentAttachments>, IEnumerable<SubmitAssignmentAttachmentsViewModel>>(results);
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
        public async Task<IActionResult> CreateAsync([FromBody] SubmitAssignmentAttachmentsViewModel newData)
        {
            var userContext = HttpContext.GetUserContext();
            var mappedData = _mapper.Map<SubmitAssignmentAttachmentsViewModel, SubmitAssignmentAttachments>(newData);
            var response = await _service.CreateAsync(mappedData, userContext);
            if (!response.Success)
            {
                return BadRequest(new ErrorResource(response.Message));
            }
            var addSession = _mapper.Map<SubmitAssignmentAttachments, SubmitAssignmentAttachmentsViewModel>(response.SubmitAssignmentAttachments);
            return Ok(addSession);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] SubmitAssignmentAttachmentsViewModel request)
        {
            var userContext = HttpContext.GetUserContext();
            var data = _mapper.Map<SubmitAssignmentAttachmentsViewModel, SubmitAssignmentAttachments>(request);
            var result = await _service.UpdateAsync(id, data, userContext);

            if (!result.Success)
                return BadRequest(new ErrorResource(result.Message));

            var resultViewModel = _mapper.Map<SubmitAssignmentAttachments, SubmitAssignmentAttachmentsViewModel>(result.SubmitAssignmentAttachments);
            return Ok(resultViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var userContext = HttpContext.GetUserContext();
            var result = await _service.DeleteAsync(id, userContext);

            if (!result.Success)
                return BadRequest(new ErrorResource(result.Message));
            var resultViewModel = _mapper.Map<SubmitAssignmentAttachments, SubmitAssignmentAttachmentsViewModel>(result.SubmitAssignmentAttachments);
            return Ok(resultViewModel);
        }

        [HttpPost]
        [Route("deletemultiple")]
        public async Task<IActionResult> MultipleDelete([FromBody] List<Guid> ids)
        {
            var userContext = HttpContext.GetUserContext();
            var result = await _service.MultipleDeleteAsync(ids, userContext);
            if (!result.Success)
                return BadRequest(new ErrorResource(result.Message));
            return Ok(result);
        }
    }
}