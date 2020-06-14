using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
    public interface ISessionRepository : IRepository<SessionDetail>
    {
        Task<SessionDetail> FindSessionBySchoolId(Guid SchoolId);
    }
}