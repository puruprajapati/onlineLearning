using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
    public class StudentResponse : BaseResponse
    {
        public Student Student { get; private set; }

        public StudentResponse(bool success, string message, Student student) : base(success, message)
        {
            Student = student;
        }

        // success response
        public StudentResponse(Student student) : this(true, string.Empty, student)
        { }

        // error response
        public StudentResponse(string message) : this(false, message, null)
        { }
    }
}
