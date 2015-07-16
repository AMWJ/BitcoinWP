using System.Collections.Generic;

namespace BitcoinWP
{
    public abstract class IBitcoinTransaction
    {
        BitcoinAccount Sender;
        BitcoinAccount Recipient;
        List<IBitcoinChange> Outputs;
        List<IBitcoinChange> Inputs;
        IBlock Block;
        double Fee;
    }
}