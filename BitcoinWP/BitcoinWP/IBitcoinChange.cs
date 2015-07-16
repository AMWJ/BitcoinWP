namespace BitcoinWP
{
    interface IBitcoinChange
    {
        double GetAmount();
        IBitcoinTransaction GetInputTransaction();
        IBitcoinTransaction GetOutputTransaction();
    }
}