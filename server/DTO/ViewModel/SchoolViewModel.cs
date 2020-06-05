using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
  public class SchoolViewModel
  {
    public Guid Id { get; set; }
    public String SchoolCode { get; set; }
    public String Name { get; set; }
    public String Address { get; set; }
    public String ContactNumber { get; set; }
    public String EmailAddress { get; set; }
    public String LogoLocation { get; set; }
    public String Active { get; set; }
  }
}