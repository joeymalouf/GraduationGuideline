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
        List<StepDto> GetStepsByUsername(string username);
        Task<StepDto> GetStepByKey(StepKeyDto stepKey);
        Task<List<UserWithStepsDto>> GetAllUsersAndSteps();
        Task<UserInfoDto> Login(LoginDto user);
        Task<StepDto> ToggleStep(StepKeyDto stepKey);
    }
}