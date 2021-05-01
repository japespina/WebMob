using Bogus.DataSets;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SeleniumWebDriver_AutomatedTesting.EWOP.PageObjects
{
    class MakeATransferObject
    {
        public MakeATransferObject()
        {
            PageFactory.InitElements(Properties.Driver, this);
        }

        public enum PaymentChannel
        {
            InstaPay,
            PesoNet,
            Others
        }

        public enum BeneficiaryType
        {
            Individual,
            Corporate
        }

        public enum CurrencyType
        {
            PHP,
            USD
        }

        public enum Charges
        {
            Beneficiary,
            Shared,
            OnUs
        }
        #region "Common Elements"
        [FindsBy(How = How.Id, Using = "C1__QUE_EDE9841D57DC645E351908")] //Name: C1__LOGIN[1].SAFENETDATA[1].SQDATA[1].SECRETANSWER
        public IWebElement SecretAnswer { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_5DFE13BF59422EAF7007")] //Name: C1____D6F0612B5612ABBA FormButton 13
        public IWebElement ConfirmButton { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_5DFE13BF59422EAF7011")] //Name: C1____D6F0612B5612ABBA FormButton 13
        public IWebElement TransactionID { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_5DFE13BF59422EAF7012")] //Name: C1____26C99DD6F5E54C90 FormButton 15
        public IWebElement ReturnToTransactionsButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//form[@id='form1']/div[5]")] //dialog 2of3 and 3of3 page
        public IWebElement DialogPage { get; set; }
        #endregion

        #region "MoveMoney"
        //select account
        [FindsBy(How = How.Id, Using = "C1__QUE_5DFE13BF59422EAF6956")] //Name: C1__MOVEMONEY[1].TOACCOUNTFORMOVEMONEY
        public IWebElement SelectAccount { get; set; }

        //amount
        [FindsBy(How = How.Id, Using = "C1__QUE_B3E644B006BFD38A1420584")] //Name: C1__MOVEMONEY[1].CREDITAMOUNT
        public IWebElement MoveMoneyAmount { get; set; }

        //continue
        [FindsBy(How = How.Id, Using = "C1__BUT_81C3453596285C9914221")] //Name: C1____81C3453596285C99 FormButton 18
        public IWebElement MoveMoneyContinueButton { get; set; }

        public void MoveMoney(string accountNumber, string amount)
        {
            Properties.Driver.Wait(SelectAccount);
            SelectAccount.SelectDropDown(accountNumber);
            MoveMoneyAmount.EnterText(amount);

            MoveMoneyContinueButton.Click();

            Properties.Driver.Wait(ConfirmButton);

            ConfirmPayment();
        }
        #endregion

        #region "SendMoney Elements"
        //Send Money Tab
        [FindsBy(How = How.Id, Using = "C1__BUT_8B85BB5844D462507604")] //Title: Send Money
        public IWebElement SendMoneyTab { get; set; }

        //Payment Types
        [FindsBy(How = How.Id, Using = "C1__QUE_05F6E678DF5E2A681729522_0")] //Name: C1__SENDMONEY[1].PAYMENTTYPE
        public IWebElement RegisteredPaymentTypeRadio { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_05F6E678DF5E2A681729522_1")] //Name: C1__SENDMONEY[1].PAYMENTTYPE
        public IWebElement OneTimePaymentTypeRadio { get; set; }

        //Payment Options
        [FindsBy(How = How.Id, Using = "C1__QUE_5DFE13BF59422EAF6958_0")] //Name: C1__SENDMONEY[1].PAYEEOPTIONS
        public IWebElement EWBPaymentOptionRadio { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_5DFE13BF59422EAF6958_1")] //Name: C1__SENDMONEY[1].PAYEEOPTIONS
        public IWebElement LocalPaymentOptionRadio { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_5DFE13BF59422EAF6958_2")] //Name: C1__SENDMONEY[1].PAYEEOPTIONS
        public IWebElement OutCountryPaymentOptionRadio { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_A314CFAB35A461F81650345")] //Name: C1____A314CFAB35A461F8 FormButton 21    Title: Continue - ORIG
        public IWebElement SendMoneyContinueButton { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_5DFE13BF59422EAF6979")] //Name:C1__WORKINGELEMENTS[1].MAKEPAYMENT[1].TRANSACTIONAMOUNT
        public IWebElement SendMoneyOneTimeAmount { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_0BF4C85C7563D6DC417666")] //Name: C1__SENDMONEY[1].TRANSACTIONAMOUNT
        public IWebElement SendMoneyRegisteredAmount { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_SMPN")] //name: C1____A9C6A5BC073A3B29 FormButton 21    title: Continue - SEND MONEY-OTP-PESONET
        public IWebElement PesoNetButton { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_SMIP")] //name: C1____3C3B4201DBD80F1C FormButton 19    title: Continue - SEND MONEY-INSTAPAY
        public IWebElement InstaPayButton { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_4454BC9D9514D21F1147599_0")] //Name: C1__SENDMONEY[1].SAVEPAYEE
        public IWebElement SaveBeneficiaryCheckBox { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_28ED70D4CAD24212496341")] //Name: C1__SENDMONEY[1].PAYEENICKNAME
        public IWebElement BeneficiaryNickName { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_5DFE13BF59422EAF6964")] //Name: C1__SENDMONEY[1].TOACCOUNTFORSENDMONEY
        public IWebElement RegisteredBeneficiaryNameDropdown { get; set; }

        #endregion

        #region "SendMoney_OneTime_EWB"
        [FindsBy(How = How.Id, Using = "C1__QUE_28ED70D4CAD24212495857")] //Name: C1__SENDMONEY[1].EWBONETIMEPAYMENT[1].PAYEEACCOUNTNUMBER
        public IWebElement EWBAccountNo { get; set; }

        public void SendMoney_OneTime_EWB(string accountNumber, string amount, string answer, bool saveBeneficiary = false, string saveBeneficiaryNickName = "")
        {
            Properties.Driver.Wait(SendMoneyTab);
            SendMoneyTab.Click();

            Properties.Driver.Wait(OneTimePaymentTypeRadio);
            OneTimePaymentTypeRadio.Click();
            Thread.Sleep(1000);
            Properties.Driver.Wait(EWBPaymentOptionRadio);
            EWBPaymentOptionRadio.Click();

            Properties.Driver.Wait(EWBAccountNo);
            Thread.Sleep(3000);
            EWBAccountNo.EnterText(accountNumber);
            SendMoneyOneTimeAmount.EnterText(amount);

            SaveBeneficiary(saveBeneficiary, saveBeneficiaryNickName);

            SendMoneyContinueButton.Click();

            Thread.Sleep(5000);
            Properties.Driver.Wait(SecretAnswer);
            SecretAnswer.EnterText(answer);

            ConfirmPayment();
        }
        #endregion

        #region "SendMoney_OneTime_LocalBank"
        [FindsBy(How = How.Id, Using = "C1__QUE_61CCCFF9BC0E9748623506")] //Name: C1__SENDMONEY[1].LOCALPAYEE[1].BENEFICIARYTYPE
        public IWebElement BeneficiaryTypeDropDown { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_E3E8D0CD7D827E541312697")] //Name: C1__SENDMONEY[1].LOCALPAYEE[1].BENEFICIARYFNAME
        public IWebElement BeneficiaryFirstName { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_E3E8D0CD7D827E541312699")] //Name: C1__SENDMONEY[1].LOCALPAYEE[1].BENEFICIARYLNAME
        public IWebElement BeneficiaryLastName { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_61CCCFF9BC0E9748653000")] //Name: C1__SENDMONEY[1].LOCALPAYEE[1].BENEFICIARYLNAME
        public IWebElement BeneficiaryFullName { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_5DFE13BF59422EAF6968")] //Name: C1__SENDMONEY[1].LOCALPAYEE[1].ACCOUNTNUMBER
        public IWebElement BeneficiaryAccount { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_28ED70D4CAD24212496797")] //Name: C1__SENDMONEY[1].LOCALPAYEE[1].PAYEECURRENCY
        public IWebElement BeneficiaryCurrencyDropDown { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_28ED70D4CAD24212496896")] //Name: C1__SENDMONEY[1].LOCALPAYEE[1].PAYEEBANK
        public IWebElement BeneficiaryBankDropDown { get; set; }

        [FindsBy(How = How.Id, Using = "BUT_33C50753EA429333132183")]
        public IWebElement AccountSummaryLink { get; set; }

        public void SendMoney_OneTime_LocalBank(string accountNumber, string amount, string firstname, string lastname, string fullname, ref string bankName, CurrencyType currency, BeneficiaryType beneType, PaymentChannel channel, string answer, bool saveBeneficiary = false, string saveBeneficiaryNickName = "")
        {
            Properties.Driver.Wait(SendMoneyTab);
            SendMoneyTab.Click();

            Properties.Driver.Wait(OneTimePaymentTypeRadio);
            OneTimePaymentTypeRadio.Click();
            Thread.Sleep(2000);
            Properties.Driver.Wait(LocalPaymentOptionRadio);
            LocalPaymentOptionRadio.Click();

            Properties.Driver.Wait(BeneficiaryTypeDropDown);
            if (beneType == BeneficiaryType.Individual)
            {
                BeneficiaryTypeDropDown.SelectDropDownText("Individual");
                Thread.Sleep(1000);
                BeneficiaryFirstName.EnterText(firstname);
                BeneficiaryLastName.EnterText(lastname);

            }
            else if (beneType == BeneficiaryType.Corporate)
            {
                BeneficiaryTypeDropDown.SelectDropDownText("Corporate");
                Thread.Sleep(1000);
                BeneficiaryFullName.EnterText(fullname);
            }

            BeneficiaryAccount.EnterText(accountNumber);
            Properties.Driver.Wait(BeneficiaryCurrencyDropDown);
            BeneficiaryCurrencyDropDown.Click();
            Thread.Sleep(1000);
            if (currency == CurrencyType.PHP)
                BeneficiaryCurrencyDropDown.SelectDropDownText("PHP");
            else
                BeneficiaryCurrencyDropDown.SelectDropDownText("USD");

            Thread.Sleep(3000);

            Properties.Driver.Wait(BeneficiaryBankDropDown);
            if (string.IsNullOrEmpty(bankName))
            {
                bool isChannelButtonEnabled = false;
                while (!isChannelButtonEnabled)
                {

                    BeneficiaryBankDropDown.SelectRandomDropDown();
                    Thread.Sleep(1000);
                    if (currency == CurrencyType.PHP)
                    {
                        if (channel == PaymentChannel.InstaPay)
                            isChannelButtonEnabled = InstaPayButton.Enabled;
                        else if (channel == PaymentChannel.PesoNet)
                            isChannelButtonEnabled = PesoNetButton.Enabled;
                    }
                    else
                        isChannelButtonEnabled = true;
                }
                bankName = BeneficiaryBankDropDown.GetTextFromDDL();
            }
            else
            {
                BeneficiaryBankDropDown.SelectDropDownText(bankName);
            }
            Thread.Sleep(1000);
            SendMoneyOneTimeAmount.EnterText(amount);
            Thread.Sleep(1000);
            SaveBeneficiary(saveBeneficiary, saveBeneficiaryNickName);
            Thread.Sleep(1000);
            if (currency == CurrencyType.PHP)
            {
                if (channel == PaymentChannel.InstaPay)
                {
                    Properties.Driver.Wait(InstaPayButton);
                    InstaPayButton.Click();
                }
                else if (channel == PaymentChannel.PesoNet)
                {
                    Properties.Driver.Wait(PesoNetButton);
                    PesoNetButton.Click();
                }
            }
            else
            {
                Properties.Driver.Wait(SendMoneyContinueButton);
                SendMoneyContinueButton.Click();
            }

            

            Thread.Sleep(5000);
            Properties.Driver.WaitTillExist(By.Id("C1__QUE_EDE9841D57DC645E351908"));
            SecretAnswer.EnterText(answer);

            ConfirmPayment();
        }
        #endregion

        #region "SendMoney_OneTime_OutCountry"

        [FindsBy(How = How.Id, Using = "C1__QUE_5DFE13BF59422EAF6973")] //name: C1__SENDMONEY[1].PAYEENAME_FORLOCAL_INTERNATIONAL
        public IWebElement OutCountryBeneficiaryName { get; set; }
        
        [FindsBy(How = How.Id, Using = "C1__QUE_5DFE13BF59422EAF6975")] //name: C1__SENDMONEY[1].INTERNATIONALPAYEE[1].ACCOUNTNO
        public IWebElement OutCountryBeneficiaryAccount { get; set; }        
        
        [FindsBy(How = How.Id, Using = "C1__QUE_3E8FC82C1E2D11CC882199")] //name: C1__SENDMONEY[1].INTERNATIONALPAYEE[1].COUNTRY
        public IWebElement OutCountryDropDown { get; set; }

        [FindsBy(How=How.Id, Using = "C1__QUE_28ED70D4CAD24212574009")] //name: C1__SENDMONEY[1].INTERNATIONALPAYEE[1].PAYEEBANK
        public IWebElement OutCountryBeneficiaryBankDropDown { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_28ED70D4CAD24212574144")] //name: C1__SENDMONEY[1].INTERNATIONALPAYEE[1].PAYEECURRENCY
        public IWebElement OutCountryBenenficiaryCurrencyDropDown { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_E450868DDA9DE57D363218_0")] //name: C1__SENDMONEY[1].INTERNATIONALPAYEE[1].CHARGES
        public IWebElement OutCountryChargesBeneficiary { get; set; }
        [FindsBy(How = How.Id, Using = "C1__QUE_E450868DDA9DE57D363218_1")]
        public IWebElement OutCountryChargesShared { get; set; }
        [FindsBy(How = How.Id, Using = "C1__QUE_E450868DDA9DE57D363218_2")]
        public IWebElement OutCountryChargesOnUs { get; set; }
        
        public void SendMoney_OneTime_OutCountry(string accountNumber, string amount, string fullname, ref string bankName, string currency, ref string country, Charges chargeTo, string answer, bool saveBeneficiary = false, string saveBeneficiaryNickName = "")
        {
            Properties.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            Properties.Driver.Wait(SendMoneyTab);
            SendMoneyTab.Click();

            Properties.Driver.Wait(OneTimePaymentTypeRadio);
            OneTimePaymentTypeRadio.Click();
            Thread.Sleep(2000);
            Properties.Driver.Wait(OutCountryPaymentOptionRadio);
            OutCountryPaymentOptionRadio.Click();

            Thread.Sleep(2000);
            Properties.Driver.Wait(OutCountryBeneficiaryName);
            OutCountryBeneficiaryName.EnterText(fullname);
            Thread.Sleep(1000);
            OutCountryBeneficiaryAccount.EnterText(accountNumber);
            Thread.Sleep(1000);
            Properties.Driver.Wait(OutCountryDropDown);
            OutCountryDropDown.Click();
            Thread.Sleep(1000);
            if(string.IsNullOrEmpty(country))
            {
                OutCountryDropDown.SelectRandomDropDown();
                Thread.Sleep(5000);
                country = OutCountryDropDown.GetTextFromDDL();
            }
            else
            { 
                OutCountryDropDown.SelectDropDownText(country); 
            }
            Thread.Sleep(2000);
            Properties.Driver.Wait(OutCountryBeneficiaryBankDropDown);
            OutCountryBeneficiaryBankDropDown.Click();
            Thread.Sleep(1000);
            if (string.IsNullOrEmpty(bankName))
            {
                OutCountryBeneficiaryBankDropDown.SelectRandomDropDown();
                Thread.Sleep(1000);
                bankName = OutCountryBeneficiaryBankDropDown.GetTextFromDDL();
                //bankName = OutCountryBeneficiaryBankDropDown.DropDownRandomPick();
                //OutCountryBeneficiaryBankDropDown.SelectDropDownText(bankName);
            }
            else
            {
                OutCountryBeneficiaryBankDropDown.SelectDropDownText(bankName);
            }
            Thread.Sleep(3000);
            Properties.Driver.Wait(OutCountryBenenficiaryCurrencyDropDown);
            OutCountryBenenficiaryCurrencyDropDown.Click();
            OutCountryBenenficiaryCurrencyDropDown.SelectDropDownText(currency);

            SendMoneyOneTimeAmount.EnterText(amount);

            if (chargeTo == Charges.Beneficiary)
                OutCountryChargesBeneficiary.Click();
            else if (chargeTo == Charges.Shared)
                OutCountryChargesShared.Click();
            else if (chargeTo == Charges.OnUs)
                OutCountryChargesOnUs.Click();

            Thread.Sleep(1000);
            SaveBeneficiary(saveBeneficiary, saveBeneficiaryNickName);
            Thread.Sleep(1000);

            Properties.Driver.Wait(SendMoneyContinueButton);
            SendMoneyContinueButton.Click();

            Thread.Sleep(5000);
            Properties.Driver.Wait(SecretAnswer);
            SecretAnswer.EnterText(answer);

            ConfirmPayment();
        }
        #endregion

        #region "SendMoney_Registered_LocalBank"

        public void SendMoney_Registered_LocalBank(string accountNumber, string amount, string beneficiaryName, PaymentChannel channel)
        {
            Properties.Driver.Wait(SendMoneyTab);
            SendMoneyTab.Click();

            Properties.Driver.Wait(RegisteredPaymentTypeRadio);
            RegisteredPaymentTypeRadio.Click();
            Thread.Sleep(2000);
            Properties.Driver.Wait(LocalPaymentOptionRadio);
            LocalPaymentOptionRadio.Click();

            Properties.Driver.Wait(RegisteredBeneficiaryNameDropdown);
            RegisteredBeneficiaryNameDropdown.SelectDropDownText(beneficiaryName);

            SendMoneyRegisteredAmount.Click();
            SendMoneyRegisteredAmount.EnterText(amount);
            Thread.Sleep(1000);

            if (channel == PaymentChannel.InstaPay)
            {
                Properties.Driver.Wait(InstaPayButton);
                InstaPayButton.Click();
            }
            else if (channel == PaymentChannel.PesoNet)
            {
                Properties.Driver.Wait(PesoNetButton);
                PesoNetButton.Click();
            }
            else
            {
                Properties.Driver.Wait(SendMoneyContinueButton);
                SendMoneyContinueButton.Click();
            }

            Thread.Sleep(2000);
            ConfirmPayment();
        }
        #endregion

        #region "SendMoney_Registered_EWB"
        [FindsBy(How = How.Id, Using = "C1__QUE_5DFE13BF59422EAF6964")] //Name: C1__SENDMONEY[1].TOACCOUNTFORSENDMONEY
        public IWebElement EWBBeneficiaryAccountNo { get; set; }

        public void SendMoney_Registered_EWB(string accountNumber, string beneficiaryName, string amount)
        {
            Properties.Driver.Wait(SendMoneyTab);
            SendMoneyTab.Click();

            Properties.Driver.Wait(RegisteredPaymentTypeRadio);
            RegisteredPaymentTypeRadio.Click();
            Thread.Sleep(1000);
            Properties.Driver.Wait(EWBPaymentOptionRadio);
            EWBPaymentOptionRadio.Click();

            Properties.Driver.Wait(EWBBeneficiaryAccountNo);
            EWBBeneficiaryAccountNo.SelectDropDownText(accountNumber + " - " + beneficiaryName);
            Thread.Sleep(1000);
            SendMoneyRegisteredAmount.EnterText(amount);

            SendMoneyContinueButton.Click();

            ConfirmPayment();
        }
        #endregion

        #region "SendMoney_Registered_OutCountry"
        [FindsBy(How = How.Id, Using = "C1__QUE_0BF4C85C7563D6DC510805")] //name: C1__SENDMONEY[1].PAYEECURRENCY
        public IWebElement OutCountryRegisteredBenenficiaryCurrencyDropDown { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_A314CFAB35A461F81323709")] //name:C1__SENDMONEY[1].LOCALPAYEE[1].BENEFICIARYFNAME
        public IWebElement OutCountryRegisteredBenenficiaryFirstName { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_A314CFAB35A461F81323711")] //name: C1__SENDMONEY[1].LOCALPAYEE[1].BENEFICIARYLNAME
        public IWebElement OutCountryRegisteredBenenficiaryLastName { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_4F57347FA6A66AB283844_0")] //name: C1__SENDMONEY[1].CHARGES
        public IWebElement OutCountryRegisteredChargesBeneficiary { get; set; }
        [FindsBy(How = How.Id, Using = "C1__QUE_4F57347FA6A66AB283844_1")]
        public IWebElement OutCountryRegisteredChargesShared { get; set; }
        [FindsBy(How = How.Id, Using = "C1__QUE_4F57347FA6A66AB283844_2")]
        public IWebElement OutCountryRegisteredChargesOnUs { get; set; }

        public void SendMoney_Registered_OutCountry(string accountNumber, string amount, string beneficiary, string firstname, string lastname, string currency, Charges chargeTo)
        {
            Properties.Driver.Wait(SendMoneyTab);
            SendMoneyTab.Click();

            Properties.Driver.Wait(RegisteredPaymentTypeRadio);
            RegisteredPaymentTypeRadio.Click();
            Thread.Sleep(2000);
            Properties.Driver.Wait(OutCountryPaymentOptionRadio);
            OutCountryPaymentOptionRadio.Click();

            Thread.Sleep(1000);
            Properties.Driver.Wait(OutCountryPaymentOptionRadio);
            RegisteredBeneficiaryNameDropdown.SelectDropDownText(beneficiary);

            Thread.Sleep(1000);
            Properties.Driver.Wait(OutCountryPaymentOptionRadio);
            OutCountryRegisteredBenenficiaryCurrencyDropDown.Click();
            OutCountryRegisteredBenenficiaryCurrencyDropDown.SelectDropDownText(currency);
            Thread.Sleep(1000);
            OutCountryRegisteredBenenficiaryFirstName.EnterText(firstname);
            OutCountryRegisteredBenenficiaryLastName.EnterText(lastname);

            SendMoneyRegisteredAmount.EnterText(amount);

            if (chargeTo == Charges.Beneficiary)
                OutCountryRegisteredChargesBeneficiary.Click();
            else if (chargeTo == Charges.Shared)
                OutCountryRegisteredChargesShared.Click();
            else if (chargeTo == Charges.OnUs)
                OutCountryRegisteredChargesOnUs.Click();

            Properties.Driver.Wait(SendMoneyContinueButton);
            SendMoneyContinueButton.Click();

            ConfirmPayment();
        }
        #endregion

        #region "Product_NewTimeDeposit"
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

        [FindsBy(How = How.Id, Using = "COL_41465922B7706F2E467995")]
        public IWebElement Product_NewTB_Div { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_AF7FB8279452BDD969410")]
        public IWebElement Product_NewTD_InterestRate { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='C1__p1_HEAD_F5CC5F368DD213F5419767']/div")]
        public IWebElement Product_NewTD_SourceOfFunds_Balance { get; set; }
        
        [FindsBy(How = How.Id, Using = "C1__BUT_BBE76E0C7BA327CB67554")]
        public IWebElement Product_NewTD_ContinueButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='C1__p1_HEAD_593B40E0DC9BFBFA212414']/div")]
        public IWebElement Product_NewTD_ErrorMessage { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_E24C54D2E91F1866799046")]
        public IWebElement Product_NewTD_BackButton { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_BBE76E0C7BA327CB67565")]
        public IWebElement Product_NewTD_CancelButton { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_BBE76E0C7BA327CB67566")]
        public IWebElement Product_NewTD_ConfirmButton { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_BBE76E0C7BA327CB67576")]
        public IWebElement Product_NewTD_ReturntoCreateTDButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@id='C1__BUT_5F981032ECE87271130624']/span")]
        public IWebElement Product_NewTD_DownloadPDFlink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@id='C1__BUT_5F981032ECE87271130627']/span")]
        public IWebElement Product_NewTD_PrintPDFlink { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='C1__COL_8373CB3E72114E0767094']")]
        public IWebElement Product_NewTD_Step2of3 { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='C1__p4_QUE_E24C54D2E91F1866799019']/div")]
        public IWebElement Product_NewTD_Step3of3 { get; set; }
        [FindsBy(How = How.XPath, Using = "//a[@id='BUT_7CFF8FBC6E55B5781176951']/span")]
        public IWebElement ApplyforaProduct { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@id='BUT_7CFF8FBC6E55B5781177036']/span")]
        public IWebElement ApplyNewTimeDeposit { get; set; }


        public void NewTimeDeposit(string strCurrency, string strSourceAccount,string strTerms, string strDepositAmount, string strSubmitButton, string strexpmsg)
        {
            string strSourceAccountBalance = string.Empty;
            string strInterestRate = string.Empty;
            bool valSelectDropDown = false;
            bool valExpectedErrMsg = false;
            string strValuetext = string.Empty;

            //Search and select from Currency dropdownlist
            Properties.Driver.Wait(Product_NewTD_DropDown_Currency);
            //Product_NewTD_DropDown_Currency.SelectDropDownText(strCurrency.ToString());
            valSelectDropDown = Product_NewTD_DropDown_Currency.valSelectDropDownText(strCurrency.ToString());
            if (valSelectDropDown == false)
            {
                strValuetext = strCurrency;
                goto GoToMainDashboard;
            }
            Thread.Sleep(3000);

            //Search and select from Source of Funds dropdownlist
            Properties.Driver.Wait(Product_NewTD_DropDown_SourceOfFunds);
            valSelectDropDown = Product_NewTD_DropDown_SourceOfFunds.valSelectDropDownText(strSourceAccount.ToString());
            if(valSelectDropDown == false)
            {
                strValuetext = strSourceAccount;
                goto GoToMainDashboard;
            }
            Thread.Sleep(3000);

            //Get value from Source of Funds Balance amount
            Properties.Driver.Wait(Product_NewTD_SourceOfFunds_Balance);
            strSourceAccountBalance = Product_NewTD_SourceOfFunds_Balance.Text.ToString();
            Thread.Sleep(3000);

            //Search and select from Terms dropdownlist
            Properties.Driver.Wait(Product_NewTD_DropDown_Terms);
            //Product_NewTD_DropDown_Terms.SelectDropDownText(strTerms.ToString());
            valSelectDropDown = Product_NewTD_DropDown_Terms.valSelectDropDownText(strTerms.ToString());
            if (valSelectDropDown == false)
            {
                strValuetext = strTerms;
                goto GoToMainDashboard;
            }
            Thread.Sleep(3000);

            //Input Deposit Amount
            Properties.Driver.Wait(Product_NewTD_Amount);
            //Product_NewTD_Amount.EnterText(strDepositAmount);
            Product_NewTD_Amount.SendKeys(strDepositAmount);
            Properties.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6000);

            //Click on div
            Properties.Driver.Wait(Product_NewTB_Div);
            Product_NewTB_Div.Click();
            Properties.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6000);

            //Get Interest Rate
            Properties.Driver.Wait(Product_NewTD_InterestRate);
            Product_NewTD_InterestRate.Click();
            strInterestRate = Product_NewTD_InterestRate.Text.ToString();
            Properties.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6000);

            //Proceed with creation of TD
            Properties.Driver.Wait(Product_NewTD_ContinueButton);
            Product_NewTD_ContinueButton.Click();
            Thread.Sleep(10000);

            //Check on Error Messages
            //if (Product_NewTD_Step2of3 == null)
            //{
            //    if (Product_NewTD_ErrorMessage != null)
            //    {
                    //Properties.Driver.Wait(Product_NewTD_ErrorMessage);
                    string strErrMsg = Product_NewTD_ErrorMessage.Text.ToString();
                    if (strErrMsg != "")
                    {
                        if (strErrMsg == strexpmsg)
                        {
                            valExpectedErrMsg = true;
                            goto GoToMainDashboard;
                        }
                    }
                //}
            //}


            //Select transaction button
            switch (strSubmitButton)
            {
                case "Back":
                    if (Product_NewTD_Step2of3 != null && Product_NewTD_BackButton != null)
                    {
                        Properties.Driver.Wait(Product_NewTD_BackButton);
                        Product_NewTD_BackButton.Click();
                        Thread.Sleep(10000);
                    }

                    //Proceed with creation of TD
                    //Properties.Driver.Wait(Product_NewTD_ContinueButton);
                    //Product_NewTD_ContinueButton.Click();
                    //Thread.Sleep(6000);

                    //if (Product_NewTD_Step2of3 != null && Product_NewTD_CancelButton != null)
                    //{
                    //    Properties.Driver.Wait(Product_NewTD_CancelButton);
                    //    Product_NewTD_CancelButton.Click();
                    //    Thread.Sleep(3000);
                    //}
                    break;

                case "Cancel":
                    Properties.Driver.Wait(Product_NewTD_CancelButton);
                    Product_NewTD_CancelButton.Click();
                    //Thread.Sleep(4000);
                    Properties.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6000);
                    break;

                case "Confirm":
                    //Properties.Driver.Wait(Product_NewTD_ConfirmButton);
                    if (Product_NewTD_Step2of3 != null && Product_NewTD_ConfirmButton != null)
                    {
                        Properties.Driver.Wait(Product_NewTD_ConfirmButton);
                        Product_NewTD_ConfirmButton.Click();
                        Thread.Sleep(20000);
                    }

                    if (Product_NewTD_Step3of3 != null && Product_NewTD_ReturntoCreateTDButton != null)
                    {
                        Properties.Driver.Wait(Product_NewTD_ReturntoCreateTDButton);
                        Product_NewTD_ReturntoCreateTDButton.Click();
                        Thread.Sleep(20000);
                    }

                    break;

                case "Download":
                    if (Product_NewTD_Step2of3 != null && Product_NewTD_ConfirmButton != null)
                    {
                        Properties.Driver.Wait(Product_NewTD_ConfirmButton);
                        Product_NewTD_ConfirmButton.Click();
                        Thread.Sleep(20000);
                    }

                    if (Product_NewTD_Step3of3 != null && Product_NewTD_DownloadPDFlink != null)
                    {
                        Properties.Driver.Wait(Product_NewTD_DownloadPDFlink);
                        Product_NewTD_DownloadPDFlink.Click();
                        Thread.Sleep(20000);
                    }
                    break;

                case "Print":
                    if (Product_NewTD_Step2of3 != null && Product_NewTD_ConfirmButton != null)
                    {
                        Properties.Driver.Wait(Product_NewTD_ConfirmButton);
                        Product_NewTD_ConfirmButton.Click();
                        Thread.Sleep(20000);
                    }

                    if (Product_NewTD_Step3of3 != null && Product_NewTD_PrintPDFlink != null)
                    {
                        Properties.Driver.Wait(Product_NewTD_PrintPDFlink);
                        Product_NewTD_PrintPDFlink.Click();
                        Thread.Sleep(20000);
                    }
                    break;

            }

        GoToMainDashboard:
            if(valSelectDropDown == false)
            {
                string strErrorMsg = "No such value in Dropdown - " + strValuetext;
            }

            if(valExpectedErrMsg == true)
            {
                string strExpectedMsg = "Error message expected the same";
            }


        }
        #endregion

        private bool isElementPresent(By by)
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
        private void ConfirmPayment()
        {
            Properties.Driver.Wait(ConfirmButton);
            ConfirmButton.Click();
            Thread.Sleep(15000);

            try
            {
                if (DialogPage != null && ReturnToTransactionsButton != null)
                {
                    //Properties.Driver.Wait(ReturnToTransactionsButton);
                    //string transactionId = TransactionID.GetText();
                    Properties.Driver.Wait(ReturnToTransactionsButton);
                    ReturnToTransactionsButton.Click();
                    Properties.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6000);
                    //Thread.Sleep(20000);
                }
            }
            catch(Exception ex)
            {
                string exInnerMsg = ex.InnerException.Message.ToString();
                string exStackTraceMsg = ex.StackTrace.ToString();
            }

        }

        private void SaveBeneficiary(bool saveBeneficiary, string saveBeneficiaryNickName)
        {
            if (saveBeneficiary)
            {
                Properties.Driver.Wait(SaveBeneficiaryCheckBox);
                SaveBeneficiaryCheckBox.Click();
                Thread.Sleep(1000);
                BeneficiaryNickName.EnterText(saveBeneficiaryNickName);
                Thread.Sleep(1000);

            }
        }
    }
}
