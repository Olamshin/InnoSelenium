using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.Seleno.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestStack.Seleno.PageObjects.Actions;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1.Pages
{
    public class SiteHomePage : SiterraComponent
    {
        public SiteHomePage()
        {
            PleaseWait();
        }
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
        public SitePopup click_edit()
        {
            SwitchIn();
           return Navigate.To<SitePopup>(By.PartialLinkText("Edit"));
        }
        public SiteHomePage add_vendor()
        {
            SwitchIn();
            Find.Element(By.XPath("//a[contains(@onclick,'addVendorResponsibility()')]")).Click();
            SwitchOut();
            return this;
        }
        public ObjectBrowserPage select_vendor()
        {
            SwitchIn();
            return Navigate.To<ObjectBrowserPage>(By.XPath("//a[contains(@onclick,'openVendorOBB(-1)')]"));
        }
        public SiteHomePage Save_Vendor()
        {
            SwitchIn();
            Find.Element(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Click();
            SwitchOut();
            Host.Wait.Until<Boolean>((d) =>
            {
                try
                {
                    return !d.FindElement(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Displayed;
                }
                catch(Exception e)
                {
                    return true;
                }
            });
            return this;
        }
    }

}
