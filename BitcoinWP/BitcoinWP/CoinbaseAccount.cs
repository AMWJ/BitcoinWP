using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sentinel.OAuth.Client;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BitcoinWP
{
    class CoinbaseAccount : IAuthenticatedBitcoinWallet
    {
        string AccessKey;
        string RefreshKey;
        public CoinbaseAccount(string AccessKey, string RefreshKey)
        {
            this.AccessKey = AccessKey;
            this.RefreshKey = RefreshKey;
        }
        public CoinbaseAccount(string AccessKey)
        {
            this.AccessKey = AccessKey;
        }
        override async public Task<int> GetTotalBalance()
        {
            HttpClient client = new HttpClient();
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("CB-VERSION", "2015-07-15");
                HttpResponseMessage message = await client.GetAsync(new Uri("https://api.sandbox.coinbase.com/v2/accounts"));
                string response = await message.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
            }
            return 4;
        }
    }
}
