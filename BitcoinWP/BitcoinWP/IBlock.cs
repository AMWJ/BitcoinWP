using System;
using System.Collections.Generic;

namespace BitcoinWP
{
    interface IBlock:IEnumerable<IBitcoinTransaction>
    {
        int GetNumber();
        DateTime Published();
    }
}