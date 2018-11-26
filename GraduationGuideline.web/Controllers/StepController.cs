using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.DataTransferObjects;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = this._stepService.GetStepsByUsername(username);
            return result;
        }

        [HttpGet, Authorize]
        [Route("api/step/GetStep/{stepName}")]
        public async Task<ObjectResult> GetStep(string stepName){
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = await this._stepService.GetStep(new StepKeyDto{Username = currentUser, StepName = stepName}).ConfigureAwait(false);
            return new OkObjectResult(result);
        }

        [HttpPost]
        [Route("api/step/ToggleStepStatus/{stepName}")]
        public async Task<ObjectResult> ToggleStep(string stepName){
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = await this._stepService.ToggleStep(new StepKeyDto{Username = currentUser, StepName = stepName}).ConfigureAwait(false);
            return new OkObjectResult(result);
        }
    }
}