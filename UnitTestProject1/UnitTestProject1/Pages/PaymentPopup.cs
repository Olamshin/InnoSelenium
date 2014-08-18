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
    public class PaymentPopup : Page
    {
        private String handle;

        private string pmtName
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_NAME"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        private string pmtNumber
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_NO"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        private string pmtFromDate
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_FROM_DATE"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        private string pmtSecondDate
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_SECOND_PAYMENT_DATE"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        private string pmtToDate
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_TO_DATE"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        private string pmtAmount
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_PAYMENT_FIXED"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public PaymentPopup()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=723030600&ClassID=723000000"))
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

        public PaymentPopup Enter_Info(string name, string type, string firstDate, string secondDate, string toDate, string amount)
        {
            pmtName = name;
            select_type(type);
            pmtFromDate = firstDate;
            pmtSecondDate = secondDate;
            pmtToDate = toDate;
            PleaseWait();
            pmtAmount = amount;

            return this;
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

        public void select_type(String pmtType)
        {
            IWebElement dropDownListBox = Find.Element(By.Id("CMB_TYPE_ID"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText(pmtType);
        }
    }
}
