using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.models;
using GraduationGuideline.domain.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GraduationGuideline.web.Controllers
{
    public class AccountsController : Controller
    {
        private IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet, Authorize]
        [Route("api/[controller]/userdata")]
        public async Task<ObjectResult> GetUserData() {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = await this._accountService.GetUserData(username).ConfigureAwait(false);
            return new OkObjectResult(result);   
        }

        [HttpPost, Authorize]
        [Route("api/[controller]/update")]
        public async Task<IActionResult> UpdateUserData([FromBody] UserInfoDto user) {
            await this._accountService.UpdateUserData(user).ConfigureAwait(false);
            return new OkResult();   
        }
    }
}
