using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
    public class MessageReply : BaseEntity
    {
        public Guid? MessageMainId { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? FromUserId { get; set; }
        public Guid? ToUserId { get; set; }
        public String Message { get; set; }
        public Boolean IsSeen { get; set; }
    }
}