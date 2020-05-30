using Microsoft.EntityFrameworkCore;
using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineLearning.EntityFramework.Abstract
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
    public IEnumerable<TModel> GetAll()
    {
      return entities.ToList();
    }
    public TModel GetById(Guid id)
    {
      return entities.Find(id);
    }
    public void Insert(TModel entity)
    {
      if (entity == null) throw new ArgumentNullException("entity");

      entities.Add(entity);
      context.SaveChanges();
    }
    public void Update(TModel entity)
    {
      if (entity == null) throw new ArgumentNullException("entity");
      context.SaveChanges();
    }
    public void Delete(Guid id)
    {
      if (id == null) throw new ArgumentNullException("entity");

      TModel entity = entities.Find(id);
      entities.Remove(entity);
      context.SaveChanges();
    }
  }
}
