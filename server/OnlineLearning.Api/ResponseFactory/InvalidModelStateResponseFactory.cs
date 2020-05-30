using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Api.Extensions;
using OnlineLearning.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Api
{
  public static class InvalidModelStateResponseFactory
  {
    public static IActionResult ProduceErrorResponse(ActionContext context)
    {
      var errors = context.ModelState.GetErrorMessages();
      var response = new ErrorResource(messages: errors);

      return new BadRequestObjectResult(response);
    }
  }
}
