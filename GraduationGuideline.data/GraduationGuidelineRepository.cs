using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GraduationGuideline.domain.contracts;
using GraduationGuideline.domain.DataTransferObjects;
using GraduationGuideline.data.entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GraduationGuideline.data
{
    public class GraduationGuidelineOptimizerRepository : IRepository
    {

        private GraduationGuidelineContext _graduationGuidelineContext;
        private String[] StepNames = new String[]{
                "GS8", "DiplomaApp", "ScheduleExam", 
                "EtdInfo", "DocSurvey", "Examination", 
                "FinalReport", "FinalVisit", "EtdSubmit", 
                "Copyright", "ProQuestFee", "GraduationFee", 
                "Completion"
        };

        public GraduationGuidelineOptimizerRepository(GraduationGuidelineContext graduationGuidelineContext)
        {
            _graduationGuidelineContext = graduationGuidelineContext ?? throw new ArgumentNullException();

        }
        public void Dispose()
        {
            _graduationGuidelineContext.Dispose();
        }

        public void CreateUser(FullUserDto user) 
        {
            if ( _graduationGuidelineContext.User.SingleOrDefault(x => x.Username == user.Username) != null ){
                throw new ArgumentException("User already exists");
            }

            UserEntity u = new UserEntity {
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

        public UserInfoDto GetUserByUsername(string username)
        {
            var result = _graduationGuidelineContext.User.SingleOrDefault(x => x.Username == username);
            var user = new UserInfoDto {
                Username = result.Username,
                StudentType = result.StudentType,
                Email = result.Email,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Admin = result.Admin
            };
            return user;
        }

        public void EditUser(UserInfoDto user) {
            var u = _graduationGuidelineContext.User.SingleOrDefault(x => x.Username == user.Username);
             u = new UserEntity {
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

        public void RemoveUser(String username) {
            var user = _graduationGuidelineContext.User.SingleOrDefault(x => x.Username == username);
            this.RemoveSteps(username);
            _graduationGuidelineContext.User.Remove(user);
            _graduationGuidelineContext.SaveChanges();
        }
        public void CreateSteps(String username) {
            
            foreach (String name in StepNames){
                StepEntity step = new StepEntity { Username = username, Status = false, StepName = name };
                _graduationGuidelineContext.Step.Add(step);
            }
        }

        public void RemoveSteps(String username) {
            var steps = _graduationGuidelineContext.Step.Where(x => x.Username == username);

            foreach (StepEntity step in steps)
            {
              _graduationGuidelineContext.Step.Remove(step);  
            } 

        }

        public Task<UserInfoDto> GetUserInfoAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}