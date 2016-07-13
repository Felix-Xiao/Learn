using System;
using BankAccount;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NunitTest
{
    [TestFixture()]
    public class NunitTest
    {
        [Test()]
        public void TestMethod1()
        {

            int a = 1;
            int b = 1;
            Assert.IsTrue(a.Equals(b));
        }

        // unit test code
        [Test()]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 16.54;
            BankAccountClass account = new BankAccountClass("Mr. Bryan Walton", beginningBalance);

            // act
            account.Debit(debitAmount);

            // assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual);
        }

        ////unit test method
        //[Test()]
        //public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        //{
        //    // arrange
        //    double beginningBalance = 11.99;
        //    double debitAmount = -100.00;
        //    BankAccountClass account = new BankAccountClass("Mr. Bryan Walton", beginningBalance);
        //    //ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
        //    //    delegate { throw new BankAccountClass("Mr. Bryan Walton", beginningBalance).Debit(debitAmount); });

        //    // act
        //    account.Debit(debitAmount);

        //    //Assert.Throws<ArgumentOutOfRangeException>(account.Debit(debitAmount));
        //    Assert.Throws<ArgumentException>(() => { throw new ArgumentException(); });
        //}

        //[Test()]
        //public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        //{
        //    // arrange
        //    double beginningBalance = 11.99;
        //    double debitAmount = 20.0;
        //    BankAccountClass account = new BankAccountClass("Mr. Bryan Walton", beginningBalance);

        //    // act
        //    try
        //    {
        //        account.Debit(debitAmount);
        //    }
        //    catch (ArgumentOutOfRangeException e)
        //    {
        //        // assert
        //        StringAssert.Contains(e.Message, BankAccountClass.DebitAmountExceedsBalanceMessage);
        //        return;
        //    }
        //    Assert.Fail("No exception was thrown.");
        //}
    }
}
