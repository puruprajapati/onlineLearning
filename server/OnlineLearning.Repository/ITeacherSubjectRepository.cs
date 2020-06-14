//ITeacherTeacherSubjectRepository

using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
    public interface ITeacherSubjectRepository : IRepository<TeacherSubject>
    {
        Task<TeacherSubject> FindById(Guid Id);
    }
}