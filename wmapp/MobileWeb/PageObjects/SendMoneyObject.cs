using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumWebDriver_AutomatedTesting.MobileWeb.PageClass;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace SeleniumWebDriver_AutomatedTesting.MobileWeb.PageObjects
{
    class SendMoneyObject
    {
        public SendMoneyObject()
        {
            PageFactory.InitElements(Properties.Driver, this);
        }

        [FindsBy(How = How.Id, Using = "Payment_Type")]
        public IWebElement BeneficiaryType { get; set; }

        [FindsBy(How = How.Id, Using = "Payee_Options")]
        public IWebElement PaymentOption { get; set; }

        [FindsBy(How = How.Id, Using = "To_Account")]
        public IWebElement AccountNumber { get; set; }

        // Registered Payee Account Number
        [FindsBy(How = How.Id, Using = "Payee")]
        public IWebElement RegisteredAccount { get; set; }

        [FindsBy(How = How.Id, Using = "Transfer_Amount")]
        public IWebElement TransferAmount { get; set; }

        #region "Local Banks"

        [FindsBy(How = How.Id, Using = "Account_Type")]
        public IWebElement PayeeType { get; set; }

        [FindsBy(How = How.Id, Using = "Payee_FName")]
        public IWebElement FirstName { get; set; }

        [FindsBy(How = How.Id, Using = "Payee_LName")]
        public IWebElement LastName { get; set; }

        #endregion

        #region "Out of the Country"

        [FindsBy(How = How.Id, Using = "Payee_Fullname")]
        public IWebElement FullName { get; set; }

        [FindsBy(How = How.Id, Using = "Country_List")]
        public IWebElement CountryList { get; set; }

        [FindsBy(How = How.Id, Using = "Payee_Currency")]
        public IWebElement Currency { get; set; }

        [FindsBy(How = How.Id, Using = "Charges")]
        public IWebElement ChargingOption { get; set; }

        #endregion

        [FindsBy(How = How.Id, Using = "Payee_Bank")]
        public IWebElement BeneficiaryBank { get; set; }

        [FindsBy(How = How.Id, Using = "Save_Payee")]
        public IWebElement SaveBeneDetails { get; set; }

        [FindsBy(How = How.Id, Using = "Payee_Nickname")]
        public IWebElement BeneficiaryNickName { get; set; }

        #region "Buttons"
        
        [FindsBy(How = How.XPath, Using = "//*[@id='Validate_Transaction']")]
        public IWebElement Button1 { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='Validate_Transaction2']")]
        public IWebElement Button2 { get; set; }

        [FindsBy(How = How.Id, Using = "Confirm_Btn")]
        public IWebElement Confirm1 { get; set; }

        [FindsBy(How = How.Id, Using = "Continue_Btn")]
        public IWebElement Continue1 { get; set; }

        #endregion

        #region "Security Question"
        [FindsBy(How = How.Id, Using = "SQ_Fld")]
        public IWebElement SQField { get; set; }
        #endregion

        [FindsBy(How = How.Id, Using = "Logout_Btn")]
        public IWebElement LogOut { get; set; }

        [FindsBy(How = How.Id, Using = "Menu_Link")]
        public IWebElement BurgerMenu { get; set; }

        public enum Beneficiary
        {
            EastWestBank,
            LocalBank,
            OutOfTheCountry
        }

        public enum PaymentMethod
        {
            INSTAPAY,
            PESONET,
            FOREIGN
        }

        public enum Payee
        {
            INDIVIDUAL,
            CORPORATE,
            FOREIGN
        }

        public enum ChargingOptions
        {
            BENEFICIARY,
            SHARED,
            ONUS
        }

        private void EndSession()
        {
            Properties.Driver.Wait(BurgerMenu);
            BurgerMenu.Click();

            Properties.Driver.Wait(LogOut);
            LogOut.Click();

            //Properties.TestLog.Close();
            Properties.Driver.Quit();
        }

        public void Submit(Beneficiary type, bool isRegistered = false, PaymentMethod payMethod = 0, string sourceAcct = null, TestLog log = null)
        {
            try
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)Properties.Driver;

                var btn1 = Properties.Driver.FindElement(By.XPath("//*[@id='Validate_Transaction']"));
                var btn2 = Properties.Driver.FindElement(By.XPath("//*[@id='Validate_Transaction2']"));

                switch (type)
                {
                    case Beneficiary.EastWestBank:
                        executor.ExecuteScript("arguments[0].click();", btn1);
                        break;

                    case Beneficiary.LocalBank:

                        if (payMethod == PaymentMethod.PESONET || payMethod == PaymentMethod.FOREIGN)
                        {
                            if (btn1.Enabled == true)
                            {
                                executor.ExecuteScript("arguments[0].click();", btn1);
                            }
                            else
                            {
                                if (isRegistered == true)
                                {
                                    IList<IWebElement> accountList = Properties.Driver.FindElements(By.CssSelector("#Payee > option"));

                                    var index = 1;
                                    foreach (var item in accountList)
                                    {
                                        RegisteredAccount.SelectDropDownByIndex(index);

                                        if (btn1.Enabled == false)
                                        {
                                            index++;
                                            continue;
                                        }
                                        else
                                        {
                                            executor.ExecuteScript("arguments[0].click();", btn1);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    IList<IWebElement> bankList = Properties.Driver.FindElements(By.CssSelector("#Payee_Bank > option"));

                                    var index = 1;
                                    foreach (var item in bankList)
                                    {
                                        BeneficiaryBank.SelectDropDownByIndex(index);

                                        if (btn1.Enabled == false)
                                        {
                                            index++;
                                            continue;
                                        }
                                        else
                                        {
                                            executor.ExecuteScript("arguments[0].click();", btn1);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else if (payMethod == PaymentMethod.INSTAPAY)
                        {
                            if (btn2.Enabled == true)
                            {
                                executor.ExecuteScript("arguments[0].click();", btn2);
                            }
                            else
                            {
                                if (isRegistered == true)
                                {
                                    IList<IWebElement> accountList = Properties.Driver.FindElements(By.CssSelector("#Payee > option"));

                                    var index = 1;
                                    foreach (var item in accountList)
                                    {
                                        RegisteredAccount.SelectDropDownByIndex(index);

                                        if (btn2.Enabled == false)
                                        {
                                            index++;
                                            continue;
                                        }
                                        else
                                        {
                                            executor.ExecuteScript("arguments[0].click();", btn2);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    IList<IWebElement> bankList = Properties.Driver.FindElements(By.CssSelector("#Payee_Bank > option"));

                                    var index = 1;
                                    foreach (var item in bankList)
                                    {
                                        BeneficiaryBank.SelectDropDownByIndex(index);

                                        if (btn2.Enabled == false)
                                        {
                                            index++;
                                            continue;
                                        }
                                        else
                                        {
                                            executor.ExecuteScript("arguments[0].click();", btn2);
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        break;

                    case Beneficiary.OutOfTheCountry:
                        executor.ExecuteScript("arguments[0].click();", btn1);
                        break;
                }

                Thread.Sleep(5000);
                bool isElementDisplayed = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Displayed;

                if (isElementDisplayed == true)
                {
                    string errorMsg = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Text;

                    log.Passed = false;
                    log.ErrorLog = errorMsg;
                    log.PushToLog();

                    var closeOTP = Properties.Driver.FindElement(By.CssSelector("#Generic_Modal > div > div > div > button#modal_close_btn"));
                    closeOTP.Click();

                    //EndSession();
                    return;
                }
                else
                {
                    // SQ Field
                    if (isRegistered == false)
                    {
                        Properties.Driver.Wait(SQField);
                        SQField.EnterText("answer");
                    }

                    // Confirm Button
                    Properties.Driver.Wait(Confirm1);
                    var confirmBtn = Properties.Driver.FindElement(By.XPath("//*[@id='Confirm_Btn']"));
                    executor.ExecuteScript("arguments[0].click();", confirmBtn);

                    Thread.Sleep(7000);
                    isElementDisplayed = Properties.Driver.FindElement(By.Id("Generic_Modal")).Displayed;

                    if (isElementDisplayed == true)
                    {
                        string errorMsg = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Text;

                        log.Passed = false;
                        log.ErrorLog = errorMsg;
                        log.PushToLog();
                        
                        var closeOTP = Properties.Driver.FindElement(By.CssSelector("#Generic_Modal > div > div > div > button#modal_close_btn"));
                        closeOTP.Click();

                        //EndSession();
                        return;
                    }
                    else
                    {
                        // Done Button
                        Properties.Driver.Wait(Continue1);
                        var continueBtn = Properties.Driver.FindElement(By.XPath("//*[@id='Continue_Btn']"));

                        Screenshot ss = ((ITakesScreenshot)Properties.Driver).GetScreenshot();
                        string imageFilePath = Properties.Configuration.GetSection("AppSettings").GetSection("OutputImageLocation").Value;

                        if (!Directory.Exists(imageFilePath))
                            Directory.CreateDirectory(imageFilePath);

                        ss.SaveAsFile(imageFilePath + "SendMoney_" + DateTime.Now.ToString("yyyyMMdd") + "_" + log.MethodUsed + "_" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + ".png", ScreenshotImageFormat.Png);

                        executor.ExecuteScript("arguments[0].click();", continueBtn);

                        IList<IWebElement> casaList = Properties.Driver.FindElements(By.CssSelector("#Client-Casa-List > .acc-details"));

                        foreach (IWebElement item in casaList)
                        {
                            var acctNumber = item.FindElement(By.CssSelector(".account-number")).GetAttribute("textContent");
                            var acctBalance = item.FindElement(By.CssSelector(".account-balance")).GetAttribute("textContent");

                            if (acctNumber == sourceAcct)
                            {
                                log.AvailableAmountAfter = acctBalance;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        log.Passed = true;
                        log.ErrorLog = "";
                        log.PushToLog();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Passed = false;
                bool isElementDisplayed = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Displayed;

                string errorMsg;

                if (isElementDisplayed == true)
                    errorMsg = Properties.Driver.FindElement(By.Id("Generic_Modal_Body")).Text;
                else
                    errorMsg = ex.ToString();

                log.ErrorLog = errorMsg;
                log.PushToLog();

                var closeOTP = Properties.Driver.FindElement(By.CssSelector("#Generic_Modal > div > div > div > button#modal_close_btn"));
                closeOTP.Click();

                EndSession();
            }
        }

        private void logSendMoney(string sourceAcct, string destinationAcct, string beneType, string payOption, string firstName, string lastName, string bank, string amount, string fullName = null, string nickName = null, bool isSaved = false, string currency = null, string chargingOption = null, TestLog log = null)
        {
            object objectParam = new SendMoneyClass()
            {
                SourceAccount = sourceAcct,
                BeneficiaryAccountNumber = destinationAcct,
                BeneficiaryType = beneType,
                PaymentOption = payOption,
                FirstName = firstName,
                LastName = lastName,
                Bank = bank,
                TransferAmount = amount,
                IsSaved = isSaved,
                BeneNickName = nickName,
                Currency = currency,
                ChargingOption = chargingOption
            };

            log.ParameterUsed = Properties.ExtractParameters(objectParam);
        }

        public void SendMoney_EWB(string sourceAcct, string destinationAcct, string amount, string nickName = null, bool isSaved = false, bool isRegistered = false, TestLog log = null)
        {
            switch (isRegistered)
            {
                case true:
                    // Drop down list
                    BeneficiaryType.SelectDropDown("RegisteredPayee");
                    PaymentOption.SelectDropDown("EWBPayee");

                    Properties.Driver.Wait(RegisteredAccount);
                    IList <IWebElement> accountList = Properties.Driver.FindElements(By.CssSelector("#Payee > option"));

                    Random rand = new Random();
                    int acctIndex = accountList.Count();

                    string selectedAcct = null;

                    if (acctIndex > 1)
                    {
                        if (string.IsNullOrEmpty(destinationAcct))
                            RegisteredAccount.SelectDropDownByIndex(rand.Next(1, acctIndex));
                        else
                            RegisteredAccount.SelectDropDown(destinationAcct);

                        selectedAcct = RegisteredAccount.GetTextFromDDL();
                    }
                    else
                    {
                        log.Passed = false;
                        EndSession();
                    }
                    
                    TransferAmount.EnterText(amount);

                    if (log.Passed == false)
                        selectedAcct = "There are no list of account(s)";

                    if (log != null)
                        logSendMoney(sourceAcct, selectedAcct, "RegisteredPayee", "EWBPayee", null, null, null, amount, null, nickName, isSaved, null, null, log);

                    break;

                case false:
                    // Drop down list
                    BeneficiaryType.SelectDropDown("OneTimePayment");
                    PaymentOption.SelectDropDown("EWBPayee");

                    // Input fields
                    AccountNumber.EnterText(destinationAcct);
                    TransferAmount.EnterText(amount);

                    if (isSaved == true)
                    {
                        Properties.Driver.Wait(SaveBeneDetails);
                        SaveBeneDetails.Click();
                        Properties.Driver.Wait(BeneficiaryNickName);
                        BeneficiaryNickName.EnterText(nickName);
                    }

                    string enteredAcct = AccountNumber.GetText();

                    if (log != null)
                        logSendMoney(sourceAcct, enteredAcct, "OneTimePayment", "EWBPayee", null, null, null, amount, null, nickName, isSaved, null, null, log);

                    break;
            }

            // Submit
            Submit(Beneficiary.EastWestBank, isRegistered, 0, sourceAcct, log);
        }

        public void SendMoney_Local(PaymentMethod payMethod, Payee payee, string bank, string sourceAcct, string destinationAcct, 
            string firstName, string lastName, string amount, string nickName = null, bool isSaved = false, bool isRegistered = false, bool isForeign = false, TestLog log = null)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Properties.Driver;
            Random rand = new Random();

            switch (isRegistered)
            {
                case true:
                    BeneficiaryType.SelectDropDown("RegisteredPayee");
                    PaymentOption.SelectDropDown("LocalInterBankPayee");

                    Properties.Driver.Wait(RegisteredAccount);
                    IList<IWebElement> accountList = Properties.Driver.FindElements(By.CssSelector("#Payee > option"));

                    var acctIndex = accountList.Count();

                    if (acctIndex > 1)
                    {
                        if (string.IsNullOrEmpty(destinationAcct))
                        {
                            RegisteredAccount.SelectDropDownByIndex(rand.Next(1, acctIndex));
                        }
                        else
                        {
                            RegisteredAccount.SelectDropDown(destinationAcct);
                        }
                    }
                    else
                    {
                        log.Passed = false;
                        log.ErrorLog = "No Destination Account(s)";
                        log.PushToLog();
                        EndSession();
                    }

                    if (isForeign == false)
                    {
                        string payeeType = PayeeType.GetAttribute("disabled");
                        string beneFirstName = FirstName.GetAttribute("disabled");
                        string beneLastName = LastName.GetAttribute("disabled");

                        if (payeeType == "true" && beneFirstName == "true" && beneLastName == "true")
                        {
                            TransferAmount.EnterText(amount);
                        }
                        else
                        {
                            switch (payee)
                            {
                                case Payee.INDIVIDUAL:
                                    PayeeType.SelectDropDown("individual");
                                    FirstName.EnterText(firstName);
                                    LastName.EnterText(lastName);
                                    break;

                                case Payee.CORPORATE:
                                    PayeeType.SelectDropDown("corporate");
                                    LastName.EnterText(lastName);
                                    break;
                            }

                            TransferAmount.EnterText(amount);
                        }
                    }
                    else
                    {
                        TransferAmount.EnterText(amount);
                    }

                    string accountBRSTN = RegisteredAccount.GetAttributeFromDDL("brstn");

                    if (log != null)
                        logSendMoney(sourceAcct, destinationAcct, "RegisteredPayee", "LocalInterBankPayee", 
                            firstName, lastName, accountBRSTN, amount, null, nickName, isSaved, null, null, log);

                    break;

                case false:
                    BeneficiaryType.SelectDropDown("OneTimePayment");
                    PaymentOption.SelectDropDown("LocalInterBankPayee");

                    switch (payee)
                    {
                        case Payee.INDIVIDUAL:
                        case Payee.FOREIGN:
                            PayeeType.SelectDropDown("individual");
                            FirstName.EnterText(firstName);
                            LastName.EnterText(lastName);
                            break;

                        case Payee.CORPORATE:
                            PayeeType.SelectDropDown("corporate");
                            LastName.EnterText(lastName);
                            break;

                        //case Payee.FOREIGN:
                        //    FullName.EnterText(nickName);   
                        //    break;
                    }

                    AccountNumber.EnterText(destinationAcct);

                    if (bank != null)
                    {
                        BeneficiaryBank.SelectDropDown(bank);
                    }
                    else
                    {
                        IList<IWebElement> bankList = Properties.Driver.FindElements(By.CssSelector("#Payee_Bank > option"));
                        int bankIndex = bankList.Count();
                        BeneficiaryBank.SelectDropDownByIndex(rand.Next(1, bankIndex));
                    }
                    
                    TransferAmount.EnterText(amount);

                    if (isSaved == true)
                    {
                        Properties.Driver.Wait(SaveBeneDetails);
                        var saveBene = Properties.Driver.FindElement(By.XPath("//*[@id='Save_Payee']"));
                        executor.ExecuteScript("arguments[0].click();", saveBene);

                        Properties.Driver.Wait(BeneficiaryNickName);
                        BeneficiaryNickName.EnterText(nickName);
                    }

                    var bankText = BeneficiaryBank.GetTextFromDDL();

                    if (log != null)
                        logSendMoney(sourceAcct, destinationAcct, "OneTimePayment", "LocalInterBankPayee",
                            firstName, lastName, bankText, amount, null, nickName, isSaved, null, null, log);

                    break;
            }

            Submit(Beneficiary.LocalBank, isRegistered, payMethod, sourceAcct, log);
        }

        public void SendMoney_OutOfTheCountry(string sourceAcct, string destinationAcct, string firstName, string lastName, string amount, ChargingOptions chargingOptions, string nickName = null, bool isSaved = false, bool isRegistered = false, TestLog log = null)
        {
            Random rand = new Random();

            IJavaScriptExecutor executor = (IJavaScriptExecutor)Properties.Driver;

            string bank = null;

            if (isRegistered == true)
            {
                BeneficiaryType.SelectDropDown("RegisteredPayee");
                PaymentOption.SelectDropDown("OutOfCountryPayee");

                Properties.Driver.Wait(RegisteredAccount);
                IList<IWebElement> accountList = Properties.Driver.FindElements(By.CssSelector("#Payee > option"));

                int acctIndex = accountList.Count();

                if (string.IsNullOrEmpty(destinationAcct))
                {
                    RegisteredAccount.SelectDropDownByIndex(rand.Next(1, acctIndex));
                }
                else
                {
                    RegisteredAccount.SelectDropDown(destinationAcct);
                }

                bank = RegisteredAccount.GetAttributeFromDDL("bicid");
            } 
            else
            {
                BeneficiaryType.SelectDropDown("OneTimePayment");
                PaymentOption.SelectDropDown("OutOfCountryPayee");
                AccountNumber.EnterText(destinationAcct);
                //FullName.EnterText(fullName);
                FirstName.EnterText(firstName);
                LastName.EnterText(lastName);

                //Properties.Driver.Wait(By.XPath("//*[@id='Country_List']"));
                Properties.Driver.Wait(CountryList);
                IList<IWebElement> countryList = Properties.Driver.FindElements(By.CssSelector("#Country_List > option"));
                int countryIndex = rand.Next(1, countryList.Count());
                CountryList.SelectDropDownByIndex(countryIndex);

                //Properties.Driver.Wait(By.XPath("//*[@id='Payee_Bank']"));
                Properties.Driver.Wait(BeneficiaryBank);
                IList<IWebElement> bankList = Properties.Driver.FindElements(By.CssSelector("#Payee_Bank > option"));
                int bankIndex = rand.Next(1, bankList.Count());
                BeneficiaryBank.SelectDropDownByIndex(bankIndex);
            }

            //Properties.Driver.Wait(By.XPath("//*[@id='Payee_Currency']"));
            Properties.Driver.Wait(Currency);
            var sourceCurrency = Properties.Driver.FindElement(By.ClassName("account-currency")).Text;
            Currency.SelectDropDown(sourceCurrency);

            TransferAmount.EnterText(amount);

            string[] charges = { "BEN", "SHA", "OUR" };

            switch (chargingOptions)
            {
                case ChargingOptions.BENEFICIARY:
                    ChargingOption.SelectDropDown(charges[0]);
                    break;
                case ChargingOptions.SHARED:
                    ChargingOption.SelectDropDown(charges[1]);
                    break;
                case ChargingOptions.ONUS:
                    ChargingOption.SelectDropDown(charges[2]);
                    break;
            }

            if (isSaved == true)
            {
                Properties.Driver.Wait(SaveBeneDetails);
                var saveBene = Properties.Driver.FindElement(By.XPath("//*[@id='Save_Payee']"));
                executor.ExecuteScript("arguments[0].click();", saveBene);

                Properties.Driver.Wait(BeneficiaryNickName);
                BeneficiaryNickName.EnterText(nickName);
            }

            string chargesText = ChargingOption.GetText();

            if (log != null)
                logSendMoney(sourceAcct, destinationAcct, "OneTimePayment", "LocalInterBankPayee",
                    null, null, bank, amount, null, nickName, isSaved, sourceCurrency, chargesText, log);

            Submit(Beneficiary.OutOfTheCountry, isRegistered, 0, sourceAcct, log);
        }
    }
}
