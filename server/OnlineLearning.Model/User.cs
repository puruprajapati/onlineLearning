using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearning.Model
{
	public class User : BaseEntity
	{
		public Guid? SchoolId { get; set; }



		[ForeignKey("SchoolId")]
		public School School { get; set; }


		public string UserName { get; set; }


		[DataType(DataType.EmailAddress)]
		[StringLength(255)]

		public string Email { get; set; }
		public string FullName { get; set; }
		public string Password { get; set; }
		public string UserRole { get; set; }
		public bool IsVerified { get; set; }
		public bool Active { get; set; }

	}
}
