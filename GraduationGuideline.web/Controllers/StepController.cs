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

        [HttpGet, Authorize]
        [Route("api/step/GetStepsByUsername/{username}")]
        public async Task<ObjectResult> GetStepsByUsername(string username)
        {
            var result = await this._stepService.GetStepsByUsername(username).ConfigureAwait(false);
            return new OkObjectResult(result);
        }

        [HttpGet, Authorize]
        [Route("api/step/GetCurrentUserSteps")]
        public async Task<List<StepDto>> GetCurrentUserSteps()
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = await this._stepService.GetStepsByUsername(username).ConfigureAwait(false);
            return result;
        }

        [HttpGet, Authorize]
        [Route("api/step/GetStep/{stepName}")]
        public async Task<ObjectResult> GetStep(string stepName){
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = await this._stepService.GetStep(new StepKeyDto{Username = currentUser, StepName = stepName}).ConfigureAwait(false);
            return new OkObjectResult(result);
        }

        [HttpPost, Authorize]
        [Route("api/step/ToggleStepStatus/{stepName}")]
        public async Task<ObjectResult> ToggleStep(string stepName){
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = await this._stepService.ToggleStep(new StepKeyDto{Username = currentUser, StepName = stepName}).ConfigureAwait(false);
            return new OkObjectResult(result);
        }

        [HttpGet, Authorize]
        [Route("api/step/GetDates")]
        public async Task<ObjectResult> GetDates()
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var result = await this._stepService.GetDates(username).ConfigureAwait(false);
            return new OkObjectResult(result);
        }
    }
}