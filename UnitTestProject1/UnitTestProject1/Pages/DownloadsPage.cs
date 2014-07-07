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
    public class DownloadsPage : Page
    {
        public static string handle;
        public DownloadsPage()
        {

            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(LandingPage.handle))
                {

                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("/Infrastructures/BrowserCheck/HTML/Downloads.html"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }

        public void PleaseWait()
        {
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.LinkText("Adobe© (Macromedia©) Flash Player"));
            });
        }

        public String CheckFileDownload()
        {
            PleaseWait();
            return Find.Element(By.LinkText("Adobe© (Macromedia©) Flash Player")).GetAttribute("href");
        }

    }
}
