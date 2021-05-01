using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Configuration;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using SeleniumWebDriver_AutomatedTesting.EWOP.PageObjects;
using System.IO;
using Bogus;
using System.Threading;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Linq;
using System.Diagnostics;
using System.Reflection;

namespace SeleniumWebDriver_AutomatedTesting.EWOP
{
    public class MoveMoney : EWOP
    {
        private string method;
        private string module = "MoveMoney";

        //******************************************************************************************************************************************
        /*
        private static void PopAccountDetails(string conAccountType, string conSourceAccountType, string uconAccountType, string unconCurrency, string strMethod)
        {
            TestLog log = new TestLog(strMethod);
            log.UserIdUsed = Properties.Username;

            string strSourceAccount, strAccountNo, strAmount;
            Faker faker = new Faker();
            Random rand = new Random();

            try
            {
                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.MoveMoneyAccounts where x.AccountType == conAccountType && x.SourceAccountType == conSourceAccountType select x;

                    int index = rand.Next(account.Count());

                    strSourceAccount = account.ToList()[index].SourceAccount;
                    strAccountNo = account.ToList()[index].Account;
                    strAmount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                }
                else
                {
                    var accounts = GetOwnAcccounts();

                    var s_account = from x in accounts where x.AccountType == uconAccountType && x.Currency == unconCurrency select x;
                    int index = rand.Next(s_account.Count());
                    strSourceAccount = s_account.ToList()[index].AccountNumber;

                    var d_account = from x in accounts where x.AccountType == uconAccountType && x.Currency == unconCurrency && x.AccountNumber != strSourceAccount select x;
                    index = rand.Next(d_account.Count());
                    strAccountNo = d_account.ToList()[index].AccountNumber;

                    strAmount = faker.Finance.Amount(0, 1000, 2).ToString();
                }

                log.ParameterUsed = "sourceaccount: " + strSourceAccount + ", destinationaccount: " + strAccountNo + ", amount: " + strAmount;
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, strSourceAccount);

                //Dapat before gamitin ung navigate for transaction.
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.MoveMoney(strAccountNo, strAmount);

                //After ng transaction balik sa account history.
                navi.BackToAccountSummary();
                //Mag-navigate sa list of account the pass the source account on the method beloww.
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, strSourceAccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                navi.Logout();

                log.Passed = true;
            }
            catch (Exception e)
            {
                log.Passed = false;
                log.ErrorLog = e.Message;
            }

            log.PushToLog();
            Properties.TestLog.Close();
            Properties.Driver.Quit();

        }
        */
        //******************************************************************************************************************************************

        [Fact, Trait("Move Money", "")]
        public void TS00001_Source_to_Savings()
        {
            //MethodInfo[] methodInfos = typeof(MoveMoney).GetMethods(BindingFlags.Public | BindingFlags.Static);
            //Array.Sort(methodInfos, delegate (MethodInfo methodInfo1, MethodInfo methodInfo2) { return methodInfo1.Name.CompareTo(methodInfo2.Name); });
            //foreach (MethodInfo methodInfo in methodInfos)
            //{
            //    Console.WriteLine(methodInfo.Name);
            //}

            //var varMethod = new StackTrace().GetFrame(1).GetMethod();
            //string MethodUsed = varMethod.Name;
            //string TestCase = varMethod.GetCustomAttributesData().Where(p => p.AttributeType == typeof(TraitAttribute)).FirstOrDefault().ConstructorArguments[1].Value.ToString();
            //TestLog log = new TestLog();
            //log.UserIdUsed = Properties.Username;

            string method = new string("TS00001_Source_to_Savings");

            //method = "TS00001_Source_to_Savings";
            Login();
            PopAccountDetails_MoveMoney("savings", "savings", "Savings Accounts", "PHP", method);

            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;
            try
            {
                Login();

                string sourceaccount;
                string accountNo;
                string amount;
                var faker = new Faker();
                Random rand = new Random();
                Boolean boolControlledTest; 
                
                boolControlledTest = Properties.ControlledTest;

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.MoveMoneyAccounts where x.AccountType == "savings" && x.SourceAccountType == "savings" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                }
                else
                {
                    var accounts = GetOwnAcccounts();

                    var s_account = from x in accounts where x.AccountType == "Savings Accounts" && x.Currency == "PHP" select x;
                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    var d_account = from x in accounts where x.AccountType == "Savings Accounts" && x.Currency == "PHP" && x.AccountNumber != sourceaccount select x;
                    index = rand.Next(d_account.Count());
                    accountNo = d_account.ToList()[index].AccountNumber;

                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                }
               

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount;
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //Dapat before gamitin ung navigate for transaction.
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.MoveMoney(accountNo, amount);

                //After ng transaction balik sa account history.
                navi.BackToAccountSummary();
                //Mag-navigate sa list of account the pass the source account on the method beloww.
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                navi.Logout();

                log.Passed = true;

            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            */


        }

