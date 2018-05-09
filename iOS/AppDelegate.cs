using System;
using System.Collections.Generic;
using System.Linq;
using Lottie.Forms.iOS.Renderers;

using Foundation;
using UIKit;

namespace TravelCommunity.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Xamarin.Auth.Presenters.OAuthLoginPresenter.PlatformLogin = (authenticator) =>
            {
                var oAuthLogin = new OAuthLoginPresenter();
               oAuthLogin.Login(authenticator);
            };

            LoadApplication(new App());
            AnimationViewRenderer.Init();
            return base.FinishedLaunching(app, options);
        }
    }
}
