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
        public static String handle;

        public override void PleaseWait()
        {

            Host.Wait.Until<Boolean>((d) =>
            {
                return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/add.gif");
            });
        }
        public NewUserPage addUser()
        {
           /* PleaseWait();
            Find.Element(By.LinkText("Add")).Click();*/
            //System.Threading.Thread.Sleep(5000);

            PleaseWait();
            handle = Browser.CurrentWindowHandle;
            return Navigate.To<NewUserPage>(By.LinkText("Add"));
        }
    }
}
