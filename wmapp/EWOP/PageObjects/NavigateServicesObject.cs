
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using Xunit;

namespace SeleniumWebDriver_AutomatedTesting.EWOP.PageObjects
{
    class NavigateServicesObject
    {
        public NavigateServicesObject()
        {
            PageFactory.InitElements(Properties.Driver, this);
        }


        [FindsBy(How = How.Id, Using = "BUT_4BC9E0FCA3A053F642535")]
        public IWebElement MobileUpdatedYesButton { get; set; }

        [FindsBy(How = How.Id, Using = "BUT_AE08013FF1733FC618763")]
        public IWebElement AccountsLink { get; set; }

        //[FindsBy(How = How.XPath, Using = "//*[@title='Savings Accounts']//ancestor::div[6]//*[@title='Make a Transfer']")] // "//*[@title='Make a Transfer']")] //"C1__BUT_B98A13A114FA087C32585")]
        //public IWebElement MakeATransferLink { get; set; }

        //[FindsBy(How = How.XPath, Using = "//*[@title='Pay a Bill']")]//[FindsBy(How = How.Id, Using = "C1__BUT_35CB2335D3FCB5E2457261")]
        //public IWebElement PayABillLink { get; set; }

        //[FindsBy(How = How.XPath, Using = "//*[@title='Card Settings']")]       //[FindsBy(How = How.Id, Using = "C1__BUT_921D9889ADE9B46183824")] //
        //public IWebElement CardSettingsLink { get; set; }

        [FindsBy(How = How.Id, Using = "QUE_BC8FE42FEEC2EAAC21745")]
        public IWebElement OTPText { get; set; }


        [FindsBy(How = How.Id, Using = "BUT_07CE10AC7E957F9157355")]
        public IWebElement OTPConfirmButton { get; set; }


        [FindsBy(How = How.Id, Using = "p1_HEAD_07CE10AC7E957F9157353")]
        public IWebElement OTPErrorMessage { get; set; }

