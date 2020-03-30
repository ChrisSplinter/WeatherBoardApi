
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherBoard2.Commands;
using WeatherBoard2.ErrorHandling;
using WeatherBoard2.Helper;
using WeatherBoard2.Models;
using WeatherBoard2.Services;

namespace WeatherBoard2.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        private IWeatherService weatherService;
        private IDialogService dialogService;

            public MainViewModel() { }
        public MainViewModel(IWeatherService ws, IDialogService ds)
        {
            weatherService = ws;
            dialogService = ds;
        }

        private List<WeatherForecastModel> _forecast;
        public List<WeatherForecastModel> Forecast
        {
            get { return _forecast; }
            set
            {
                _forecast = value;
                OnPropertyChanged();
            }
        }

        private WeatherForecastModel _currentWeather = new WeatherForecastModel();
        public WeatherForecastModel CurrentWeather
        {
            get { return _currentWeather; }
            set
            {
                _currentWeather = value;
                OnPropertyChanged();
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        private ICommand _getWeatherCommand;
        public ICommand GetWeatherCommand
        {
            get
            {
                if (_getWeatherCommand == null) _getWeatherCommand =
                        new RelayCommandAsync(() => GetWeather(), (o) => CanGetWeather());
                return _getWeatherCommand;
            }
        }

        public async Task GetWeather()
        {
            WeatherService serv = new WeatherService();

            await weatherService.GetCurrentWeatherAsync(Location);

            //try
            //{
            //    var weather = await weatherService.GetForecastAsync(Location, 3);
            //    CurrentWeather = weather.First();
            //    Forecast = weather.Skip(1).Take(2).ToList();
            //}
            //catch (HttpRequestException ex)
            //{
            //    dialogService.ShowNotification("Ensure you're connected to the internet!", "Open Weather");
            //}
            //catch (LocationNotFoundException ex)
            //{
            //    dialogService.ShowNotification("Location not found!", "Open Weather");
            //}


        }

        public bool CanGetWeather()
        {
            return Location != string.Empty;
        }
    }
}
