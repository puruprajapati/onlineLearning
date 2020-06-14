//ISubmitAssignmentRepository
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
    public interface ISubmitAssignmentRepository : IRepository<SubmitAssignment>
    {
        Task<SubmitAssignment> FindById(Guid Id);
    }
}