using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
  public class SessionViewModel
  {
    public String SessionTitle { get; set; }
    public String SessionDesc { get; set; }
    public Guid ClassId { get; set; }
    public String ClassName { get; set; }
    public Guid TeacherId { get; set; }
    public String TeacherName { get; set; }
    public DateTime ScheduledDate { set; get; }
    public String StartingTime { get; set; }
    public String EndingTime { get; set; }
    public Guid? SessionStatusId { get; set; }

    public String SessionStatus { get; set; }
  }
}