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
    public class MessageReplyRepository : Repository<MessageReply>, IMessageReplyRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public MessageReplyRepository(ApplicationDatabaseContext context) : base(context) { }

        public async Task<MessageReply> FindByMessageId(Guid MessageReplyId)
        {
            return await context.Set<MessageReply>().FirstOrDefaultAsync(x => x.Id == MessageReplyId);
        }
    }
}