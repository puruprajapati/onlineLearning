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
    public class SessionRepository : Repository<SessionDetail>, ISessionRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public SessionRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<SessionDetail> FindSessionBySchoolId(Guid SchoolId)
        {
            return await context.Set<SessionDetail>().FirstOrDefaultAsync(session => session.SchoolId == SchoolId);
        }
    }
}