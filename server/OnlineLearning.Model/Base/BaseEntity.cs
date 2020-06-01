using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineLearning.Model
{
	public class BaseEntity
	{
		[Key]
		[Required]
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime ModifiedAt { get; set; } = DateTime.Now;
		public string IPAddress { get; set; }
		public Guid CreatedByUserId { get; set; }
		public Guid ModifiedByUserId { get; set; }
		public string Active { get; set; } = "Active";
	}
}