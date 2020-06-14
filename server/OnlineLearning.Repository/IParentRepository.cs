using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
    public interface IParentRepository : IRepository<Parent>
    {
        Task<Parent> FindBySection(string ParentName);
    }
}