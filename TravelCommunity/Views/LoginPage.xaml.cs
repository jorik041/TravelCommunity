using System;
using System.Collections.Generic;
using TravelCommunity.Custom;
using TravelCommunity.Helper;
using TravelCommunity.Models;
using Xamarin.Forms;

namespace TravelCommunity.Views
{
    public partial class LoginPage : ContentPage
    {
        private List<PinMedia> PinList { get; set; }

        public LoginPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Ons the login button tapped.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
		private async void OnLoginButtonTapped(object sender, EventArgs e)
		{
    		await LoginButton.ScaleTo(0.95, 50, Easing.CubicOut);
            await LoginButton.ScaleTo(1, 50, Easing.CubicIn);
            //LoginButton.IsEnabled = false;
            NavigateToMap();
		}
        /// <summary>
        /// Navigates to map.
        /// </summary>
        private void NavigateToMap()
        {
            DependencyService.Get<IClearCookies>().Clear();
            App._NavPage = new NavigationPage(new InstagramLogin());
            App.Navigation = App._NavPage.Navigation;
            Application.Current.MainPage = App._NavPage;

        }
    }
}
