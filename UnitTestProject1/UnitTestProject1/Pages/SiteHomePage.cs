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
        public override void PleaseWait()
        {
            Browser.SwitchTo().Frame("MainFrame");
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.PageSource.Contains("indicator_gray.gif");
                //return !d.FindElement(By.Id("NavContainer")).ToString().Contains("indicator_gray.gif");
            });
            Browser.SwitchTo().DefaultContent();
        }

        public SitePopup click_edit()
        {
            Browser.SwitchTo().Frame("MainFrame");
           return Navigate.To<SitePopup>(By.PartialLinkText("Edit"));
        }
        public SiteHomePage add_vendor()
        {
            Browser.SwitchTo().Frame("MainFrame");
            IWebElement a = Find.Element(By.XPath("//a[@onclick='addVendorResponsibility();return false;']"));
            Browser.SwitchTo().DefaultContent();
            return this;
        }
    }

}
