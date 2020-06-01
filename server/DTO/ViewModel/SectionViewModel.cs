using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLearning.DTO.ViewModel
{
    public class SectionViewModel
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public String SectionName { get; set; }
        public String Description { get; set; }
    }
}