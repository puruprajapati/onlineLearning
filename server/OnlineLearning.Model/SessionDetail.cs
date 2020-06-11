using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
    public class SessionDetail
            : BaseEntity
    {
        public Guid SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public School School { get; set; }
        public String SessionTitle { get; set; }
        public String SessionDesc { get; set; }
        public Guid ClassId { get; set; }
        [ForeignKey("ClassId")]
        public ClassDetail ClassDetail { get; set; }
        public Guid TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
        public DateTime ScheduledDate { set; get; }
        public string StartingTime { get; set; }
        public string EndingTime { get; set; }
        public Guid? SessionStatusId { get; set; }
    }
}