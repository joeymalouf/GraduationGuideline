using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.models;
using GraduationGuideline.domain.DataTransferObjects;
using System.Security.Claims;

namespace GraduationGuideline.web.Controllers
{
    public class StepController : Controller
    {
        private IStepService _stepService;
        public StepController(IStepService stepService)
        {
            _stepService = stepService;
        }

        [HttpGet]
        [Route("api/step/GetStepsByUsername/{username}")]
        public List<StepDto> GetStepsByUsername(string username)
        {
            return this._stepService.GetStepsByUsername(username);
        }

        [HttpGet]
        [Route("api/step/GetCurrentUserSteps")]
        public List<StepDto> GetCurrentUserSteps()
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._stepService.GetStepsByUsername(username);
            return result;
        }

        [HttpGet]
        [Route("api/step/GetStep/{stepName}")]
        public async Task<ObjectResult> GetStep(string stepName){
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await this._stepService.GetStep(new StepKeyDto{Username = currentUser, StepName = stepName}).ConfigureAwait(false);
            return new OkObjectResult(result);
        }
    }
}