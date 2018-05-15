using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using MapKit;
using TravelCommunity.iOS.Custom;
using TravelCommunity.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace TravelCommunity.iOS.Custom
{
    public class CustomMapRenderer : MapRenderer
    {
        List<CustomPin> customPins;
        CustomPin customPin;
        UIImageView urlImage;
        UIView stack;


        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                nativeMap.MapType = MKMapType.Standard;
                nativeMap.GetViewForAnnotation = null;
                //nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
                ////nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                //nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;
                customPins = formsMap.CustomPins;

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                //nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
                ////nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
                //nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }

        //Bindable properties
        MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (annotation is MKUserLocation)
                return null;

            customPin = GetCustomPin(annotation as MKPointAnnotation);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            annotationView = mapView.DequeueReusableAnnotation(customPin.Id.ToString());
            if (annotationView == null)
            {
                //var formsView = new MKAnnotationViewRenderer();
                //var child = formsView.Children;
                //foreach (var son in child)
                //{
                //    var b = son.FindByName<View>("PinImage");
                //    var img = b as Image;
                //    img.Source = customPin.Url;
                //}
                //var rect = new CGRect(0, 0, 280, 300);
                //iOSView = FormsViewToNativeiOS.ConvertFormsToNative(formsView, rect);

                //var viewController = new UIViewController();
                //viewController.Add(iOSView);
                //viewController.View.Frame = rect;
                //var frame = UIApplication.SharedApplication.KeyWindow.RootViewController.View.Frame;
                ////MKAnnotationViewRenderer mkView = new MKAnnotationViewRenderer();
              

                //var size = new CGRect(0, 0, 280, 300);
                //var renderer = Platform.CreateRenderer(mkView);
                //var ctrl = renderer.ViewController;
                //renderer.NativeView.Frame = size;
                //renderer.NativeView.AutoresizingMask = UIViewAutoresizing.All;
                //renderer.NativeView.ContentMode = UIViewContentMode.ScaleToFill;
                //renderer.Element.Layout(size.ToRectangle());
                //var nativeView = renderer.NativeView;
                //nativeView.SetNeedsLayout();

                //nativeView.SizeToFit();

                annotationView = new CustomMKAnnotationView(annotation, customPin.Id.ToString());
                annotationView.Image = UIImage.FromFile("mapPin.png");
                annotationView.CalloutOffset = new CGPoint(0, 0);
     
                //var image = new UIImageView(new CGRect(0,0,160,200));
                //image.ClipsToBounds = true;
                //var data = NSData.FromUrl(url);
                //image.Image = UIImage.LoadFromData(data);
                //image.Image.Scale(new CGSize(160, 200),0);
                //image.SizeToFit();
                //urlImage.ContentMode = UIViewContentMode.ScaleAspectFit;
                //urlImage.LayoutSubviews();
               
                //urlImage.AutoresizingMask = UIViewAutoresizing.All;
                //var widthConstraint = Constraints(item: myView, attribute: .Width, relatedBy: .Equal, toItem: nil, attribute: .NotAnAttribute, multiplier: 1, constant: 40)
                //var heightConstraint = NSLayoutConstraint(item: myView, attribute: .Height, relatedBy: .Equal, toItem: nil, attribute: .NotAnAttribute, multiplier: 1, constant: 20)
               
                //stack.ClipsToBounds = true;
                //stack.LayoutMargins = new UIEdgeInsets(0, 0, 0, 0);
                //var test = new UIImageView(UIImage.FromFile("test.jpg"));

                //stack.AddSubview(test);
                //var constraaa = NSLayoutConstraint.Create(urlImage, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.Height,1, 200);
                //constraaa.Active = true;
                //urlImage.AddConstraint(constraaa);
                //var constreee = NSLayoutConstraint.Create(urlImage, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.Width, 1, 160);
                //constreee.Active = true;
                //urlImage.AddConstraint(constreee)
                var data = NSData.FromUrl(new NSUrl(customPin.Url));
                urlImage = new UIImageView(new CGRect(0, 0, 200, 220));
                urlImage.Image = UIImage.LoadFromData(data);
                stack = new UIView(new CGRect(0, 0, 200, 220));
                stack.ClipsToBounds = true;
                stack.Layer.CornerRadius = 8;
                stack.AddSubview(urlImage);
                var heightContraint = NSLayoutConstraint.Create(stack, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.Height,1, 220);
                heightContraint.Active = true;
                stack.AddConstraint(heightContraint);
                var widthConstraint = NSLayoutConstraint.Create(stack, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.Width, 1, 200);
                widthConstraint.Active = true;
                stack.AddConstraint(widthConstraint);
                annotationView.DetailCalloutAccessoryView = stack;
                //annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                ((CustomMKAnnotationView)annotationView).Id = customPin.Id.ToString();
                ((CustomMKAnnotationView)annotationView).Url = customPin.Url;
            }
            annotationView.CanShowCallout = true;

            return annotationView;
        }

        void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;
            if (!string.IsNullOrWhiteSpace(customView.Url))
            {
                UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.Url));
            }
        }

        void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            var customView = e.View as CustomMKAnnotationView;
            //customPinView = new UIView();

            //if (customView.Id != "Xamarin")
            //{

            //    //var formsView = new MKAnnotationViewRenderer();
            //    //var child = formsView.Children;
            //    //foreach (var son in child)
            //    //{
            //    //    var b = son.FindByName<View>("PinImage");
            //    //    var img = b as Image;
            //    //    img.Source = customPin.Url;
            //    //}
            //    //var rect = new CGRect(0, 0, 280, 300);
            //    //var iOSView = FormsViewToNativeiOS.ConvertFormsToNative(formsView, rect);

            //    //var viewController = new UIViewController();
            //    //viewController.Add(iOSView);
            //    //viewController.View.Frame = rect;
            //    //var frame = UIApplication.SharedApplication.KeyWindow.RootViewController.View.Frame;
            //    //customPinView.Frame = new CGRect(0, 0, 280, 300);
            //    //var image = new UIImageView(new CGRect(0, 0, 280, 300));
            //    //var data = NSData.FromUrl(url);
            //    //image.Image = UIImage.LoadFromData(data);
            //    ////image.Image = UIImage.FromFile("xamarin.png");
            //    //customPinView.AddSubview(image);
            //    //customPinView.Center = new CGPoint(0,0);
            //    //e.View.AddSubview(iOSView);
            //    var rec = new CGRect(0, 0, 280, 300);
            //    e.View.DetailCalloutAccessoryView.Frame = rec;
            //}
        }

        void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (!e.View.Selected)
            {
                stack.RemoveFromSuperview();
                stack.Dispose();
                stack = null;
            }
        }

        CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            if(customPins != null)
            {
                foreach (var pin in customPins)
                {
                    if (pin.Position == position)
                    {
                        return pin;
                    }
                }
            }
            return null;
        }
    }
}