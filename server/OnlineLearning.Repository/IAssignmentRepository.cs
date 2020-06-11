using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        Task<Assignment> FindAssignmentById(Guid Id);
    }
}
