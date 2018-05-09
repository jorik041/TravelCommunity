using System;
using UIKit;
using Xamarin.Auth;

namespace TravelCommunity.iOS
{
    public class OAuthLoginPresenter
    {
        public OAuthLoginPresenter()
        {
        }

        UIViewController rootViewController;
    
        public void Login(Authenticator authenticator)
        {
           authenticator.Completed += AuthenticatorCompleted;


            rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        }

        void AuthenticatorCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            rootViewController.DismissViewController(true, null);
           ((Authenticator)sender).Completed -= AuthenticatorCompleted;
        }
    }
}
