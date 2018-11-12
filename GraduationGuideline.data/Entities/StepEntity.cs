using System.ComponentModel.DataAnnotations;

namespace GraduationGuideline.data.entities
{
    public class StepEntity
    {
        public string StepName { get; set; }
        public bool Status { get; set; }
        public string Username { get; set; }
        public UserEntity UserEntity { get; set;}
    }
}