        [FindsBy(How = How.LinkText, Using = "Manage Billers")]
        public IWebElement ManageBillers { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_6C78D72D417D9876145706")]
        public IWebElement EnrollBiller { get; set; }


        [FindsBy(How = How.Id, Using = "BUT_33C50753EA429333132183")]
        public IWebElement AccountSummaryLink { get; set; }

        [FindsBy(How = How.Id, Using = "BUT_67E3C0380200D3B3121043")]
        public IWebElement LogoutLink { get; set; }

        [FindsBy(How = How.Id, Using = "LOGOUT_INTER")]
        public IWebElement LogoutYesButton { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_E20F18C91F7739741063983")]
        public IWebElement EnrollBillerOTP { get; set; }

        [FindsBy(How = How.Id, Using = "C1__p4_BUT_E20F18C91F7739741063985")]
        public IWebElement EnrollBillerOTPConfirm { get; set; }

        [FindsBy(How = How.XPath , Using = "//a[@id='BUT_7CFF8FBC6E55B5781176951']/span")]
        public IWebElement ApplyforaProduct { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@id='BUT_7CFF8FBC6E55B5781177036']/span")]
        public IWebElement ApplyNewTimeDeposit { get; set; }

        [FindsBy(How = How.Id, Using = "BUT_07CE10AC7E957F9157356")]
        public IWebElement OTPCancel_NewTimeDeposit { get; set; }

        [FindsBy(How = How.Id, Using = "BUT_07CE10AC7E957F9157355")]
        public IWebElement OTPConfirm_NewTimeDeposit { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_034026BCDF2409E192141")]
        public IWebElement Product_NewTD_DropDown_Currency { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_E902A72764B5301F109034")]
        public IWebElement Product_NewTD_DropDown_SourceOfFunds { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_BBE76E0C7BA327CB67550")]
        public IWebElement Product_NewTD_DropDown_Terms { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_BBE76E0C7BA327CB67548")]
        public IWebElement Product_NewTD_Amount { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_DECE623C6873ABBE757829")]
        public IWebElement Product_NewTD_MinAmount { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_AF7FB8279452BDD969410")]
        public IWebElement Product_NewTD_InterestRate { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@id='p4_BUT_4BC9E0FCA3A053F642535']/div")]
        public IWebElement Message_ConfirmMobile { get; set; }

        [FindsBy(How = How.Id, Using = "BUT_9D19C4CD5CC65D46395554")]
        public IWebElement Message_NoButton { get; set; }

        [FindsBy(How = How.Id, Using = "BUT_4BC9E0FCA3A053F642535")]
        public IWebElement Message_YesButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='SPC_59FD015C46F8F31B54280']/div")]
        public IWebElement CreditCardOnly_DialogboxMessage { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='p1_HEAD_59FD015C46F8F31B132224']/div")]
        public IWebElement CreditCardOnly_Message { get; set; }

        [FindsBy(How = How.Id, Using = "BUT_59FD015C46F8F31B54282")]
        public IWebElement CreditCardOnly_Confirmbutton { get; set; }


        public enum AccountServices
        {
            MakeATransfer,
            PayABill,
            CardSettings,
            InquireBalance
        }

        public enum BillsPaymentService
        {
            ManageBillers,
            EnrollBiller
        }

        public enum ProductServices
        {
            NewTimeDeposit
        }

        public string BeforeTransactionAmount { get; set; }
        public string AfterTransactionAmount { get; set; }
        public string SourceAccount { get; set; }
        public void Navigate(AccountServices service, string accountNumber)
        {
            try
            {
                if (MobileUpdatedYesButton != null)
                    MobileUpdatedYesButton.Click();
            }
            catch
            { }

            try
            {
                if (AccountsLink.GetAttribute("title").ToLower().Trim() == "view  accounts")
                {
                    AccountsLink.Click();
                    
                }
            }
            catch { }
            if (!string.IsNullOrEmpty(accountNumber))
            {
                if (service == AccountServices.MakeATransfer)
                {
                    BeforeTransactionAmount = ExtractAvailableAmount(accountNumber);
                    SourceAccount = accountNumber;
                    IWebElement MakeATransferLink = Properties.Driver.FindElement(By.XPath("//span[text()='" + accountNumber + "']//ancestor::div[8]//*[@title='Make a Transfer']"));
                    MakeATransferLink.Click();
                }
                else if (service == AccountServices.PayABill)
                {
                    BeforeTransactionAmount = ExtractAvailableAmount(accountNumber);
                    SourceAccount = accountNumber;
                    //Thread.Sleep(5000);
                    IWebElement PayABillLink = Properties.Driver.FindElement(By.XPath("//span[text()='" + accountNumber + "']//ancestor::div[8]//*[@title='Pay a Bill']"));

                    Properties.Driver.Wait(PayABillLink);
                    PayABillLink.Click();
                }
                else if (service == AccountServices.CardSettings)
                {
                    BeforeTransactionAmount = ExtractAvailableAmount(accountNumber);
                    SourceAccount = accountNumber;
                    IWebElement CardSettingsLink = Properties.Driver.FindElement(By.XPath("//span[text()='" + accountNumber + "']//ancestor::div[8]//*[@title='Card Settings']"));
                    CardSettingsLink.Click();
                }
                else if (service == AccountServices.InquireBalance)
                {
                    AfterTransactionAmount = ExtractAvailableAmount(accountNumber);
                }
            }
            else
            {
                if (service == AccountServices.MakeATransfer)
                {
                    AccountRandomPick("Make a Transfer");
                }
                else if (service == AccountServices.PayABill)
                {
                    AccountRandomPick("Pay a Bill");
                }
                else if (service == AccountServices.CardSettings)
                {
                    AccountRandomPick("Card Settings");
                }
            }

            try
            {

                Thread.Sleep(2000);
                if (OTPText != null && OTPConfirmButton != null)
                {
                    Properties.Driver.Wait(OTPConfirmButton);
                    OTPText.SendKeys("123456");
                    Thread.Sleep(1000);
                    OTPConfirmButton.Click();
                    OTPConfirmButton = null;
                }
            }
            catch { }
        }

        public bool validateNavigate(AccountServices service, string accountNumber)
        {
            bool Result = false;
            try
            {
                if (MobileUpdatedYesButton != null)
                {
                    Result = true;
                    MobileUpdatedYesButton.Click();
                }
            }
            catch
            { }

            try
            {
                if (AccountsLink.GetAttribute("title").ToLower().Trim() == "view  accounts")
                {
                    Result = true;
                    AccountsLink.Click();
                }
            }
            catch { }
            if (!string.IsNullOrEmpty(accountNumber))
            {
                if (service == AccountServices.MakeATransfer)
                {
                    BeforeTransactionAmount = ExtractAvailableAmount(accountNumber);
                    if (BeforeTransactionAmount == string.Empty)
                    {
                        Result = false;
                        goto SkipOTPMessage;
                        //Logout();
                    }
                    else
                    {
                        SourceAccount = accountNumber;
                        IWebElement MakeATransferLink = Properties.Driver.FindElement(By.XPath("//span[text()='" + accountNumber + "']//ancestor::div[8]//*[@title='Make a Transfer']"));
                        Result = true;
                        MakeATransferLink.Click();
                    }
                }
                else if (service == AccountServices.PayABill)
                {
                    BeforeTransactionAmount = ExtractAvailableAmount(accountNumber);
                    if (BeforeTransactionAmount == string.Empty)
                    {
                        Result = false;
                        Logout();
                    }
                    else
                    {
                        SourceAccount = accountNumber;
                        //Thread.Sleep(5000);
                        IWebElement PayABillLink = Properties.Driver.FindElement(By.XPath("//span[text()='" + accountNumber + "']//ancestor::div[8]//*[@title='Pay a Bill']"));

                        Properties.Driver.Wait(PayABillLink);
                        Result = true;
                        PayABillLink.Click();
                    }

                }
                else if (service == AccountServices.CardSettings)
                {
                    BeforeTransactionAmount = ExtractAvailableAmount(accountNumber);
                    SourceAccount = accountNumber;
                    IWebElement CardSettingsLink = Properties.Driver.FindElement(By.XPath("//span[text()='" + accountNumber + "']//ancestor::div[8]//*[@title='Card Settings']"));
                    Result = true;
                    CardSettingsLink.Click();
                }
                else if (service == AccountServices.InquireBalance)
                {
                    AfterTransactionAmount = ExtractAvailableAmount(accountNumber);
                    if (AfterTransactionAmount == string.Empty)
                    {
                        Result = false;
                    }
                    else
                    { 
                        Result = true;
                    }
                }
            }
            else
            {
                if (service == AccountServices.MakeATransfer)
                {
                    AccountRandomPick("Make a Transfer");
                }
                else if (service == AccountServices.PayABill)
                {
                    AccountRandomPick("Pay a Bill");
                }
                else if (service == AccountServices.CardSettings)
                {
                    AccountRandomPick("Card Settings");
                }
            }

            try
            {

                Thread.Sleep(2000);
                if (OTPText != null && OTPConfirmButton != null)
                {
                    Properties.Driver.Wait(OTPConfirmButton);
                    OTPText.SendKeys("123456");
                    Thread.Sleep(1000);
                    OTPConfirmButton.Click();
                    OTPConfirmButton = null;
                }
            }
            catch { }

            SkipOTPMessage:
            return Result;

        }
        private void AccountRandomPick(string navi)
        {
            var casas = Properties.Driver.FindElements(By.ClassName("account-sublist-block"));
            IList<IWebElement> qualified = new List<IWebElement>();
            foreach (var casa in casas)
            {
                var availableAmountElement = casa.FindElement(By.XPath(".//span[@title='Available balance']"));//.GetAttribute("textContent");
                var currencyElement = availableAmountElement.FindElement(By.XPath("preceding-sibling::span[1]"));
                var availableAmountVal = availableAmountElement.GetAttribute("textContent");
                var currencyVal = currencyElement.GetAttribute("textContent");
                var acc = casa.FindElement(By.XPath(".//span[@title='Account Number']")).GetAttribute("textContent");

                if (Convert.ToDouble(availableAmountVal) >= 1000 && currencyVal == "PHP")
                {
                    BeforeTransactionAmount = availableAmountVal;
                    SourceAccount = acc;
                    qualified.Add(casa.FindElement(By.XPath("//*[@title='" + navi + "']")));

                }
            }
            Random rand = new Random();
            var randomPick = qualified[rand.Next(0, qualified.Count() - 1)];
            randomPick.Click();
        }

        private string ExtractAvailableAmount(string accountNumber)
        {
            var result = string.Empty;
            var casas = Properties.Driver.FindElements(By.ClassName("account-sublist-block"));
            IList<IWebElement> qualified = new List<IWebElement>();
            foreach (var casa in casas)
            {
                var acc = casa.FindElement(By.XPath(".//span[@title='Account Number']")).GetAttribute("textContent");
                if (acc == accountNumber)
                {
                    result = casa.FindElement(By.XPath(".//span[@title='Available balance']")).GetAttribute("textContent");
                    break;
                }

            }
            return result;
        }


        public void Navigate(BillsPaymentService service)
        {

            if (service == BillsPaymentService.ManageBillers)
            {
                //if (MobileUpdatedYesButton != null)
                    //MobileUpdatedYesButton.Click();
                ManageBillers.Click();
            }

            if (service == BillsPaymentService.EnrollBiller)
            {

                EnrollBiller.Click();
                Thread.Sleep(2000);
                Properties.Driver.Wait(EnrollBillerOTP);
                if (EnrollBillerOTPConfirm != null && EnrollBillerOTP != null)
                {
                    EnrollBillerOTP.SendKeys("123456");
                    EnrollBillerOTPConfirm.Click();
                }
                Thread.Sleep(2000);

            }
        }

        public void WaitUntilObjectLoads(string xpath, IWebDriver webDriver)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webDriver.FindElement(By.XPath("//*[@title='Manage Beneficieries']"))));
        }

        public void BackToAccountSummary()
        {
            try
            {
                if (MobileUpdatedYesButton != null)
                MobileUpdatedYesButton = null;
            }
            catch
            { }

            try
            {
                if (AccountSummaryLink != null)
                {
                    Properties.Driver.Wait(AccountSummaryLink);
                    AccountSummaryLink.Click();
                }
            }
            catch
            { }
        }

        public void Logout()
        {
            try
            {
                BackToAccountSummary();
                LogoutLink.Click();
                Properties.Driver.Wait(LogoutYesButton);
                LogoutYesButton.Click();
            }
            catch { }
        }

        public void Navigate(ProductServices service, string strMobileConfirmResponse, string strOTPResponse)
        {
            bool boolMobileConfirm = false;
            try
            {
                if (Message_ConfirmMobile != null && Message_NoButton != null && Message_YesButton != null)
                {
                    switch (strMobileConfirmResponse)
                    {
                        case "No":
                            boolMobileConfirm = true;
                            Properties.Driver.Wait(Message_NoButton);
                            Message_NoButton.Click();
                            goto MobileConfirmNo;
                            //break;

                        case "Yes":
                            boolMobileConfirm = true;
                            Properties.Driver.Wait(Message_YesButton);
                            Message_YesButton.Click();
                            goto MobileConfirmNo;
                        //break;

                        default:
                            break;
                    }
                }
            }
            catch{ }

            try
            {
                
                if(ApplyforaProduct != null)
                {
                    Properties.Driver.Wait(ApplyforaProduct);
                    ApplyforaProduct.Click();
                    Properties.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6000);
                }
            }
            catch { }

            try
            {
                if(ApplyNewTimeDeposit != null)
                {
                    Properties.Driver.Wait(ApplyNewTimeDeposit);
                    ApplyNewTimeDeposit.Click();
                    Properties.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6000);
                }
            }
            catch { }

            try
            {
                if (CreditCardOnly_DialogboxMessage != null && CreditCardOnly_Message != null && CreditCardOnly_Confirmbutton != null && strMobileConfirmResponse != "")
                {
                    Properties.Driver.Wait(CreditCardOnly_Confirmbutton);
                    CreditCardOnly_Confirmbutton.Click();
                    goto MobileConfirmNo;
                }

            }
            catch { }

            if (strOTPResponse == "confirm")
            {
                try
                {
                    Properties.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4000);
                    if (OTPText != null && OTPConfirm_NewTimeDeposit != null)
                    {
                        Properties.Driver.Wait(OTPConfirm_NewTimeDeposit);
                        OTPText.SendKeys("123456");
                        Properties.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4000);
                        OTPConfirm_NewTimeDeposit.Click();
                        Thread.Sleep(4000);
                        OTPConfirm_NewTimeDeposit = null;
                    }
                }
                catch { }

            }
            else if(strOTPResponse == "cancel")
            {
                try
                {

                    Thread.Sleep(2000);
                    if (OTPText != null && OTPCancel_NewTimeDeposit != null)
                    {
                        Properties.Driver.Wait(OTPCancel_NewTimeDeposit);
                        //OTPText.SendKeys("123456");
                        Thread.Sleep(1000);
                        OTPCancel_NewTimeDeposit.Click();
                        OTPCancel_NewTimeDeposit = null;
                    }
                }
                catch { }
            }

            MobileConfirmNo:
                if (boolMobileConfirm == true && strMobileConfirmResponse == "No" || strMobileConfirmResponse == "Yes")
                {
                    string strCreditCardOnly = "Credit Card only";
                }

        }

    }

}
