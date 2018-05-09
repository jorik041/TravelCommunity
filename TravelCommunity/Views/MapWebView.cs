using System;

using Xamarin.Forms;

namespace TravelCommunity.Views
{
    public class MapWebView : ContentPage
    {
        public MapWebView()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var browser = new WebView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            browser.Source = "https://travelcommunity.github.io/index.html";

            Content = browser;
        }
    }
}

