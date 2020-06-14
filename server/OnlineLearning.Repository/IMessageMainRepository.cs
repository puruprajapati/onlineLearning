using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Repository
{
    public interface IMessageMainRepository : IRepository<MessageMain>
    {
        Task<MessageMain> FindByMessageId(Guid MessageId);
    }
}
