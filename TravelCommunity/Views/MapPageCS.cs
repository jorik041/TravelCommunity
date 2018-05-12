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
        private List<PinMedia> Media { get; set; }
        private Uri uri;
        Grid container;
        private string result;
        private CustomPin pin;
		private string accessToken { get; set; }

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TravelCommunity.Views.MapPageCS"/> class.
        /// </summary>
        public MapPageCS()
        {
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
            Device.BeginInvokeOnMainThread(async () =>
            {
                await JsonResult();

            });
		}

		#endregion

		#region private methods

		/// <summary>
		/// Jsons the result.
		/// </summary>
		async Task JsonResult()
        {
            Media = new List<PinMedia>();
			if (Application.Current.Properties.ContainsKey("access_token"))
            {
				var token = Application.Current.Properties["access_token"] as string;
				// do something with id
				accessToken = token;
            }
			else 
			{
				accessToken = "217783145.ff04465.e645f7fa04024ffc922e95d671ab9cab";
			}
            client = new HttpClient();
			/// Need to use your access token
			//string accesrequest = "https://api.instagram.com/v1/users/217783145/media/recent?access_token=217783145.ff04465.e645f7fa04024ffc922e95d671ab9cab";
			string accessUrl = TravelCommunity.Resources.Client.GetRecentMediaUrl + accessToken;
			uri = new Uri(accessUrl);
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

        #endregion
    }
}
