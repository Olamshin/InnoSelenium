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
    public class LandingPage : Page
    {
        LoginFrame _LoginPage;
        public static String handle;

        public void PleaseWait()
        {
            Host.Wait.Until<IWebElement>((d) =>
            {
                //System.Threading.Thread.Sleep(3000);
                return d.FindElement(By.Id("contentFrame"));
            });
        }

        public LoginFrame InnerLoginPage
        {
            get
            {
                if (_LoginPage == null)
                { _LoginPage = this.GetComponent<LoginFrame>(); }
                PleaseWait();
                return _LoginPage;
            }
            set
            {
                _LoginPage = value;
            }
        }

        public DownloadsPage Click_Downloads()
        {
            PleaseWait();
            handle = Browser.CurrentWindowHandle;
            return Navigate.To<DownloadsPage>(By.LinkText("Recommended Downloads"));
        }

    }
}
