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
    public class Products : MobileWeb 
    {
        private void TimeDeposit(string strTestCase, TestLog log = null)
        {
            bool boolLogResult = false;
            try
            {
                Login();
                if(log != null)
                { 
                    log.UserIdUsed = Properties.Username;
                }

                /*
                var appConfig = Properties.Configuration
                    .GetSection("Products:TimeDeposit:TestCases").GetChildren().ToList()
                    .Select( x => (
                        x.GetValue<string>("TestCaseID")    
                                  )
                           ).ToList<(string testcaseid)>();
                */

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
                            string strterm = testdatalist.Terms;
                            string strdepositamount = testdatalist.DepositAmount;
                            string strmobileconfirm = testdatalist.MobileConfirm;
                            string strotpresponse = testdatalist.OTPResponse;
                            string strsubmitbutton = testdatalist.SubmitButton;
                            string strexpectederrmsg = testdatalist.ExpectedMessage;
                            string strselectmenu = "ApplyNewTimeDeposit";

                            //log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                            //log.SourceAccount = navi.SourceAccount;

                            //navi.Navigate(NavigateServicesObject.ProductServices.NewTimeDeposit, strmobileconfirm, strotpresponse);
                            //if (strotpresponse == "confirm" && strmobileconfirm == "")
                            //{
                            //    maketransfer.NewTimeDeposit(strcurrency, strsourceoffunds, strterms, strdepositamount, strsubmitbutton, strexpectederrmsg);
                            //}
                            boolLogResult = ApplyforaProduct_TD(strcurrency, strsourceoffunds, strterm, strdepositamount, strselectmenu, strsubmitbutton);
                            
                        }
                        log.Passed = boolLogResult;
                        
                        break;
                    }
                }
            }
            catch (Exception ex)
            { 
                string strErrorExcep = ex.Message.ToString(); 
            }

            log.PushToLog();
            Logout(log, boolLogResult);

        }

        [Fact, Trait("Product", "ApplyNewTimeDeposit_Cancel")]
        public void TS00034_ApplyNewTimeDeposit_Cancel()
        {
            string method = new string("TS00034_ApplyNewTimeDeposit_Cancel");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_Cancel");
            TestLog log = new TestLog();

            TimeDeposit(testcase, log);

        }

        #region "ApplyNewTimeDeposit_USD_Back"
        [Fact, Trait("Products", "NewTimeDeposit_Confirm")]
        public void TS00035_ApplyNewTimeDeposit_USD_Back()
        {
            //**************************************************************************************************************************************
            // TS00034 - Create new Time Deposit, with USD Currency and Account selected back button  then cancelled transaction
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00035_ApplyNewTimeDeposit_USD_Back");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_USD_Back");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_USDCancel"
        [Fact, Trait("Products", "NewTimeDeposit_USDCancel")]
        public void TS00036_ApplyNewTimeDeposit_USDCancel()
        {
            //**************************************************************************************************************************************
            // TS00035 - Create new Time Deposit, with USD Currency and Account selected cancel button
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00036_ApplyNewTimeDeposit_USDCancel");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_USDCancel");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_USDConfirm"
        [Fact, Trait("Products", "NewTimeDeposit_USDConfirm")]
        public void TS00037_ApplyNewTimeDeposit_USDConfirm()
        {
            //**************************************************************************************************************************************
            // TS00036 - Create new Time Deposit, with USD Currency and Account confirmed transaction
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00037_ApplyNewTimeDeposit_USDConfirm");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_USDConfirm");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPBack"
        [Fact, Trait("Products", "NewTimeDeposit_PHPBack")]
        public void TS00038_ApplyNewTimeDeposit_PHPBack()
        {
            //**************************************************************************************************************************************
            // TS00037 - Create new Time Deposit, with PHP Currency and Account selected back button  then cancelled transaction
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00038_ApplyNewTimeDeposit_PHPBack");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPBack");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPCancel"
        [Fact, Trait("Products", "NewTimeDeposit_PHPCancel")]
        public void TS00039_ApplyNewTimeDeposit_PHPCancel()
        {
            //**************************************************************************************************************************************
            // TS00038 - Create new Time Deposit, with PHP Currency and Account selected cancel button
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00039_ApplyNewTimeDeposit_PHPCancel");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPCancel");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPConfirm"
        [Fact, Trait("Products", "NewTimeDeposit_PHPConfirm")]
        public void TS00040_ApplyNewTimeDeposit_PHPConfirm()
        {
            //**************************************************************************************************************************************
            // TS00039 - Create new Time Deposit, with PHP Currency and Account confirmed transaction
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00040_ApplyNewTimeDeposit_PHPConfirm");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPConfirm");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPDownloadPDF"
        [Fact, Trait("Products", "NewTimeDeposit_PHPDownloadPDF")]
        public void TS00041_ApplyNewTimeDeposit_PHPDownloadPDF()
        {
            //**************************************************************************************************************************************
            // TS00040 - Create new Time Deposit then download PDF
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00041_ApplyNewTimeDeposit_PHPDownloadPDF");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPDownloadPDF");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPPrintPDF"
        [Fact, Trait("Products", "NewTimeDeposit_PHPPrintPDF")]
        public void TS00041_ApplyNewTimeDeposit_PHPPrintPDF()
        {
            //**************************************************************************************************************************************
            // TS00041 - Create new Time Deposit then print PDF
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00041_ApplyNewTimeDeposit_PHPPrintPDF");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPPrintPDF");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPPrintPDF"
        [Fact, Trait("Products", "NewTimeDeposit_PHPPrintPDF")]
        public void TS00042_ApplyNewTimeDeposit_PHPPrintPDF()
        {
            //**************************************************************************************************************************************
            // TS00042 - Create new Time Deposit then Logout
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00042_ApplyNewTimeDeposit_PHPPrintPDF");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPPrintPDF");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_USDBulk"
        [Fact, Trait("Products", "NewTimeDeposit_USDBulk")]
        public void TS00043_ApplyNewTimeDeposit_USDBulk()
        {
            //**************************************************************************************************************************************
            // TS00043 - Create new Time Deposit, bulk USD transaction 
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00043_ApplyNewTimeDeposit_USDBulk");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_USDBulk");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPBulk"
        [Fact, Trait("Products", "NewTimeDeposit_PHPBulk")]
        public void TS00044_ApplyNewTimeDeposit_PHPBulk()
        {
            //**************************************************************************************************************************************
            // TS00044 - Create new Time Deposit, bulk PHP transaction
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00044_ApplyNewTimeDeposit_PHPBulk");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPBulk");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_InvalidCurrency"
        [Fact, Trait("Products", "NewTimeDeposit_InvalidCurrency")]
        public void TS00045_ApplyNewTimeDeposit_InvalidCurrency()
        {
            //**************************************************************************************************************************************
            // TS00045 - Create new Time Deposit with Invalid Currency
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00045_ApplyNewTimeDeposit_InvalidCurrency");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_InvalidCurrency");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_InvalidSourceOfFunds"
        [Fact, Trait("Products", "NewTimeDeposit_InvalidSourceOfFunds")]
        public void TS00046_ApplyNewTimeDeposit_InvalidSourceOfFunds()
        {
            //**************************************************************************************************************************************
            // TS00046 - Create new Time Deposit with Invalid Source of Funds
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00046_ApplyNewTimeDeposit_InvalidSourceOfFunds");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_InvalidSourceOfFunds");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_USDInvalidTerms"
        [Fact, Trait("Products", "NewTimeDeposit_USDInvalidTerms")]
        public void TS00047_ApplyNewTimeDeposit_USDInvalidTerms()
        {
            //**************************************************************************************************************************************
            // TS00047 - Create new Time Deposit with USD Currency and Invalid Terms
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00047_ApplyNewTimeDeposit_USDInvalidTerms");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_USDInvalidTerms");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPInvalidTerms"
        [Fact, Trait("Products", "NewTimeDeposit_PHPInvalidTerms")]
        public void TS00048_ApplyNewTimeDeposit_PHPInvalidTerms()
        {
            //**************************************************************************************************************************************
            // TS00048 - Create new Time Deposit with PHP Currency and Invalid Terms
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00048_ApplyNewTimeDeposit_PHPInvalidTerms");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPInvalidTerms");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPLessMin"
        [Fact, Trait("Products", "NewTimeDeposit_PHPLessMin")]
        public void TS00049_ApplyNewTimeDeposit_PHPLessMin()
        {
            //**************************************************************************************************************************************
            // TS00049 - Create new Time Deposit with Invalid Amount or less than 10k php
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00049_ApplyNewTimeDeposit_PHPLessMin");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPLessMin");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_USDLessMin"
        [Fact, Trait("Products", "NewTimeDeposit_USDLessMin")]
        public void TS00050_ApplyNewTimeDeposit_USDLessMin()
        {
            //**************************************************************************************************************************************
            // TS00050 - Create new Time Deposit with Invalid Amount or less than 1k usd
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00050_ApplyNewTimeDeposit_USDLessMin");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_USDLessMin");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_USDMorethanBal"
        [Fact, Trait("Products", "NewTimeDeposit_USDMorethanBal")]
        public void TS00051_ApplyNewTimeDeposit_USDMorethanBal()
        {
            //**************************************************************************************************************************************
            // TS00051 - Create new Time Deposit with USD Source of Funds and Insufficient Account fund
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00051_ApplyNewTimeDeposit_USDMorethanBal");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_USDMorethanBal");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPMorethanBal"
        [Fact, Trait("Products", "NewTimeDeposit_PHPMorethanBal")]
        public void TS00052_ApplyNewTimeDeposit_PHPMorethanBal()
        {
            //**************************************************************************************************************************************
            // TS00052 - Create new Time Deposit with PHP Source of Funds and Insufficient Account fund
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00052_ApplyNewTimeDeposit_PHPMorethanBal");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPMorethanBal");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion


        #region "ApplyNewTimeDeposit_CreditCardOnlyNo"
        [Fact, Trait("Products", "NewTimeDeposit_CreditCardOnlyNo")]
        public void TS00053_ApplyNewTimeDeposit_CreditCardOnlyNo()
        {
            //**************************************************************************************************************************************
            // Create new Time Deposit, with USD Currency and Account selected back button  then cancelled transaction
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00053_ApplyNewTimeDeposit_CreditCardOnlyNo");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_CreditCardOnlyNo");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_CreditCardOnlyYes"
        [Fact, Trait("Products", "NewTimeDeposit_CreditCardOnlyYes")]
        public void TS00054_ApplyNewTimeDeposit_CreditCardOnlyYes()
        {
            //**************************************************************************************************************************************
            // Create new Time Deposit, with USD Currency and Account selected back button  then cancelled transaction
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00054_ApplyNewTimeDeposit_CreditCardOnlyYes");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_CreditCardOnlyYes");

            TimeDeposit(testcase);
            //**************************************************************************************************************************************
        }
        #endregion


/*
#region "ApplyNewTimeDeposit_ZeroInterest"
[Fact, Trait("Products", "NewTimeDeposit_ZeroInterest")]
public void TS00054_ApplyNewTimeDeposit_ZeroInterest()
{
    //**************************************************************************************************************************************
    // TS00054 - Create new Time Deposit with zero Interest Rate
    //**************************************************************************************************************************************

    //**************************************************************************************************************************************
    string method = new string("TS00054_ApplyNewTimeDeposit_ZeroInterest");
    string testcase = new string(method.Substring(0, 7));
    string strTitle = new string("ApplyNewTimeDeposit_ZeroInterest");
    //string strOTPResponse = new string("confirm");
    Login();
    PopProductApplyNewTimeDeposit(method, testcase);
    //**************************************************************************************************************************************
}
#endregion
*/
    }
}
