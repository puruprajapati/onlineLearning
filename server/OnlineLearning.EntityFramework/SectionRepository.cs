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
    public class SectionRepository : Repository<SectionDetail>, ISectionRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public SectionRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<SectionDetail> FindBySection(string section)
        {
            return await context.Set<SectionDetail>().FirstOrDefaultAsync(x => x.SectionName == section);
        }
    }
}