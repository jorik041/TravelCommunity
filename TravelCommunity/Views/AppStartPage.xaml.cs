using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelCommunity.Views
{
    public partial class AppStartPage : ContentPage
    {
        public AppStartPage()
        {
            InitializeComponent();

            Device.BeginInvokeOnMainThread(async () =>
            {
                await SimulateStartup();
            });
        }

        async Task SimulateStartup()
        {
            Debug.WriteLine("Performing some startup work that takes a bit of time.");
            await Task.Delay(3000); // Simulate a bit of startup work.
            Debug.WriteLine("Startup work is finished - starting MainActivity.");
            Application.Current.MainPage = new  LoginPage();
        }
    }
}
