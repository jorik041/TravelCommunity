using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using TravelCommunity.Custom;
using TravelCommunity.Helper;
using TravelCommunity.Models;
using Xamarin.Forms;

namespace TravelCommunity.Views
{
	public partial class LoginPage : ContentPage
	{
		private string UserProfilePic { get; set; }
		private string UserName { get; set; }
		private string AccessToken { get; set; }


		public LoginPage()
		{
			InitializeComponent();
			//if (Application.Current.Properties.ContainsKey("access_token"))
			//{
			//	Application.Current.Properties["access_token"] = AccessToken;
			//	GetUserInfo();
			//}
			//else if(Application.Current.Properties.ContainsKey("profilePicture")&& Application.Current.Properties.ContainsKey("userName"))
			//{
			//	LoginImage.Source = UserProfilePic;
			//	LoginImage.IsRounded = true;
			//	LoginNotification.Text = UserName;
			//} else
			//{
			//	LoginImage.Source = "instagram_logo.png";
			//	LoginImage.IsRounded = false;
   //             LoginNotification.Text = "Login with Instagram";
			//}
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
			//DependencyService.Get<IClearCookies>().Clear();
			App._NavPage = new NavigationPage(new InstagramLogin());
			App.Navigation = App._NavPage.Navigation;
			Application.Current.MainPage = App._NavPage;

		}

		private async void GetUserInfo()
		{
			var client = new HttpClient();
			string userIDUrl = TravelCommunity.Resources.Client.GetUserIDUrl + AccessToken;
			Uri userUri = new Uri(userIDUrl);
			var result = await client.GetStringAsync(userUri);
			var userData = new UserModel();
			userData = JsonConvert.DeserializeObject<UserModel>(result);
			if (userData.data.id != null)
			{
				var userID = userData.data.id;
				Application.Current.Properties["userID"] = userID;
				if (userData.data.profile_picture != null)
				{
                    UserProfilePic = userData.data.profile_picture;
                    Application.Current.Properties["profilePicture"] = UserProfilePic.ToString();
				}

				else if (userData.data.username != null)
				{
					UserName = userData.data.username;
					Application.Current.Properties["userName"] = UserName.ToString();
				}
			}
		}
	}
}
