using System;
using System.Collections.Generic;
using System.Text;

namespace BranchingExample.Interfaces
{
    public interface ITransaction
    {
        int TransactionID { get;}
        DateTime Time   { get;}
        string Message { get;}
        double Amount { get;}
    }
}
