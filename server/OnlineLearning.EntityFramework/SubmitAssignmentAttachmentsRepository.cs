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
    public class SubmitAssignmentAttachmentsRepository
 : Repository<SubmitAssignmentAttachments>, ISubmitAssignmentAttachmentsRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return ApplicationDatabaseContext as ApplicationDatabaseContext; }
        }

        public SubmitAssignmentAttachmentsRepository
(ApplicationDatabaseContext context) : base(context) { }

        public async Task<SubmitAssignmentAttachments> FindById(Guid Id)
        {
            return await context.Set<SubmitAssignmentAttachments>().FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}