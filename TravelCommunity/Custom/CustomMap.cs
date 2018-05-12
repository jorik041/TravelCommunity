using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TravelCommunity.Views
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }

        public static readonly BindableProperty LocationNameProperty =
            BindableProperty.Create("LocationName", typeof(string), typeof(CustomMap), null);


        public static readonly BindableProperty CustomPinProperty =
            BindableProperty.Create("BindableCustomPin", typeof(View), typeof(CustomMap), null);


        public View BindableCustomPin
        {
            get { return (View)GetValue(CustomPinProperty); }
            set { SetValue(CustomPinProperty, value); }
        }

        public string LocationName
        {
            get { return (string)GetValue(LocationNameProperty); }
            set { SetValue(LocationNameProperty, value); }
        }
    }
}

