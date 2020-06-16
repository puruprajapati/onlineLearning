using Microsoft.AspNetCore.Http;
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Api.Extensions
{
  public static class GeneralExtensions
  {
    public static UserContextInfo GetUserContext(this HttpContext httpContext)
    {
      if (httpContext.User == null)
      {
        return null;
      }

      try
      {
        var userClaim = httpContext.User.Claims;
        var schoolId = Guid.Empty;

        if (!String.IsNullOrEmpty(userClaim.ToList().Where(x => x.Type == "SchoolId").FirstOrDefault().Value))
        {
          schoolId = Guid.Parse(userClaim.ToList().Where(x => x.Type == "SchoolId").FirstOrDefault().Value);
        }

        return new UserContextInfo()
        {
          Id = Guid.Parse(userClaim.ToList().Where(x => x.Type == "UserId").FirstOrDefault().Value),
          FullName = userClaim.ToList().Where(x => x.Type == "FullName").FirstOrDefault().Value,
          SchoolId = schoolId,
          UserName = userClaim.ToList().Where(x => x.Type == "UserName").FirstOrDefault().Value,
          UserRole = userClaim.ToList().Where(x => x.Type == "UserRole").FirstOrDefault().Value
        };
      }
      catch
      {

        return null;
      }

    }
  }
}