using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBoard2.Models;

namespace WeatherBoard2.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecastModel>> GetForecastAsync(string location);
        Task<CurrentWeatherModel> GetCurrentWeatherAsync(string location);
    }
}
