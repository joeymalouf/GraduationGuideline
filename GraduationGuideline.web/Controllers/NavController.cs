using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.models;
using GraduationGuideline.domain.DataTransferObjects;

namespace GraduationGuideline.web.Controllers
{
    public class NavController : Controller
    {
        private IStepService _stepService;
        public NavController(IStepService stepService)
        {
            _stepService = stepService;
        }

        [HttpGet]
        [Route("api/nav/GetStepsByUsername/{username}")]
        public async Task<ObjectResult> GetStepsByUsername(string username)
        {
            var result = await this._stepService.GetStepsByUsername(username).ConfigureAwait(false);
            return new OkObjectResult(result);
        }
    }
}
