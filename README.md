## BitcoinWP ##

BitcoinWP is a Windows Bitcoin wallet app, compatible with both Windows 10 and Windows 10 Mobile.

### Requirements ###

I believe the only thing needed to build this application on your own machine that's not in the repository is APIKeys.json. Add APIKeys.json to the project in Visual Studio, and set it similar to below (replacing your own keys as appropriate):

	{
		coinbase : 
		{
			name: 'Coinbase',
			client_id: '<Client ID',
			client_secret: '<Client Secret>',
			redirect: 'urn:ietf:wg:oauth:2.0:oob',
			image: 'Assets/Coinbase.png',
			url: 'https://www.coinbase.com/',
			api_url: 'https://api.coinbase.com/',
			signup:'https://coinbase.com/signup/'
		},
		coinbase_sandbox : 
		{
			name: 'Coinbase (Sandbox)',
			client_id: '<Sandbox Client ID>',
			client_secret: '<Sandbox Client Secret>',
			redirect: 'urn:ietf:wg:oauth:2.0:oob',
			image: 'Assets/Coinbase.png',
			url: 'https://sandbox.coinbase.com/',
			api_url: 'https://api.sandbox.coinbase.com/',
			signup:'https://coinbase.com/signup/',
			developer_access_token:'<Sandbox Developer Access Token>'
	  }
	}

### Plan ###

For now, the goal is for the app to work with Coinbase wallets, allowing both the sending and receiving of bitcoin, as well as viewing account details.

Later, the app may support many other web wallets.

Finally, the app might eventually support local wallets.

### I'm no expert ###
Please do not use this application for critical data. It's still in development, and I'm not very familiar with proper security policies and procedures.

**Your help is always appreciated.** If you find a bug, please tell me. If you can contribute any help, feel free to request a pull.