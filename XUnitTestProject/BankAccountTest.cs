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
            IBankAccount acc = new BankAccount(accNumber);

            Assert.NotNull(acc);
            Assert.Equal(accNumber, acc.AccountNumber);
            Assert.Equal(0, acc.Balance);
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
        public void CreateValidBankAccountNonNegativeInitialBalance(double initialAmount)
        {
            int accNumber = 1;
            IBankAccount acc = new BankAccount(accNumber, initialAmount);

            Assert.Equal(accNumber, acc.AccountNumber);
            Assert.Equal(initialAmount, acc.Balance);
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

            acc.Deposit(amount);

            Assert.Equal(expected, acc.Balance);
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

            acc.Withdraw(amount);

            Assert.Equal(expected, acc.Balance);
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
