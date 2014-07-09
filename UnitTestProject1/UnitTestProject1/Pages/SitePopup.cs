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
    public class SitePopup : Page
    {
        private String handle;

        public string address
        {
            set
            {   
                Execute.ActionOnLocator(By.Name("1190183"), e => { e.Clear(); e.SendKeys(value); }); 
            }
        }
        public string siteName
        {
            set
            {
                Execute.ActionOnLocator(By.Id("1190170"), e => { e.Clear(); e.SendKeys(value); });
            }
        }
        public string siteNumber
        {
            set
            {
                Execute.ActionOnLocator(By.Id("1190164"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public SitePopup()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=701030300&ClassID=701000000"))
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
                return d.FindElement(By.Id("editForm"));
            });
        }
        public void Save()
        {
            Find.Element(By.LinkText("Finish")).Click();
            Host.Instance.Application.Browser.SwitchTo().Window(Host.mainWindowHandle); //Implement stack for handles
            Browser.SwitchTo().DefaultContent();
            //return Navigate.To<SiteHomePage>(By.LinkText("Finish"));
        }
        public SitePopup select_type()
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Host.Wait.Until<Boolean>((Browser) =>
            {
                return executor.ExecuteScript("return document.readyState").Equals("complete");
            });
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("popupHeaderLinks"));
            });
            return Navigate.To<SitePopup>(By.LinkText("Next"));
            //System.Threading.Thread.Sleep(6000);
            //return this;
        }
    }
}
