using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelCommunity.Custom;
using TravelCommunity.Helper;
using TravelCommunity.Models;
using Xamarin.Forms;

namespace TravelCommunity.Views
{
	public partial class LoginPage : ContentPage
	{
        private string UserProfilePic{ get; set; }
        private string UserName { get; set; }
        public static string AccessToken{ get; set; }
        private UserModel userData; 

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

            //if ((Application.Current.Properties.ContainsKey("access_token")))
            AccessToken = DependencyService.Get<IGetUserStorage>().RetrieveUserStorage("accessToken");
            if(!string.IsNullOrEmpty(AccessToken))
            {
                GetUserInfo();
            }
            else
            {
                LoginImage.Source = "instagram_logo.png";
                LoginImage.IsRounded = false;
                LoginNotification.Text = "Login with Instagram";
            }
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
            if (string.IsNullOrEmpty(AccessToken))
            {
                //if (string.IsNullOrEmpty(Application.Current.Properties["access_token"].ToString()))
                App._NavPage = new NavigationPage(new InstagramLogin());
                App.Navigation = App._NavPage.Navigation;
                Application.Current.MainPage = App._NavPage; 
            }else
            {
                Application.Current.MainPage = new MapPageCS();
            }		
		}

		private async void GetUserInfo()
		{
            await GetUserID();
			if (userData.data.id != null)
			{
				var userID = userData.data.id;
                DependencyService.Get<IGetUserStorage>().StoreUserData(userID, "userID");
				//Application.Current.Properties["userID"] = userID;
				if (userData.data.profile_picture != null)
				{
                    UserProfilePic = userData.data.profile_picture;
                    DependencyService.Get<IGetUserStorage>().StoreUserData(UserProfilePic, "profilePicture");
                    LoginImage.Source = UserProfilePic;
                    LoginImage.IsRounded = true;
                    //Application.Current.Properties["profilePicture"] = UserProfilePic;
				}

				if (userData.data.username != null)
				{
					UserName = userData.data.username;
                    DependencyService.Get<IGetUserStorage>().StoreUserData(UserProfilePic, "userName");
                    LoginNotification.Text = UserName;
					//Application.Current.Properties["userName"] = UserName.ToString();
                } else 
                {
                    return;
                }
			}
		}

        private async Task GetUserID()
        {
            try
            {
                var client = new HttpClient();
                string userIDUrl = TravelCommunity.Resources.Client.GetUserIDUrl + AccessToken;
                Uri userUri = new Uri(userIDUrl);
                var result = await client.GetStringAsync(userUri);
                userData = new UserModel();
                userData = JsonConvert.DeserializeObject<UserModel>(result);
            } 
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
	}
}
