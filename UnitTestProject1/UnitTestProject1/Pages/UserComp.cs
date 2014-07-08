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
    class UserComp : SiterraComponent
    {
        

        public override void PleaseWait()
        {
            Browser.SwitchTo().Frame("MainFrame");
            Host.Wait.Until<Boolean>((d) =>
            {
                return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/add.gif");
            });
            Browser.SwitchTo().DefaultContent();
        }
        public void PleaseWaitForSearch() 
        {
            Browser.SwitchTo().DefaultContent();
            Browser.SwitchTo().Frame("MainFrame");
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("CHK_MATCH_WHOLE_754040200"));
            });
            Browser.SwitchTo().DefaultContent();
        }
        public NewUserPage addUser()
        {
           /* PleaseWait();
            Find.Element(By.LinkText("Add")).Click();*/
            //System.Threading.Thread.Sleep(5000);

            PleaseWait();
            Browser.SwitchTo().DefaultContent();
            Browser.SwitchTo().Frame("MainFrame");
            return Navigate.To<NewUserPage>(By.LinkText("Add"));
        }
        public UserComp findUser(String userName)
        {
            PleaseWait();
            Browser.SwitchTo().Frame("MainFrame");
            //Open up search
            //Find.Element(By.Id("IMG_SEARCH_754040200")).Click();

            PleaseWaitForSearch();
            //Enter search Parameters
            Execute.ActionOnLocator(By.Id("TXT_SEARCH_FOR_754040200"), e => { e.Clear(); e.SendKeys(userName); });

            IWebElement dropDownListBox = Find.Element(By.Id("CBO_SEARCH_754040200"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText("User Name");

            /*//Click Search
            Find.Element(By.LinkText("Search")).Click();*/

            System.Threading.Thread.Sleep(2000);

            return this;
        }
    }
}
