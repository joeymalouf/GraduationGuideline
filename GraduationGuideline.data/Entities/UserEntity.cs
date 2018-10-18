using System.ComponentModel.DataAnnotations;

namespace GraduationGuideline.data.entities
{
 public class UserEntity
 {
     [Key]
     public string Username { get; set; }
     public string Password { get; set; }
 }   
}