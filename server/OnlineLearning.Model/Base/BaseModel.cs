using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.Model
{
  public class BaseModel
  {
    public Guid UserId { get; set; }
    public Guid SchoolId { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string UserRole { get; set; }

  }
}
