using System.Collections.Generic;
using System.Threading.Tasks;
using GraduationGuideline.domain.DataTransferObjects;
using GraduationGuideline.domain.models;

namespace GraduationGuideline.domain.interfaces
{
    public interface IStepService
    {
        Task<List<StepDto>> GetStepsByUsername(string username);
        Task<StepDto> GetStep(StepKeyDto step);
        Task<StepDto> ToggleStep(StepKeyDto step);
        Task<DeadlineDto> GetDates(string username);
    }
}