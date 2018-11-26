using System;
using System.ComponentModel.DataAnnotations;

namespace GraduationGuideline.data.entities
{
    public class DeadlineEntity
    {
        public string Semester { get; set; }
        public int year { get; set; }
        public DateTime GS8 { get; set; }
        public DateTime ProQuest { get; set; }
        public DateTime FinalVisit { get; set; }
        public DateTime FinalExam { get; set; }
        public DateTime Survey { get; set; }
        public DateTime Graduation { get; set; }
        public DateTime Commencement { get; set; }
        public DateTime Hooding { get; set; }
        public DateTime Audit { get; set; }
    }
}