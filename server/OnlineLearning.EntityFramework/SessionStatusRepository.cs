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
    public class SessionStatusRepository : Repository<SessionStatus>, ISessionStatusRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public SessionStatusRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<SessionStatus> FindById(Guid Id)
        {
            return await context.Set<SessionStatus>().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}

