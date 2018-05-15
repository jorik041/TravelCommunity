// IClearCookies.cs$
// 15.05.2018 Aimoré Sá 
using System;
using System.Diagnostics;
using Foundation;
using TravelCommunity.Helper;
using TravelCommunity.iOS.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(IClearCookiesImplementation))]
namespace TravelCommunity.iOS.Helper
{
    public class IClearCookiesImplementation : IClearCookies
    {
        public void Clear()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
            {
                CookieStorage.DeleteCookie(cookie);
                Debug.WriteLine("Cookie" + cookie.Name.ToString() + "Deleted/n" );
            }
            Debug.WriteLine("Total of cookies: " + CookieStorage.Cookies.Length.ToString());
        }
    }
}