        [Fact, Trait("Move Money", "")]
        public void TS00002_Source_to_Current()
        {
            string method = new string("TS00002_Source_to_Current");
            Login();
            PopAccountDetails_MoveMoney("current", "savings", "Savings Accounts", "PHP", method);

            //**************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;
            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.MoveMoneyAccounts where x.AccountType == "current" && x.SourceAccountType == "savings" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                }
                else
                {
                    var accounts = GetOwnAcccounts();

                    var s_account = from x in accounts where x.AccountType == "Savings Accounts" && x.Currency == "PHP" select x;
                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    var d_account = from x in accounts where x.AccountType == "Current Accounts" && x.Currency == "PHP" && x.AccountNumber != sourceaccount select x;
                    index = rand.Next(d_account.Count());
                    accountNo = d_account.ToList()[index].AccountNumber;

                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                }

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount;
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.MoveMoney(accountNo, amount);

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                navi.Logout();

                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }
            log.PushToLog();
            Properties.TestLog.Close();
            Properties.Driver.Quit();
            */
            //**************************************************************************************************************************************
        }

        [Fact, Trait("Move Money", "")]
        public void TS00003_Source_to_Prepaid()
        {
            string method = new string("TS00003_Source_to_Prepaid");
            Login();
            PopAccountDetails_MoveMoney("prepaid", "savings", "Savings Accounts", "PHP", method);

            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.MoveMoneyAccounts where x.AccountType == "prepaid" && x.SourceAccountType == "savings" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.AccountType == "Savings Accounts" && x.Currency == "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    var d_account = from x in accounts where x.AccountType == "Prepaid Account" && x.Currency == "PHP" && x.AccountNumber != sourceaccount select x;

                    index = rand.Next(d_account.Count());
                    accountNo = d_account.ToList()[index].AccountNumber;

                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                }

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount;
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.MoveMoney(accountNo, amount);

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                navi.Logout();

                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }
            log.PushToLog();
            Properties.TestLog.Close();
            Properties.Driver.Quit();

            */

        }

        [Fact, Trait("Move Money", "")]
        public void TS00004_Dollar_to_Dollar()
        {
            string method = new string("TS00004_Dollar_to_Dollar");
            Login();
            PopAccountDetails_MoveMoney("savings_usd", "savings_usd", "Savings Accounts", "USD", method);
            //**************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.MoveMoneyAccounts where x.AccountType == "savings_usd" && x.SourceAccountType == "savings_usd" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.AccountType == "Savings Accounts" && x.Currency == "USD" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    var d_account = from x in accounts where x.AccountType == "Savings Accounts" && x.Currency == "USD" && x.AccountNumber != sourceaccount select x;

                    index = rand.Next(d_account.Count());
                    accountNo = d_account.ToList()[index].AccountNumber;

                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                }

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount;
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.MoveMoney(accountNo, amount);

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                navi.Logout();

                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }
            log.PushToLog();
            Properties.TestLog.Close();
            Properties.Driver.Quit();
            */
            //**************************************************************************************************************************************

        }
    }
}
