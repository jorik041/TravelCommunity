using System;
using TravelCommunity.iOS;
using TravelCommunity.Views;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(InstagramLogin), typeof(LoginPageRenderer))]

namespace TravelCommunity.iOS
{
    public class LoginPageRenderer : PageRenderer
    {

        bool IsShown;


		public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            // Fixed the issue that on iOS 8, the modal wouldn't be popped.
            // url : http://stackoverflow.com/questions/24105390/how-to-login-to-facebook-in-xamarin-forms
            if (!IsShown)
            {

                IsShown = true;

                var auth = new OAuth2Authenticator(
                    clientId: App.XamarinAuthSettings.ClientId, // your OAuth2 client id
                    scope: App.XamarinAuthSettings.Scope, // The scopes for the particular API you're accessing. The format for this will vary by API.
                    authorizeUrl: new Uri(App.XamarinAuthSettings.AuthorizeUrl), // the auth URL for the service
                    redirectUrl: new Uri(App.XamarinAuthSettings.RedirectUrl)); // the redirect URL for the service

                auth.Completed += async (sender, eventArgs) => {
                    if (eventArgs.IsAuthenticated)
                    {
                        // Use eventArgs.Account to do wonderful things
                        //App.CompleteLogin(eventArgs.Account.Properties["access_token"]);
						string token = eventArgs.Account.Properties["access_token"].ToString();
						Xamarin.Forms.Application.Current.Properties["access_token"] = token;
                        //Xamarin.Forms.Application.Current.Properties["token"] = token;
                        App.UserAccessToken = token;
                        await App.PerformSuccessfulLoginAction();
                    }
                    else
                    {
                        // The user cancelled
                    }
                };

                var frame = auth.GetUI();

                PresentViewController(frame, true, null);
                //TODO find out how to set the tip padding
                //frame.View.Bounds = new CoreGraphics.CGRect(100, 100, 80, 30);

            }
        }
    }
}

