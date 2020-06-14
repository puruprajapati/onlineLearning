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
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public AssignmentRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<Assignment> FindAssignmentById(Guid Id)
        {
            return await context.Set<Assignment>().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}