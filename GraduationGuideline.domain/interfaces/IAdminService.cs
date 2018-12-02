using System.Collections.Generic;
using System.Threading.Tasks;
using GraduationGuideline.domain.DataTransferObjects;
using GraduationGuideline.domain.models;

namespace GraduationGuideline.domain.interfaces
{
    public interface IAdminService
    {
        Task AdminDeleteUser(UserInfoDto user);
        Task AdminUpdateUser(UserWithStepsDto user);
        Task<List<UserWithStepsDto>> GetallUserData(string semester, int year);
        Task CreateDeadline(PartialDeadlineDto deadline);
        Task EditDeadline(DeadlineDto deadline);
        Task<List<DeadlineDto>> GetAllDeadlines();
        Task RemoveDeadline(DeadlineKeyDto deadline);
    }
}