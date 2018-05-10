﻿using System;
using TravelCommunity.Custom;
using TravelCommunity.iOS;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(InstaLoginPage), typeof(LoginPageRenderer))]

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
                        await App.PerformSuccessfulLoginAction();
                        string token = eventArgs.Account.Properties["access_token"].ToString();
                        Xamarin.Forms.Application.Current.Properties["token"] = token;
                        App.UserAccessToken = token;

                    }
                    else
                    {
                        // The user cancelled
                    }
                };
                PresentViewController(auth.GetUI(), true, null);

            }
        }
    }
}

