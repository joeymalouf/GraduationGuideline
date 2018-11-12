using System;
using System.Threading.Tasks;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.DataTransferObjects;
using System.Collections.Generic;

namespace GraduationGuideline.domain.services
{
    
    public class StepService : IStepService
    {
        private IRepository _repository;
        public StepService(IRepository _repository)
        {
            this._repository = _repository ?? throw new ArgumentNullException();
        }

        public List<StepDto> GetStepsByUsername(String username)
        {
            return this._repository.GetStepsByUsername(username);
        }
    }
}