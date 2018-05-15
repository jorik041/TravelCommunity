// CustomMKAnnotationView.xaml.cs$
// 13.05.2018 Aimoré Sá 
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TravelCommunity.Test
{
    public partial class MKAnnotationViewRenderer : Grid
    {
        public MKAnnotationViewRenderer()
        {
            InitializeComponent();
            CaptionText.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
        }
    }
}
