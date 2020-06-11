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
    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public ParentRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<Parent> FindBySection(string ParentName)
        {
            return await context.Set<Parent>().FirstOrDefaultAsync(x => x.ParentName == ParentName);
        }
    }
}