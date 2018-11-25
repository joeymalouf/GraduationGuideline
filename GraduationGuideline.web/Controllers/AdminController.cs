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

        [HttpGet, Authorize(Roles = "Admin")]
        [Route("api/[controller]/AllUserData")]
        public async Task<ObjectResult> GetAllUserData() {
            var result = await this._adminService.GetallUserData();
            return new OkObjectResult(result);   
        }
    }
}
