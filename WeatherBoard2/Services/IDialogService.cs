using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBoard2.Services
{
    public interface IDialogService
    {

        void ShowNotification(string message, string caption = "");
        bool ShowConfirmationRequest(string message, string caption = "");
    }
}
