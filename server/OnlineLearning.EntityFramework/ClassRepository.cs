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
    public class ClassRepository : Repository<ClassDetail>, IClassRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public ClassRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<ClassDetail> FindByClass(string ClassName)
        {
            return await context.Set<ClassDetail>().FirstOrDefaultAsync(x => x.ClassName == ClassName);
        }
    }
}