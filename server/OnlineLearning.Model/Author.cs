using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
	public class Author : BaseEntity
	{
		public String Name { get; set; }
		public String Country { get; set; }
		public virtual ICollection<Book> Books { get; set; }
	}
}
