using BranchingExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BranchingExample.BE
{
    public class Transaction : ITransaction
    {
        public int TransactionID { get; private set;}

        public DateTime Time { get; private set;}

        public string Message { get; private set;}

        public double Amount { get; private set;}

        public Transaction(int id, DateTime dt, string message, double amount)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid transaction id");

            if (message == null || message == "")
                throw new ArgumentException("Invalid transaction message");

            TransactionID = id;
            Time = dt;
            Message = message;
            Amount = amount;
        }
    }
}
