//ISessionStatusRepository

using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
    public interface ISessionStatusRepository : IRepository<SessionStatus>
    {
        Task<SessionStatus> FindById(Guid Id);
    }
}