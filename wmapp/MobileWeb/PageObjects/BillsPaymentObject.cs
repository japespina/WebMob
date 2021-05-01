using Bogus;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SeleniumWebDriver_AutomatedTesting.MobileWeb.PageObjects
{
    class BillsPaymentObject
    {
        public BillsPaymentObject()
        {
            PageFactory.InitElements(Properties.Driver, this);
        }

        [FindsBy(How = How.Id, Using = "CategoryList")]
        public IWebElement BillerCategoryList { get; set; }

        [FindsBy(How = How.Id, Using = "BillerList")]
        public IWebElement BillerList { get; set; }

        [FindsBy(How = How.Id, Using = "")]
        public IWebElement EnrolledBillerList { get; set; }

        [FindsBy(How = How.Id, Using = "BillerNickname")]
        public IWebElement BillerNickName { get; set; }

        [FindsBy(How = How.Id, Using = "Amount")]
        public IWebElement BillAmount { get; set; }

        public IWebElement BackToBillerList { get; set; }

        [FindsBy(How = How.Id, Using = "enrolledBiller")]
        public IWebElement RegisteredPayee { get; set; }

        [FindsBy(How = How.Id, Using = "oneTimePayment")]
        public IWebElement OneTimePayment { get; set; }

        [FindsBy(How = How.ClassName, Using = "billspay")]
        public IWebElement PayBillBtn { get; set; }

        [FindsBy(How = How.Id, Using = "btnConfirm1")]
        public IWebElement ConfirmButton1 { get; set; }

        [FindsBy(How = How.Id, Using = "btnConfirm2")]
        public IWebElement ConfirmButton2 { get; set; }

        [FindsBy(How = How.Id, Using = "btnDone")]
        public IWebElement DoneButton { get; set; }

        [FindsBy(How = How.ClassName, Using = "ewb-popover")]
        public IWebElement PopOver { get; set; }

        [FindsBy(How = How.Id, Using = "Logout_Btn")]
        public IWebElement LogoutBtn { get; set; }

        [FindsBy(How = How.Id, Using = "Menu_Link")]
        public IWebElement MenuLink { get; set; }

        [FindsBy(How = How.ClassName, Using = "managebillers")]
        public IWebElement ManageBillers { get; set; }

        [FindsBy(How = How.Id, Using = "enroll-biller-btn")]
        public IWebElement EnrollBillerBtn { get; set; }

        [FindsBy(How = How.Id, Using = "Category_List")]
        public IWebElement EnrollBillerCategoryList { get; set; }

        [FindsBy(How = How.Id, Using = "Biller_List")]
        public IWebElement EnrollBillerList { get; set; }

        [FindsBy(How = How.Id, Using = "Create_Biller_Btn")]
        public IWebElement CreateBillerBtn { get; set; }

        [FindsBy(How = How.Id, Using = "Confirm_CreateBiller")]
        public IWebElement ConfirmCreateBillerBtn { get; set; }

        [FindsBy(How = How.Id, Using = "Continue_Btn")]
        public IWebElement ContinueButton { get; set; }

        [FindsBy(How = How.Id, Using = "Bills_Pay_Confirm_Btn")]
        public IWebElement ConfirmButton { get; set; }

        [FindsBy(How = How.ClassName, Using = "account-balance")]
        public IWebElement AccountBalance { get; set; }

        [FindsBy(How = How.ClassName, Using = "workBal0")]
        public IWebElement BPAccountBalance { get; set; }

        [FindsBy(How = How.ClassName, Using = "workBal1")]
        public IWebElement BPAccountBalanceDec { get; set; }

        [FindsBy(How = How.ClassName, Using = "btnDone")]
        public IWebElement BtnDone { get; set; }

        [FindsBy(How = How.ClassName, Using = "menu-popover-innercontent")]
        public IWebElement AccountDetails { get; set; }

        [FindsBy(How = How.Id, Using = "Bills_Pay_Continue_Sum_Btn")]
        public IWebElement BillsPayContinue { get; set; }

        [FindsBy(How = How.ClassName, Using = "sub-menu-item")]
        public IWebElement ManageAccount { get; set; }

        [FindsBy(How = How.Id, Using = "Sum_TranID")]
        public IWebElement TransactionId { get; set; }

        [FindsBy(How = How.Id, Using = "modal_close_btn")]
        public IWebElement CloseModalButton { get; set; }
        
        [FindsBy(How = How.Id, Using = "SaveBiller")]
        public IWebElement SaveBiller { get; set; }

        [FindsBy(How = How.Id, Using = "Biller_List_Wrap1")]
        public IWebElement BillerListWrap { get; set; }

        public int CountBillers()
        {
            int result = 0;
            Properties.Driver.Wait(MenuLink);
            MenuLink.Click();
            Properties.Driver.Wait(ManageBillers);
            ManageBillers.Click();
            Properties.Driver.Wait(BillerListWrap);
            try
            {
                result = Properties.Driver.FindElements(By.ClassName("biller_item")).Count();
            }
            catch
            {
                result = 0;
            }
            Properties.Driver.Wait(MenuLink);
            MenuLink.Click();
            Properties.Driver.Wait(LogoutBtn);
            LogoutBtn.Click();
            return result;
        }

        public void PayBill(ref PayBill bp, ref TestLog log)
        {
            try
            {
                Properties.Driver.Manage().Window.Maximize();
                //Properties.Driver.Wait(PopOver);
                //Properties.Driver.Wait(PopOver);
                //PopOver.Click();
                var casas = Properties.Driver.FindElements(By.CssSelector("#Client-Casa-List > .acc-details"));
                int counter = 0;
                if (!string.IsNullOrEmpty(bp.AccountToUse))
                {
                    foreach (var casa in casas)
                    {
                        var cur = casa.FindElement(By.CssSelector(".account-number")).GetAttribute("textContent");
                        if (cur == bp.AccountToUse)
                        {
                            break;
                        }
                        counter = counter + 1;
                    }
                }
           
                var selectedAcc = Properties.Driver.FindElements(By.CssSelector("#Client-Casa-List > .acc-details"))[counter];
                log.AvailableAmountBefore = selectedAcc.FindElement(By.CssSelector(".account-balance")).GetAttribute("textContent");
                log.SourceAccount = selectedAcc.FindElement(By.CssSelector(".account-number")).GetAttribute("textContent");

                var popover = selectedAcc.FindElement(By.ClassName("ewb-account-popover"));
                IJavaScriptExecutor executor = (IJavaScriptExecutor)Properties.Driver;
                executor.ExecuteScript("arguments[0].click();", popover);

                Properties.Driver.WaitTillExist(By.ClassName("popover-content"));
                var link = Properties.Driver.FindElement(By.ClassName("menu-popover-innercontent-label"));
                link.Click();


                Properties.Driver.Wait(MenuLink);
                MenuLink.Click();
                Properties.Driver.Wait(PayBillBtn);
                PayBillBtn.Click();


                if (bp.BillType == PayBillType.OneTimePayment)
                {
                    Properties.Driver.Wait(OneTimePayment);
                    OneTimePayment.Click();
                    RemoveOTP(executor);
                    executor.ExecuteScript("document.getElementById('OneTimeWrap').classList.remove('hide')");
                    //Properties.Driver.Wait(BPAccountBalance);
                    //log.AvailableAmountBefore = BPAccountBalance.GetAttribute("textContent") + "." + BPAccountBalanceDec.GetAttribute("textContent");
                    Properties.Driver.Wait(BillerCategoryList);
                    BillerCategoryList.SelectDropDownText(bp.Category);
                    BillerList.SelectDropDownText(bp.Biller);
                    BillAmount.EnterText(bp.Amount);
                    Properties.Driver.Wait(Properties.Driver.FindElement(By.Id("autoGenFields")));
                    List<IWebElement> parameterInputs = new List<IWebElement>();
                    parameterInputs = Properties.Driver.FindElement(By.Id("autoGenFields")).FindElements(By.XPath(".//input[@type='text']")).ToList();//.FindElements(By.XPath(" //input[@type='text']")).ToList();
                    int selector = 0;
                    executor.ExecuteScript("scroll(0, 250)");
                    bp.Parameter.ForEach(p =>
                    {
                        parameterInputs[selector].EnterText(p.Value);
                        Thread.Sleep(500);
                        executor.ExecuteScript("scroll(0, 250)");
                        selector++;
                    });
                    Thread.Sleep(500);
                    executor.ExecuteScript("scroll(0, 250)");
                    Properties.Driver.Wait(SaveBiller);
                    SaveBiller.Click();
                    executor.ExecuteScript("scroll(0, 250)");
                   // Properties.Driver.Wait(BillerNickName);
                    Faker faker = new Faker();

                    Properties.Driver.FindElement(By.Id("BillerNickName")).EnterText(faker.Finance.AccountName());
                    executor.ExecuteScript("scroll(0, 400)");
                    Properties.Driver.Wait(ConfirmButton1);
                    ConfirmButton1.Click();
                    Thread.Sleep(1000);

                    Properties.Driver.Wait(ConfirmButton2);
                    ConfirmButton2.Click();
                    Properties.Driver.Wait(DoneButton);
                    DoneButton.Click();
                }
                else
                {
                    Properties.Driver.Wait(RegisteredPayee);
                    RegisteredPayee.Click();

                    var billers = Properties.Driver.FindElements(By.ClassName("biller_item"));

                    var selectedBiller = billers[bp.EnrolledEntryNo];
                    bp.Biller = selectedBiller.FindElement(By.ClassName("biller-name")).Text;
                    
                    executor.ExecuteScript("arguments[0].scrollIntoView()", selectedBiller);
                    try
                    {
                        Thread.Sleep(500);
                        selectedBiller.Click();
                    }
                    catch
                    {
                        executor.ExecuteScript(string.Format("scroll(0, {0})", selectedBiller.Location.Y - 50));
                        Thread.Sleep(500);
                        selectedBiller.Click();
                    }
                    
                    
                    
                    RemoveOTP(executor);
                    executor.ExecuteScript("document.getElementById('Bills_Pay_Wrap').classList.remove('hide')");                 

                    BillAmount.EnterText(bp.Amount);
                    executor.ExecuteScript("scroll(0, 250)");
                    Properties.Driver.Wait(ContinueButton);
                    ContinueButton.Click();
                    Properties.Driver.Wait(ConfirmButton);
                    ConfirmButton.Click();
                    Properties.Driver.Wait(TransactionId);
                    executor.ExecuteScript("scroll(0, 250)");
                    Properties.Driver.Wait(BillsPayContinue);
                    BillsPayContinue.Click();
                    Properties.Driver.Wait(MenuLink);
                    MenuLink.Click();
                    Properties.Driver.Wait(ManageAccount);
                    ManageAccount.Click();

                }

                log.AvailableAmountAfter = Properties.Driver.FindElements(By.CssSelector("#Client-Casa-List > .acc-details"))[counter].FindElement(By.CssSelector(".account-balance")).GetAttribute("textContent");
                log.Passed = true;
            }
            catch (Exception ex)
            {
                
                log.ErrorLog = ex.Message;
                log.Passed = false;
                log.ParameterUsed = Properties.ExtractParameters(bp);
            }


            var modal = Properties.Driver.FindElement(By.Id("Generic_Modal")).GetAttribute("class");
            if (modal == "modal fade in")
            {
                var closeOTP = Properties.Driver.FindElement(By.CssSelector("#Generic_Modal > div > div > div > #modal_close_btn"));
                closeOTP.Click();
                //var exception = new Exception();
                //throw new Exception();
            }
            
            Properties.Driver.Wait(MenuLink);
            MenuLink.Click();

            Properties.Driver.Wait(LogoutBtn);
            LogoutBtn.Click();
            Thread.Sleep(2000);

        }

        public void EnrollBiller(ref PayBill payBill, ref TestLog log)
        {
            try
            {
                Faker faker = new Faker();
                IJavaScriptExecutor executor = (IJavaScriptExecutor)Properties.Driver;
                Properties.Driver.Manage().Window.Maximize();
                Properties.Driver.Wait(MenuLink);
                MenuLink.Click();
                Properties.Driver.Wait(ManageBillers);
                ManageBillers.Click();
                Properties.Driver.Wait(EnrollBillerBtn);
                EnrollBillerBtn.Click();
                RemoveOTP(executor);
                executor.ExecuteScript("document.getElementById('Create_Biller_Wrap').classList.remove('hide')");
                Properties.Driver.Wait(EnrollBillerCategoryList);
                payBill.Category = EnrollBillerCategoryList.DropDownRandomPick();
                EnrollBillerCategoryList.SelectDropDownText(payBill.Category);
                Properties.Driver.Wait(EnrollBillerList);
                payBill.Biller = EnrollBillerList.DropDownRandomPick();
                EnrollBillerList.SelectDropDownText(payBill.Biller);
                Properties.Driver.Wait(BillerNickName);
                BillerNickName.EnterText(faker.Name.FirstName(null));
                Thread.Sleep(3000);
                payBill.Parameter = EnrollParameters();
                Properties.Driver.Wait(CreateBillerBtn);
                CreateBillerBtn.Click();
                Properties.Driver.Wait(ConfirmCreateBillerBtn);
                ConfirmCreateBillerBtn.Click();
                Properties.Driver.Wait(MenuLink);
                MenuLink.Click();
                Properties.Driver.Wait(LogoutBtn);
                LogoutBtn.Click();
                Properties.Driver.Quit();
                log.Passed = true; 
            }
            catch (Exception ex)
            {
                log.ErrorLog = ex.Message;
                log.Passed = false;
            }
        }

        private static void RemoveOTP(IJavaScriptExecutor executor)
        {
            Properties.Driver.Wait(By.Id("Generic_Modal"));
            var closeOTP = Properties.Driver.FindElement(By.CssSelector("#Generic_Modal > div > div > div > #modal_close_btn"));
            closeOTP.Click();
            Properties.Driver.Wait(By.Id("OTP_Wrap"));
            executor.ExecuteScript("document.getElementById('OTP_Wrap').classList.add('hide')");
        }

        private List<BillingParameter> EnrollParameters()
        {
            Thread.Sleep(5000);
            List<BillingParameter> parameter = new List<BillingParameter>();
            List<IWebElement> allFormChildElements = Properties.Driver.FindElements(By.ClassName("bills-pay-param")).ToList();
            var filterdFormChildElements = allFormChildElements.Where(p => p.GetAttribute("type") == "text");
            var counter = 0;
            foreach (var item in filterdFormChildElements)
            {
                counter++;
                var maxLength = item.GetAttribute("maxlength") != null ? int.Parse(item.GetAttribute("maxlength")) : 20;
                Faker faker = new Faker();
                var randomText = faker.Finance.Account(maxLength);
                item.EnterText(randomText);
                parameter.Add(new BillingParameter() { Id = counter, Value = randomText });

                Thread.Sleep(2000);
            }

            return parameter;
        }
    }
}
