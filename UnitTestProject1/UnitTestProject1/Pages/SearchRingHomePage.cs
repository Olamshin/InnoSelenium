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
    public class SearchRingHomePage : SiterraComponent
    {
        public SearchRingHomePage()
        {
            PleaseWait();
        }
        public override void PleaseWait()
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Host.Wait.Until<Boolean>((Browser) =>
            {
                return executor.ExecuteScript("return document.readyState").Equals("complete");
            });

            SwitchIn();
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("TXT_NAME"));
            });
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.PageSource.Contains("indicator_gray.gif");
                //return !d.FindElement(By.Id("NavContainer")).ToString().Contains("indicator_gray.gif");
            });
            SwitchOut();
        }

        private void SwitchIn()
        {
            Host.Instance.Application.Browser.SwitchTo().Frame("MainFrame");
        }

        private void SwitchOut()
        {
            Host.Instance.Application.Browser.SwitchTo().DefaultContent();
        }
        public SitePopup add_site()
        {
            Actions builder = new Actions(Browser);
            builder.MoveToElement(Find.Element(By.Id("MainMenuContainer"))).Build().Perform();
            SwitchIn();
            return Navigate.To<SitePopup>(By.XPath("//a[contains(@onclick, \"addSite();\")]"));
            //return Navigate.To<SitePopup>(By.LinkText("Add"));
        }
        public SearchRingPopup edit_sr()
        {
            SwitchIn();
            return Navigate.To<SearchRingPopup>(By.XPath("//a[contains(@onclick, 'editSearchRingData()')]"));
        }
    }
}
