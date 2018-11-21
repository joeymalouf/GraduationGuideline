using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.DataTransferObjects;
using GraduationGuideline.data.entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GraduationGuideline.data
{
    public class GraduationGuidelineRepository : IRepository
    {

        private GraduationGuidelineContext _graduationGuidelineContext;
        
        //names and description should  be matched by index
        //make 2d array for it when time allows
        private String[] StepNames = new String[]{
                "GS8", "Diploma App", "Schedule Exam",
                "ETD Info", "Survey of Earned Doctorates", "Thesis-Disseration and Exam",
                "Report of Final Exam", "Final Visit", "Submit Thesis-Dissertation",
                "Publishing and Copyright", "ProQuest Fee", "Graduation Fee",
                "Completion"
        };
        private String [] StepDescriptions = new String[]{
            "Complete a GS8, Application for Graduate Degree Form.",
            "Complete an online Diploma Application. Email from registrar@olemiss.edu.",
            "Complete A GS7 to schedule your final examination.",
            "Obtain information about formatting and rights for your ETD.",
            "Complete a Survey of Earned Doctorates.",
            "Complete Thesis/Dissertation and Final Examination.",
            "Your adviser or department chair must submit a Report of Final Examination.",
            "Make a final trip tp the graduate school with completed forms.",
            "Electronically Submit your Thesis/Dissertation.",
            "Learn about and select your copyright binding options for yur work.",
            "Pay fees to ProQuest to submit your thesis/dissertation.",
            "Pay Graduation fee to the University of Mississipi.",
            "Celebrate!"

        };

        public GraduationGuidelineRepository(GraduationGuidelineContext graduationGuidelineContext)
        {
            _graduationGuidelineContext = graduationGuidelineContext ?? throw new ArgumentNullException();

        }
        public void Dispose()
        {
            _graduationGuidelineContext.Dispose();
        }

        public void CreateUser(FullUserDto user)
        {
            if (_graduationGuidelineContext.User.SingleOrDefault(x => x.Username == user.Username) != null)
            {
                throw new ArgumentException("User already exists");
            }

            UserEntity u = new UserEntity
            {
                Username = user.Username,
                Password = user.Password,
                StudentType = user.StudentType,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Admin = user.Admin
            };
            _graduationGuidelineContext.User.Add(u);
            this.CreateSteps(user.Username);
            _graduationGuidelineContext.SaveChanges();
        }

        public List<UserWithStepsDto> GetAllUsersAndSteps()
        {
            var result = _graduationGuidelineContext.User;
            List<UserWithStepsDto> users = new List<UserWithStepsDto>();
            foreach (UserEntity user in result)
            {
                var steps = this.GetStepsByUsername(user.Username);
                var u = new UserWithStepsDto
                {
                    Username = user.Username,
                    StudentType = user.StudentType,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Admin = user.Admin,
                    Steps = steps
                };
                users.Add(u);
            }
            return users;

        }
        public UserInfoDto GetUserByUsername(string username)
        {
            var result = _graduationGuidelineContext.User.SingleOrDefault(x => x.Username == username);

            if (result == null)
            {
                return null;
            }
            var user = new UserInfoDto
            {
                Username = result.Username,
                StudentType = result.StudentType,
                Email = result.Email,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Admin = result.Admin
            };
            return user;
        }

        public void EditUser(UserInfoDto user)
        {
            var u = _graduationGuidelineContext.User.SingleOrDefault(x => x.Username == user.Username);
            u = new UserEntity
            {
                Username = user.Username,
                Password = u.Password,
                StudentType = user.StudentType,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Admin = user.Admin
            };
            _graduationGuidelineContext.User.Update(u);
            _graduationGuidelineContext.SaveChanges();
        }

        public void RemoveUser(String username)
        {
            var user = _graduationGuidelineContext.User.SingleOrDefault(x => x.Username == username);
            this.RemoveSteps(username);
            _graduationGuidelineContext.User.Remove(user);
            _graduationGuidelineContext.SaveChanges();
        }
        public void CreateSteps(String username)
        {

            for(int i = 0; i < StepNames.Length; i++)
            {
                StepEntity step = new StepEntity { Username = username, Status = false, StepName = StepNames[i], Description = StepDescriptions[i] };
                _graduationGuidelineContext.Step.Add(step);
            }
        }

        public void RemoveSteps(String username)
        {
            var steps = _graduationGuidelineContext.Step.Where(x => x.Username == username);

            foreach (StepEntity step in steps)
            {
                _graduationGuidelineContext.Step.Remove(step);
            }

        }

        public void EditStepStatus(StepDto step)
        {
            var Step = _graduationGuidelineContext.Step.SingleOrDefault(x => x.Username == step.Username && x.StepName == step.StepName);
            Step.Status = step.Status;
            _graduationGuidelineContext.Step.Update(Step);
            _graduationGuidelineContext.SaveChanges();
        }

        public List<StepDto> GetStepsByUsername(String username)
        {
            var Steps = _graduationGuidelineContext.Step.Where(x => x.Username == username);
            List<StepDto> steps = new List<StepDto>();
            foreach (StepEntity step in Steps)
            {
                steps.Add(new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description
                });
            }
            return steps;
        }
        public Task<UserInfoDto> GetUserInfoAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}