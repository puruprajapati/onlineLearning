using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
    public class TeacherSubjectViewModel
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid TeacherId { get; set; }

        public Guid SubjectId { get; set; }

        public Guid ClassId { get; set; }
    }
}