using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Configuration;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using SeleniumWebDriver_AutomatedTesting.EWOP.PageObjects;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using System.Net.NetworkInformation;
using Bogus.DataSets;
using System.IO;
using System.Reflection;
using System.Text;
using System.Reflection.Metadata;
using System.Linq.Expressions;
using System.Diagnostics.Tracing;
using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit.Sdk;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;


namespace SeleniumWebDriver_AutomatedTesting.EWOP
{
    public class EWOP
    {

        //public string strSourceAccount { get; set; }
        //public string strAccountNo; 
        //public string strAmount;

        #region "EWOP"
        public EWOP()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("config-ewop.json");
            Properties.Configuration = configurationBuilder.Build();

            IWebDriver webDriver = new ChromeDriver();
            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            Properties.Driver = webDriver;

            Properties.BaseURL = Properties.Configuration.GetSection("AppSettings").GetSection("BaseURL").Value;
            Properties.Username = Properties.Configuration.GetSection("AppSettings").GetSection("Credentials").GetSection("username").Value;
            Properties.Password = Properties.Configuration.GetSection("AppSettings").GetSection("Credentials").GetSection("password").Value;

            Properties.UsernameCC = Properties.Configuration.GetSection("AppSettings").GetSection("CC_Credentials").GetSection("username").Value;
            Properties.PasswordCC = Properties.Configuration.GetSection("AppSettings").GetSection("CC_Credentials").GetSection("password").Value;

            //string outputDir = Properties.Configuration.GetSection("AppSettings").GetSection("OutputFileLocation").Value;

            // create directory if not exists
            //if (!Directory.Exists(outputDir))
            //    Directory.CreateDirectory(outputDir);

            // set file name
            //string filename = outputDir + string.Format("TestLogs.Web_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
            //Properties.TestLog = new StreamWriter(filename, true);

            // write header to text file
            //if (new FileInfo(Path.GetFullPath(filename)).Length == 0)
            //    Properties.TestLog.WriteLine("Method Used|Duration|Test Case|Test Result|Source Account|Avail Amount Before|Avail Amount After|Parameter Used|Error Log|Date Tested|User Id Used");

            //Properties.TestLog.AutoFlush = true;

            // set controlled/random test via boolean
            Properties.ControlledTest = Convert.ToBoolean(Properties.Configuration.GetSection("AppSettings").GetSection("ControlledTest").Value);

            Properties.MoveMoneyAccounts = new List<MoveMoneyAccount>();
            Properties.SendMoneyAccounts = new List<SendMoneyAccount>();
            Properties.InstaPayAccounts = new List<LocalBankSendMoneyAccount>();
            Properties.PesoNetAccounts = new List<LocalBankSendMoneyAccount>();
            //Properties.NewTimeDeposits = new List<Product_NewTimeDeposit>();
            Properties.NewTimeDepositsTestCases = new List<Product_NewTimeDeposit_TestCases>();

            //Properties.NewTimeDepositsTestCases_TestData = new List<Product_NewTimeDeposit_TestCases_TestData>();

            Properties.Configuration.Bind("Accounts:SendMoney", Properties.SendMoneyAccounts);
            Properties.Configuration.Bind("Accounts:MoveMoney", Properties.MoveMoneyAccounts);
            Properties.Configuration.Bind("Accounts:InstaPay", Properties.InstaPayAccounts);
            Properties.Configuration.Bind("Accounts:PesoNet", Properties.PesoNetAccounts);
            //Properties.Configuration.Bind("Products:TimeDeposit", Properties.NewTimeDeposits);
            Properties.Configuration.Bind("Products:TimeDeposit:TestCases", Properties.NewTimeDepositsTestCases);

            Properties.PayBill = (List<PayBill>)GetBillsPayment();
            var outputDir = Properties.Configuration.GetSection("AppSettings").GetSection("OutputFileLocation").Value;

            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
            var filename = outputDir + string.Format("TestLogs.Web_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
            Properties.TestLog = new StreamWriter(filename, true);

            if (new FileInfo(Path.GetFullPath(filename)).Length == 0)
                Properties.TestLog.WriteLine("Method Used|Duration|Test Case|Test Result|Source Account|Avail Amount Before|Avail Amount After|Parameter Used|Error Log|Date Tested|User Id Used");

            Properties.TestLog.AutoFlush = true;
            Properties.ControlledTest = Convert.ToBoolean(Properties.Configuration.GetSection("AppSettings").GetSection("ControlledTest").Value);
            Properties.EWBAccount = Properties.Configuration.GetSection("AppSettings").GetSection("EWBAccount").Value;
            Properties.InstaPayBanks = Properties.Configuration.GetSection("InstaPayBanks").Get<List<string>>();
            Properties.PesoNetBanks = Properties.Configuration.GetSection("PesoNetBanks").Get<List<string>>();
        }
        #endregion "EWOP"

