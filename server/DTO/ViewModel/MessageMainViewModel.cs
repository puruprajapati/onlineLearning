using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
    public class MessageMainViewModel
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public string Subject { get; set; }
        public Boolean HighPriority { get; set; }
        public String Message { get; set; }
        public Boolean IsSeen { get; set; }
    }
}