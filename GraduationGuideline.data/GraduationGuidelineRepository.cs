using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.DataTransferObjects;
using GraduationGuideline.data.entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

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
        private String[] StepDescriptions = new String[]{
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

        public async Task<bool> CreateUser(FullUserDto user)
        {

            user.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: Encoding.ASCII.GetBytes("skdnasd91231923ji!@#!009vsi=="),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8)
            );

            if (await _graduationGuidelineContext.User.SingleOrDefaultAsync(x => x.Username == user.Username).ConfigureAwait(false) != null)
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
                Admin = user.Admin,
                year = user.year,
                Semester = user.Semester
            };
            await _graduationGuidelineContext.User.AddAsync(u).ConfigureAwait(false);
            this.CreateSteps(user.Username);
            await _graduationGuidelineContext.SaveChangesAsync().ConfigureAwait(false);
            if (await _graduationGuidelineContext.User.SingleOrDefaultAsync(x => x.Username == user.Username).ConfigureAwait(false) != null)
            {
                return true;
            }
            return false;
        }


        public async Task<List<UserWithStepsDto>> GetAllUsersAndSteps(string semester, int year)
        {
            var result = new List<UserEntity>();
            if ((semester == "Fall" || semester == "Spring" || semester == "Summer") && year > 2010 && year < 2050) {
                result = await _graduationGuidelineContext.User.Where(x => x.Semester == semester && x.year == year).ToListAsync().ConfigureAwait(false);
            }
            else {
                result = await _graduationGuidelineContext.User.ToListAsync().ConfigureAwait(false);
            }
            List<UserWithStepsDto> users = new List<UserWithStepsDto>();
            foreach (UserEntity user in result)
            {
                var steps = await this.GetStepsByUsername(user.Username);
                var u = new UserWithStepsDto
                {
                    Username = user.Username,
                    StudentType = user.StudentType,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Admin = user.Admin,
                    Semester = user.Semester,
                    year = user.year,
                    Steps = steps
                };
                users.Add(u);
            }
            return users;

        }
        public async Task<UserInfoDto> GetUserByUsername(string username)
        {
            var result = await _graduationGuidelineContext.User.SingleOrDefaultAsync(x => x.Username == username).ConfigureAwait(false);

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
                Admin = result.Admin,
                Semester = result.Semester,
                year = result.year,
            };
            return user;
        }

        public async Task EditUser(UserInfoDto user)
        {
            var u = await _graduationGuidelineContext.User.SingleOrDefaultAsync(x => x.Username == user.Username).ConfigureAwait(false);
            u.StudentType = user.StudentType;
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Semester = user.Semester;
            u.year = user.year;
            u.Email = user.Email;

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

            for (int i = 0; i < StepNames.Length; i++)
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

        public async Task<List<StepDto>> GetStepsByUsername(String username)
        {
            var Steps = await _graduationGuidelineContext.Step.Where(x => x.Username == username).ToListAsync().ConfigureAwait(false);
            List<StepDto> steps = new List<StepDto>();
            foreach (StepEntity step in Steps)
            {
                steps.Add(new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description,
                });
            }
            return steps;
        }

        public async Task<StepDto> GetStepByKey(StepKeyDto stepKey)
        {
            var step = await _graduationGuidelineContext.Step.SingleOrDefaultAsync(x => x.Username == stepKey.Username && x.StepName == stepKey.StepName).ConfigureAwait(false);
            var Step = new StepDto();
            var user = await GetUserByUsername(stepKey.Username).ConfigureAwait(false);
            if (step.StepName == "GS8")
            {
                Step = new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description,
                    Deadline = _graduationGuidelineContext.Deadline.SingleOrDefault(x => x.Semester == user.Semester && x.year == user.year).GS8,
                };
            }
            else if (step.StepName == "Schedule Exam")
            {
                Step = new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description,
                    Deadline = _graduationGuidelineContext.Deadline.SingleOrDefault(x => x.Semester == user.Semester && x.year == user.year).FinalExam,

                };
            }
            else if (step.StepName == "Survey of Earned Doctorates")
            {
                Step = new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description,
                    Deadline = (await _graduationGuidelineContext.Deadline.SingleOrDefaultAsync(x => x.Semester == user.Semester && x.year == user.year).ConfigureAwait(false)).Survey,

                };
            }
            else if (step.StepName == "Final Visit")
            {
                Step = new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description,
                    Deadline = (await _graduationGuidelineContext.Deadline.SingleOrDefaultAsync(x => x.Semester == user.Semester && x.year == user.year).ConfigureAwait(false)).FinalVisit,

                };
            }
            else if (step.StepName == "ProQuest Fee")
            {
                Step = new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description,
                    Deadline = (await _graduationGuidelineContext.Deadline.SingleOrDefaultAsync(x => x.Semester == user.Semester && x.year == user.year).ConfigureAwait(false)).FinalVisit,

                };
            }
            else if (step.StepName == "Submit Thesis-Dissertation")
            {
                Step = new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description,
                    Deadline = (await _graduationGuidelineContext.Deadline.SingleOrDefaultAsync(x => x.Semester == user.Semester && x.year == user.year).ConfigureAwait(false)).FinalVisit,

                };
            }
            else if (step.StepName == "ETD Info")
            {
                Step = new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description,
                    Deadline = (await _graduationGuidelineContext.Deadline.SingleOrDefaultAsync(x => x.Semester == user.Semester && x.year == user.year).ConfigureAwait(false)).FinalVisit,

                };
            }
            else if (step.StepName == "Publishing and Copyright")
            {
                Step = new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description,
                    Deadline = (await _graduationGuidelineContext.Deadline.SingleOrDefaultAsync(x => x.Semester == user.Semester && x.year == user.year).ConfigureAwait(false)).FinalVisit,

                };
            }
            else if (step.StepName == "Report of Final Exam")
            {
                Step = new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description,
                    Deadline = (await _graduationGuidelineContext.Deadline.SingleOrDefaultAsync(x => x.Semester == user.Semester && x.year == user.year).ConfigureAwait(false)).FinalVisit,

                };
            }
            else
            {
                Step = new StepDto
                {
                    Username = step.Username,
                    StepName = step.StepName,
                    Status = step.Status,
                    Description = step.Description
                };
            }

            return Step;
        }

        public async Task<UserInfoDto> Login(LoginDto user)
        {
            user.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: Encoding.ASCII.GetBytes("skdnasd91231923ji!@#!009vsi=="),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8)
            );

            var User = await _graduationGuidelineContext.User.SingleOrDefaultAsync(x => x.Username == user.Username && x.Password == user.Password).ConfigureAwait(false);

            if (User.Username == "")
            {
                throw new ArgumentException();
            }
            var UserInfo = new UserInfoDto
            {
                Username = User.Username,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Admin = User.Admin,
                StudentType = User.StudentType,
                Email = User.Email,
                Semester = User.Semester,
                year = User.year
            };

            return UserInfo;
        }

        public async Task<StepDto> ToggleStep(StepKeyDto stepKey)
        {
            var step = await _graduationGuidelineContext.Step.SingleOrDefaultAsync(x => x.Username == stepKey.Username && x.StepName == stepKey.StepName).ConfigureAwait(false);

            step.Status = !step.Status;
            _graduationGuidelineContext.Step.Update(step);
            await _graduationGuidelineContext.SaveChangesAsync().ConfigureAwait(false);
            var result = await GetStepByKey(stepKey).ConfigureAwait(false);
            var Step = new StepDto
            {
                Username = result.Username,
                StepName = result.StepName,
                Status = result.Status,
                Description = result.Description,
                Deadline = result.Deadline
            };

            return Step;
        }

        public async Task CreateDeadline(DeadlineDto deadline)
        {
            var Deadline = new DeadlineEntity
            {
                Semester = deadline.Semester,
                year = deadline.year,
                GS8 = deadline.GS8,
                ProQuest = deadline.ProQuest,
                FinalExam = deadline.FinalExam,
                FinalVisit = deadline.FinalVisit,
                Survey = deadline.Survey,
                Graduation = deadline.Graduation,
                Commencement = deadline.Commencement,
                Hooding = deadline.Hooding,
                Audit = deadline.Audit,
            };
            await _graduationGuidelineContext.Deadline.AddAsync(Deadline).ConfigureAwait(false);
            await _graduationGuidelineContext.SaveChangesAsync().ConfigureAwait(false);

        }

        public async Task EditDeadline(DeadlineDto deadline)
        {

            var Deadline = new DeadlineEntity
            {
                Semester = deadline.Semester,
                year = deadline.year,
                GS8 = deadline.GS8,
                ProQuest = deadline.ProQuest,
                FinalVisit = deadline.FinalVisit,
                FinalExam = deadline.FinalExam,
                Survey = deadline.Survey,
                Graduation = deadline.Graduation,
                Commencement = deadline.Commencement,
                Hooding = deadline.Hooding,
                Audit = deadline.Audit,
            };

            _graduationGuidelineContext.Deadline.Update(Deadline);
            await _graduationGuidelineContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteDeadline(DeadlineKeyDto deadline)
        {

            var result = await _graduationGuidelineContext.Deadline.SingleOrDefaultAsync(x => x.year == deadline.year && x.Semester == deadline.Semester).ConfigureAwait(false);
            _graduationGuidelineContext.Remove(result);
            await _graduationGuidelineContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<DeadlineDto> GetDeadline(DeadlineKeyDto deadline)
        {
            var result = await _graduationGuidelineContext.Deadline.SingleOrDefaultAsync(x => x.year == deadline.year && x.Semester == deadline.Semester).ConfigureAwait(false);
            var Deadline = new DeadlineDto
            {
                Semester = result.Semester,
                year = result.year,
                GS8 = result.GS8,
                ProQuest = result.ProQuest,
                FinalVisit = result.FinalVisit,
                FinalExam = result.FinalExam,
                Survey = result.Survey,
                Graduation = result.Graduation,
                Commencement = result.Commencement,
                Hooding = result.Hooding,
                Audit = result.Audit,
            };
            return Deadline;
        }

        public async Task<List<DeadlineDto>> GetAllDeadlines()
        {
            var result = await _graduationGuidelineContext.Deadline.ToListAsync().ConfigureAwait(false);
            List<DeadlineDto> deadlines = new List<DeadlineDto>();
            foreach (DeadlineEntity deadline in result)
            {
                var Deadline = new DeadlineDto
                {
                    Semester = deadline.Semester,
                    year = deadline.year,
                    GS8 = deadline.GS8,
                    ProQuest = deadline.ProQuest,
                    FinalVisit = deadline.FinalVisit,
                    FinalExam = deadline.FinalExam,
                    Survey = deadline.Survey,
                    Graduation = deadline.Graduation,
                    Commencement = deadline.Commencement,
                    Hooding = deadline.Hooding,
                    Audit = deadline.Audit,
                };
                deadlines.Add(Deadline);
            }
            return deadlines;
        }

        public async Task<DeadlineDto> GetDates(string semester, int year)
        {
            var deadline = await _graduationGuidelineContext.Deadline.SingleOrDefaultAsync(x => x.Semester == semester && x.year == year).ConfigureAwait(false);
            var Deadline = new DeadlineDto
                {
                    Semester = deadline.Semester,
                    year = deadline.year,
                    GS8 = deadline.GS8,
                    ProQuest = deadline.ProQuest,
                    FinalVisit = deadline.FinalVisit,
                    FinalExam = deadline.FinalExam,
                    Survey = deadline.Survey,
                    Graduation = deadline.Graduation,
                    Commencement = deadline.Commencement,
                    Hooding = deadline.Hooding,
                    Audit = deadline.Audit,
                };
            return Deadline;
        }

        public async Task AdminEditUser(UserWithStepsDto user)
        {
            var User = await _graduationGuidelineContext.User.SingleOrDefaultAsync(x => x.Username == user.Username).ConfigureAwait(false);
            User.StudentType = user.StudentType;
            User.FirstName = user.FirstName;
            User.LastName = user.LastName;
            User.Semester = user.Semester;
            User.year = user.year;
            User.Email = user.Email;
            User.Admin = user.Admin;

            _graduationGuidelineContext.User.Update(User);
            foreach (StepDto step in user.Steps) {
                var Step = await _graduationGuidelineContext.Step.SingleOrDefaultAsync(x => x.Username == step.Username && x.StepName == step.StepName).ConfigureAwait(false);
                Step.Status = step.Status;
                _graduationGuidelineContext.Step.Update(Step);
            }
            _graduationGuidelineContext.SaveChanges();
        }

        public Task AdminCreateUser(UserInfoDto user)
        {
            throw new NotImplementedException();
        }

        public async Task AdminDeleteUser(UserInfoDto user)
        {
            this.RemoveSteps(user.Username);
            var result = await _graduationGuidelineContext.User.SingleOrDefaultAsync(x => x.Username == user.Username).ConfigureAwait(false);
            _graduationGuidelineContext.User.Remove(result);
            _graduationGuidelineContext.SaveChanges();
        }
    }
}
