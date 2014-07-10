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
            //switchOut();
            //switchIn();
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("CHK_MATCH_WHOLE_754040200"));
            });
            //switchOut();
        }
        public void switchIn() 
        {
            Browser.SwitchTo().Frame("MainFrame");
        }
        public void switchOut()
        {
            Browser.SwitchTo().DefaultContent();
        }
        public NewUserPage addUser()
        {
           /* PleaseWait();
            Find.Element(By.LinkText("Add")).Click();*/
            //System.Threading.Thread.Sleep(5000);

            PleaseWait();
            System.Threading.Thread.Sleep(3000);
            switchOut();
            switchIn();
            return Navigate.To<NewUserPage>(By.LinkText("Add"));
        }
        public UserComp findUser(String userName, String searchBy)
        {
            PleaseWait();
            //switchOut();
            switchIn();
            
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
            clickThis.SelectByText(searchBy);

            //Click Search
            Find.Element(By.LinkText("Search")).Click();

            System.Threading.Thread.Sleep(4000);
            PleaseWaitForSearch();
            switchOut();

            return this;
        }
        public NewUserPage selectFirstUser() {
            /*OpenQA.Selenium.
                Click("//a[starts-with(@id, '754040200_')]");
            Find.Element(By.Id("754040200_%")).Click();*/
            switchIn();
            PleaseWaitForSearch();
            System.Threading.Thread.Sleep(2000);

            return Navigate.To<NewUserPage>(By.CssSelector("a[id$='_DISPLAY_NAME']"));
                
        }

        
    }
}
