using System;
using System.Collections.Generic;

namespace GraduationGuideline.domain.DataTransferObjects
{
    public class FullUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string StudentType { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Admin { get; set; }
        public string Semester { get; set; }
        public int year { get; set; }

    }
}