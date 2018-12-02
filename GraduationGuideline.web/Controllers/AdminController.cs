using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.models;
using GraduationGuideline.domain.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;

namespace GraduationGuideline.web.Controllers
{
    public class AdminController : Controller
    {
        private IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("api/[controller]/AllUserData")]
        public async Task<ObjectResult> GetAllUserData([FromBody] DeadlineKeyDto key) {
            var result = await this._adminService.GetallUserData(key.Semester, key.year);
            return new OkObjectResult(result);   
        }
        [HttpPost, Authorize(Roles = "Admin")]
        [Route("api/[controller]/CreateDeadline")]
        public async Task<IActionResult> CreateDeadline([FromBody] PartialDeadlineDto deadline) {
            await this._adminService.CreateDeadline(deadline).ConfigureAwait(false);
            return new OkResult();   
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("api/[controller]/EditDeadline")]
        public async Task<IActionResult> EditDeadline([FromBody] DeadlineDto deadline) {
            await this._adminService.EditDeadline(deadline).ConfigureAwait(false);
            return new OkResult();   
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("api/[controller]/DeleteDeadline")]
        public async Task<IActionResult> DeleteDeadline([FromBody] DeadlineKeyDto deadline) {
            await this._adminService.RemoveDeadline(deadline).ConfigureAwait(false);
            return new OkResult();   
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("api/[controller]/EditUser")]
        public async Task<IActionResult> EditUser([FromBody] UserWithStepsDto user) {
            await this._adminService.AdminUpdateUser(user).ConfigureAwait(false);
            return new OkResult();   
        }

        [HttpGet, Authorize(Roles = "Admin")]
        [Route("api/[controller]/GetDeadlines")]
        public async Task<ObjectResult> GetAllDeadlines() {
            var result = await this._adminService.GetAllDeadlines().ConfigureAwait(false);
            return new OkObjectResult(result);   
        }
    }
}
