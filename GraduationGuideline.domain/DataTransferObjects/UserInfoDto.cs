namespace GraduationGuideline.domain.DataTransferObjects
{
    public class UserInfoDto
    {
        public string Username { get; set; }
        public string StudentType { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public bool Admin { get; set; }
    }
}