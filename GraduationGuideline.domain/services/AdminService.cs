using System;
using System.Threading.Tasks;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.DataTransferObjects;
using System.Collections.Generic;

namespace GraduationGuideline.domain.services
{
    
    public class AdminService : IAdminService
    {
        private IRepository _repository;
        
        public AdminService(IRepository _repository)
        {
            this._repository = _repository ?? throw new ArgumentNullException();
        }

        public async Task<List<UserWithStepsDto>> GetallUserData(string semester, int year)
        {
            var result = await this._repository.GetAllUsersAndSteps(semester, year).ConfigureAwait(false);
            foreach (UserWithStepsDto user in result) {
                user.Steps = SelectionSortByName(user.Steps);
                user.Steps = TrimStepsByType(user.Steps, user.StudentType);
            }
            return result;
        }

        public async Task AdminDeleteUser(UserInfoDto user){
            await this._repository.AdminDeleteUser(user).ConfigureAwait(false);
        }
        public async Task AdminUpdateUser(UserWithStepsDto user){
            await this._repository.AdminEditUser(user).ConfigureAwait(false);
        }
        public async Task CreateDeadline(PartialDeadlineDto deadline){
            var Deadline = new DeadlineDto(); 
            Deadline.Semester = deadline.Semester;
            Deadline.year = deadline.year;
            Deadline.GS8 = deadline.GS8;
            Deadline.ProQuest = deadline.ProQuest;
            Deadline.FinalVisit = deadline.ProQuest;
            Deadline.FinalExam = deadline.ProQuest;
            Deadline.Survey = deadline.ProQuest.AddDays(-25);
            Deadline.Graduation = deadline.Graduation;
            Deadline.Commencement = deadline.Commencement;
            Deadline.Hooding = deadline.Hooding;
            Deadline.Audit = deadline.Audit;

            await this._repository.CreateDeadline(Deadline).ConfigureAwait(false);  
        }

        public async Task EditDeadline(DeadlineDto deadline) {
            await this._repository.EditDeadline(deadline).ConfigureAwait(false);
        }

        public async Task<List<DeadlineDto>> GetAllDeadlines() {
            var result = await this._repository.GetAllDeadlines().ConfigureAwait(false);
            return result;
        }
        public async Task RemoveDeadline(DeadlineKeyDto deadline) {
            await this._repository.DeleteDeadline(deadline);
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

        private List<StepDto> SelectionSortByName(List<StepDto> steps){

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

    }
}