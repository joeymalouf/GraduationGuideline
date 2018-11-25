using System.Collections.Generic;
using System.Threading.Tasks;
using GraduationGuideline.domain.DataTransferObjects;
using GraduationGuideline.domain.models;

namespace GraduationGuideline.domain.interfaces
{
    public interface IAdminService
    {
        Task<List<UserWithStepsDto>> GetallUserData();
    }
}