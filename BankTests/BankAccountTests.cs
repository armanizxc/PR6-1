using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;

namespace BankTests
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);

            account.Debit(debitAmount);

            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            double beginningBalance = 11.99;
            double debitAmount = -8.99;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            double beginningBalance = 17.99;
            double creditAmount = 20.0;
            double expected = 37.99;
            BankAccount account = new BankAccount("Mr. Zakhar", beginningBalance);

            account.Credit(creditAmount);

            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account balance was not updated correctly after credit.");
        }

        [TestMethod]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            double beginningBalance = 17.99;
            double creditAmount = -20.0; 
            BankAccount account = new BankAccount("Mr. Zakhar", beginningBalance);

            try
            {
                account.Credit(creditAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.CreditAmountLessThanZeroMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void Credit_WhenAmountIsZero_ShouldNotChangeBalance()
        {
            double beginningBalance = 17.99;
            double creditAmount = 0.0;
            double expected = 17.99;
            BankAccount account = new BankAccount("Mr. Zakhar", beginningBalance);

            account.Credit(creditAmount);

            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account balance should not change when crediting zero.");
        }
    }
}
