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
using System.Globalization;

namespace SeleniumWebDriver_AutomatedTesting.EWOP
{
    public class Products : EWOP
    {
        string method = string.Empty;
        string strTitle = string.Empty;
        string strOTPResponse = string.Empty;

        #region "ApplyNewTimeDeposit_CancelOTP"
        [Fact, Trait("Products", "NewTimeDeposit_CancelOTP")]
        public void TS00033_ApplyNewTimeDeposit_CancelOTP()
        {
            //**************************************************************************************************************************************
            // TS00033 - Create new Time Deposit but cancelled OTP
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
 
            string method = new string("TS00033_ApplyNewTimeDeposit_Cancel");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_Cancel");
            //string strOTPResponse = new string("cancel");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_USD_Back"
        [Fact, Trait("Products", "NewTimeDeposit_Confirm")]
        public void TS00034_ApplyNewTimeDeposit_USD_Back()
        {
            //**************************************************************************************************************************************
            // TS00034 - Create new Time Deposit, with USD Currency and Account selected back button  then cancelled transaction
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00034_ApplyNewTimeDeposit_Confirm");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_Confirm");
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_USDCancel"
        [Fact, Trait("Products", "NewTimeDeposit_USDCancel")]
        public void TS00035_ApplyNewTimeDeposit_USDCancel()
        {
            //**************************************************************************************************************************************
            // TS00035 - Create new Time Deposit, with USD Currency and Account selected cancel button
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00035_ApplyNewTimeDeposit_USDCancel");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_USDCancel");
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_USDConfirm"
        [Fact, Trait("Products", "NewTimeDeposit_USDConfirm")]
        public void TS00036_ApplyNewTimeDeposit_USDConfirm()
        {
            //**************************************************************************************************************************************
            // TS00036 - Create new Time Deposit, with USD Currency and Account confirmed transaction
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00036_ApplyNewTimeDeposit_USDConfirm");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_USDConfirm");
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPBack"
        [Fact, Trait("Products", "NewTimeDeposit_PHPBack")]
        public void TS00037_ApplyNewTimeDeposit_PHPBack()
        {
            //**************************************************************************************************************************************
            // TS00037 - Create new Time Deposit, with PHP Currency and Account selected back button  then cancelled transaction
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00037_ApplyNewTimeDeposit_PHPBack");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPBack");
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPCancel"
        [Fact, Trait("Products", "NewTimeDeposit_PHPCancel")]
        public void TS00038_ApplyNewTimeDeposit_PHPCancel()
        {
            //**************************************************************************************************************************************
            // TS00038 - Create new Time Deposit, with PHP Currency and Account selected cancel button
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string strthismethod = this.method.ToString();
            string method = new string("TS00038_ApplyNewTimeDeposit_PHPCancel");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPCancel");
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPConfirm"
        [Fact, Trait("Products", "NewTimeDeposit_PHPConfirm")]
        public void TS00039_ApplyNewTimeDeposit_PHPConfirm()
        {
            //**************************************************************************************************************************************
            // TS00039 - Create new Time Deposit, with PHP Currency and Account confirmed transaction
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00039_ApplyNewTimeDeposit_PHPConfirm");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("NewTimeDeposit_PHPConfirm");
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
            //**************************************************************************************************************************************
        }
        #endregion

        #region "ApplyNewTimeDeposit_PHPDownloadPDF"
        [Fact, Trait("Products", "NewTimeDeposit_PHPDownloadPDF")]
        public void TS00040_ApplyNewTimeDeposit_PHPDownloadPDF()
        {
            //**************************************************************************************************************************************
            // TS00040 - Create new Time Deposit then download PDF
            //**************************************************************************************************************************************

            //**************************************************************************************************************************************
            string method = new string("TS00040_ApplyNewTimeDeposit_PHPDownloadPDF");
            string testcase = new string(method.Substring(0, 7));
            string strTitle = new string("ApplyNewTimeDeposit_PHPDownloadPDF");
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("confirm");
            Login();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("");
            Login_CreditCardOnly();
            PopProductApplyNewTimeDeposit(method, testcase);
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
            //string strOTPResponse = new string("");
            Login_CreditCardOnly();
            PopProductApplyNewTimeDeposit(method, testcase);
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
