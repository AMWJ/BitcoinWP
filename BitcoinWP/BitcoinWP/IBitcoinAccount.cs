using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinWP
{
    interface IBitcoinAccount
    {
        BitcoinAddress GetAddress();
        PublicKey GetPublicKey();
        List<IBitcoinTransaction> Transactions();
    }
}
