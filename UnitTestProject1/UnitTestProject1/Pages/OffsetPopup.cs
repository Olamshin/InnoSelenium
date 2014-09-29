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
    public class OffsetPopup : Page
    {
        private String handle;

        private string Type
        {
            set
            {

                SelectElement search_option = new SelectElement(Find.Element(By.Id("CMB_OFFSET_TYPE_ID")));
                search_option.SelectByText(value);
            }
        }

        private string GLAccount
        {
            set
            {

                SelectElement search_option = new SelectElement(Find.Element(By.Id("CMB_GL_ACCOUNT_ID")));
                search_option.SelectByText(value);
            }
        }

        public string Number
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_NO"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        private string SubAddOperator
        {
            set
            {
                SelectElement search_option = new SelectElement(Find.Element(By.Id("CMB_IS_POSITIVE")));
                search_option.SelectByText(value);
            }
        }

        private string FixedPercent
        {
            set
            {
                SelectElement search_option = new SelectElement(Find.Element(By.Id("CMB_IS_BY_PERCENT")));
                search_option.SelectByText(value);
            }
        }

        private string OffsetAmount
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_AMOUNT"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        private string OngoingMethod
        {
            set
            {
                SelectElement search_option = new SelectElement(Find.Element(By.Id("CMB_METHOD")));
                search_option.SelectByText(value);
            }
        }

        private string Description
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_DESCRIPTION"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public OffsetPopup()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=743030100&ClassID=743000000"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }

        public OffsetPopup Enter_Info(string type, string glaccount, string number, string subaddoperator, string fixedpercent, string offsetamount)
        {
            Type = type;
            GLAccount = glaccount;
            Number = number;
            SubAddOperator = subaddoperator;
            FixedPercent = fixedpercent;
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
    }
}
