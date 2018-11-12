using System;
using System.Threading.Tasks;
using GraduationGuideline.domain.DataTransferObjects;

namespace GraduationGuideline.domain.contracts {
    public interface IRepository : IDisposable
    {
        Task<UserInfoDto> GetUserInfoAsync(string username);
        void CreateUser(FullUserDto user);
        void CreateSteps(String username);
    }
}