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
            Browser.SwitchTo().DefaultContent();
            try //is browser already in mainframe?
            {
                Browser.SwitchTo().Frame("MainFrame");
                Host.Wait.Until<Boolean>((d) =>
                {
                    return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/add.gif");
                });
            }
            catch
            { //browser is already in mainframe
                Host.Wait.Until<Boolean>((d) =>
                {
                    return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/add.gif");
                });
            }
            
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
            System.Threading.Thread.Sleep(3000);
            Browser.SwitchTo().DefaultContent();
            Browser.SwitchTo().Frame("MainFrame");
            return Navigate.To<NewUserPage>(By.LinkText("Add"));
        }
        public UserComp findUser(String userName)
        {
            PleaseWait();
            Browser.SwitchTo().DefaultContent();
            Browser.SwitchTo().Frame("MainFrame");
            
            try
            {
                //Is search already open?
                Find.Element(By.Id("TXT_SEARCH_FOR_754040200"));
            }
            catch
            {
                //If not, Open up search
                Find.Element(By.Id("IMG_SEARCH_754040200")).Click();

                PleaseWaitForSearch();
            }
            //Enter search Parameters
            Execute.ActionOnLocator(By.Id("TXT_SEARCH_FOR_754040200"), e => { e.Clear(); e.SendKeys(userName); });

            IWebElement dropDownListBox = Find.Element(By.Id("CBO_SEARCH_754040200"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText("User Name");

            //Click Search
            Find.Element(By.LinkText("Search")).Click();

            System.Threading.Thread.Sleep(2000);
            Browser.SwitchTo().DefaultContent();

            return this;
        }
        public void selectFirstUser() {
            /*OpenQA.Selenium.
                Click("//a[starts-with(@id, '754040200_')]");
            Find.Element(By.Id("754040200_%")).Click();*/
            PleaseWaitForSearch();
            Find.Element(By.CssSelector("a[id$='_DISPLAY_NAME']")).Click();
                
        }
        
    }
}
