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
    public class SearchRingPopup : Page
    {
        private String handle;

        public string srAddress
        {
            set
            {
                Execute.ActionOnLocator(By.Id("1190194"), e => { e.Clear(); e.SendKeys(value); });
            }
        }
        public string srName
        {
            set
            {
                Execute.ActionOnLocator(By.Id("1190206"), e => { e.Clear(); e.SendKeys(value); });
            }
        }
        public string srNumber
        {
            set
            {
                Execute.ActionOnLocator(By.Id("1190203"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public SearchRingPopup()
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
                return d.FindElement(By.XPath("//div[contains(@id, 'DataDivContainer')]"));
            });
        }
        public void Save()
        {
            string temphandle = Browser.CurrentWindowHandle;
            Find.Element(By.LinkText("Finish")).Click();
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.WindowHandles.Contains(temphandle);
            });
            Host.Instance.Application.Browser.SwitchTo().Window(Host.mainWindowHandle); //Implement stack for handles
            Browser.SwitchTo().DefaultContent();
        }

        public SearchRingPopup select_type(string srType)
        {
            PleaseWait();

            IWebElement dropDownListBox = Find.Element(By.Id("CMB_TYPE_ID"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText(srType);

            return Navigate.To<SearchRingPopup>(By.LinkText("Next"));
        }
    }
}
