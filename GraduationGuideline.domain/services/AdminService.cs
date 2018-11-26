using System;
using System.Threading.Tasks;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.DataTransferObjects;
using System.Collections.Generic;

namespace GraduationGuideline.domain.services
{
    
    public class AdminService : IAdminService
    {
        private IRepository _repository;
        public AdminService(IRepository _repository)
        {
            this._repository = _repository ?? throw new ArgumentNullException();
        }

        public async Task<List<UserWithStepsDto>> GetallUserData()
        {
            var result = await this._repository.GetAllUsersAndSteps().ConfigureAwait(false);
            return result;
        }

        public async Task CreateDeadline(DeadlineDto deadline){
            await this._repository.CreateDeadline(deadline).ConfigureAwait(false);  
        }
    }
}