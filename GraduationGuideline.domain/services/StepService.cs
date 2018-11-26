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

        public async Task<DeadlineDto> GetDates(string username)
        {
            var user = await _repository.GetUserByUsername(username).ConfigureAwait(false);
            var result = await _repository.GetDates(user.Semester, user.year).ConfigureAwait(false);

            return result;

        }

        public async Task<StepDto> GetStep(StepKeyDto stepKey)
        {
            if (stepKey.StepName == null || stepKey.Username == null){
                throw new ArgumentException();
            }
            StepDto step = await _repository.GetStepByKey(stepKey).ConfigureAwait(false);

            return step;
        }

        public async Task<List<StepDto>> GetStepsByUsername(String username)
        {
            var user = await this._repository.GetUserByUsername(username).ConfigureAwait(false);
            var steps = await this._repository.GetStepsByUsername(username).ConfigureAwait(false);
            steps = SelectionSortByName(steps);
            steps = TrimStepsByType(steps, user.StudentType);
            return steps;
        }

        private List<StepDto> TrimStepsByType(List<StepDto> steps, string studentType)
        {

            String[] MastersWoT = new String[]{
                "GS8", "Diploma App", "Schedule Exam",
                "Report of Final Exam", "Final Visit",
                "Graduation Fee",
                "Completion"
            };
            String[] MastersWT = new String[]{
                "GS8", "Diploma App", "Schedule Exam",
                "ETD Info", "Thesis-Disseration and Exam",
                "Report of Final Exam", "Final Visit", "Submit Thesis-Dissertation",
                "Publishing and Copyright", "ProQuest Fee", "Graduation Fee",
                "Completion"
            };
            if (studentType == "Master's Thesis") {
                steps.Remove(steps.Find(x => x.StepName == "Survey of Earned Doctorates"));
            }
            else if (studentType == "Master's Nonthesis") {
                steps.Remove(steps.Find(x => x.StepName == "ETD Info"));
                steps.Remove(steps.Find(x => x.StepName == "Thesis-Disseration and Exam"));
                steps.Remove(steps.Find(x => x.StepName == "Submit Thesis-Dissertation"));
                steps.Remove(steps.Find(x => x.StepName == "Publishing and Copyright"));
                steps.Remove(steps.Find(x => x.StepName == "ProQuest Fee"));
                steps.Remove(steps.Find(x => x.StepName == "Survey of Earned Doctorates"));
            }
            return steps;
        }

        public List<StepDto> SelectionSortByName(List<StepDto> steps){

            String[] StepNames = new String[]{
                "GS8", "Diploma App", "Schedule Exam",
                "ETD Info", "Survey of Earned Doctorates", "Thesis-Disseration and Exam",
                "Report of Final Exam", "Final Visit", "Submit Thesis-Dissertation",
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

        public async Task<StepDto> ToggleStep(StepKeyDto stepKey)
        {
            if (stepKey.StepName == null || stepKey.Username == null){
                throw new ArgumentException();
            }
            StepDto step = await _repository.ToggleStep(stepKey).ConfigureAwait(false);

            return step;
        }
    }
}