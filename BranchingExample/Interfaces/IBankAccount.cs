using System.Collections.Generic;

namespace BranchingExample.Interfaces
{
    public interface IBankAccount
    {
        int AccountNumber { get; }
        double Balance { get; }
        List<ITransaction> Transactions { get; }

        void Deposit(double amount);
        void Withdraw(double amount);
    }
}
