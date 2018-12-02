using System;
using System.Threading.Tasks;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.DataTransferObjects;
using System.Collections.Generic;

namespace GraduationGuideline.domain.services
{
    
    public class AccountService : IAccountService
    {
        private IRepository _repository;
        public AccountService(IRepository _repository)
        {
            this._repository = _repository ?? throw new ArgumentNullException();
        }

        public async Task<bool> CreateAccount(FullUserDto user)
        {
            if (user.Username == null || user.StudentType == null || user.FirstName == null || user.LastName == null || user.Email == null || user.Password == null) {
                throw new ArgumentNullException();
            }

            var result = await _repository.CreateUser(user).ConfigureAwait(false);
            return result;
        }

        public async Task<UserInfoDto> GetUserData(string username)
        {
            if (username == "") {
                throw new ArgumentNullException();
            }
            var result = await this._repository.GetUserByUsername(username).ConfigureAwait(false);
            return result;
        }

        public Task<UserInfoDto> Login(LoginDto user)
        {
            if (user.Username == "" || user.Password == "") {
                throw new ArgumentNullException();
            }
            var result = this._repository.Login(user);
            return result;
        }

        public async Task UpdateUserData(UserInfoDto user)
        {
            await this._repository.EditUser(user).ConfigureAwait(false);
        }
    }
}