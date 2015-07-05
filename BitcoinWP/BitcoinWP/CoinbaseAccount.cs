using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinWP
{
    class CoinbaseAccount : IAuthenticatedBitcoinAccount
    {
        public BitcoinAddress GetAddress()
        {
            throw new NotImplementedException();
        }

        public PrivateKey GetPrivateKey()
        {
            throw new NotImplementedException();
        }

        public PublicKey GetPublicKey()
        {
            throw new NotImplementedException();
        }

        public List<IBitcoinTransaction> Transactions()
        {
            throw new NotImplementedException();
        }
    }
}
