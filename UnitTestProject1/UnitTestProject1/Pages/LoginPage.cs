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
    public class LoginFrame : UiComponent
    {

        public string username
        {
            set
            {
                Browser.SwitchTo().Frame("contentFrame");
                Execute.ActionOnLocator(By.Name("tb_login"), e => { e.Clear(); e.SendKeys(value); });
                Browser.SwitchTo().DefaultContent();
            }
        }

        public string password
        {
            set
            {
                Browser.SwitchTo().Frame("contentFrame");
                Execute.ActionOnLocator(By.Name("tb_password"), e => { e.Clear(); e.SendKeys(value); });
                Browser.SwitchTo().DefaultContent();
            }
        }

        public string domain
        {
            set
            {
                Browser.SwitchTo().Frame("contentFrame");
                Execute.ActionOnLocator(By.Name("tb_domain"), e => { e.Clear(); e.SendKeys(value); });
                Browser.SwitchTo().DefaultContent();
            }
        }

        public void PleaseWait()
        {
            Browser.SwitchTo().Frame("contentFrame");
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("btnLoginFancy"));
            });
            Browser.SwitchTo().DefaultContent();

        }
        public MainPage Click_Login()
        {
            Browser.SwitchTo().Frame("contentFrame");
            return Navigate.To<MainPage>(By.Id("btnLoginFancy"));
        }
        public MainPage Login()
        {
            PleaseWait();

            username = "admin_user";
            password = "Potato3$";
            domain = "Orbitel";

            return Click_Login();
            
        }
    }
}
