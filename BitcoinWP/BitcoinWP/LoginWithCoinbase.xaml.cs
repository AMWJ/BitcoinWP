#define NOTTESTLOGIN //Set this variable to run the app using a dev token provided by Coinbase. developer_access_token must be set in APIKeys.json if this variable is set.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Windows.Storage;
using System.Net;
using System.Net.Http;
using Windows.UI.Popups;
using Newtonsoft.Json.Linq;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BitcoinWP
{
    /// <summary>
    /// Page to use OAuth to login to user's Coinbase account.
    /// </summary>
    public sealed partial class LoginWithCoinbase : Page
    {
        JToken coinbase;
        public LoginWithCoinbase()
        {
            this.InitializeComponent();
        }
        async private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var location = Windows.ApplicationModel.Package.Current.InstalledLocation;
            coinbase = JToken.FromObject("");
            try
            {
                var file = await location.GetFileAsync("APIKeys.json");
                string json = await FileIO.ReadTextAsync(file);
#if DEBUG
                coinbase = JObject.Parse(json).SelectToken("coinbase_sandbox");
#else
                coinbase = JObject.Parse(json).SelectToken("coinbase");
#endif
            }
            catch (Exception)
            {
                NavigationTransitionInfo transition = new DrillInNavigationTransitionInfo();
                this.Frame.Navigate(typeof(MainPage), "Coinbase", transition);
            }
#if !TESTLOGIN && DEBUG
            NavigateToWallet((string)coinbase["developer_access_token"]);
#endif
            Uri coinBaseAuthURL = new Uri(string.Format("{0}oauth/authorize?response_type=code&client_id={1}&redirect_uri={2}&scope=wallet:accounts:read",coinbase["url"], coinbase["client_id"], coinbase["redirect"]));
            LoginWebView.Navigate(coinBaseAuthURL);
        }
        /// <summary>
        /// Set the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void LoginWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri.ToString().StartsWith(String.Format("{0}oauth/authorize/",coinbase["url"]))) //This passes after the user has signed in, and the web view is navigated to JSON for the app to use.
            {
                string authCode = args.Uri.ToString().Split('/').Last();
                Login(authCode);    //Use the Authorization Code to grab an access key, and set up the user's wallet.
            }
        }
        /// <summary>
        /// Grabs the Coinbase account's Access and Refresh Tokens, and navigates to the wallet list.
        /// </summary>
        /// <param name="authCode">The Authorization Code provided by Coinbase.</param>
        async void Login(string authCode)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(new Uri((string)coinbase["api_url"]), "oauth/token");
                string post = String.Format("grant_type='authorization_code'&code='{0}'&redirect_uri='{1}'&client_id='{2}'&client_secret='{3}'", authCode, coinbase["redirect"], coinbase["client_id"], coinbase["client_secret"]);
                var content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string,string>("grant_type","authorization_code"),
                        new KeyValuePair<string, string>("code",authCode),
                        new KeyValuePair<string, string>("redirect_uri",(string)coinbase["redirect"]),
                        new KeyValuePair<string, string>("client_id",(string)coinbase["client_id"]),
                        new KeyValuePair<string, string>("client_secret",(string)coinbase["client_secret"])
                    });
                // New code:
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);
                if (response.IsSuccessStatusCode)
                {
                    string coinbaseAuthString = await response.Content.ReadAsStringAsync();
                    var coinbaseAuth = JObject.Parse(coinbaseAuthString);
                    NavigateToWallet((string)coinbaseAuth["access_token"], (string)coinbaseAuth["refresh_token"]);
                }
            }
        }
        /// <summary>
        /// Use the newly received Access Token to create a wallet. This method is for testing purposes.
        /// </summary>
        /// <param name="AccessToken">The Access Token given by Coinbase</param>
        public void NavigateToWallet(string AccessToken)
        {
            var wallet = new CoinbaseAccount(AccessToken);
            this.Frame.Navigate(typeof(WalletView), wallet);
        }
        /// <summary>
        /// Use both the newly received Access Token, as well as the Refresh Token, to create a wallet
        /// </summary>
        /// <param name="AccessToken">The Access Token given by Coinbase.</param>
        /// <param name="RefreshToken">The Refresh Token given by Coinbase.</param>
        public void NavigateToWallet(string AccessToken, string RefreshToken)
        {
            var wallet = new CoinbaseAccount(AccessToken, RefreshToken);
            this.Frame.Navigate(typeof(WalletView), wallet);
        }
    }
}
