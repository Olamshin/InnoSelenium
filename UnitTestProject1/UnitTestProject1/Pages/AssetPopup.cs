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
    public class AssetPopup : Page
    {
        private String handle;

        public string name
        {
            set
            {
                Execute.ActionOnLocator(By.Name("tb_login"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public AssetPopup()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=711030100&ClassID=711000000"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }

        public AssetPopup Select_Type()
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Host.Wait.Until<Boolean>((Browser) =>
            {
                return executor.ExecuteScript("return document.readyState").Equals("complete");
            });
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("SectionForm"));
            });
            return Navigate.To<AssetPopup>(By.LinkText("Next"));
            //Find.Element(By.LinkText("Next")).Click();
            //System.Threading.Thread.Sleep(6000);
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

    }
}
