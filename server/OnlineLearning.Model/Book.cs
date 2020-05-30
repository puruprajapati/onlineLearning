using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Model
{
	public class Book : BaseEntity
  {

    [Required]
    [StringLength(64, MinimumLength = 5)]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
  }
}
