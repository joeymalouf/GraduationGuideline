
using System;

namespace GraduationGuideline.domain.DataTransferObjects
{
    public class StepDto
    {
        public string StepName { get; set; }
        public bool Status { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set;}
    }
}