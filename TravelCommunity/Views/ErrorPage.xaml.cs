// ErrorPage.xaml.cs$
// 15.05.2018 Aimoré Sá 
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TravelCommunity.Views
{
    public partial class ErrorPage : ContentPage
    {
        public ErrorPage()
        {
            InitializeComponent();
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			Application.Current.MainPage = new LoginPage();
		}
    }
}
