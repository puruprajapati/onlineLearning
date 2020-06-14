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
    public class AssignmentSubmissionRepository : Repository<AssignmentSubmission>, IAssignmentSubmissionRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public AssignmentSubmissionRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<AssignmentSubmission> FindAssignmentSubmissionById(Guid Id)
        {
            return await context.Set<AssignmentSubmission>().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}