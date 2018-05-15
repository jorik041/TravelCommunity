// Reachability.cs$
// 14.05.2018 Aimoré Sá 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TravelCommunity.Helper
{
    public class Reachability
    {
        HttpWebRequest iNetRequest;

        public async Task<bool> CheckInternetConnection()
        {
            string CheckUrl = "http://google.com";

            try
            {
                iNetRequest = (System.Net.HttpWebRequest)WebRequest.Create(CheckUrl);

                await GetResponse();
                Debug.WriteLine("...connection established..." + iNetRequest.ToString());
                return true;

            }
            catch (WebException ex)
            {

                Debug.WriteLine (".....no connection..." + ex.ToString ());
                return false;
            }
        }

        async Task GetResponse()
        {
            await iNetRequest.GetResponseAsync();
        }
    }
}
