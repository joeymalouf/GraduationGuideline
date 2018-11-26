using System;

namespace GraduationGuideline.domain.DataTransferObjects
{
    public class DeadlineDto
    {
        public string Semester { get; set; }
        public int year { get; set; }
        public DateTime GS8 { get; set; }
        public DateTime FinalVisit { get; set; }
        public DateTime FinalExam { get; set; }
        public DateTime Survey { get; set; }
        public DateTime? Graduation { get; set; }
        public DateTime? Commencement { get; set; }
        public DateTime? Hooding { get; set; }
        public DateTime? Audit { get; set; }
    }
}