using Microsoft.EntityFrameworkCore;
using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.EntityFramework
{
  public class Repository<TModel> : IRepository<TModel> where TModel : class
  {
    protected readonly ApplicationDatabaseContext context;
    private DbSet<TModel> entities;
    string errorMessage = string.Empty;
    public Repository(ApplicationDatabaseContext context)
    {
      this.context = context;
      entities = context.Set<TModel>();
    }
    public async Task<IEnumerable<TModel>> GetAll()
    {
      return await entities.ToListAsync();
    }
    public async Task<TModel> GetById(Guid id)
    {
      return await entities.FindAsync(id);
    }
    public async Task Insert(TModel entity)
    {
      if (entity == null) throw new ArgumentNullException("entity");

      await entities.AddAsync(entity);
      //context.SaveChanges();
    }
    public void Update(TModel entity)
    {
      if (entity == null) throw new ArgumentNullException("entity");
      entities.Update(entity);
      //context.SaveChanges();
    }
    public void Delete(Guid id)
    {
      if (id == null) throw new ArgumentNullException("entity");

      TModel entity = entities.Find(id);
      entities.Remove(entity);
      //context.SaveChanges();
    }
  }
}
