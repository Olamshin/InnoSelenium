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
    public class LeasePopup : Page
    {
        private String handle;

        public string ctrName
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_NAME"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public string ctrNumber
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_NO"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public string ctrDescription
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_STATUS_DESCRIPTION"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public LeasePopup()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=716030200&ClassID=716000000"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }

        public void PleaseWait()
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Host.Wait.Until<Boolean>((Browser) =>
            {
                return executor.ExecuteScript("return document.readyState").Equals("complete");
            });
            
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.XPath("//div[contains(@id, 'DataDivContainer')]"));
            });
        }

        public void Save()
        {
            string temphandle = Browser.CurrentWindowHandle;
            Find.Element(By.LinkText("Save")).Click();
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.WindowHandles.Contains(temphandle);
            });
            Host.Instance.Application.Browser.SwitchTo().Window(Host.mainWindowHandle); //Implement stack for handles
            Browser.SwitchTo().DefaultContent();
        }

    }
}
