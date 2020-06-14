using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
  public class BaseViewModel
  {
    public Guid UserId { get; set; }
    public Guid? SchoolId { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string UserRole { get; set; }
  }
}
