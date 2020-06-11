using OnlineLearning.DTO.Queries;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Shared.Enums;

namespace OnlineLearning.EntityFramework
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class, IEntity
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
        }
        public void Update(TModel entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            entities.Update(entity);
        }
        public void Delete(Guid id)
        {
            if (id == null) throw new ArgumentNullException("entity");

            TModel entity = entities.Find(id);
            entity.Active = ActiveStatus.Deleted.ToString();
            entities.Update(entity);
            // entities.Remove(entity);
        }

        public async void MultipleDelete(List<Guid> ListOfId)
        {
            var result = await entities.Where(x => x.Active == ActiveStatus.Active.ToString()).ToListAsync();
            List<TModel> entityCollection = new List<TModel>();
            entityCollection = result;
            if (entityCollection == null || entityCollection.Count == 0)
                throw new ArgumentNullException("No data found");
            entityCollection.ForEach(x => x.Active = ActiveStatus.Deleted.ToString());
        }

        public async Task<PagedList<TModel>> GetPaginatedList(BaseParameter baseParameter)
        {
            var result = await entities.Where(x => x.Active == ActiveStatus.Active.ToString()).ToListAsync();
            return PagedList<TModel>.ToPagedList(result, baseParameter.PageNumber, baseParameter.PageSize);
        }

        public async Task<IEnumerable<TModel>> GetByIds(List<Guid> ids)
        {
            return await entities.Where(r => ids.Contains(r.Id)).ToListAsync();
        }
    }
}