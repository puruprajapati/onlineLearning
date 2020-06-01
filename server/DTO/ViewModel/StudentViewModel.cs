using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
  public class StudentViewModel
  {
    public Guid Id { get; set; }
    public Guid SchoolId { get; set; }
    public String Name { get; set; }
    public Guid ClassId { get; set; }
    public Guid SectionId { get; set; }
    public String RollNumber { get; set; }
    public Guid ParentId { set; get; }
    public Boolean Active { get; set; }

    public String Email { get; set; }
    public String UserName { get; set; }
  }
}