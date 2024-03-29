﻿using System;
using CoreGraphics;
using MapKit;
using UIKit;

namespace TravelCommunity.iOS.Custom
{
    public class CustomMKAnnotationView : MKAnnotationView
    {
        public string Id { get; set; }

        public string Url { get; set; }
        public CustomMKAnnotationView(IMKAnnotation annotation, string id): base(annotation, id)
        {

        }

    }
}
