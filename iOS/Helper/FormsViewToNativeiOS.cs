// FormsViewToNativeiOS.cs$
// 13.05.2018 Aimoré Sá 
using System;
using CoreGraphics;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace TravelCommunity.iOS.Helper
{
    public class FormsViewToNativeiOS
    {
        public FormsViewToNativeiOS()
        {
        }
        public static UIView ConvertFormsToNative(Xamarin.Forms.View view, CGRect size)
        {
            //var renderer = RendererFactory.GetRenderer(view);
            var renderer = Platform.CreateRenderer(view);

            renderer.NativeView.Frame = size;

            renderer.NativeView.AutoresizingMask = UIViewAutoresizing.All;
            renderer.NativeView.ContentMode = UIViewContentMode.ScaleToFill;

            renderer.Element.Layout(size.ToRectangle());

            var nativeView = renderer.NativeView;

            nativeView.SetNeedsLayout();

            return nativeView;
        }
    }
}
