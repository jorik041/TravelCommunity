// CustomVideoPlayer.cs$
// 14.05.2018 Aimoré Sá 
using System;
using System.ComponentModel;
using Foundation;
using TravelCommunity.iOS.Helper;
using TravelCommunity.Test;
using UIKit;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(CustomVideoPlayer))]
namespace TravelCommunity.iOS.Helper
{
    public class CustomVideoPlayer : ViewRenderer<VideoPlayer,WKWebView> 
    {
        WKWebView webview;
        NSUrlRequest request;
        NSUrl myUrl;

        public CustomVideoPlayer()
        {
            var webConfiguration = new WKWebViewConfiguration();
            webview = new WKWebView(new CoreGraphics.CGRect(0, 0, 170, 200), webConfiguration);
            this.AddSubview(webview);
        }

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            base.OnElementPropertyChanged(sender, e);
            myUrl = new NSUrl("https://scontent.cdninstagram.com/vp/ee9503d9f4c038c6d268818af5007f89/5AFBD578/t50.2886-16/24127848_1597286597023752_1755576729872629760_n.mp4");
            request = new NSUrlRequest(myUrl);
            webview.LoadRequest(request);
            //webview.UIDelegate =  self;
		}
	}
}
