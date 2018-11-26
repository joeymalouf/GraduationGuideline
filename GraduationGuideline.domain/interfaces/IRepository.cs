using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraduationGuideline.domain.DataTransferObjects;

namespace GraduationGuideline.domain.interfaces {
    public interface IRepository : IDisposable
    {
        Task<UserInfoDto> GetUserByUsername(string username);
        Task<bool> CreateUser(FullUserDto user);
        void CreateSteps(String username);
        Task<List<StepDto>> GetStepsByUsername(string username);
        Task<StepDto> GetStepByKey(StepKeyDto stepKey);
        Task<List<UserWithStepsDto>> GetAllUsersAndSteps();
        Task<UserInfoDto> Login(LoginDto user);
        Task<StepDto> ToggleStep(StepKeyDto stepKey);
        Task CreateDeadline(DeadlineDto deadline);
        Task<DeadlineDto> GetDates(string semester, int year);
        Task EditDeadline(DeadlineDto deadline);
        Task DeleteDeadline(DeadlineKeyDto deadline);
        Task<DeadlineDto> GetDeadline(DeadlineKeyDto deadline);
        Task<List<DeadlineDto>> GetAllDeadlines();
        
    }
}