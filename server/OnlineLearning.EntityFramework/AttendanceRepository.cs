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
    public class AttendanceRepository : Repository<Attendence>, IAttendanceRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public AttendanceRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<Attendence> FindById(Guid Id)
        {
            return await context.Set<Attendence>().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}