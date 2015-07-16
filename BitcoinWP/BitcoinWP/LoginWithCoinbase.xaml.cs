#define NOTTESTLOGIN

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
        private void LoginWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri.ToString().StartsWith(String.Format("{0}oauth/authorize/",coinbase["url"])))
            {
                string authCode = args.Uri.ToString().Split('/').Last();
                Login(authCode);
            }
        }
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
        public void NavigateToWallet(string AccessToken)
        {
            var wallet = new CoinbaseAccount(AccessToken);
            this.Frame.Navigate(typeof(WalletView), wallet);
        }
        public void NavigateToWallet(string AccessToken, string RefreshToken)
        {
            var wallet = new CoinbaseAccount(AccessToken, RefreshToken);
            this.Frame.Navigate(typeof(WalletView), wallet);
        }
    }
}
