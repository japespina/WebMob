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
using System.Globalization;

namespace SeleniumWebDriver_AutomatedTesting.EWOP
{
    public class SendMoney : EWOP
    {
        private string method;
        private string module = "SendMoney";
        public string strTitle;

        #region "SendMoney_OneTimePayment_EWB"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00015_Source_To_EWB()
        {
            //**************************************************************************************************************************************
            string method = new string("TS00015_Source_To_EWB");
            string strTitle = new string("SendMoney_OneTimePayment_EWB");
            Login();
            PopAccountDetails_SendMoney("onetime","EWB","", "PHP", method, strTitle);
            //**************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string amount = string.Empty;
                string accountNo = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "EWB" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = Properties.EWBAccount; //"2000" + faker.Finance.Account(8);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                }

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount;

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_EWB(accountNo, amount, "answer");

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                navi.Logout();

                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.ToString();
            }

            log.PushToLog();
            Properties.Driver.Quit();
            */
            //**************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_OneTimePayment_LocalBank_InstaPay"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00016_Source_to_Local_Individual_INSTAPAY()
        {
            //**************************************************************************************************************************************
            string method = new string("TS00016_Source_to_Local_Individual_INSTAPAY");
            string strTitle = new string("SendMoney_OneTimePayment_LocalBank_InstaPay");
            Login();
            PopAccountDetails_SendMoney("onetime", "LocalBank", "Individual", "PHP", method, strTitle);
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
                string bank = string.Empty;
                string firstname = string.Empty;
                string lastname = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Individual" && x.Currency == "PHP" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    firstname = account.ToList()[index].BeneficiaryFirstName;
                    lastname = account.ToList()[index].BeneficiaryLastName;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();

                    firstname = faker.Name.FirstName(null).Replace("'", "");
                    lastname = faker.Name.LastName(null).Replace("'", "");
                }

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Individual, MakeATransferObject.PaymentChannel.InstaPay, "answer");

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", firstname: " + firstname + ", lastname: " + lastname + ", bank: " + bank;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }
            log.PushToLog();
            Properties.Driver.Quit();
            */
            //**************************************************************************************************************************************

        }
        #endregion

        #region "SendMoney_OneTimePayment_LocalBank_Pesonet"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00017_Source_to_Local_Individual_PESONET()
        {
            //**************************************************************************************************************************************
            string method = new string("TS00017_Source_to_Local_Individual_PESONET");
            string strTitle = new string("SendMoney_OneTimePayment_LocalBank_Pesonet");
            Login();
            PopAccountDetails_SendMoney("onetime", "LocalBank", "Individual", "PHP", method, strTitle);
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
                string bank = string.Empty;
                string firstname = string.Empty;
                string lastname = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Individual" && x.Currency == "PHP" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    firstname = account.ToList()[index].BeneficiaryFirstName;
                    lastname = account.ToList()[index].BeneficiaryLastName;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    firstname = faker.Name.FirstName(null).Replace("'", "");
                    lastname = faker.Name.LastName(null).Replace("'", "");
                }

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Individual, MakeATransferObject.PaymentChannel.PesoNet, "answer");

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", firstname: " + firstname + ", lastname: " + lastname + ", bank: " + bank;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();
            */
            //*******************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_OneTimePayment_Local_Corporate_InstaPay"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00019_Source_to_Local_Corporate_INSTAPAY()
        {
            //*******************************************************************************************************************************************
            string method = new string("TS00019_Source_to_Local_Corporate_INSTAPAY");
            string strTitle = new string("SendMoney_OneTimePayment_Local_Corporate_InstaPay");
            Login();
            PopAccountDetails_SendMoney("onetime", "LocalBank", "Corporate", "PHP", method, strTitle);
            //*******************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                string bank = string.Empty;
                string fullname = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Corporate" && x.Currency == "PHP" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    fullname = account.ToList()[index].BeneficiaryName;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    fullname = faker.Name.FullName().Replace("'", "");
                }


                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Corporate, MakeATransferObject.PaymentChannel.InstaPay, "answer");

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", fullname: " + fullname + ", bank: " + bank;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();

            */
            //***********************************************************************************************************************************************
        }

        #endregion

        #region "SendMoney_OneTimePayment_Local_Corporate_Pesonet"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00020_Source_to_Local_Corporate_PESONET()
        {
            //*******************************************************************************************************************************************
            string method = new string("TS00020_Source_to_Local_Corporate_PESONET");
            string strTitle = new string("SendMoney_OneTimePayment_Local_Corporate_Pesonet");
            Login();
            PopAccountDetails_SendMoney("onetime", "LocalBank", "Corporate", "PHP", method, strTitle);
            //*******************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                string bank = string.Empty;
                string fullname = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Corporate" && x.Currency == "PHP" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    fullname = account.ToList()[index].BeneficiaryName;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    fullname = faker.Name.FullName().Replace("'", "");
                }


                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Corporate, MakeATransferObject.PaymentChannel.PesoNet, "answer");

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", fullname: " + fullname + ", bank: " + bank;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();
            */
            //*******************************************************************************************************************************************
        }
        #endregion


        #region "SendMoney_OneTimePayment_AllBanks_Instapay"
        [Fact, Trait("Send Money", "All Banks")]
        public void TS00029_SendMoney_OneTimePayment_INSTAPAY_AllBanks()
        {
            //*******************************************************************************************************************************************
            string method = new string("TS00029_SendMoney_OneTimePayment_INSTAPAY_AllBanks");
            string strTitle = new string("SendMoney_OneTimePayment_AllBanks_Instapay");
            Login();
            PopAccountDetailsAllBanks_SendMoney("Individual", "PHP", method, strTitle);
            //*******************************************************************************************************************************************
            /*
            NavigateServicesObject navi = new NavigateServicesObject();
            MakeATransferObject sendMoney = new MakeATransferObject();

            foreach (LocalBankSendMoneyAccount account in Properties.InstaPayAccounts)
            {

                string sourceaccount = account.SourceAccount;
                string accountNo = account.Account;
                string amount = account.Amount;
                string bank = account.Bank;
                string firstname = account.BeneficiaryFirstName;
                string lastname = account.BeneficiaryLastName;
                string fullname = account.BeneficiaryName;

                MakeATransferObject.CurrencyType currency = account.Currency == "PHP" ? MakeATransferObject.CurrencyType.PHP : MakeATransferObject.CurrencyType.USD;

                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                if (account.BeneficiaryType == "Individual")
                    sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank, currency, MakeATransferObject.BeneficiaryType.Individual, MakeATransferObject.PaymentChannel.InstaPay, "answer");
                else
                    sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank, currency, MakeATransferObject.BeneficiaryType.Corporate, MakeATransferObject.PaymentChannel.InstaPay, "answer");

                navi.BackToAccountSummary();
            }

            navi.Logout();
            */
            //*******************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_OneTimePayment_AllBanks_Pesonet"
        [Fact, Trait("Send Money", "All Banks")]
        public void TS00030_SendMoney_OneTimePayment_PESONET_AllBanks()
        {
            //*******************************************************************************************************************************************
            string method = new string("TS00030_SendMoney_OneTimePayment_PESONET_AllBanks");
            string strTitle = new string("SendMoney_OneTimePayment_AllBanks_Pesonet");
            Login();
            PopAccountDetailsAllBanks_SendMoney("Individual", "PHP", method, strTitle);
            //*******************************************************************************************************************************************
            /*
            Login();

            NavigateServicesObject navi = new NavigateServicesObject();
            MakeATransferObject sendMoney = new MakeATransferObject();

            foreach (LocalBankSendMoneyAccount account in Properties.InstaPayAccounts)
            {

                string sourceaccount = account.SourceAccount;
                string accountNo = account.Account;
                string amount = account.Amount;
                string bank = account.Bank;
                string firstname = account.BeneficiaryFirstName;
                string lastname = account.BeneficiaryLastName;
                string fullname = account.BeneficiaryName;

                MakeATransferObject.CurrencyType currency = account.Currency == "PHP" ? MakeATransferObject.CurrencyType.PHP : MakeATransferObject.CurrencyType.USD;

                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                if (account.BeneficiaryType == "Individual")
                    sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank, currency, MakeATransferObject.BeneficiaryType.Individual, MakeATransferObject.PaymentChannel.PesoNet, "answer");
                else
                    sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank, currency, MakeATransferObject.BeneficiaryType.Corporate, MakeATransferObject.PaymentChannel.PesoNet, "answer");

                navi.BackToAccountSummary();
            }

            navi.Logout();
            */
            //*******************************************************************************************************************************************
        }
        #endregion


        #region "SendMoney_OneTimePayment_Local_Individual_DollarAccount"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00018_Source_to_Local_Individual_DollarAccount()
        {
            //*******************************************************************************************************************************************
            string method = new string("TS00018_Source_to_Local_Individual_DollarAccount");
            string strTitle = new string("SendMoney_OneTimePayment_Local_Individual_DollarAccount");
            Login();
            PopAccountDetails_SendMoney("onetime", "LocalBank", "Individual", "USD", method, strTitle);
            //*******************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                string bank = string.Empty;
                string firstname = string.Empty;
                string lastname = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Individual" && x.Currency == "USD" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    firstname = account.ToList()[index].BeneficiaryFirstName;
                    lastname = account.ToList()[index].BeneficiaryLastName;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == "USD" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    firstname = faker.Name.FirstName(null).Replace("'", "");
                    lastname = faker.Name.LastName(null).Replace("'", "");

                }

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank,
                                                        MakeATransferObject.CurrencyType.USD,
                                                        MakeATransferObject.BeneficiaryType.Individual,
                                                        MakeATransferObject.PaymentChannel.Others,
                                                        "answer");

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", firstname: " + firstname + ", lastname: " + lastname + ", bank: " + bank;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();
            */
            //********************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_OneTimePayment_Local_Corporate_DollarAccount"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00021_Source_to_Local_Corporate_DollarAccount()
        {
            //*******************************************************************************************************************************************
            string method = new string("TS00021_Source_to_Local_Corporate_DollarAccount");
            string strTitle = new string("SendMoney_OneTimePayment_Local_Corporate_DollarAccount");
            Login();
            PopAccountDetails_SendMoney("onetime", "LocalBank", "Corporate", "USD", method, strTitle);
            //*******************************************************************************************************************************************
            /*

            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                string bank = string.Empty;
                string fullname = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Corporate" && x.Currency == "USD" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    fullname = account.ToList()[index].BeneficiaryName;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == "USD" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    fullname = faker.Name.FullName().Replace("'", "");
                }

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank,
                                                        MakeATransferObject.CurrencyType.USD,
                                                        MakeATransferObject.BeneficiaryType.Corporate,
                                                        MakeATransferObject.PaymentChannel.Others,
                                                        "answer");

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", fullname: " + fullname + ", bank: " + bank;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message + " " + ex.InnerException.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();

            */
            //********************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_OneTimePayment_OutCountry_Beneficiary"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00022_Source_to_OutOfTheCountry_Beneficiary()
        {
            //*******************************************************************************************************************************************
            string method = new string("TS00022_Source_to_OutOfTheCountry_Beneficiary");
            string strTitle = new string("SendMoney_OneTimePayment_OutCountry_Beneficiary");
            Login();
            PopAccountDetails_SendMoney("onetime", "outofcountry", "", "PHP", method, strTitle);
            //*******************************************************************************************************************************************
            /*

            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                string bank = string.Empty;
                string fullname = string.Empty;
                string country = string.Empty;
                string currency = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "outofcountry" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    fullname = account.ToList()[index].BeneficiaryName;
                    country = account.ToList()[index].Country;
                    currency = account.ToList()[index].Currency;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency != "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    fullname = faker.Name.FullName().Replace("'", "");
                    currency = s_account.ToList()[index].Currency;
                }

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_OutCountry(accountNo, amount, fullname, ref bank, currency, ref country, MakeATransferObject.Charges.Beneficiary, "answer");

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", bank: " + bank + ", fullname: " + fullname + ", country: " + country + ", currency: " + currency;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();

            */
            //************************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_OneTimePayment_OutCountry_Shared"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00023_Source_to_OutOfTheCountry_Shared()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00023_Source_to_OutOfTheCountry_Shared");
            string strTitle = new string("SendMoney_OneTimePayment_OutCountry_Shared");
            Login();
            PopAccountDetails_SendMoney("onetime", "outofcountry", "", "PHP", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                string bank = string.Empty;
                string fullname = string.Empty;
                string country = string.Empty;
                string currency = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "outofcountry" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    fullname = account.ToList()[index].BeneficiaryName;
                    country = account.ToList()[index].Country;
                    currency = account.ToList()[index].Currency;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency != "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    fullname = faker.Name.FullName().Replace("'", "");
                    currency = s_account.ToList()[index].Currency;
                }

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_OutCountry(accountNo, amount, fullname, ref bank, currency, ref country, MakeATransferObject.Charges.Shared, "answer");

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", bank: " + bank + ", fullname: " + fullname + ", country: " + country + ", currency: " + currency;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();
            */
            //*******************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_OneTimePayment_OutCountry_OnUs"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00024_Source_to_OutOfTheCountry_OnUs()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00024_Source_to_OutOfTheCountry_OnUs");
            string strTitle = new string("SendMoney_OneTimePayment_OutCountry_OnUs");
            Login();
            PopAccountDetails_SendMoney("onetime", "outofcountry", "", "PHP", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                string bank = string.Empty;
                string fullname = string.Empty;
                string country = string.Empty;
                string currency = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "outofcountry" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    fullname = account.ToList()[index].BeneficiaryName;
                    country = account.ToList()[index].Country;
                    currency = account.ToList()[index].Currency;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency != "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    fullname = faker.Name.FullName().Replace("'", "");
                    currency = s_account.ToList()[index].Currency;
                }

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_OutCountry(accountNo, amount, fullname, ref bank, currency, ref country, MakeATransferObject.Charges.OnUs, "answer");

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", bank: " + bank + ", fullname: " + fullname + ", country: " + country + ", currency: " + currency;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();

            */
            //*******************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_OneTimePayment_SaveBene_EWBPayee"
        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00025_SavedBeneficiary_EWBPayee()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00025_SavedBeneficiary_EWBPayee");
            string strTitle = new string("SendMoney_OneTimePayment_SaveBene_EWBPayee");
            Login();
            PopAccountDetails_SendMoney("onetime_savebene", "EWB", "", "PHP", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                string nickname = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime_savebene" && x.PaymentOption == "EWB" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                    nickname = faker.Name.FullName();
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = Properties.EWBAccount;
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    nickname = faker.Name.FullName().Replace("'", "");
                }

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_EWB(accountNo, amount, "answer", true, nickname);

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", nickname: " + nickname;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();

            */
            //***************************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_OneTimePayment_SaveBene_Local_Individual"
        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00026_SavedBeneficiary_Local_Individual()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00026_SavedBeneficiary_Local_Individual");
            string strTitle = new string("SendMoney_OneTimePayment_SaveBene_Local_Individual");
            Login();
            PopAccountDetails_SendMoney("onetime", "LocalBank", "Individual", "PHP", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                string bank = string.Empty;
                string firstname = string.Empty;
                string lastname = string.Empty;
                string nickname = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Individual" && x.Currency == "PHP" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    firstname = account.ToList()[index].BeneficiaryFirstName == string.Empty ? faker.Name.FirstName() : account.ToList()[index].BeneficiaryFirstName;
                    lastname = account.ToList()[index].BeneficiaryLastName == string.Empty ? faker.Name.LastName() : account.ToList()[index].BeneficiaryLastName;
                    nickname = faker.Name.FullName();
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    firstname = faker.Name.FirstName().Replace("'", "");
                    lastname = faker.Name.LastName().Replace("'", "");
                    nickname = faker.Name.FullName().Replace("'", "");
                }

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Individual, MakeATransferObject.PaymentChannel.InstaPay, "answer", true, nickname);

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", firstname: " + firstname + ", lastname: " + lastname + ", nickname: " + nickname;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();
            */
            //*******************************************************************************************************************************************
        }
        #endregion


        #region "SendMoney_OneTimePayment_SaveBene_Local_Corporate"
        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00027_SavedBeneficiary_Local_Corporate()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00027_SavedBeneficiary_Local_Corporate");
            string strTitle = new string("SendMoney_OneTimePayment_SaveBene_Local_Corporate");
            Login();
            PopAccountDetails_SendMoney("onetime", "LocalBank", "Corporate", "PHP", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                string bank = string.Empty;
                string fullname = string.Empty;
                string nickname = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Corporate" && x.Currency == "PHP" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    fullname = account.ToList()[index].BeneficiaryName == string.Empty ? faker.Name.FullName() : account.ToList()[index].BeneficiaryName;
                    nickname = fullname;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == "PHP" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    fullname = faker.Name.FullName().Replace("'", "");
                    nickname = fullname;
                }

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Corporate, MakeATransferObject.PaymentChannel.InstaPay, "answer", true, nickname);

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", fullname: " + fullname + ", nickname: " + nickname;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();

            */
            //********************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_OneTimePayment_SaveBene_OutOftheCountry"
        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00028_SavedBeneficiary_OutOfTheCountry()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00028_SavedBeneficiary_OutOfTheCountry");
            string strTitle = new string("SendMoney_OneTimePayment_SaveBene_OutOftheCountry");
            Login();
            PopAccountDetails_SendMoney("onetime", "outofcountry", "", "PHP", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                string sourceaccount = string.Empty;
                string accountNo = string.Empty;
                string amount = string.Empty;
                string bank = string.Empty;
                string fullname = string.Empty;
                string country = string.Empty;
                string currency = string.Empty;
                string nickname = string.Empty;

                var faker = new Faker();
                Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "outofcountry" select x;

                    int index = rand.Next(account.Count());

                    sourceaccount = account.ToList()[index].SourceAccount;
                    accountNo = account.ToList()[index].Account;
                    amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                    bank = account.ToList()[index].Bank;
                    fullname = account.ToList()[index].BeneficiaryName == string.Empty ? faker.Name.FullName() : account.ToList()[index].BeneficiaryName;
                    nickname = fullname;
                    country = account.ToList()[index].Country;
                    currency = account.ToList()[index].Currency;
                }
                else
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == "USD" select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    accountNo = faker.Finance.Account(16);
                    amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    fullname = faker.Name.FullName().Replace("'", "");
                    nickname = fullname;
                    currency = s_account.ToList()[index].Currency;
                }

                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_OneTime_OutCountry(accountNo, amount, fullname, ref bank, currency, ref country, MakeATransferObject.Charges.Beneficiary, "answer", true, nickname);

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", bank: " + bank + ", fullname: " + fullname + ", country: " + country + ", currency: " + currency + ", nickname: " + nickname;

                navi.Logout();
                log.Passed = true;
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

            log.PushToLog();
            Properties.Driver.Quit();

            */
            //********************************************************************************************************************************************
        }
        #endregion

        //****************************************************************************************************************************************
        //Registered Payee
        //****************************************************************************************************************************************
        #region "SendMoney_RegisteredPayee_EWB"
        //****************************************************************************************************************************************
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00005_Source_To_EWB()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00005_Source_To_EWB");
            string strTitle = new string("SendMoney_RegisteredPayee_EWB");
            Login();
            PopAccountDetailsRegisterPayee_SendMoney("registered", "EWB", "", "", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "registered" && x.PaymentOption == "EWB" select x;

                var faker = new Faker();
                Random rand = new Random();
                int index = rand.Next(account.Count());

                string sourceaccount = account.ToList()[index].SourceAccount;
                string accountNo = account.ToList()[index].Account;
                string amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                string beneficiary = account.ToList()[index].BeneficiaryName;

                log.ParameterUsed = Properties.ExtractParameters(account.ToList()[index]);
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //DruEsepin - 07/21/2020
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_Registered_EWB(accountNo, beneficiary, amount);

                //DruEsepin - 07/21/2020
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
            //*******************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_RegisteredPayee_LocalIndividual_Instapay"
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00006_Source_to_Local_Individual_INSTAPAY()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00006_Source_to_Local_Individual_INSTAPAY");
            string strTitle = new string("SendMoney_RegisteredPayee_LocalIndividual_Instapay");
            Login();
            PopAccountDetails_SendMoney("registered", "LocalBank", "Individual", "PHP", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "registered" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Individual" && x.Currency == "PHP" select x;

                var faker = new Faker();
                Random rand = new Random();
                int index = rand.Next(account.Count());

                string sourceaccount = account.ToList()[index].SourceAccount;
                string accountNo = account.ToList()[index].Account;
                string amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                string beneficiaryName = account.ToList()[index].BeneficiaryName;

                log.ParameterUsed = Properties.ExtractParameters(account.ToList()[index]);
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //DruEsepin - 07/21/2020
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_Registered_LocalBank(accountNo, amount, beneficiaryName, MakeATransferObject.PaymentChannel.InstaPay);

                //DruEsepin - 07/21/2020
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
            //*******************************************************************************************************************************************
        }

        #endregion

        #region "SendMoney_RegisteredPayee_LocalIndividual_Pesonet"
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00007_Source_to_Local_Individual_PESONET()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00007_Source_to_Local_Individual_PESONET");
            string strTitle = new string("SendMoney_RegisteredPayee_LocalIndividual_Pesonet");
            Login();
            PopAccountDetails_SendMoney("registered", "LocalBank", "Individual", "PHP", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "registered" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Individual" && x.Currency == "PHP" select x;

                var faker = new Faker();
                Random rand = new Random();
                int index = rand.Next(account.Count());

                string sourceaccount = account.ToList()[index].SourceAccount;
                string accountNo = account.ToList()[index].Account;
                string amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                string beneficiaryName = account.ToList()[index].BeneficiaryName;

                log.ParameterUsed = Properties.ExtractParameters(account.ToList()[index]);
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //DruEsepin - 07/21/2020
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_Registered_LocalBank(accountNo, amount, beneficiaryName, MakeATransferObject.PaymentChannel.PesoNet);

                //DruEsepin - 07/21/2020
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
            //*******************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_RegisteredPayee_LocalCorporate_Instapay"
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00009_Source_to_Local_Corporate_INSTAPAY()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00009_Source_to_Local_Corporate_INSTAPAY");
            string strTitle = new string("SendMoney_RegisteredPayee_LocalCorporate_Instapay");
            Login();
            PopAccountDetails_SendMoney("registered", "LocalBank", "Corporate", "PHP", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "registered" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Corporate" && x.Currency == "PHP" select x;

                var faker = new Faker();
                Random rand = new Random();
                int index = rand.Next(account.Count());

                string sourceaccount = account.ToList()[index].SourceAccount;
                string accountNo = account.ToList()[index].Account;
                string amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                string beneficiaryName = account.ToList()[index].BeneficiaryName;

                log.ParameterUsed = Properties.ExtractParameters(account.ToList()[index]);
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //DruEsepin - 07/21/2020
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_Registered_LocalBank(accountNo, amount, beneficiaryName, MakeATransferObject.PaymentChannel.InstaPay);

                //DruEsepin - 07/21/2020
                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, sourceaccount);
                log.AvailableAmountAfter = navi.AfterTransactionAmount;

                navi.Logout();

                log.Passed = false;
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
            //********************************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_RegisteredPayee_LocalCorporate_Pesonet"
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00010_Source_to_Local_Corporate_PESONET()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00010_Source_to_Local_Corporate_PESONET");
            string strTitle = new string("SendMoney_RegisteredPayee_LocalCorporate_Pesonet");
            Login();
            PopAccountDetails_SendMoney("registered", "LocalBank", "Corporate", "PHP", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "registered" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Corporate" && x.Currency == "PHP" select x;

                var faker = new Faker();
                Random rand = new Random();
                int index = rand.Next(account.Count());

                string sourceaccount = account.ToList()[index].SourceAccount;
                string accountNo = account.ToList()[index].Account;
                string amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                string beneficiaryName = account.ToList()[index].BeneficiaryName;

                log.ParameterUsed = Properties.ExtractParameters(account.ToList()[index]);
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //DruEsepin - 07/21/2020
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_Registered_LocalBank(accountNo, amount, beneficiaryName, MakeATransferObject.PaymentChannel.PesoNet);

                //DruEsepin - 07/21/2020
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
            //*********************************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_RegisteredPayee_LocalIndividual_DollarAccount"
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00008_Source_to_Local_Individual_DollarAccount()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00008_Source_to_Local_Individual_DollarAccount");
            string strTitle = new string("SendMoney_RegisteredPayee_LocalIndividual_DollarAccount");
            Login();
            PopAccountDetails_SendMoney("registered", "LocalBank", "Individual", "USD", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "registered" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Individual" && x.Currency == "USD" select x;

                var faker = new Faker();
                Random rand = new Random();
                int index = rand.Next(account.Count());

                string sourceaccount = account.ToList()[index].SourceAccount;
                string accountNo = account.ToList()[index].Account;
                string amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                string beneficiaryName = account.ToList()[index].BeneficiaryName;

                log.ParameterUsed = Properties.ExtractParameters(account.ToList()[index]);
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //DruEsepin - 07/21/2020
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_Registered_LocalBank(accountNo, amount, beneficiaryName, MakeATransferObject.PaymentChannel.Others);

                //DruEsepin - 07/21/2020
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
            //********************************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_RegisteredPayee_LocalCorporate_DollarAccount"
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00011_Source_to_Local_Corporate_DollarAccount()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00011_Source_to_Local_Corporate_DollarAccount");
            string strTitle = new string("SendMoney_RegisteredPayee_LocalCorporate_DollarAccount");
            Login();
            PopAccountDetails_SendMoney("registered", "LocalBank", "Corporate", "USD", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "registered" && x.PaymentOption == "LocalBank" && x.BeneficiaryType == "Corporate" && x.Currency == "USD" select x;

                var faker = new Faker();
                Random rand = new Random();
                int index = rand.Next(account.Count());

                string sourceaccount = account.ToList()[index].SourceAccount;
                string accountNo = account.ToList()[index].Account;
                string amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                string beneficiaryName = account.ToList()[index].BeneficiaryName;

                log.ParameterUsed = Properties.ExtractParameters(account.ToList()[index]);
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //DruEspina - 07/21/2020
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_Registered_LocalBank(accountNo, amount, beneficiaryName, MakeATransferObject.PaymentChannel.Others);

                //DruEspina - 07/21/2020
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
            //********************************************************************************************************************************************
        }

        #endregion

        #region "SendMoney_RegisteredPayee_OutCountry_Bene"
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00012_Source_to_OutOfTheCountry_Beneficiary()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00012_Source_to_OutOfTheCountry_Beneficiary");
            string strTitle = new string("SendMoney_RegisteredPayee_OutCountry_Bene");
            Login();
            PopAccountDetails_SendMoney("registered", "outofcountry", "", "", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "registered" && x.PaymentOption == "outofcountry" select x;

                var faker = new Faker();
                Random rand = new Random();
                int index = rand.Next(account.Count());

                string sourceaccount = account.ToList()[index].SourceAccount;
                string accountNo = account.ToList()[index].Account;
                string amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                string firstname = account.ToList()[index].BeneficiaryFirstName == string.Empty ? faker.Name.FirstName(null) : account.ToList()[index].BeneficiaryFirstName;
                string lastname = account.ToList()[index].BeneficiaryLastName == string.Empty ? faker.Name.LastName(null) : account.ToList()[index].BeneficiaryLastName;
                string beneficiary = account.ToList()[index].BeneficiaryName;
                string currency = account.ToList()[index].Currency;

                beneficiary = accountNo + " - " + beneficiary;

                log.ParameterUsed = Properties.ExtractParameters(account.ToList()[index]);
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //DruEsepin - 07/21/2020
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_Registered_OutCountry(accountNo, amount, beneficiary, firstname, lastname, currency, MakeATransferObject.Charges.Beneficiary);

                //DruEsepin - 07/21/2020
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
            //********************************************************************************************************************************************
        }
        #endregion

        #region "SendMoney_RegisteredPayee_OutCountry_Shared"
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00013_Source_to_OutOfTheCountry_Shared()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00013_Source_to_OutOfTheCountry_Shared");
            string strTitle = new string("SendMoney_RegisteredPayee_OutCountry_Shared");
            Login();
            PopAccountDetails_SendMoney("registered", "outofcountry", "", "", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "registered" && x.PaymentOption == "outofcountry" select x;

                var faker = new Faker();
                Random rand = new Random();
                int index = rand.Next(account.Count());

                string sourceaccount = account.ToList()[index].SourceAccount;
                string accountNo = account.ToList()[index].Account;
                string amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                string firstname = account.ToList()[index].BeneficiaryFirstName == string.Empty ? faker.Name.FirstName(null) : account.ToList()[index].BeneficiaryFirstName;
                string lastname = account.ToList()[index].BeneficiaryLastName == string.Empty ? faker.Name.LastName(null) : account.ToList()[index].BeneficiaryLastName;
                string beneficiary = account.ToList()[index].BeneficiaryName;
                string currency = account.ToList()[index].Currency;

                beneficiary = accountNo + " - " + beneficiary;

                log.ParameterUsed = Properties.ExtractParameters(account.ToList()[index]);
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //DruEsepin - 07/21/2020
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_Registered_OutCountry(accountNo, amount, beneficiary, firstname, lastname, currency, MakeATransferObject.Charges.Shared);

                //DruEsepin - 07/21/2020
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
            //********************************************************************************************************************************************
        }
        #endregion


        #region "SendMoney_RegisteredPayee_OutCountry_Onus"
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00014_Source_to_OutOfTheCountry_OnUs()
        {
            //********************************************************************************************************************************************
            string method = new string("TS00014_Source_to_OutOfTheCountry_OnUs");
            string strTitle = new string("SendMoney_RegisteredPayee_OutCountry_Onus");
            Login();
            PopAccountDetails_SendMoney("registered", "outofcountry", "", "", method, strTitle);
            //********************************************************************************************************************************************
            /*
            TestLog log = new TestLog();
            log.UserIdUsed = Properties.Username;

            try
            {
                Login();

                var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "registered" && x.PaymentOption == "outofcountry" select x;

                var faker = new Faker();
                Random rand = new Random();
                int index = rand.Next(account.Count());

                string sourceaccount = account.ToList()[index].SourceAccount;
                string accountNo = account.ToList()[index].Account;
                string amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                string firstname = account.ToList()[index].BeneficiaryFirstName == string.Empty ? faker.Name.FirstName(null) : account.ToList()[index].BeneficiaryFirstName;
                string lastname = account.ToList()[index].BeneficiaryLastName == string.Empty ? faker.Name.LastName(null) : account.ToList()[index].BeneficiaryLastName;
                string beneficiary = account.ToList()[index].BeneficiaryName;
                string currency = account.ToList()[index].Currency;

                beneficiary = accountNo + " - " + beneficiary;

                log.ParameterUsed = Properties.ExtractParameters(account.ToList()[index]);
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //DruEsepin - 07/21/2020
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();
                sendMoney.SendMoney_Registered_OutCountry(accountNo, amount, beneficiary, firstname, lastname, currency, MakeATransferObject.Charges.OnUs);

                //DruEsepin - 07/21/2020
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
            //********************************************************************************************************************************************
        }
        #endregion




    }
}
