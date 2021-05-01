using Bogus;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace SeleniumWebDriver_AutomatedTesting.EWOP.PageObjects
{
    class BillsPaymentObject
    {

        public BillsPaymentObject()
        {
            PageFactory.InitElements(Properties.Driver, this);
        }



        [FindsBy(How = How.Id, Using = "C1__QUE_4FB647B57295CBCF551780")]
        public IWebElement BillerCategoryList { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_4FB647B57295CBCF551824")]
        public IWebElement BillerList { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_6C78D72D417D9876183802")]
        public IWebElement EnrolledBillerList { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_4FB647B57295CBCF552010")]
        public IWebElement BillerNickName { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_A3701E3CE0D5D80437467")]
        public IWebElement BillAmount { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_6C78D72D417D9876199932")]
        public IWebElement Continue { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_AE9EEE38A503124C726764")]
        public IWebElement Confirm { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_6C78D72D417D9876617461")]
        public IWebElement BackToBillerList { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_AC4FFB05B556000075228_0")]
        public IWebElement RegisteredPayee { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_AC4FFB05B556000075228_1")]
        public IWebElement OneTimePayment { get; set; }

        [FindsBy(How = How.Id, Using = "C1__p4_BUT_4FB647B57295CBCF551950")]
        public IWebElement EnrollmentContinue { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_F9681C8D446CB77B231344")]
        public IWebElement EnrollmentConfirm { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_4FB647B57295CBCF561895")]
        public IWebElement EnrollmentBackToBillerList { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_2F6EAD915133DE6450711_0")]
        public IWebElement SaveBiller { get; set; }
        
        
        [FindsBy(How = How.Id, Using = "C1__p1_TBL_42843BBE7F710E20345753")]
        public IWebElement BillerTable { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//td[@headers='C1__p1_QUE_6C78D72D417D9876199921']")]
        public IWebElement ReadOnlyParam { get; set; }
            /*
        public void EnrollBiller(string categoryval, string listvalue, string nickname, List<BillingParameter> parameters)
        {
            Properties.Driver.Wait(BillerCategoryList);
            BillerCategoryList.SelectDropDownText(categoryval);
            BillerList.SelectDropDown(listvalue);
            BillerNickName.EnterText(nickname);
            parameters.ForEach(bp =>
            {
                SetParameterValue(bp);
            });
            Properties.Driver.Wait(Continue);
            Continue.Click();
            Properties.Driver.Wait(Confirm);
            Confirm.Click();
            Properties.Driver.Wait(BackToBillerList);
            BackToBillerList.Click();

        }
        
        /// <summary>
        /// For one time payment transaction.
        /// </summary>
        /// <param name="categoryval"></param>
        /// <param name="listvalue"></param>
        /// <param name="amount"></param>
        /// <param name="parameters"></param>
        public void PayBill(string categoryval, string listvalue, string amount, List<BillingParameter> parameters)
        {
            Properties.Driver.Wait(OneTimePayment);
            OneTimePayment.Click();
            Properties.Driver.Wait(BillerCategoryList);
            BillerCategoryList.SelectDropDown(categoryval);
            Properties.Driver.Wait(BillerList);
            BillerList.SelectDropDown(listvalue);
            parameters.ForEach(bp =>
            {
                SetParameterValue(bp);
            });
            BillAmount.EnterText(amount);
            Properties.Driver.Wait(Continue);
            Continue.Click();
            Properties.Driver.Wait(Confirm);
            Confirm.Click();
        }

        /// <summary>
        /// For registered payee payment transaction.
        /// </summary>
        /// <param name="categoryval"></param>
        /// <param name="listvalue"></param>
        /// <param name="payee"></param>
        /// <param name="amount"></param>
        /// <param name="parameters"></param>
        public void PayBill(string categoryval, string listvalue, string payee, string amount, List<BillingParameter> parameters)
        {
            Properties.Driver.Wait(RegisteredPayee);
            RegisteredPayee.Click();
            Properties.Driver.Wait(EnrolledBillerList);
            EnrolledBillerList.SelectDropDown(payee);
            Properties.Driver.Wait(BillerCategoryList);
            BillerCategoryList.SelectDropDown(categoryval);
            Properties.Driver.Wait(BillerList);
            BillerList.SelectDropDown(listvalue);
            parameters.ForEach(bp =>
            {
                SetParameterValue(bp);
            });
            BillAmount.EnterText(amount);
            Properties.Driver.Wait(Continue);
            Continue.Click();
            Properties.Driver.Wait(Confirm);
            Confirm.Click();

        }
        */
        public void PayBill(ref PayBill payBill, ref TestLog testLog)
        {
            try
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)Properties.Driver;
                if (payBill.BillType == PayBillType.OneTimePayment)
                {
                    Properties.Driver.Wait(OneTimePayment);
                    OneTimePayment.Click();
                    Properties.Driver.Wait(BillerCategoryList);
                    BillerCategoryList.SelectDropDownText(payBill.Category);
                    executor.ExecuteScript("scroll(0,250)");
                    Thread.Sleep(3000);
                    Properties.Driver.Wait(BillerList);
                    BillerList.SelectDropDownText(payBill.Biller);
                    executor.ExecuteScript("scroll(0,250)");
                    Thread.Sleep(3000);
                    var isTelephone = payBill.Category == "Telephone / Pager / Cellphone" ? true : false;
                    payBill.Parameter.ForEach(bp =>
                    {
                        SetParameterValue(bp,  isTelephone);
                        Thread.Sleep(1000);
                        executor.ExecuteScript("scroll(0,250)");
                    });
                    BillAmount.EnterText(payBill.Amount);
                    executor.ExecuteScript("scroll(0,250)");
                    Properties.Driver.Wait(SaveBiller);
                    SaveBiller.Click();
                    executor.ExecuteScript("scroll(0,250)");
                    Properties.Driver.Wait(BillerNickName);
                    Faker faker = new Faker();
                    BillerNickName.EnterText(faker.Finance.AccountName());
                    Properties.Driver.Wait(Continue);
                    Continue.Click();
                    Properties.Driver.Wait(Confirm);
                    Confirm.Click();
                    Properties.Driver.Wait(BackToBillerList);
                    BackToBillerList.Click();
                    
                    testLog.Passed = true;
                }
                else
                {
                    Properties.Driver.Wait(RegisteredPayee);
                    RegisteredPayee.Click();
                    Properties.Driver.Wait(EnrolledBillerList);
                    executor.ExecuteScript("scroll(0,250)");
                    EnrolledBillerList.SelectDropDownByIndex(payBill.EnrolledEntryNo+1);
                    
                    
                    Thread.Sleep(1000);
                    //var selected = new SelectElement(EnrolledBillerList).Options[payBill.EnrolledEntryNo];
                    //selected.Click();
                    //payBill.Biller = selected.Text();
                    //payBill.Biller = EnrolledBillerList.DropDownRandomPick();
                    //EnrolledBillerList.SelectDropDownText(payBill.Biller);
                    //Properties.Driver.Wait(ReadOnlyParam);
                    Thread.Sleep(1000);

                    executor.ExecuteScript("scroll(0,300)");
                    
                    WebDriverWait wait = new WebDriverWait(Properties.Driver, new TimeSpan(0,1,0));
                    wait.Until(ExpectedConditions.ElementToBeClickable(BillAmount));
                    Properties.Driver.Wait(BillAmount);
                    
                    BillAmount.EnterText(payBill.Amount);
                    Thread.Sleep(1000);
                    executor.ExecuteScript("scroll(0,1000)");
                    payBill.Biller = new SelectElement(EnrolledBillerList).SelectedOption.Text;
                    Properties.Driver.Wait(Continue);
                    Continue.Click();
                    Properties.Driver.Wait(Confirm);
                    Confirm.Click();
                    Properties.Driver.Wait(BackToBillerList);
                    BackToBillerList.Click();
                    testLog.Passed = true;
                }
            }
            catch (Exception ex)
            {
                if (IsElementPresent(By.Id("C1__BUT_AE9EEE38A503124C726759")))
                {
                    Properties.Driver.FindElement(By.Id("C1__BUT_AE9EEE38A503124C726759")).Click();
                }

                testLog.ErrorLog = ex.Message;
                testLog.Passed = false;
                testLog.ParameterUsed = Properties.ExtractParameters(payBill);
            }
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                Properties.Driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private void SetParameterValue(BillingParameter bpParam, bool isTelephone)
        {
            
            By Id =isTelephone ? By.Id(string.Format("C1__QUE_2848D327779F8B1C36667_R{0}", bpParam.Id)) : By.Id(string.Format("C1__QUE_2848D327779F8B1C36668_R{0}", bpParam.Id));
            Properties.Driver.FindElement(Id).EnterText(bpParam.Value);
            Thread.Sleep(2000);
        }

        private List<BillingParameter> EnrollParameters()
        {
            Thread.Sleep(5000);
            List<BillingParameter> parameter = new List<BillingParameter>();

            // IWebElement listElement = Properties.Driver.FindElement(By.Id("C1__TBL_2848D327779F8B1C36665")); C1__QUE_2848D327779F8B1C36668_R1
            List<IWebElement> allFormChildElements = Properties.Driver.FindElements(By.XPath("//input[contains(@id, 'C1__QUE_2848D327779F8B1C36668')]")).ToList();
            
            var filterdFormChildElements = allFormChildElements.Where(p => p.GetAttribute("type") == "text");
            
            var counter = 0;
            foreach(var item in filterdFormChildElements)
            {
                counter++;
                var maxLength = item.GetAttribute("maxlength") != null ? int.Parse(item.GetAttribute("maxlength")) : 100;
                Faker faker = new Faker();
                var randomText = faker.Finance.Account(maxLength);
                item.EnterText(randomText);
                parameter.Add(new BillingParameter() { Id = counter, Value = randomText });

                Thread.Sleep(2000);
            }

            return parameter; 
        }

        public void EnrollBiller(ref PayBill payBill, ref TestLog log)
        {
            try
            {
                Properties.Driver.Wait(BillerCategoryList);
                Random rand = new Random();

                payBill.Category = BillerCategoryList.DropDownRandomPick();
                BillerCategoryList.SelectDropDownText(payBill.Category);
                Thread.Sleep(3000);
                Properties.Driver.Wait(BillerList);
                var billers = new SelectElement(BillerList).Options.Select(p => p.Text).ToList();
                payBill.Biller = BillerList.DropDownRandomPick();
                BillerList.SelectDropDownText(payBill.Biller);
                Thread.Sleep(3000);
                Faker faker = new Faker();
                payBill.NickName = faker.Name.FirstName(null);
                Properties.Driver.Wait(BillerNickName);
                BillerNickName.EnterText(payBill.NickName);
                payBill.Parameter = EnrollParameters();
                Properties.Driver.Wait(EnrollmentContinue);
                EnrollmentContinue.Click();
                Properties.Driver.Wait(EnrollmentConfirm);
                EnrollmentConfirm.Click();
                Properties.Driver.Wait(EnrollmentBackToBillerList);
                EnrollmentBackToBillerList.Click();
                log.Passed = true;
            }
            catch(Exception ex)
            {
                log.Passed = false;
                log.ErrorLog = ex.Message;
            }

        }

        public int CountBillers()
        {
            int result = 0;
            Properties.Driver.Wait(RegisteredPayee);
            RegisteredPayee.Click();
            Properties.Driver.Wait(EnrolledBillerList);

            result = new SelectElement(EnrolledBillerList).Options.Count() - 1;

            return result;
        }
    }
            
}
