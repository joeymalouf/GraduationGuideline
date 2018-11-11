using System.ComponentModel.DataAnnotations;

namespace GraduationGuideline.data.entities
{
    public class StepEntity
    {
        [Key]
        public string StepName { get; set; }
        public bool Status { get; set; }
        public string Username { get; set; }
        public UserEntity UserEntity { get; set;}
    }
}