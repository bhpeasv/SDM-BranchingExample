using BranchingExample.BE;
using BranchingExample.Interfaces;
using System;
using Xunit;

namespace XUnitTestProject
{
    public class TransactionTest
    {
        [Fact]
        public void CreatevalidTransaction()
        {
            int id = 1;
            DateTime dt = new DateTime(2019, 1, 1, 10, 11, 12);
            string message = "Just a message";
            double amount = 0.01;

            ITransaction t = new Transaction(id, dt, message, amount);

            Assert.Equal(id, t.TransactionID);
            Assert.Equal(dt, t.Time);
            Assert.Equal(message, t.Message);
            Assert.Equal(amount, t.Amount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateTransactionInvalidTransactionID_ExpectArgumentException(int id)
        {
            DateTime dt = new DateTime(2019, 1, 1, 10, 11, 12);
            string message = "Just a message";
            double amount = 0.01;

            ITransaction t = null;

            var ex = Assert.Throws<ArgumentException>(() => t = new Transaction(id, dt, message, amount));

            Assert.Equal("Invalid transaction id", ex.Message);
            Assert.Null(t);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CreateTransactionInvalidMessage_ExpectArgumentException(string message)
        {
            int id = 1;
            DateTime dt = new DateTime(2019, 1, 1, 10, 11, 12);
            double amount = 0.01;

            ITransaction t = null;

            var ex = Assert.Throws<ArgumentException>(() => t = new Transaction(id, dt, message, amount));

            Assert.Equal("Invalid transaction message", ex.Message);
            Assert.Null(t);
        }


    }
}
