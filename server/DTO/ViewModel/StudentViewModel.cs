using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
  public class StudentViewModel
  {
    public Guid Id { get; set; }
    public String Name { get; set; }

    public Guid SchoolId { get; set; }
    public Guid ClassId { get; set; }
    public Guid SectionId { get; set; }
    public String RollNumber { get; set; }
    public Guid ParentId { set; get; }
    public String Active { get; set; }
    public String Email { get; set; }
    public String UserName { get; set; }

    public String ParentName { get; set; }
    public String PrimaryContactNo { get; set; }
    public String SecondaryContactNo { get; set; }
    public String ParentEmailAddress { get; set; }
    public String Address { get; set; }


  }
}
