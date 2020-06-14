using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
    public interface ISubmissionStatusRepository : IRepository<SubmissionStatus>
    {
        Task<SubmissionStatus> FindById(Guid Id);
    }
}