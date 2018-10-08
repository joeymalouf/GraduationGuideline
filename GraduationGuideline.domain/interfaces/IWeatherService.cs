using System.Collections.Generic;
using GraduationGuideline.domain.models;

namespace GraduationGuideline.domain.interfaces
{
    public interface IWeatherService
    {   
        IEnumerable<WeatherForecast> GetForecast();
    }
}