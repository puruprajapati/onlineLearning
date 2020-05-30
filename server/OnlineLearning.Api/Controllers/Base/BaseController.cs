using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Api.Controllers.Base
{
	public class BaseController<TModel, TRepository> : ControllerBase where TModel : class where TRepository : IRepository<TModel>
	{
    protected readonly TRepository Repository;

    public BaseController(TRepository repository)
    {
      this.Repository = repository;
    }

    [HttpPost]
    public void Add([FromBody] TModel item)
    {
      Repository.Insert(item);
    }
  }
}
