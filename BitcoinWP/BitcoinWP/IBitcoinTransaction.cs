using System.Collections.Generic;

namespace BitcoinWP
{
    public interface IBitcoinTransaction
    {
        List<IBitcoinChange> Outputs();
        List<IBitcoinChange> Inputs();
    }
}