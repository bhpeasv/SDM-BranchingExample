﻿using BranchingExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BranchingExample.BE
{
    public class BankAccount : IBankAccount
    {
        public int AccountNumber { get; private set; }
        public double Balance { get; private set; }


        public BankAccount(int accNumber) : this(accNumber, 0)
        {
        }

        public BankAccount(int accNumber, double initialAmount)
        {
            if (accNumber <= 0)
                throw new ArgumentException("Invalid Account Number");

            if (initialAmount < 0)
                throw new ArgumentException("Initial balance cannot be negative");

            AccountNumber = accNumber;
            Balance = initialAmount;
        }

        public void Deposit(double amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount to deposit must be positive");

            Balance = (double)((decimal)Balance + (decimal)amount);
        }

        public void Withdraw(double amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount to withdraw must be positive");
            
            if (amount > Balance)
                throw new ArgumentException("Amount to withdraw exceeds the balance");

            Balance = (double)((decimal)Balance - (decimal)amount);
        }
    }
}
