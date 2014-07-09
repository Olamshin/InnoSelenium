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
    public class UnitHomePage : SiterraComponent
    {

        public override void PleaseWait()
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Host.Wait.Until<Boolean>((Browser) =>
            {
                return executor.ExecuteScript("return document.readyState").Equals("complete");
            });

            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.PageSource.Contains("indicator_gray.gif");
                //return !d.FindElement(By.Id("NavContainer")).ToString().Contains("indicator_gray.gif");
            });
        }

        public UnitHomePage Select_Search_Ring_Grid()
        {
            PleaseWait();
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Browser.SwitchTo().Frame("MainFrame");
            Actions builder = new Actions(Browser);
            builder.MoveToElement(Find
                .Element(By.XPath("//a[@id='btnContentSelector']//span/img[@src='skins/common/images/menu_arrow_down.gif']"))
                ).Build().Perform();
            Find.Element(By.XPath("//table[@id='tblDynamicMenu1']//tr[@onclick='closeMenu(1,2);loadContents(\'searchring\', \'list\');']"))
                .Click();
            Browser.SwitchTo().DefaultContent();
            return this;
        }

        public SearchRingPopup Create_Search_Ring()
        {
            PleaseWait();
            Browser.SwitchTo().Frame("MainFrame");
            return Navigate.To<SearchRingPopup>(By.LinkText("Add"));
        }
    }
}
