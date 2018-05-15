using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TravelCommunity.Test;
using Xamarin.Forms;

namespace TravelCommunity.Views
{
    public partial class AppStartPage : ContentPage
    {
        public AppStartPage()
        {
            InitializeComponent();

            //this.Content = new Test.MKAnnotationViewRenderer();


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
            Application.Current.MainPage = new LoginPage();
			//if (Application.Current.Properties.ContainsKey("access_token"))
    //        {
    //            System.Diagnostics.Debug.WriteLine("User has access token");
    //            // do something with id
				//Application.Current.MainPage = new MapPageCS();
    //        }
    //        else
    //        {
    //            System.Diagnostics.Debug.WriteLine("User has no access token");
				//Application.Current.MainPage = new LoginPage();
            //}
           
        }
    }
}
