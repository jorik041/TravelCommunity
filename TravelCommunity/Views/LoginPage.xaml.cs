using System;
using System.Collections.Generic;
using System.Net.Http;
using TravelCommunity.Models;
using Xamarin.Forms;

namespace TravelCommunity.Views
{
    public partial class LoginPage : ContentPage
    {
        private HttpClient client;
        private readonly CustomMap customMap;
        private InstagramModel RecentMedia;
        private List<PinMedia> PinList { get; set; }
        private Uri uri;
        private string result;
        private CustomPin pin;
        TapGestureRecognizer _tapGestureRecognizer;

        public LoginPage()
        {
            _tapGestureRecognizer = new TapGestureRecognizer();

            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            FixButtonSizeDroid();

            _tapGestureRecognizer.Tapped += async (object sender, EventArgs e) =>
            {
                await LoginButton.ScaleTo(0.95, 50, Easing.CubicOut);
                await LoginButton.ScaleTo(1, 50, Easing.CubicIn);
                LoginButton.IsEnabled = false;
                NavigateToMap();
            };

            LoginButton.GestureRecognizers.Add(_tapGestureRecognizer);
        }

        private void NavigateToMap()
        {
            Application.Current.MainPage = new MapPageCS();
            //await JsonResult();
        }


        async System.Threading.Tasks.Task JsonResult()
        {
            PinList = new List<PinMedia>();

            client = new HttpClient();
            uri = new Uri("https://api.instagram.com/v1/users/217783145/media/recent?access_token=217783145.ff04465.e645f7fa04024ffc922e95d671ab9cab");
            result = await client.GetStringAsync(uri);
            RecentMedia = Newtonsoft.Json.JsonConvert.DeserializeObject<InstagramModel>(result);

            foreach (var item in RecentMedia.data)
            {
                PinMedia cada = new PinMedia();
                if (item.images.low_resolution.url != null)
                {
                    cada.ImageUrl = item.images.low_resolution.url;
                }
                if (item.location != null)
                {
                    cada.Latitude = item.location.latitude;
                    cada.Longitude = item.location.longitude;
                    cada.LocationName = item.location.name;
                    PinList.Add(cada);
                }
            }
        }

        private void FixButtonSizeDroid()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                LoginButton.HeightRequest = 80;
            }
        }

    }
}
