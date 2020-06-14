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
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public SubjectRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<Subject> FindById(Guid Id)
        {
            return await context.Set<Subject>().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}

