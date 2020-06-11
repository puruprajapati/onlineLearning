using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
    public class SessionViewModel
    {
        public Guid SchoolId { get; set; }
        public String SessionTitle { get; set; }
        public String SessionDesc { get; set; }
        public Guid ClassId { get; set; }
        public Guid TeacherId { get; set; }
        public DateTime ScheduledDate { set; get; }
        public TimeSpan StartingTime { get; set; }
        public TimeSpan EndingTime { get; set; }
        public Guid? SessionStatusId { get; set; }
    }
}