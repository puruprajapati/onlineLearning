using Microsoft.EntityFrameworkCore;
using OnlineLearning.EntityFramework;
using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.EntityFramework
{
    public class SessionReferenceRepository : Repository<SessionReference>, ISessionReferenceRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public SessionReferenceRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<SessionReference> FindById(Guid Id)
        {
            return await context.Set<SessionReference>().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}

