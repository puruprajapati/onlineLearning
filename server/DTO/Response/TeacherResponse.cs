using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
    public class TeacherResponse : BaseResponse
    {
        public Teacher Teacher { get; private set; }

        public TeacherResponse(bool success, string message, Teacher teacher) : base(success, message)
        {
            Teacher = teacher;
        }

        // success response
        public TeacherResponse(Teacher teacher) : this(true, string.Empty, teacher)
        { }

        // error response
        public TeacherResponse(string message) : this(false, message, null)
        { }
    }
}
