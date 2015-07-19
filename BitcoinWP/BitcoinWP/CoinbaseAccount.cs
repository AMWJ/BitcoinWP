using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sentinel.OAuth.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace BitcoinWP
{
    /// <summary>
    /// Logged in account on Coinbase. This gets created whenever a user authenticates their Coinbase account in the application.
    /// </summary>
    class CoinbaseAccount : IAuthenticatedBitcoinWallet
    {
        /// <summary>
        /// Should be passed to the server whenever user's data is needed.
        /// </summary>
        string AccessToken;
        /// <summary>
        /// The refresh token can be used to gain a new access token when the previous one runs out.
        /// </summary>
        string RefreshToken;
        /// <summary>
        /// Construct a Coinbase account with an Access Key and a Refresh Key.
        /// </summary>
        /// <param name="AccessKey">Access Key</param>
        /// <param name="RefreshKey">Refresh Key</param>
        public CoinbaseAccount(string AccessKey, string RefreshKey)
        {
            this.AccessToken = AccessKey;
            this.RefreshToken = RefreshKey;
        }
        /// <summary>
        /// Constructs a Coinbase Account with an Access Key. This method is for testing purposes.
        /// </summary>
        /// <param name="AccessKey">Access Key</param>
        public CoinbaseAccount(string AccessKey)
        {
            this.AccessToken = AccessKey;
        }
        /// <summary>
        /// Gets the balance in this Coinbase account.
        /// </summary>
        /// <returns>The Total value of Bitcoin in the user's account.</returns>
        override async public Task<int> GetTotalBalance()
        {
            HttpClient client = new HttpClient();
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                client.DefaultRequestHeaders.TryAddWithoutValidation("CB-VERSION", "2015-07-15");
                HttpResponseMessage message = await client.GetAsync(new Uri("https://api.sandbox.coinbase.com/v2/accounts"));
                string response = await message.Content.ReadAsStringAsync();
                var accountInfo = JObject.Parse(response);
            }
            catch (Exception e)
            {
            }
            return 4;
        }
    }
}
