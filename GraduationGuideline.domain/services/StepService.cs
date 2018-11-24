using System;
using System.Threading.Tasks;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.DataTransferObjects;
using System.Collections.Generic;

namespace GraduationGuideline.domain.services
{
    
    public class StepService : IStepService
    {
        private IRepository _repository;
        public StepService(IRepository _repository)
        {
            this._repository = _repository ?? throw new ArgumentNullException();
        }

        public async Task<StepDto> GetStep(StepKeyDto stepKey)
        {
            if (stepKey.StepName == null || stepKey.Username == null){
                throw new ArgumentException();
            }
            StepDto step = await _repository.GetStepByKey(stepKey).ConfigureAwait(false);

            return step;
        }

        public List<StepDto> GetStepsByUsername(String username)
        {
            return SelectionSortByName(this._repository.GetStepsByUsername(username));
        }

        public List<StepDto> SelectionSortByName(List<StepDto> steps){

            String[] StepNames = new String[]{
                "GS8", "Diploma App", "Schedule Exam",
                "ETD Info", "Survey of Earned Doctorates", "Thesis/Disseration and Exam",
                "Report of Final Exam", "Final Visit", "Submit Thesis/Dissertation",
                "Publishing and Copyright", "ProQuest Fee", "Graduation Fee",
                "Completion"
            };
            //counter needed because lists may not be same size
            //it keeps up with the current index in the DTO list

            int counter = 0;
            for(int i = 0; i < StepNames.Length; i++) {
                for(int j = counter; j < steps.Count; j++) {
                    if (steps[j].StepName == StepNames[i]){
                        StepDto temp = steps[counter];
                        steps[counter] = steps[j];
                        steps[j] = temp;
                        counter++;
                    }
                }
            }
            return steps;
        }
    }
}