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

        /// <summary>
        /// Checks the user.
        /// </summary>
        /// <returns>The user.</returns>
        private async Task CheckUser()
        {
            if (Application.Current.Properties.ContainsKey("access_token") && !(Application.Current.Properties.ContainsKey("userID")) )
            {
                accessToken = Application.Current.Properties["access_token"] as string;
                await GetUserID();
                // do something with id
            }
            else if(Application.Current.Properties.ContainsKey("access_token") && (Application.Current.Properties.ContainsKey("userID")))
            {
                accessToken = Application.Current.Properties["access_token"] as string;
                UserId = Application.Current.Properties["userID"] as string;
                await JsonResult();
            }
            else
            {
                //TODO create error page inside navigation go back to login button
                accessToken = Client.DefaultAccessToken;
                UserId = Client.DefaultUserID;
                await JsonResult();
            }
        }

		/// <summary>
		/// Jsons the result.
		/// </summary>
		private async Task JsonResult()
        {
            Media = new List<PinMedia>();
            client = new HttpClient();
			/// Need to use your access token
            GetRecentMediaUrl = Client.GetRecentMediaBaseUrl + UserId + Client.GetRecentMediaEndPoint + accessToken;
            uri = new Uri(GetRecentMediaUrl);
            result = await client.GetStringAsync(uri);
            RecentMedia = JsonConvert.DeserializeObject<InstagramModel>(result);

            foreach (var item in RecentMedia.data)
            {
                PinMedia cada = new PinMedia();
                if (item.images.standard_resolution.url != null)
                {
                    cada.ImageUrl = item.images.standard_resolution.url;
                }
                if (item.location != null)
                {
                    cada.Latitude = item.location.latitude;
                    cada.Longitude = item.location.longitude;
                    cada.LocationName = item.location.name;
                    if (item.caption.text != null)
                        cada.CaptionText = item.caption.text;
                    Media.Add(cada);
                }
            }
            FillMap();
        }

        /// <summary>
        /// Fills the map.
        /// </summary>
        private void FillMap()
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
                        Label = Truncate(item.LocationName,20),
                        // Label = Truncate(item.CaptionText,20),
                        Id = "Xamarin",
                        Url = item.ImageUrl

                    };
                    customMap.CustomPins.Add(pin);
                    customMap.Pins.Add(pin);
                }
            } 
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            Content = customMap;
        }

        /// <summary>
        /// Creates the content.
        /// </summary>
        private void CreateContent()
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
            stack.Children.Add(progressView);
            container.Children.Add(customMap);
            container.Children.Add(stack);
            Content = container;
        }
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <returns>The user identifier.</returns>
        private async Task GetUserID()
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
        /// <summary>
        /// Truncate the specified value and maxChars.
        /// </summary>
        /// <returns>The truncate.</returns>
        /// <param name="value">Value.</param>
        /// <param name="maxChars">Max chars.</param>
        private string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }

        private void ShowErrorPage()
        {
            
        }
        #endregion
    }
}
