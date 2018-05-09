using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace TravelCommunity.Views
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }
    }
}

