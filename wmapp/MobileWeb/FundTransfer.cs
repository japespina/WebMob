using Bogus;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SeleniumWebDriver_AutomatedTesting.MobileWeb.PageClass;
using SeleniumWebDriver_AutomatedTesting.MobileWeb.PageObjects;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SeleniumWebDriver_AutomatedTesting.MobileApp
{
    public class FundTransfer : MobileWeb
    {
        private void MoveMoney(MoveMoneyClass.DestinationType destinationType, bool isForeign = false, TestLog log = null)
        {
            try
            {
                Login();

                if (log != null)
                    log.UserIdUsed = Properties.Username;

                var appConfig = Properties.Configuration
                   .GetSection("AppSettings:MoveMoney").GetChildren().ToList()
                   .Select(x => (
                           x.GetValue<string>("SourceAcct"),
                           x.GetValue<string>("DestinationAcct"),
                           x.GetValue<string>("Amount"),
                           x.GetValue<string>("Remarks")
                       )
                   ).ToList<(string SourceAcct, string DestinationAcct, string Amount, string Remarks)>();

                // randomize array selection
                Random rand = new Random();
                int index = rand.Next(appConfig.Count());

                // set input variables
                string sourceAcct = appConfig[index].SourceAcct;
                string destinationAcct = appConfig[index].DestinationAcct;
                string amount = appConfig[index].Amount;
                string remarks = appConfig[index].Remarks;

                // set if controlled/random testing
                if (Properties.ControlledTest == false)
                {
                    var faker = new Faker();
                    sourceAcct = PopOver_Click(NavigateTo.MoveMoney, isForeign, null, log);
                    destinationAcct = null;
                    amount = faker.Parse("{{finance.Amount}}");
                    remarks = faker.Parse("{{randomizer.Word}}");

                    if (sourceAcct == "0")
                    {
                        Logout(log, false);
                        return;
                    }
                }
                else
                {
                    sourceAcct = PopOver_Click(NavigateTo.MoveMoney, isForeign, sourceAcct, log);

                    if (sourceAcct == "0")
                    {
                        Logout(log, false);
                        return;
                    }
                }

                RemoveOTP(NavigateTo.MoveMoney);
                MoveMoneyObject mmObject = new MoveMoneyObject();
                mmObject.MoveMoney(sourceAcct, destinationType, destinationAcct, amount, remarks, log);
                Properties.Driver.Quit();
            }
            catch (Exception ex)
            {
                if (log != null)
                {
                    log.Passed = false;
                    bool isElementDisplayed = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Displayed;

                    Properties.Driver.Wait(By.Id("Generic_Modal_Body"));
                    string errorMsg;

                    if (isElementDisplayed == true)
                        errorMsg = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Text;
                    else
                        errorMsg = ex.ToString();

                    log.ErrorLog = errorMsg;
                }
            }

            log.PushToLog();
        }
        private void SendMoney_EWB(bool isSaved = false, bool isRegistered = false, TestLog log = null)
        {
            try
            {
                Login();

                if (log != null)
                    log.UserIdUsed = Properties.Username;

                var appConfig = Properties.Configuration
                    .GetSection("AppSettings:SendMoney:EWB").GetChildren().ToList()
                    .Select(x => (
                            x.GetValue<string>("SourceAcct"),
                            x.GetValue<string>("DestinationAcct"),
                            x.GetValue<string>("Amount")
                        )
                    ).ToList<(string SourceAcct, string DestinationAcct, string Amount)>();

                // randomize array selection
                Random rand = new Random();
                int index = rand.Next(appConfig.Count());

                var faker = new Faker();

                // set input variables
                string sourceAcct = appConfig[index].SourceAcct;
                string destinationAcct = appConfig[index].DestinationAcct;
                string amount = appConfig[index].Amount;
                string nickName = faker.Parse("{{hacker.Noun}}");

                if (string.IsNullOrEmpty(sourceAcct))
                    sourceAcct = PopOver_Click(NavigateTo.SendMoney, false, null, log);
                
                RemoveOTP(NavigateTo.SendMoney);

                SendMoneyObject smObject = new SendMoneyObject();
                smObject.SendMoney_EWB(sourceAcct, destinationAcct, amount, nickName, isSaved, isRegistered, log);

            }
            catch (Exception ex)
            {
                if (log != null)
                {
                    log.Passed = false;
                    bool isElementDisplayed = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Displayed;

                    Properties.Driver.Wait(By.Id("Generic_Modal_Body"));
                    string errorMsg;

                    if (isElementDisplayed == true)
                        errorMsg = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Text;
                    else
                        errorMsg = ex.ToString();

                    log.ErrorLog = errorMsg;
                }
            }
            finally
            {
                log.PushToLog();
            }
        }
        private void SendMoney_Local(SendMoneyObject.PaymentMethod payMethod, SendMoneyObject.Payee payee, bool isSaved = false, bool isRegistered = false, bool isForeign = false, TestLog log = null)
        {
            try
            {
                Login();

                if (log != null)
                    log.UserIdUsed = Properties.Username;

                string appSection = null;

                switch (payMethod)
                {
                    case SendMoneyObject.PaymentMethod.INSTAPAY:
                        appSection = "Instapay";
                        break;
                    case SendMoneyObject.PaymentMethod.PESONET:
                        appSection = "Pesonet";
                        break;
                    case SendMoneyObject.PaymentMethod.FOREIGN:
                        appSection = "Foreign";
                        break;
                }

                var appConfig = Properties.Configuration.GetSection("AppSettings:SendMoney:Local")
                    .GetSection(appSection).GetChildren().ToList()
                    .Select(x => (
                            x.GetValue<string>("SourceAcct"),
                            x.GetValue<string>("DestinationAcct"),
                            x.GetValue<string>("FirstName"),
                            x.GetValue<string>("LastName"),
                            x.GetValue<string>("BankID"),
                            x.GetValue<string>("Amount"),
                            x.GetValue<string>("NickName")
                        )
                    ).ToList<(string SourceAcct, string DestinationAcct, string FirstName, string LastName, string BankID, string Amount, string NickName)>();

                // randomize array selection
                Random rand = new Random();
                int index = rand.Next(appConfig.Count());

                // set input variables
                string sourceAcct = appConfig[index].SourceAcct;
                string destinationAcct = appConfig[index].DestinationAcct;
                string firstName = appConfig[index].FirstName;
                string lastName = appConfig[index].LastName;
                string bankID = appConfig[index].BankID;
                string amount = appConfig[index].Amount;
                string nickName = appConfig[index].NickName;

                if (Properties.ControlledTest == false)
                {
                    var faker = new Faker();

                    sourceAcct = PopOver_Click(NavigateTo.SendMoney, isForeign, null, log);

                    if (isRegistered == true)
                        destinationAcct = null;
                    else
                        destinationAcct = faker.Parse("{{finance.CreditCardNumber}}");

                    firstName = faker.Parse("{{name.FirstName}}");
                    lastName = faker.Parse("{{name.LastName}}");
                    bankID = null;
                    nickName = faker.Parse("{{name.FirstName}}");
                    amount = faker.Parse("{{finance.Amount}}");
                }
                else
                {
                    sourceAcct = PopOver_Click(NavigateTo.SendMoney, isForeign, sourceAcct, log);

                    if (sourceAcct == "0")
                    {
                        Logout(log, false);
                        return;
                    }
                }

                RemoveOTP(NavigateTo.SendMoney);

                SendMoneyObject smObject = new SendMoneyObject();
                smObject.SendMoney_Local(payMethod, payee, bankID, sourceAcct, destinationAcct, firstName, lastName,
                    amount, nickName, isSaved, isRegistered, isForeign, log);
            }
            catch (Exception ex)
            {
                if (log != null)
                {
                    log.Passed = false;
                    bool isElementDisplayed = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Displayed;

                    Properties.Driver.Wait(By.Id("Generic_Modal_Body"));
                    string errorMsg;

                    if (isElementDisplayed == true)
                        errorMsg = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Text;
                    else
                        errorMsg = ex.ToString();

                    log.ErrorLog = errorMsg;
                }
            }
            finally
            {
                log.PushToLog();
            }
        }
        private void SendMoney_OutOfTheCountry(bool isSaved = false, SendMoneyObject.ChargingOptions chargingOptions = 0, bool isRegistered = false, TestLog log = null)
        {
            try
            {
                Login();

                if (log != null)
                    log.UserIdUsed = Properties.Username;

                var appConfig = Properties.Configuration.GetSection("AppSettings:SendMoney:OutOfTheCountry").GetChildren().ToList()
                   .Select(x => (
                            x.GetValue<string>("SourceAcct"),
                            x.GetValue<string>("DestinationAcct"),
                            x.GetValue<string>("FirstName"),
                            x.GetValue<string>("LastName"),
                            x.GetValue<string>("BankID"),
                            x.GetValue<string>("Amount"),
                            x.GetValue<string>("NickName")
                        )
                    ).ToList<(string SourceAcct, string DestinationAcct, string FirstName, string LastName, string BankID, string Amount, string NickName)>();

                Random rand = new Random();
                int index = rand.Next(appConfig.Count());

                string sourceAcct = appConfig[index].SourceAcct;
                string destinationAcct = appConfig[index].DestinationAcct;
                //string fullName = appConfig[index].FullName;
                string firstName = appConfig[index].FirstName;
                string lastName = appConfig[index].LastName;
                string bankID = appConfig[index].BankID;
                string amount = appConfig[index].Amount;
                string nickName = appConfig[index].NickName;

                if (Properties.ControlledTest == false)
                {
                    var faker = new Faker();
                    sourceAcct = PopOver_Click(NavigateTo.SendMoney, true, null, log);

                    if (isRegistered == true)
                        destinationAcct = null;
                    else
                        destinationAcct = faker.Parse("{{finance.CreditCardNumber}}");

                    //fullName = faker.Parse("{{name.FirstName}} {{name.LastName}}");
                    firstName = faker.Parse("{{name.FirstName}}");
                    lastName = faker.Parse("{{name.LastName}}");
                    bankID = null;
                    nickName = faker.Parse("{{name.FirstName}}");
                    amount = faker.Parse("{{finance.Amount}}");

                    if (sourceAcct == "0")
                    {
                        Logout(log, false);
                        return;
                    }
                }
                else
                {
                    sourceAcct = PopOver_Click(NavigateTo.SendMoney, true, sourceAcct, log);

                    if (sourceAcct == "0")
                    {
                        Logout(log, false);
                        return;
                    }
                }

                RemoveOTP(NavigateTo.SendMoney);

                SendMoneyObject smObject = new SendMoneyObject();
                smObject.SendMoney_OutOfTheCountry(sourceAcct, destinationAcct, firstName, lastName, amount, chargingOptions, nickName, isSaved, isRegistered, log);
            }
            catch (Exception ex)
            {
                if (log != null)
                {
                    log.Passed = false;
                    bool isElementDisplayed = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Displayed;

                    Properties.Driver.Wait(By.Id("Generic_Modal_Body"));
                    string errorMsg;

                    if (isElementDisplayed == true)
                        errorMsg = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Text;
                    else
                        errorMsg = ex.ToString();

                    log.ErrorLog = errorMsg;
                }
            }
            finally
            {
                log.PushToLog();
            }
        }
        private void AllBanks(SendMoneyObject.PaymentMethod payMethod, SendMoneyObject.Payee payee, bool isSaved = false, bool isRegistered = false, bool isForeign = false, TestLog log = null)
        {
            try
            {
                string appSection = null;

                switch (payMethod)
                {
                    case SendMoneyObject.PaymentMethod.INSTAPAY:
                        appSection = "Instapay";
                        break;
                    case SendMoneyObject.PaymentMethod.PESONET:
                        appSection = "Pesonet";
                        break;
                }

                var appConfig = Properties.Configuration.GetSection("AppSettings:SendMoney:Local")
                    .GetSection(appSection).GetChildren().ToList()
                    .Select(x => (
                            x.GetValue<string>("SourceAcct"),
                            x.GetValue<string>("DestinationAcct"),
                            x.GetValue<string>("FirstName"),
                            x.GetValue<string>("LastName"),
                            x.GetValue<string>("BankID"),
                            x.GetValue<string>("Amount"),
                            x.GetValue<string>("NickName")
                        )
                    ).ToList<(string SourceAcct, string DestinationAcct, string FirstName, string LastName, string BankID, string Amount, string NickName)>();

                for (int i = 0; i < appConfig.Count(); i++)
                {
                    Login();

                    if (log != null)
                        log.UserIdUsed = Properties.Username;

                    // set input variables
                    string sourceAcct = appConfig[i].SourceAcct;
                    string destinationAcct = appConfig[i].DestinationAcct;
                    string firstName = appConfig[i].FirstName;
                    string lastName = appConfig[i].LastName;
                    string bankID = appConfig[i].BankID;
                    string amount = appConfig[i].Amount;
                    string nickName = appConfig[i].NickName;

                    //if (Properties.ControlledTest == false)
                    //{
                    //    var faker = new Faker();

                    //    sourceAcct = PopOver_Click(NavigateTo.SendMoney, false, null, log);

                    //    if (isRegistered == true)
                    //        destinationAcct = null;
                    //    //else
                    //    //    destinationAcct = faker.Parse("{{finance.CreditCardNumber}}");

                    //    firstName = faker.Parse("{{name.FirstName}}");
                    //    lastName = faker.Parse("{{name.LastName}}");
                    //    //bankID = null;
                    //    nickName = faker.Parse("{{name.FirstName}}");
                    //    amount = faker.Parse("{{finance.Amount}}");
                    //}
                    //else
                    //{
                    //    sourceAcct = PopOver_Click(NavigateTo.SendMoney, false, sourceAcct, log);

                    //    if (sourceAcct == "0")
                    //    {
                    //        Logout(log, false);
                    //        return;
                    //    }
                    //}

                    sourceAcct = PopOver_Click(NavigateTo.SendMoney, false, sourceAcct, log);

                    if (sourceAcct == "0")
                    {
                        Logout(log, false);
                        return;
                    }

                    RemoveOTP(NavigateTo.SendMoney);

                    SendMoneyObject smObject = new SendMoneyObject();
                    smObject.SendMoney_Local(payMethod, payee, bankID, sourceAcct, destinationAcct, firstName, lastName,
                        amount, nickName, isSaved, isRegistered, isForeign, log);
                }
            }
            catch (Exception ex)
            {
                if (log != null)
                {
                    log.Passed = false;
                    bool isElementDisplayed = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Displayed;

                    Properties.Driver.Wait(By.Id("Generic_Modal_Body"));
                    string errorMsg;

                    if (isElementDisplayed == true)
                        errorMsg = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Text;
                    else
                        errorMsg = ex.ToString();

                    log.ErrorLog = errorMsg;
                }
            }
            finally
            {
                log.PushToLog();
            }
        }

        #region Move Money
        [Fact, Trait("Move Money", "Move Money")]
        public void TS00001_Source_to_Savings()
        {
            TestLog log = new TestLog();
            MoveMoney(MoveMoneyClass.DestinationType.Savings, false, log);
        }

        [Fact, Trait("Move Money", "Move Money")]
        public void TS00002_Source_to_Current()
        {
            TestLog log = new TestLog();
            MoveMoney(MoveMoneyClass.DestinationType.Current, false, log);
        }

        [Fact, Trait("Move Money", "Move Money")]
        public void TS00003_Source_to_Prepaid()
        {
            TestLog log = new TestLog();
            MoveMoney(MoveMoneyClass.DestinationType.Prepaid, false, log);
        }

        [Fact, Trait("Move Money", "Move Money")]
        public void TS00004_Dollar_to_Dollar()
        {
            TestLog log = new TestLog();
            MoveMoney(MoveMoneyClass.DestinationType.Dollar, true, log);
        }
        #endregion

        #region Registered Payee
        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00005_Source_To_EWB()
        {
            TestLog log = new TestLog();
            SendMoney_EWB(false, true, log);
        }


        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00006_Source_to_Local_Individual_INSTAPAY()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.INSTAPAY, SendMoneyObject.Payee.INDIVIDUAL, false, true, false, log);
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00007_Source_to_Local_Individual_PESONET()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.PESONET, SendMoneyObject.Payee.INDIVIDUAL, false, true, false, log);
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00008_Source_to_Local_Individual_DollarAccount()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.FOREIGN, SendMoneyObject.Payee.INDIVIDUAL, false, true, true, log);
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00009_Source_to_Local_Corporate_INSTAPAY()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.INSTAPAY, SendMoneyObject.Payee.CORPORATE, false, true, false, log);
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00010_Source_to_Local_Corporate_PESONET()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.PESONET, SendMoneyObject.Payee.CORPORATE, false, true, false, log);
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00011_Source_to_Local_Corporate_DollarAccount()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.FOREIGN, SendMoneyObject.Payee.CORPORATE, false, true, true, log);
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00012_Source_to_OutOfTheCountry_Beneficiary()
        {
            TestLog log = new TestLog();
            SendMoney_OutOfTheCountry(false, SendMoneyObject.ChargingOptions.BENEFICIARY, true, log);
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00013_Source_to_OutOfTheCountry_Shared()
        {
            TestLog log = new TestLog();
            SendMoney_OutOfTheCountry(false, SendMoneyObject.ChargingOptions.SHARED, true, log);
        }

        [Fact, Trait("Send Money", "Registered Payee")]
        public void TS00014_Source_to_OutOfTheCountry_OnUs()
        {
            TestLog log = new TestLog();
            SendMoney_OutOfTheCountry(false, SendMoneyObject.ChargingOptions.ONUS, true, log);
        }
        #endregion

        #region One Time Payment
        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00015_Source_To_EWB()
        {
            TestLog log = new TestLog();
            SendMoney_EWB(false, false, log);
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00016_Source_to_Local_Individual_INSTAPAY()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.INSTAPAY, SendMoneyObject.Payee.INDIVIDUAL, false, false, false, log);
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00017_Source_to_Local_Individual_PESONET()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.PESONET, SendMoneyObject.Payee.INDIVIDUAL, false, false, false, log);
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00018_Source_to_Local_Individual_DollarAccount()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.FOREIGN, SendMoneyObject.Payee.FOREIGN, false, false, true, log);
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00019_Source_to_Local_Corporate_INSTAPAY()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.INSTAPAY, SendMoneyObject.Payee.CORPORATE, false, false, false, log);
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00020_Source_to_Local_Corporate_PESONET()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.PESONET, SendMoneyObject.Payee.CORPORATE, false, false, false, log);
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00021_Source_to_Local_Corporate_DollarAccount()
        {
            TestLog log = new TestLog();
            SendMoney_Local(SendMoneyObject.PaymentMethod.FOREIGN, SendMoneyObject.Payee.FOREIGN, false, false, true, log);
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00022_Source_to_OutOfTheCountry_Beneficiary()
        {
            TestLog log = new TestLog();
            SendMoney_OutOfTheCountry(false, SendMoneyObject.ChargingOptions.BENEFICIARY, false, log);
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00023_Source_to_OutOfTheCountry_Shared()
        {
            TestLog log = new TestLog();
            SendMoney_OutOfTheCountry(false, SendMoneyObject.ChargingOptions.SHARED, false, log);
        }

        [Fact, Trait("Send Money", "One Time Payment")]
        public void TS00024_Source_to_OutOfTheCountry_OnUs()
        {
            TestLog log = new TestLog();
            SendMoney_OutOfTheCountry(false, SendMoneyObject.ChargingOptions.ONUS, false, log);
        }

        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00025_SavedBeneficiary_EWBPayee()
        {
            TestLog log = new TestLog();
            SendMoney_EWB(true, false, log);
        }

        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00026_SavedBeneficiary_Local_Individual()
        {
            TestLog log = new TestLog();

            Random rand = new Random();
            int index = rand.Next(0, 1);

            SendMoneyObject.PaymentMethod payment;

            switch (index)
            {
                case 0:
                    payment = SendMoneyObject.PaymentMethod.PESONET;
                    break;

                default:
                    payment = SendMoneyObject.PaymentMethod.INSTAPAY;
                    break;
            }

            SendMoney_Local(payment, SendMoneyObject.Payee.INDIVIDUAL, true, false, false, log);
        }

        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00027_SavedBeneficiary_Local_Corporate()
        {
            TestLog log = new TestLog();

            Random rand = new Random();
            int index = rand.Next(0, 1);

            SendMoneyObject.PaymentMethod payment;

            switch (index)
            {
                case 0:
                    payment = SendMoneyObject.PaymentMethod.PESONET;
                    break;

                default:
                    payment = SendMoneyObject.PaymentMethod.INSTAPAY;
                    break;
            }

            SendMoney_Local(payment, SendMoneyObject.Payee.CORPORATE, true, false, false, log);
        }

        [Fact, Trait("Send Money", "Save Beneficiary")]
        public void TS00028_SavedBeneficiary_OutOfTheCountry()
        {
            TestLog log = new TestLog();
            SendMoney_OutOfTheCountry(true, 0, false, log);
        }

        [Fact, Trait("Send Money", "All Banks")]
        public void TS00029_SendMoney_OneTimePayment_INSTAPAY_AllBanks()
        {
            TestLog log = new TestLog();
            AllBanks(SendMoneyObject.PaymentMethod.INSTAPAY, SendMoneyObject.Payee.INDIVIDUAL, false, false, false, log);
        }

        [Fact, Trait("Send Money", "All Banks")]
        public void TS00030_SendMoney_OneTimePayment_PESONET_AllBanks()
        {
            TestLog log = new TestLog();
            AllBanks(SendMoneyObject.PaymentMethod.PESONET, SendMoneyObject.Payee.INDIVIDUAL, false, false, false, log);
        }


        #endregion
    }
}
