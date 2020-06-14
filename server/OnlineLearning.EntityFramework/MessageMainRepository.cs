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
    public class MessageMainRepository : Repository<MessageMain>, IMessageMainRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public MessageMainRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<MessageMain> FindByMessageId(Guid MessageId)
        {
            return await context.Set<MessageMain>().FirstOrDefaultAsync(x => x.Id == MessageId);
        }
    }
}