using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GraduationGuideline.domain.contracts;
using GraduationGuideline.domain.DataTransferObjects;
using GraduationGuideline.data.entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GraduationGuideline.data
{
    public class DateOptimizerRepository : IRepository
    {

        private GraduationGuidelineContext _dateOptimizerContext;

        public DateOptimizerRepository(GraduationGuidelineContext dateOptimizerContext)
        {
            _dateOptimizerContext = dateOptimizerContext ?? throw new ArgumentNullException();

        }
        public void Dispose()
        {
            _dateOptimizerContext.Dispose();
        }

        public Task<UserInfoDto> GetUserInfoAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}