        #region "Login"
        public void Login()
        {
            // Navigate to URL using pre-defined setting
            Properties.Driver.Navigate().GoToUrl(Properties.BaseURL);
            Thread.Sleep(2000);
            // Test automated login
            var exec = new LoginObject();
            exec.Login(Properties.Username, Properties.Password);
        }
        #endregion "Login"

        #region "Login_CreditCardOnly"
        public void Login_CreditCardOnly()
        {
            // Navigate to URL using pre-defined setting
            Properties.Driver.Navigate().GoToUrl(Properties.BaseURL);
            Thread.Sleep(2000);
            // Test automated login
            var exec = new LoginObject();
            exec.Login(Properties.UsernameCC, Properties.PasswordCC);
        }
        #endregion "Login_CreditCardOnly"

        #region "EnrollABiller"
        //[Fact, Trait("Bills Pay", "Enroll a Random Biller")]
        public void EnrollABiller()
        {

            Login();
            TestLog testLog = new TestLog();
            PayBill payBill = new PayBill();
            NavigateServicesObject navi = new NavigateServicesObject();
            navi.Navigate(NavigateServicesObject.BillsPaymentService.ManageBillers);
            navi.Navigate(NavigateServicesObject.BillsPaymentService.EnrollBiller);
            BillsPaymentObject enroll = new BillsPaymentObject();
            enroll.EnrollBiller(ref payBill, ref testLog);
            testLog.ParameterUsed = Properties.ExtractParameters(payBill);
            testLog.UserIdUsed = Properties.Username;
            testLog.PushToLog();

            navi.Logout();

        }
        #endregion "EnrollABiller"

        #region "PopAccountDetails_SendMoney"
        public static void PopAccountDetails_SendMoney(string conPaymentType, string conPaymentOption, string conBeneType, string Currency, string strMethod, string strRegion)
        {
            TestLog log = new TestLog(strMethod);
            log.UserIdUsed = Properties.Username;

            string strSourceAccount = string.Empty;
            string strAccountNo = string.Empty;
            string strAmount = string.Empty;

            Faker faker = new Faker();
            Random rand = new Random();

            try
            {

                string sourceaccount = string.Empty;
                string amount = string.Empty;
                string accountNo = string.Empty;
                string bank = string.Empty;
                string firstname = string.Empty;
                string lastname = string.Empty;
                string fullname = string.Empty;
                string country = string.Empty;
                string currency = string.Empty;
                string nickname = string.Empty;
                string beneficiary = string.Empty;
                NavigateServicesObject navi = new NavigateServicesObject();
                MakeATransferObject sendMoney = new MakeATransferObject();

                //var faker = new Faker();
                //Random rand = new Random();

                if (Properties.ControlledTest)
                {
                    var account = from x in Properties.SendMoneyAccounts select x;
                    int index = 0;
                    switch (strRegion)
                    {
                        case "SendMoney_OneTimePayment_EWB":
                        case "SendMoney_RegisteredPayee_EWB":
                            account = from x in Properties.SendMoneyAccounts where x.PaymentType == conPaymentType && x.PaymentOption == conPaymentOption select x;
                            index = rand.Next(account.Count());

                            sourceaccount = account.ToList()[index].SourceAccount;
                            accountNo = account.ToList()[index].Account;
                            amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;

                            country = account.ToList()[index].Country;
                            currency = account.ToList()[index].Currency;

                            beneficiary = account.ToList()[index].BeneficiaryName;
                            break;

                        case "SendMoney_OneTimePayment_OutCountry_Beneficiary":
                        case "SendMoney_OneTimePayment_OutCountry_Shared":
                        case "SendMoney_OneTimePayment_OutCountry_OnUs":
                        case "SendMoney_OneTimePayment_SaveBene_EWBPayee":
                        case "SendMoney_OneTimePayment_SaveBene_OutOftheCountry":
                            account = from x in Properties.SendMoneyAccounts where x.PaymentType == conPaymentType && x.PaymentOption == conPaymentOption select x;
                            index = rand.Next(account.Count());

                            sourceaccount = account.ToList()[index].SourceAccount;
                            accountNo = account.ToList()[index].Account;
                            amount = account.ToList()[index].Amount;
                            bank = account.ToList()[index].Bank;
                            fullname = account.ToList()[index].BeneficiaryName;
                            country = account.ToList()[index].Country;
                            currency = account.ToList()[index].Currency;
                            nickname = faker.Name.FullName();

                            break;

                        default:
                            account = from x in Properties.SendMoneyAccounts where x.PaymentType == conPaymentType && x.PaymentOption == conPaymentOption && x.BeneficiaryType == conBeneType && x.Currency == Currency select x;
                            index = rand.Next(account.Count());

                            sourceaccount = account.ToList()[index].SourceAccount;
                            accountNo = account.ToList()[index].Account;
                            amount = account.ToList()[index].Amount;
                            bank = account.ToList()[index].Bank;
                            firstname = account.ToList()[index].BeneficiaryFirstName;
                            lastname = account.ToList()[index].BeneficiaryLastName;
                            fullname = account.ToList()[index].BeneficiaryName;
                            nickname = faker.Name.FullName();

                            break;

                    }
                }

                else if (Properties.ControlledTest != true)
                {
                    var accounts = GetOwnAcccounts();
                    var s_account = from x in accounts where x.Currency == Currency select x;

                    int index = rand.Next(s_account.Count());
                    sourceaccount = s_account.ToList()[index].AccountNumber;

                    //SendMoney_OneTimePayment_EWB
                    switch (strRegion)
                    {
                        case "SendMoney_OneTimePayment_EWB":
                        case "SendMoney_OneTimePayment_SaveBene_EWBPayee":
                            accountNo = Properties.EWBAccount; //"2000" + faker.Finance.Account(8);
                            amount = faker.Finance.Amount(0, 1000, 2).ToString();
                            firstname = faker.Name.FirstName(null).Replace("'", "");
                            lastname = faker.Name.LastName(null).Replace("'", "");
                            fullname = faker.Name.FullName().Replace("'", "");
                            currency = s_account.ToList()[index].Currency;
                            break;

                        case "SendMoney_OneTimePayment_SaveBene_Local_Corporate":
                            accountNo = faker.Finance.Account(16);
                            amount = faker.Finance.Amount(0, 1000, 2).ToString();
                            fullname = faker.Name.FullName().Replace("'", "");
                            nickname = fullname;
                            break;

                        default:
                            accountNo = faker.Finance.Account(16);
                            amount = faker.Finance.Amount(0, 1000, 2).ToString();
                            firstname = faker.Name.FirstName(null).Replace("'", "");
                            lastname = faker.Name.LastName(null).Replace("'", "");
                            fullname = faker.Name.FullName().Replace("'", "");
                            currency = s_account.ToList()[index].Currency;
                            nickname = faker.Name.FullName().Replace("'", "");
                            break;
                    }
                }
                

                //NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                //MakeATransferObject sendMoney = new MakeATransferObject();
                switch (strRegion)
                {
                    case "SendMoney_OneTimePayment_EWB":
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount;
                        sendMoney.SendMoney_OneTime_EWB(accountNo, amount, "answer");
                        break;

                    case "SendMoney_OneTimePayment_LocalBank_InstaPay":
                        sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Individual, MakeATransferObject.PaymentChannel.InstaPay, "answer");
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", firstname: " + firstname + ", lastname: " + lastname + ", bank: " + bank;
                        break;

                    case "SendMoney_OneTimePayment_LocalBank_Pesonet":
                        sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Individual, MakeATransferObject.PaymentChannel.PesoNet, "answer");
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", firstname: " + firstname + ", lastname: " + lastname + ", bank: " + bank;
                        break;

                    case "SendMoney_OneTimePayment_Local_Corporate_InstaPay":
                        sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Corporate, MakeATransferObject.PaymentChannel.InstaPay, "answer");
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", fullname: " + fullname + ", bank: " + bank;
                        break;

