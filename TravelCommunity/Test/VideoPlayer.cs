// VideoPlayer.cs$
// 14.05.2018 Aimoré Sá 
using System;
using TravelCommunity.Helper;
using Xamarin.Forms;

namespace TravelCommunity.Test
{
    public class VideoPlayer : View
    {
        Reachability rech;

        public VideoPlayer()
        {
            rech = new Reachability();
            rech.CheckInternetConnection();
        }
    }
}
