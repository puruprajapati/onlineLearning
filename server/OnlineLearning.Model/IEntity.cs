using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineLearning.Model
{
	public interface IEntity
	{
		[Key]
		[Required]
		public Guid Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
		public string IPAddress { get; set; }
	}
}
