using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
    public class TeacherSubject
        : BaseEntity
    {
        public Guid? SchoolId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? SubjectId { get; set; }
        public Guid? ClassId { get; set; }
    }
}