                    case "SendMoney_OneTimePayment_Local_Corporate_Pesonet":
                        sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Corporate, MakeATransferObject.PaymentChannel.PesoNet, "answer");
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", fullname: " + fullname + ", bank: " + bank;
                        break;

                    case "SendMoney_OneTimePayment_Local_Individual_DollarAccount":
                        sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank,
                                                        MakeATransferObject.CurrencyType.USD,
                                                        MakeATransferObject.BeneficiaryType.Individual,
                                                        MakeATransferObject.PaymentChannel.Others,
                                                        "answer");
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", firstname: " + firstname + ", lastname: " + lastname + ", bank: " + bank;
                        break;

                    case "SendMoney_OneTimePayment_Local_Corporate_DollarAccount":
                        sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank,
                                                        MakeATransferObject.CurrencyType.USD,
                                                        MakeATransferObject.BeneficiaryType.Corporate,
                                                        MakeATransferObject.PaymentChannel.Others,
                                                        "answer");
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", fullname: " + fullname + ", bank: " + bank;
                        break;

                    case "SendMoney_OneTimePayment_OutCountry_Beneficiary":
                        sendMoney.SendMoney_OneTime_OutCountry(accountNo, amount, fullname, ref bank, currency, ref country, MakeATransferObject.Charges.Beneficiary, "answer");
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", bank: " + bank + ", fullname: " + fullname + ", country: " + country + ", currency: " + currency;
                        break;

                    case "SendMoney_OneTimePayment_OutCountry_Shared":
                        sendMoney.SendMoney_OneTime_OutCountry(accountNo, amount, fullname, ref bank, currency, ref country, MakeATransferObject.Charges.Shared, "answer");
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", bank: " + bank + ", fullname: " + fullname + ", country: " + country + ", currency: " + currency;
                        break;

                    case "SendMoney_OneTimePayment_OutCountry_OnUs":
                        sendMoney.SendMoney_OneTime_OutCountry(accountNo, amount, fullname, ref bank, currency, ref country, MakeATransferObject.Charges.OnUs, "answer");
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", bank: " + bank + ", fullname: " + fullname + ", country: " + country + ", currency: " + currency;
                        break;

                    case "SendMoney_OneTimePayment_SaveBene_EWBPayee":
                        sendMoney.SendMoney_OneTime_EWB(accountNo, amount, "answer", true, nickname);
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", nickname: " + nickname;
                        break;

                    case "SendMoney_OneTimePayment_SaveBene_Local_Individual":
                        sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Individual, MakeATransferObject.PaymentChannel.InstaPay, "answer", true, nickname);
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", firstname: " + firstname + ", lastname: " + lastname + ", nickname: " + nickname;
                        break;

                    case "SendMoney_OneTimePayment_SaveBene_Local_Corporate":
                        sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank, MakeATransferObject.CurrencyType.PHP, MakeATransferObject.BeneficiaryType.Corporate, MakeATransferObject.PaymentChannel.InstaPay, "answer", true, nickname);
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", fullname: " + fullname + ", nickname: " + nickname;
                        break;

                    case "SendMoney_OneTimePayment_SaveBene_OutOftheCountry":
                        sendMoney.SendMoney_OneTime_OutCountry(accountNo, amount, fullname, ref bank, currency, ref country, MakeATransferObject.Charges.Beneficiary, "answer", true, nickname);
                        log.ParameterUsed = "sourceaccount: " + sourceaccount + ", destinationaccount: " + accountNo + ", amount: " + amount + ", bank: " + bank + ", fullname: " + fullname + ", country: " + country + ", currency: " + currency + ", nickname: " + nickname;
                        break;

                }


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
            log.CloseLog();
            Properties.Driver.Quit();
        }
        #endregion "PopAccountDetails_SendMoney"

        #region "PopAccountDetailsAllBanks_SendMoney"
        public static void PopAccountDetailsAllBanks_SendMoney(string strBeneType, string strCurrency, string strMethod, string strRegion)
        {
            TestLog log = new TestLog(strMethod);
            log.UserIdUsed = Properties.Username;

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

                MakeATransferObject.CurrencyType currency = account.Currency == strCurrency ? MakeATransferObject.CurrencyType.PHP : MakeATransferObject.CurrencyType.USD;

                //navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);
                bool naviResult = navi.validateNavigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);
                if (naviResult == false)
                {
                    navi.BackToAccountSummary();
                }
                else
                {
                    if (account.BeneficiaryType == strBeneType)
                        sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank, currency, MakeATransferObject.BeneficiaryType.Individual, MakeATransferObject.PaymentChannel.InstaPay, "answer");
                    else
                        sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank, currency, MakeATransferObject.BeneficiaryType.Corporate, MakeATransferObject.PaymentChannel.InstaPay, "answer");

                    navi.BackToAccountSummary();
                }

            }

            navi.Logout();
            log.CloseLog();
            Properties.Driver.Quit();
        }
        #endregion "PopAccountDetailsAllBanks_SendMoney"

        #region "PopAccountDetailsRegisterPayee_SendMoney"
        public static void PopAccountDetailsRegisterPayee_SendMoney(string conPaymentType, string conPaymentOption, string conBeneType, string Currency, string strMethod, string strRegion)
        {
            TestLog log = new TestLog(strMethod);
            log.UserIdUsed = Properties.Username;

            try
            {
                var account = from x in Properties.SendMoneyAccounts select x;
                switch (strRegion)
                {
                    case "SendMoney_RegisteredPayee_EWB":
                    case "SendMoney_RegisteredPayee_OutCountry_Bene":
                    case "SendMoney_RegisteredPayee_OutCountry_Shared":
                    case "SendMoney_RegisteredPayee_OutCountry_Onus":
                        account = from x in Properties.SendMoneyAccounts where x.PaymentType == conPaymentType && x.PaymentOption == conPaymentOption select x;
                        break;

                    default:
                        account = from x in Properties.SendMoneyAccounts where x.PaymentType == conPaymentType && x.PaymentOption == conPaymentOption && x.BeneficiaryType == conBeneType && x.Currency == Currency select x;
                        break;
                }


                var faker = new Faker();
                Random rand = new Random();
                int index = rand.Next(account.Count());

                string sourceaccount = account.ToList()[index].SourceAccount;
                string accountNo = account.ToList()[index].Account;
                string amount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                string beneficiary = account.ToList()[index].BeneficiaryName;
                string beneficiaryName = account.ToList()[index].BeneficiaryName;

                string firstname = account.ToList()[index].BeneficiaryFirstName == string.Empty ? faker.Name.FirstName(null) : account.ToList()[index].BeneficiaryFirstName;
                string lastname = account.ToList()[index].BeneficiaryLastName == string.Empty ? faker.Name.LastName(null) : account.ToList()[index].BeneficiaryLastName;
                string currency = account.ToList()[index].Currency;

                log.ParameterUsed = Properties.ExtractParameters(account.ToList()[index]);
                NavigateServicesObject navi = new NavigateServicesObject();
                navi.Navigate(NavigateServicesObject.AccountServices.MakeATransfer, sourceaccount);

                //DruEsepin - 07/21/2020
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                log.SourceAccount = navi.SourceAccount;

                MakeATransferObject sendMoney = new MakeATransferObject();

                switch (strRegion)
                {
                    case "SendMoney_RegisteredPayee_EWB":
                        sendMoney.SendMoney_Registered_EWB(accountNo, beneficiary, amount);
                        break;

                    case "SendMoney_RegisteredPayee_LocalIndividual_Instapay":
                    case "SendMoney_RegisteredPayee_LocalCorporate_Instapay":
                        sendMoney.SendMoney_Registered_LocalBank(accountNo, amount, beneficiaryName, MakeATransferObject.PaymentChannel.InstaPay);
                        break;

                    case "SendMoney_RegisteredPayee_LocalIndividual_Pesonet":
                    case "SendMoney_RegisteredPayee_LocalCorporate_Pesonet":
                        sendMoney.SendMoney_Registered_LocalBank(accountNo, amount, beneficiaryName, MakeATransferObject.PaymentChannel.PesoNet);
                        break;

                    case "SendMoney_RegisteredPayee_LocalIndividual_DollarAccount":
                    case "SendMoney_RegisteredPayee_LocalCorporate_DollarAccount":
                        sendMoney.SendMoney_Registered_LocalBank(accountNo, amount, beneficiaryName, MakeATransferObject.PaymentChannel.Others);
                        break;

                    case "SendMoney_RegisteredPayee_OutCountry_Bene":
                        beneficiary = accountNo + " - " + beneficiary;
                        sendMoney.SendMoney_Registered_OutCountry(accountNo, amount, beneficiary, firstname, lastname, currency, MakeATransferObject.Charges.Beneficiary);
                        break;

                    case "SendMoney_RegisteredPayee_OutCountry_Shared":
                        beneficiary = accountNo + " - " + beneficiary;
                        sendMoney.SendMoney_Registered_OutCountry(accountNo, amount, beneficiary, firstname, lastname, currency, MakeATransferObject.Charges.Shared);
                        break;

                    case "SendMoney_RegisteredPayee_OutCountry_Onus":
                        beneficiary = accountNo + " - " + beneficiary;
                        sendMoney.SendMoney_Registered_OutCountry(accountNo, amount, beneficiary, firstname, lastname, currency, MakeATransferObject.Charges.OnUs);
                        break;
                }

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
            //Properties.TestLog.Close();
            log.CloseLog();

            Properties.Driver.Quit();
        }
        #endregion "PopAccountDetailsRegisterPayee_SendMoney"

        #region "PopAccountDetails_MoveMoney"
        public static void PopAccountDetails_MoveMoney(string conAccountType, string conSourceAccountType, string uconAccountType, string unconCurrency,  string strMethod)
        {
            TestLog log = new TestLog(strMethod);
            log.UserIdUsed = Properties.Username;

            string strSourceAccount, strAccountNo, strAmount;
            Faker faker = new Faker();
            Random rand = new Random();

            try
            {
                strSourceAccount = "";
                strAccountNo = "";
                strAmount = "";

                if (Properties.ControlledTest)
                {

                    //if (strModule == "MoveMoney")
                    //{
                        var account = from x in Properties.MoveMoneyAccounts where x.AccountType == conAccountType && x.SourceAccountType == conSourceAccountType select x;

                        int index = rand.Next(account.Count());

                        strSourceAccount = account.ToList()[index].SourceAccount;
                        strAccountNo = account.ToList()[index].Account;
                        strAmount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;

                    //}
                    //else if (strModule == "SendMoney")
                    //{
                    //    var account = from x in Properties.SendMoneyAccounts where x.PaymentType == "onetime" && x.PaymentOption == "EWB" select x;
                    //    int index = rand.Next(account.Count());
                    //    strSourceAccount = account.ToList()[index].SourceAccount;
                    //    strAccountNo = account.ToList()[index].Account;
                    //    strAmount = account.ToList()[index].Amount == string.Empty ? faker.Finance.Amount(0, 1000, 2).ToString() : account.ToList()[index].Amount;
                    //}
                }
                else
                {
                    //if (strModule == "MoveMoney")
                    //{
                        var accounts = GetOwnAcccounts();

                        var s_account = from x in accounts where x.AccountType == uconAccountType && x.Currency == unconCurrency select x;
                        int index = rand.Next(s_account.Count());
                        strSourceAccount = s_account.ToList()[index].AccountNumber;

                        var d_account = from x in accounts where x.AccountType == uconAccountType && x.Currency == unconCurrency && x.AccountNumber != strSourceAccount select x;
                        index = rand.Next(d_account.Count());
                        strAccountNo = d_account.ToList()[index].AccountNumber;
                        strAmount = faker.Finance.Amount(0, 1000, 2).ToString();
                    //}
                    //else if (strModule == "SendMoney")
                    //{
                    //    var accounts = GetOwnAcccounts();
                    //    var s_account = from x in accounts where x.Currency == "PHP" select x;
                    //    int index = rand.Next(s_account.Count());
                    //    strSourceAccount = s_account.ToList()[index].AccountNumber;
                    //    strAccountNo = Properties.EWBAccount; //"2000" + faker.Finance.Account(8);
                    //    strAmount = faker.Finance.Amount(0, 1000, 2).ToString();
                    //}

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
            //Properties.TestLog.Close();
            log.CloseLog();
            Properties.Driver.Quit();

        }
        #endregion "PopAccountDetails_MoveMoney"

        #region "SendMoney_OneTimePayment"
        //****************************************************************************************************************************************
        //Refer to SendMoney Class
        //****************************************************************************************************************************************
        /*
        #region "SendMoney_OneTimePayment_EWB"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00015_Source_To_EWB()
        {
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
        }
        #endregion

        #region "SendMoney_OneTimePayment_LocalBank"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00016_Source_to_Local_Individual_INSTAPAY()
        {
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
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00017_Source_to_Local_Individual_PESONET()
        {
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
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00019_Source_to_Local_Corporate_INSTAPAY()
        {
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
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00020_Source_to_Local_Corporate_PESONET()
        {
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
        }

        [Fact, Trait("Send Money", "All Banks")]
        public void TS00029_SendMoney_OneTimePayment_INSTAPAY_AllBanks()
        {
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
                    sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, firstname, lastname, string.Empty, ref bank, currency, MakeATransferObject.BeneficiaryType.Individual, MakeATransferObject.PaymentChannel.InstaPay, "answer");
                else
                    sendMoney.SendMoney_OneTime_LocalBank(accountNo, amount, string.Empty, string.Empty, fullname, ref bank, currency, MakeATransferObject.BeneficiaryType.Corporate, MakeATransferObject.PaymentChannel.InstaPay, "answer");

                navi.BackToAccountSummary();
            }

            navi.Logout();

        }

        [Fact, Trait("Send Money", "All Banks")]
        public void TS00030_SendMoney_OneTimePayment_PESONET_AllBanks()
        {
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
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00018_Source_to_Local_Individual_DollarAccount()
        {
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
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00021_Source_to_Local_Corporate_DollarAccount()
        {
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
        }
        #endregion

        #region "SendMoney_OneTimePayment_OutCountry"
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00022_Source_to_OutOfTheCountry_Beneficiary()
        {
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
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00023_Source_to_OutOfTheCountry_Shared()
        {
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
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00024_Source_to_OutOfTheCountry_OnUs()
        {
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
        }
        #endregion

        #region "SendMoney_OneTimePayment_SaveBene
        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00025_SavedBeneficiary_EWBPayee()
        {
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
        }

        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00026_SavedBeneficiary_Local_Individual()
        {
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
        }

        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00027_SavedBeneficiary_Local_Corporate()
        {
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
        }

        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00028_SavedBeneficiary_OutOfTheCountry()
        {
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
        }
        #endregion-
        */
        //****************************************************************************************************************************************
        #endregion

        #region"MoveMoney"
        //****************************************************************************************************************************************
        //Refer to MoveMoney Class
        //****************************************************************************************************************************************
        /*
        [Fact, Trait("Move Money", "")]
        public void TS00001_Source_to_Savings()
        {
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

            log.PushToLog();
            Properties.TestLog.Close();
            Properties.Driver.Quit();
        }

        [Fact, Trait("Move Money", "")]
        public void TS00002_Source_to_Current()
        {
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
        }

        [Fact, Trait("Move Money", "")]
        public void TS00003_Source_to_Prepaid()
        {
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
        }

        [Fact, Trait("Move Money", "")]
        public void TS00004_Dollar_to_Dollar()
        {
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
        }
        //****************************************************************************************************************************************
        */
        #endregion

        #region "SendMoney_RegisteredPayee"
        //****************************************************************************************************************************************
        //Refer to SendMoney Class
        //****************************************************************************************************************************************
        /*
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00005_Source_To_EWB()
        {
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
        }
        #endregion

        #region "SendMoney_RegisteredPayee_LocalBank"
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00006_Source_to_Local_Individual_INSTAPAY()
        {
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
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00007_Source_to_Local_Individual_PESONET()
        {
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
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00009_Source_to_Local_Corporate_INSTAPAY()
        {
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
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00010_Source_to_Local_Corporate_PESONET()
        {
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
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00008_Source_to_Local_Individual_DollarAccount()
        {
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
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00011_Source_to_Local_Corporate_DollarAccount()
        {
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
        }

        #region "SendMoney_RegisteredPayee_OutCountry"
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00012_Source_to_OutOfTheCountry_Beneficiary()
        {
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
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00013_Source_to_OutOfTheCountry_Shared()
        {
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
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00014_Source_to_OutOfTheCountry_OnUs()
        {
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
        }
        */
        //****************************************************************************************************************************************

        //*************************************************************************************************************************
        //Refer to EWOP - BillsPayment Class
        //*************************************************************************************************************************
        /*
        [Fact, Trait("Bills Pay", "Pay to All Billers")]
        public void TS00031_PayBill_All_Biller_Payment()
        {
            foreach (var item in Properties.PayBill)
            {
                NavigateServicesObject navi = new NavigateServicesObject();

                try
                {
                    var outputDir = Properties.Configuration.GetSection("AppSettings").GetSection("OutputFileLocation").Value;
                    var filename = outputDir + string.Format("TestLogs.Web_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                    Properties.TestLog = new StreamWriter(filename, true);
                    if (new FileInfo(Path.GetFullPath(filename)).Length == 0)
                        Properties.TestLog.WriteLine("Method Used|Duration|Test Case|Test Result|Source Account|Avail Amt Before|Avail Amt After|Parameter Used|Error Log|Date Tested|User Id Used");
                    Properties.TestLog.AutoFlush = true;
                }
                catch { }

                TestLog log = new TestLog();
                var billsPay = item;
                try
                {
                    Login();

                    log.UserIdUsed = Properties.Username;
                    BillsPaymentObject bpObject = new BillsPaymentObject();
                    navi.Navigate(NavigateServicesObject.AccountServices.PayABill, billsPay.AccountToUse);
                    log.SourceAccount = navi.SourceAccount;
                    log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                    bpObject.PayBill(ref billsPay, ref log);
                    Properties.Driver.Wait(navi.AccountSummaryLink);
                    navi.BackToAccountSummary();
                    navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, billsPay.AccountToUse);
                    log.AvailableAmountAfter = navi.AfterTransactionAmount;
                    navi.Logout();
                    log.ParameterUsed = Properties.ExtractParameters(billsPay);

                    //navi.Logout();
                }
                catch (Exception ex)
                {


                    log.Passed = false;
                    log.ErrorLog = ex.ToString();
                    log.ParameterUsed = Properties.ExtractParameters(billsPay);
                }
                navi.Logout();
                log.PushToLog();
            }

            Properties.Driver.Quit();

        }
        */

        //*************************************************************************************************************************
        //Refer to EWOP - BillsPayment Class
        //*************************************************************************************************************************
        /*
        [Fact, Trait("Bills Pay", "All Enrolled Merchant")]
        public void TS00032_PayBill_All_Enrolled_Merchant()
        {
            Login();
            BillsPaymentObject bp = new BillsPaymentObject();
            NavigateServicesObject navi = new NavigateServicesObject();
            navi.Navigate(NavigateServicesObject.AccountServices.PayABill, null);
            var noOfBillers = bp.CountBillers();

            navi.Logout();
            bp = null;
            navi = null;
            Faker faker = new Faker();
            for (int i = 0; i < noOfBillers; i++)
            {
                try
                {
                    var outputDir = Properties.Configuration.GetSection("AppSettings").GetSection("OutputFileLocation").Value;
                    var filename = outputDir + string.Format("TestLogs.Web_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                    Properties.TestLog = new StreamWriter(filename, true);
                    if (new FileInfo(Path.GetFullPath(filename)).Length == 0)
                        Properties.TestLog.WriteLine("Method Used|Duration|Test Case|Test Result|Source Account|Avail Amt Before|Avail Amt After|Parameter Used|Error Log|Date Tested|User Id Used");
                    Properties.TestLog.AutoFlush = true;
                }
                catch { }
                TestLog testLog = new TestLog();
                PayBill billsPay = new PayBill();
                bp = new BillsPaymentObject();
                try
                {
                    Login();
                    billsPay.BillType = PayBillType.RegisterdPayee;
                    billsPay.Amount = faker.Finance.Amount(1000, 2).ToString();
                    billsPay.EnrolledEntryNo = i;
                    testLog.UserIdUsed = Properties.Username;

                    navi = new NavigateServicesObject();
                    navi.Navigate(NavigateServicesObject.AccountServices.PayABill, null);
                    billsPay.AccountToUse = navi.SourceAccount;
                    testLog.SourceAccount = navi.SourceAccount;
                    testLog.AvailableAmountBefore = navi.BeforeTransactionAmount;
                    bp.PayBill(ref billsPay, ref testLog);

                    navi.BackToAccountSummary();
                    navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, billsPay.AccountToUse);
                    testLog.AvailableAmountAfter = navi.AfterTransactionAmount;
                    //navi.BackToAccountSummary();

                    testLog.ParameterUsed = Properties.ExtractParameters(billsPay);
                }
                catch (Exception ex)
                {
                    testLog.Passed = false;
                    testLog.ErrorLog = ex.Message;
                    testLog.ParameterUsed = Properties.ExtractParameters(billsPay);

                }

                testLog.PushToLog();
                navi.Logout();
            }
        }
        */
        #endregion

        #region "GetBillsPayment"
        internal object GetBillsPayment()
        {
            var billsPayments = new List<PayBill>();
            var bpConfig = Properties.Configuration.GetSection("Bills Pay").GetSection("Merchants");

            bpConfig.GetChildren().ToList().ForEach(p =>
            {
                PayBill entry = new PayBill();
                entry.AccountToUse = p["AccountToUse"];
                entry.Category = p["Category"];
                entry.Amount = p["Amount"];
                entry.Biller = p["Biller"];
                entry.NickName = p["NickName"];
                entry.BillType = PayBillType.OneTimePayment;
                entry.Parameter = new List<BillingParameter>();
                var parameters = p.GetSection("Parameters");
                int counter = 0;
                parameters.GetChildren().ToList().ForEach(o =>
                {
                    counter++;
                    entry.Parameter.Add(new BillingParameter()
                    {
                        Id = counter,
                        Value = o.Value
                    });

                });
                billsPayments.Add(entry);
            });

            return billsPayments;

        }
        #endregion "GetBillsPayment"

        #region "GetOwnAcccounts"
        public static List<OwnAccounts> GetOwnAcccounts() //void GetOwnAcccounts() //
        {
            //Login();
            try
            {
                IWebElement MobileUpdatedYes = Properties.Driver.FindElement(By.Id("BUT_4BC9E0FCA3A053F642535"));
                if (MobileUpdatedYes != null)
                    MobileUpdatedYes.Click();
            }
            catch { }

            IWebElement AccountsLink = Properties.Driver.FindElement(By.Id("BUT_AE08013FF1733FC618763"));
            Properties.Driver.Wait(AccountsLink);
            AccountsLink.Click();

            IList<IWebElement> accountsElement = Properties.Driver.FindElements(By.XPath("//div[@title='Account Number']//label[text()='Account number']//ancestor::div[4]//span[@title='Account Number']"));
            List<OwnAccounts> accounts = new List<OwnAccounts>();
            foreach (IWebElement element in accountsElement)
            {
                try
                {
                    OwnAccounts account = new OwnAccounts();

                    account.AccountNumber = element.GetAttribute("textContent");
                    //var b= element.GetAttribute("textContent");
                    //var c = element.GetAttribute("innerHTML");
                    var displayed = element.Displayed;
                    IWebElement accountType = element.FindElement(By.XPath("//span[text()='" + account.AccountNumber + "']//ancestor::div[8]//a//span"));
                    IWebElement currency = element.FindElement(By.XPath("//span[text()='" + account.AccountNumber + "']//ancestor::div[6]//div[@title='Current balance']//ancestor::div[3]//span[@title='Current balance']//ancestor::div[1]//span"));

                    account.AccountType = accountType.GetAttribute("textContent");
                    account.Currency = currency.GetAttribute("textContent");

                    accounts.Add(account);
                }
                catch (Exception ex)
                {
                    string strMsg;
                    strMsg = ex.Message.ToString();
                    continue;
                }
            }

            // var a = accounts;
            return accounts;
        }
        #endregion "GetOwnAcccounts"

        #region "ApplyforaProduct"
        public static void PopProductApplyNewTimeDeposit(string strMethod, string strTestCase)
        {
            TestLog log = new TestLog(strMethod);
            log.UserIdUsed = Properties.Username;

            NavigateServicesObject navi = new NavigateServicesObject();
            MakeATransferObject maketransfer = new MakeATransferObject();

            try
            {
                //var testcase = from x in Properties.NewTimeDepositsTestCases select x;
                foreach (Product_NewTimeDeposit_TestCases testcaselist in Properties.NewTimeDepositsTestCases)
                {
                    string testcaseid = testcaselist.TestCaseID;
                    var testcasedata = testcaselist.TestCaseData;
                    if (testcaseid == strTestCase)
                    {
                        foreach (Product_NewTimeDeposit_TestCases_TestData testdatalist in testcasedata)
                        {
                            string strcurrency = testdatalist.Currency;
                            string strsourceoffunds = testdatalist.SourceOfFunds;
                            string strterms = testdatalist.Terms;
                            string strdepositamount = testdatalist.DepositAmount;
                            string strmobileconfirm = testdatalist.MobileConfirm;
                            string strotpresponse = testdatalist.OTPResponse;
                            string strsubmitbutton = testdatalist.SubmitButton;
                            string strexpectederrmsg = testdatalist.ExpectedMessage;

                            log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                            log.SourceAccount = navi.SourceAccount;

                            navi.Navigate(NavigateServicesObject.ProductServices.NewTimeDeposit, strmobileconfirm, strotpresponse);
                            if (strotpresponse == "confirm" && strmobileconfirm == "")
                            {
                                maketransfer.NewTimeDeposit(strcurrency, strsourceoffunds, strterms, strdepositamount, strsubmitbutton, strexpectederrmsg);
                            }
                        }
                        log.Passed = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.ToString();
            }

            log.PushToLog();
            log.CloseLog();
            navi.Logout();
            Properties.Driver.Quit();

        }
        #endregion
    }

    public class OwnAccounts
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }

        public string Currency { get; set; }
    }

}
