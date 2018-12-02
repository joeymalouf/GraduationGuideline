using System;

namespace GraduationGuideline.domain.DataTransferObjects
{
    public class PartialDeadlineDto
    {
        public string Semester { get; set; }
        public int year { get; set; }
        public DateTime GS8 { get; set; }
        public DateTime ProQuest { get; set; }
        public DateTime Graduation { get; set; }
        public DateTime Commencement { get; set; }
        public DateTime Hooding { get; set; }
        public DateTime Audit { get; set; }
    }
}