using Microsoft.EntityFrameworkCore;
using OnlineLearning.DTO.Queries;
using OnlineLearning.EntityFramework;
using OnlineLearning.EntityFramework.Context;
using OnlineLearning.Model;
using OnlineLearning.Repository;
using OnlineLearning.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<PagedList<SessionDetail>> GetCustomPaginatedList(BaseParameter baseParameter)
    {
      var result = await context.Set<SessionDetail>().Where(x => x.Active == ActiveStatus.Active.ToString()).Include(t => t.Teacher).Include(c => c.ClassDetail).ToListAsync();
      return PagedList<SessionDetail>.ToPagedList(result, baseParameter.PageNumber, baseParameter.PageSize);
    }
  }
}