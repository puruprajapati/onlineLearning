//TeacherSubjectResponse
using OnlineLearning.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.Response
{
	public class TeacherSubjectResponse : BaseResponse
	{
		public TeacherSubject TeacherSubject { get; private set; }

		public TeacherSubjectResponse(bool success, string message, TeacherSubject classObject) : base(success, message)
		{
			TeacherSubject = classObject;
		}

		// success response
		public TeacherSubjectResponse(TeacherSubject classObject) : this(true, string.Empty, classObject)
		{ }

		// error response
		public TeacherSubjectResponse(string message) : this(false, message, null)
		{ }
	}
}