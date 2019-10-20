namespace BranchingExample.Interfaces
{
    public interface IBankAccount
    {
        int AccountNumber { get; }
        double Balance { get; }

        void Deposit(double amount);
        void Withdraw(double amount);
    }
}
