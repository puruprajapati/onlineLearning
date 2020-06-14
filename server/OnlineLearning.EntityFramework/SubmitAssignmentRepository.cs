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
    public class SubmitAssignmentRepository : Repository<SubmitAssignment>, ISubmitAssignmentRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public SubmitAssignmentRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<SubmitAssignment> FindById(Guid Id)
        {
            return await context.Set<SubmitAssignment>().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}

