using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraduationGuideline.domain.DataTransferObjects;

namespace GraduationGuideline.domain.interfaces {
    public interface IRepository : IDisposable
    {
        Task<UserInfoDto> GetUserInfoAsync(string username);
        void CreateUser(FullUserDto user);
        void CreateSteps(String username);
        List<StepDto> GetStepsByUsername(string username);
        Task<StepDto> GetStepByKey(StepKeyDto stepKey);
    }
}