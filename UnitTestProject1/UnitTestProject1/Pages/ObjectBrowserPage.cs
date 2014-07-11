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
    public class ObjectBrowserPage : Page
    {
        public static string handle;

        private void SwitchIn()
        {
            Browser.SwitchTo().Frame(Find.Element(By.XPath("//iframe[contains(@id,'ifrmOb')]")));
        }

        private void SwitchOut()
        {
            Browser.SwitchTo().DefaultContent();
        }

        public ObjectBrowserPage()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=76030100&ClassID=76000000"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }

        public T Select_Object<T>(String number, T s)
        {
           ObjectBrowserGrid objgrid = this.GetComponent<ObjectBrowserGrid>();
            objgrid.Search_Object<T>(number, "Number", s);
            objgrid.Select_FirstItem();
           
            return s;
        }

    }

    public class ObjectBrowserGrid : GridComponent
    {
        protected override void SwitchIn()
        {
            Browser.SwitchTo().Frame(Find.Element(By.XPath("//iframe[contains(@id,'ifrmOb')]")));
        }

        protected override void SwitchOut()
        {
            Browser.SwitchTo().DefaultContent();
        }
    }
}
