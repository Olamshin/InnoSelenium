using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestStack.Seleno.PageObjects.Actions;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1.Pages
{
    public class ObjectBrowserPage :Page
    {
        public static string handle;

        public string search_input
        {
            set
            {
                Browser.SwitchTo().Frame(Find.Element(By.XPath("//iframe[contains(@id,'ifrmOb')]")));
                Execute.ActionOnLocator(By.XPath("//input[contains(@id,'TXT_SEARCH_')]"), e => { e.Clear(); e.SendKeys(value); });
                Browser.SwitchTo().DefaultContent();
            }
        }


        public ObjectBrowserPage()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(LandingPage.handle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=76030100&ClassID=76000000"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }

        public T Select_Object<T>(T s)
        {   
            Browser.SwitchTo().Frame(Find.Element(By.XPath("//iframe[contains(@id,'ifrmOb')]")));
            SelectElement search_option=new SelectElement(Find.Element(By.XPath("//select[contains(@id,'CBO_SEARCH_')]")));
            search_option.SelectByText("Number");
            Browser.SwitchTo().DefaultContent();
            search_input = "645430";
            Click_Search();
            Select_FirstItem();
            
            return s;
        }

        public ObjectBrowserPage Select_FirstItem()
        {
            Browser.SwitchTo().Frame(Find.Element(By.XPath("//iframe[contains(@id,'ifrmOb')]")));
            Find.Element(By.XPath("//table[contains(@id, 'GRID_DATA_')]/tbody/tr[1]/td/a[@Class='ShortButton']"))
                .Click();
            Host.Instance.Application.Browser.SwitchTo().Window(Host.mainWindowHandle); //Implement stack for handles
            Browser.SwitchTo().DefaultContent();
            System.Threading.Thread.Sleep(3000);
            return this;
        }

        public ObjectBrowserPage Click_Search()
        {
            Browser.SwitchTo().Frame(Find.Element(By.XPath("//iframe[contains(@id,'ifrmOb')]")));
            Find.Element(By.XPath("//a[contains(@onclick,'getGridByID(') and contains(@onclick,'.ApplySearch()')]"))
                .Click();
            Browser.SwitchTo().DefaultContent();
            return this;
        }
    }
}
