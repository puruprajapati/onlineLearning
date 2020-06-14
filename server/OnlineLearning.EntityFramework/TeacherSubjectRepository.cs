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
    public class TeacherSubjectRepository
 : Repository<TeacherSubject>, ITeacherSubjectRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public TeacherSubjectRepository
(ApplicationDatabaseContext context) : base(context) { }

        public async Task<TeacherSubject> FindById(Guid Id)
        {
            return await context.Set<TeacherSubject>().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}