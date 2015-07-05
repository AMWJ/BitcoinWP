namespace BitcoinWP
{
    public interface IBitcoinChange
    {
        IBitcoinTransaction GetInputTransaction();
        IBitcoinTransaction GetOutputTransaction();
    }
}