// IGetUserStorage.cs$
// 16.05.2018 Aimoré Sá 
using System;
namespace TravelCommunity.Helper
{
    public interface IGetUserStorage
    {
        string RetrieveUserStorage(string key);
        //void ClearUserStorage(string key);
        void StoreUserData(string value, string key);
    }
}
