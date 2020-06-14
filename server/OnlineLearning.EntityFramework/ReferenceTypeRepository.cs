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
    public class ReferenceTypeRepository : Repository<ReferenceType>, IReferenceTypeRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public ReferenceTypeRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<ReferenceType> FindById(Guid Id)
        {
            return await context.Set<ReferenceType>().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
