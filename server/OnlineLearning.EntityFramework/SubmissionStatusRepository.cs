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
    public class SubmissionStatusRepository : Repository<SubmissionStatus>, ISubmissionStatusRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public SubmissionStatusRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<SubmissionStatus> FindById(Guid Id)
        {
            return await context.Set<SubmissionStatus>().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}

