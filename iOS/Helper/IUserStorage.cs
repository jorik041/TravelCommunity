// IUserStorage.cs$
// 16.05.2018 Aimoré Sá 
using System;
using Foundation;
using TravelCommunity.Helper;
using TravelCommunity.iOS.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(IUserStorage))]
namespace TravelCommunity.iOS.Helper
{
    public class IUserStorage : IGetUserStorage
    {

        //public void ClearUserStorage(string key)
        //{
        //    NSUserDefaults.StandardUserDefaults.RemoveObject(key);
        //}

        public string RetrieveUserStorage(string key)
        {
            return NSUserDefaults.StandardUserDefaults.StringForKey(key);
        }

        public void StoreUserData(string value, string key)
        {
            NSUserDefaults.StandardUserDefaults.SetString(value,key);
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }
    }
}
