using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Lottie.Forms;
using Newtonsoft.Json;
using TravelCommunity.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using TravelCommunity.Resources;

namespace TravelCommunity.Views
{
    public class MapPageCS : ContentPage
    {
        private double _width;
        private double _height;
        private HttpClient client;
        private CustomMap customMap;
        AnimationView progressView;
        private InstagramModel RecentMedia;
        private UserModel UserData;
        private List<PinMedia> Media { get; set; }
        private Uri uri;
        Grid container;
        private string result;
        private string UserId { get; set; }
        private string GetRecentMediaUrl { get; set; }
        private CustomPin pin;
        private string accessToken { get; set; }

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TravelCommunity.Views.MapPageCS"/> class.
        /// </summary>
        public MapPageCS()
        {
            Debug.WriteLine("Constructor coming");
            CreateContent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        #endregion

        #region Override Methods
        /// <summary>
        /// Ons the size allocated.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            _width = width;
            _height = height;
        }


        protected override void OnAppearing()
        {
            Debug.WriteLine("OnAppering coming");
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CheckUser();

            });
        }

        #endregion

        #region private methods

        async Task CheckUser()
        {
            if (Application.Current.Properties.ContainsKey("access_token") && !(Application.Current.Properties.ContainsKey("userID")) )
            {
                accessToken = Application.Current.Properties["access_token"] as string;
                await GetUserID();
                // do something with id
            }
            else
            {
                accessToken = Client.DefaultAccessToken;
                UserId = Client.DefaultUserID;
                await JsonResult();
            }
        }


		/// <summary>
		/// Jsons the result.
		/// </summary>
		async Task JsonResult()
        {
            Media = new List<PinMedia>();
            client = new HttpClient();
			/// Need to use your access token
            //string accessUrl = "https://api.instagram.com/v1/users/3032568183/media/recent?access_token=3032568183.ff04465.4ad8960fe586448ea5661b75c081963f";
            //GetRecentMediaUrl = Client.GetRecentMediaBaseUrl + UserId + Client.GetRecentMediaEndPoint + accessToken;
            uri = new Uri(GetRecentMediaUrl);
            result = await client.GetStringAsync(uri);
            RecentMedia = JsonConvert.DeserializeObject<InstagramModel>(result);

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
                    Media.Add(cada);
                }
            }
            Debug.WriteLine("Performing some startup work that takes a bit of time.");
            await Task.Delay(3000); // Simulate a bit of startup work.
            Debug.WriteLine("Startup work is finished - starting MainActivity.");
            FillMap();
        }

        /// <summary>
        /// Fills the map.
        /// </summary>
        void FillMap()
        {
            customMap.CustomPins = new List<CustomPin>();
            try
            {
                foreach (var item in Media)
                {
                    pin = new CustomPin
                    {
                        Type = PinType.Place,
                        Position = new Position(item.Latitude, item.Longitude),
                        Label = item.LocationName,
                        Id = "Xamarin",
                        Url = item.ImageUrl

                    };
                    customMap.CustomPins.Add(pin);
                    customMap.Pins.Add(pin);
                }
                Content = customMap;
            } 
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Creates the content.
        /// </summary>
        void CreateContent()
        {
            container = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

            };
            progressView = new AnimationView
            {
                Animation = "Plane.json",
                BackgroundColor = Color.Transparent,
                AutoPlay = true,
                Loop = true,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 80,
                WidthRequest = 80,
            };
            customMap = new CustomMap
            {
                MapType = MapType.Street,
                WidthRequest = _width,
                HeightRequest = _height
            };
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(10.786193, -29.735161), Distance.FromKilometers(100000.0)));
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    await JsonResult();
            //});
            stack.Children.Add(progressView);
            container.Children.Add(customMap);
            container.Children.Add(stack);
            Content = container;
        }

        async Task GetUserID()
        {
            client = new HttpClient();
            string userIDUrl = TravelCommunity.Resources.Client.GetUserIDUrl + accessToken;
            Uri userUri = new Uri(userIDUrl);
            result = await client.GetStringAsync(userUri);
            UserData = new UserModel();
            UserData = JsonConvert.DeserializeObject<UserModel>(result);
            if (UserData.data.id != null)
            {
                UserId = UserData.data.id;
                Application.Current.Properties["userID"] = UserId;
            } else
            {
                UserId = Client.DefaultUserID;
            }
            GetRecentMediaUrl = Client.GetRecentMediaBaseUrl + UserId + Client.GetRecentMediaEndPoint + accessToken;
            await JsonResult();
        }

        #endregion
    }
}
