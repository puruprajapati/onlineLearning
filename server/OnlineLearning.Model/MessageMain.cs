using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
    public class MessageMain : BaseEntity
    {
        public Guid SchoolId { get; set; }
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public string Subject { get; set; }
        public Boolean HighPriority { get; set; }
        public String Message { get; set; }
        public Boolean IsSeen { get; set; }
    }
}