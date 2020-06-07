using OnlineLearning.DTO.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
  public interface IRepository<TModel> where TModel : class
  {
    Task<PagedList<TModel>> GetPaginatedList(BaseParameter baseParameter);
    Task<IEnumerable<TModel>> GetAll();
    Task<TModel> GetById(Guid id);
    Task Insert(TModel entity);
    void Update(TModel entity);
    void Delete(Guid id);

    void MultipleDelete(List<Guid> ids);
    Task<IEnumerable<TModel>> GetByIds(List<Guid> ids);
  }
}
