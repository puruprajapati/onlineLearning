using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineLearning.Model
{
    public class MessageMain : BaseEntity
    {
        public Guid SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public School School { get; set; }
        public Guid FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        public User UserFrom { get; set; }
        public Guid ToUserId { get; set; }
        [ForeignKey("ToUserId")]
        public User UserTo { get; set; }
        public string Subject { get; set; }
        public Boolean HighPriority { get; set; }
        public String Message { get; set; }
        public Boolean IsSeen { get; set; }
    }
}