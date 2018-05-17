using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using TravelCommunity.Models;
using TravelCommunity.Views;
using TravelCommunity.Test;
using TravelCommunity.Helper;

namespace TravelCommunity
{
    public class App : Application
    {
        public static NavigationPage _NavPage;

        public static  INavigation Navigation { get;  set; }

        public static  OAuthSettings XamarinAuthSettings { get; set; }

        public static string UserAccessToken { get; set; }

        public static bool IsAuthenticated
        {
            get { return (!String.IsNullOrWhiteSpace(Token)); }
        }

        /// <summary>
        /// The Instagram API token returned from a successful login. This token is unique to each Instagram user.
        /// </summary>
        /// <value>The Instagram API token.</value>
        public static string Token { get; private set; }

        public App()
        {
            // clientId: Your OAuth2 client id (get from Instagram API management portal)
            // scope: The scopes for the particular API you're accessing. The format for this will vary by API. ("basic" is all you need for this spp)
            // authorizeUrl: The auth URL for the service (at the time of this writing, it's "https://api.instagram.com/oauth/authorize/")
            // redirectUrl: The redirect URL for the service (as you've specified in the Instagram API management portal)

            // If you'd like to know more about how to integrate with an OAuth provider, 
            // I personally like the Instagram API docs: http://instagram.com/developer/authentication/

            XamarinAuthSettings =
                new OAuthSettings(
                    clientId: TravelCommunity.Resources.Client.ClientId,
                    scope: "basic",
                    authorizeUrl: "https://api.instagram.com/oauth/authorize/",
                    redirectUrl: "https://aimore.github.io/");
            
            // Hold on to the NavigationPage as a static, so that we can easily access it via App later.
            //_NavPage = new NavigationPage(new InstagramLogin());
            //Navigation = _NavPage.Navigation;
            MainPage = new AppStartPage();
            //var page = new ContentPage();
            //page.Content = new VideoPlayer();
            //MainPage = page;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
			return;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        //public static void CompleteLogin(string token)
        //{
        //  Token = token;

        //  InstagramClient = new InstagramClient(token);
        //}

        // Not currently being used anywhere in the app, but hopefully soon.
        public static void Logout()
        {
            Token = null;

            MessagingCenter.Send<App>((App)App.Current, "LoggedOut");
        }

        // Allows the LoginPageRenderers to signal the app to pop off the login modal.
        public static void PerformSuccessfulLoginAction()
        {
			//await _NavPage.Navigation.PushAsync(new MapPageCS());
			_NavPage = new NavigationPage(new MapPageCS());
			Navigation = _NavPage.Navigation;
			Current.MainPage = _NavPage;
        }

        public static void PerformCancelLogin()
        {
            var MyAppsFirstPage = new LoginPage();
            Application.Current.MainPage = new NavigationPage(MyAppsFirstPage);

            //and from then on you

            Application.Current.MainPage.Navigation.PushAsync(new ContentPage());
            Application.Current.MainPage.Navigation.PopAsync(); //Remove the page currently on top.
        }
    }
}

