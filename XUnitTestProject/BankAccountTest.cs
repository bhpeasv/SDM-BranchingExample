using BranchingExample.BE;
using BranchingExample.Interfaces;
using System;
using Xunit;

namespace XUnitTestProject
{
    public class BankAccountTest
    {
        [Fact]
        public void CreateValidEmptyBankAccount()
        {
            int accNumber = 1;

            DateTime before = DateTime.Now;
            IBankAccount acc = new BankAccount(accNumber);
            DateTime after = DateTime.Now;

            Assert.NotNull(acc);
            Assert.Equal(accNumber, acc.AccountNumber);
            Assert.Equal(0, acc.Balance);

            Assert.True(acc.Transactions.Count == 1);
            ITransaction t = acc.Transactions[0];
            Assert.Equal(1, t.TransactionID);
            Assert.True(before <= t.Time && t.Time <= after);
            Assert.Equal("CREATED", t.Message);
            Assert.Equal(acc.Balance, t.Amount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateEmptyBankAccount_InvalidAccountNumber_ExpectArgumentException(int accNumber)
        {
            IBankAccount acc = null;

            var ex = Assert.Throws<ArgumentException>(() => acc = new BankAccount(accNumber));

            Assert.Equal("Invalid Account Number", ex.Message);
            Assert.Null(acc);
        }

        [Theory]
        [InlineData(123.45)]
        [InlineData(0.00)]
        public void CreateValidBankAccountNonNegativeInitialBalance(double initialBalance)
        {
            int accNumber = 1;

            DateTime before = DateTime.Now;
            IBankAccount acc = new BankAccount(accNumber, initialBalance);
            DateTime after = DateTime.Now;

            Assert.NotNull(acc);
            Assert.Equal(accNumber, acc.AccountNumber);
            Assert.Equal(initialBalance, acc.Balance);

            Assert.True(acc.Transactions.Count == 1);
            ITransaction t = acc.Transactions[0];
            Assert.Equal(1, t.TransactionID);
            Assert.True(before <= t.Time && t.Time <= after);
            Assert.Equal("CREATED", t.Message);
            Assert.Equal(acc.Balance, t.Amount);
        }

        [Fact]
        public void CreateBankAcccount_InvalidInitialBalance_ExpectArgumentException()
        {
            double initialBalance = (double)-0.01;
            IBankAccount acc = null;

            var ex = Assert.Throws<ArgumentException>(() => acc = new BankAccount(1, initialBalance));

            Assert.Equal("Initial balance cannot be negative", ex.Message);
            Assert.Null(acc);
        }

        [Theory]
        [InlineData(123.45, 0.01, 123.46)]
        [InlineData(123.45, 67.89, 191.34)]
        public void DepositValidAmount(double initialBalance, double amount, double expected)
        {
            IBankAccount acc = new BankAccount(1, initialBalance);

            DateTime before = DateTime.Now;
            acc.Deposit(amount);
            DateTime after = DateTime.Now;

            Assert.Equal(expected, acc.Balance);

            Assert.True(acc.Transactions.Count == 2);
            ITransaction t = acc.Transactions[1];
            Assert.Equal(2, t.TransactionID);
            Assert.True(before <= t.Time && t.Time <= after);
            Assert.Equal("DEPOSIT", t.Message);
            Assert.Equal(amount, t.Amount);

        }

        [Fact]
        public void DepositNonPositiveAmount_ExpectArgumentException()
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            var ex = Assert.Throws<ArgumentException>(() => acc.Deposit(-0.01));

            Assert.Equal("Amount to deposit must be positive", ex.Message);
            Assert.Equal(initialBalance, acc.Balance);
        }

        [Theory]
        [InlineData(123.45, 0.01, 123.44)]
        [InlineData(123.45, 67.89, 55.56)]
        public void WithdrawValidAmount(double initialBalance, double amount, double expected)
        {
            IBankAccount acc = new BankAccount(1, initialBalance);

            DateTime before = DateTime.Now;
            acc.Withdraw(amount);
            DateTime after = DateTime.Now;

            Assert.Equal(expected, acc.Balance);

            Assert.True(acc.Transactions.Count == 2);
            ITransaction t = acc.Transactions[1];
            Assert.Equal(2, t.TransactionID);
            Assert.True(before <= t.Time && t.Time <= after);
            Assert.Equal("WITHDRAW", t.Message);
            Assert.Equal(-amount, t.Amount);
        }

        [Fact]
        public void WithdrawNonPositiveAmount_ExpectArgumentException()
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            var ex = Assert.Throws<ArgumentException>(() => acc.Withdraw(-0.01));

            Assert.Equal("Amount to withdraw must be positive", ex.Message);
            Assert.Equal(initialBalance, acc.Balance);
        }

        [Fact]
        public void Withdraw_AmountExceedsBalance_ExpectArgumentException()
        {
            double initialBalance = 123.45;
            IBankAccount acc = new BankAccount(1, initialBalance);

            var ex = Assert.Throws<ArgumentException>(() => acc.Withdraw(initialBalance + 0.01));

            Assert.Equal("Amount to withdraw exceeds the balance", ex.Message);
            Assert.Equal(initialBalance, acc.Balance);
        }

    }
}
