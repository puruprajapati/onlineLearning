


using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
    public interface IAssignmentSubmissionRepository : IRepository<AssignmentSubmission>
    {
        Task<AssignmentSubmission> FindAssignmentSubmissionById(Guid Id);
    }
}