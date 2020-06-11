using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
    public class SessionViewModel
    {
        public String SessionTitle { get; set; }
        public String SessionDesc { get; set; }
        public Guid ClassId { get; set; }
        public Guid TeacherId { get; set; }
        public DateTime ScheduledDate { set; get; }
        public string StartingTime { get; set; }
        public string EndingTime { get; set; }
        public Guid? SessionStatusId { get; set; }
    }
}