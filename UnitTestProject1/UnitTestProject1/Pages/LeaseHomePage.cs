using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.Seleno.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestStack.Seleno.PageObjects.Actions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace UnitTestProject1.Pages
{
    public class LeaseHomePage : SiterraComponent
    {
        //private PaymentSection _paymentsection;
        //private PaymentSection paymentSection
        //{
        //    get
        //    {
        //        if (_paymentsection == null)
        //        { _paymentsection = this.GetComponent<PaymentSection>(); }
        //        _paymentsection.Show();
        //        return _paymentsection;
        //    }
        //}
        public override void PleaseWait()
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Host.Wait.Until<Boolean>((Browser) =>
            {
                return executor.ExecuteScript("return document.readyState").Equals("complete");
            });

            SwitchIn();
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("TXT_NAME"));
            });
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.PageSource.Contains("indicator_gray.gif");
                //return !d.FindElement(By.Id("NavContainer")).ToString().Contains("indicator_gray.gif");
            });
            SwitchOut();
        }

        private void SwitchIn()
        {
            Host.Instance.Application.Browser.SwitchTo().Frame("MainFrame");
        }

        private void SwitchOut()
        {
            Host.Instance.Application.Browser.SwitchTo().DefaultContent();
        }

        public LeasePopup Click_Edit()
        {
            SwitchIn();
            return Navigate.To<LeasePopup>(By.PartialLinkText("Edit"));
        }

        public PaymentPopup Add_Payment()
        {
            SwitchIn();
            return Navigate.To<PaymentPopup>(By.XPath("//a[contains(@onclick, 'addPayment')]"));
        }

        //private class PaymentSection : SectionComponent
        //{
        //    public PaymentSection()
        //    {
        //        section_id = "114";
        //        grid_id = "727040300";
        //    }
        //    protected override void SwitchIn()
        //    {
        //        Browser.SwitchTo().Frame("MainFrame");
        //    }

        //    public PaymentSection SwitchFromPopup()
        //    {
        //        SwitchIn();
        //        return this;
        //    }

        //    protected override void SwitchOut()
        //    {
        //        Browser.SwitchTo().DefaultContent();
        //    }

        //    public PaymentSection Add_Payment()
        //    {
        //        Find.Element(By.XPath("//a[contains(@onclick,'addPayment()')]")).Click();
        //        return this;
        //    }

        //    //public ObjectBrowserPage Payment_Select_Button()
        //    //{
        //    //    return Navigate.To<ObjectBrowserPage>(By.XPath("//a[contains(@onclick,'openVendorOBB(-1)')]"));
        //    //}

        //    //public VendorSection Submit_Vendor()
        //    //{
        //    //    Find.Element(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Click();
        //    //    return this;
        //    //}

        //    public PaymentSection PleaseWait()
        //    {
        //        Host.Wait.Until<Boolean>((d) =>
        //        {
        //            return !d.FindElement(By.Id("PLACEHOLDER_" + grid_id)).Displayed;
        //            //return !d.FindElement(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Displayed;
        //        });
        //        return this;
        //    }

        //}
    }
}
