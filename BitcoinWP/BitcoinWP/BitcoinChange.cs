using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinWP
{
    class BitcoinChange : IBitcoinChange
    {
        IBitcoinTransaction Transaction;
        int Vout;
        public IBitcoinTransaction GetInputTransaction()
        {
            throw new NotImplementedException();
        }

        public IBitcoinTransaction GetOutputTransaction()
        {
            throw new NotImplementedException();
        }
        public BitcoinChange(IBitcoinTransaction Transaction, int Vout)
        {
            this.Transaction = Transaction;
            this.Vout = Vout;
        }
    }
}