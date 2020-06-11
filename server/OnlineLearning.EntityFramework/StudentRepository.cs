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
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public StudentRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<Student> FindByStudentName(string StudentName)
        {
            return await context.Set<Student>().FirstOrDefaultAsync(x => x.Name == StudentName);
        }
    }
}