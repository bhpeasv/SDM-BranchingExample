using System;
using System.Collections.Generic;
using System.Text;

namespace BranchingExample.Interfaces
{
    public interface IBankAccount
    {
        double Balance { get;}
        void Deposit(double amount);
        void Withdraw(double amount);
        void TransferTo(IBankAccount otherAccount, double amount);
    }
}
