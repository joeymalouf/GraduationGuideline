using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.models;
using GraduationGuideline.domain.DataTransferObjects;

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

        [HttpPost]
        [Route("api/step/GetStep")]
        public async Task<ObjectResult> GetStep([FromBody] StepKeyDto step ){
            
            StepDto result = await this._stepService.GetStep(step);
            return new OkObjectResult(result);
        }
    }
}
