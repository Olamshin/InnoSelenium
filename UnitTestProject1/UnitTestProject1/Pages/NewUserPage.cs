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
    public class NewUserPage : Page
    {
        public static String handle;

        public NewUserPage()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(LandingPage.handle))
                {

                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=754030300&ClassID=754000000&ObjectID=-1"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }
        public void PleaseWait()
        {
          
            Host.Wait.Until<Boolean>((d) =>
            {
                return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/disk.gif");
            });
        }
        public string addValues() 
        {
            String firstName = "Sanity";
            String lastName = "Test User";
            String email = "noreply@siterra.com";
            String userName = "Sanity_" + DateTime.Now.Ticks.ToString();
            String password = "q1w2e3r4t5y6";
            Boolean isSSOUser = false;
            Boolean isAdmin = true;
            Boolean showInContactsList = true;     
            String notes = "note";
            String title = "team Princess";
            String company = "accruent";
            String street1 = "Lake Travis rd";
            String city = "Austin";
            String state = "Texas";
            String businessPhone = "111-222-3333";

            Execute.ActionOnLocator(By.Id("1190254"), e => { e.Clear(); e.SendKeys(firstName); });
            Execute.ActionOnLocator(By.Id("1190255"), e => { e.Clear(); e.SendKeys(lastName); });
            Execute.ActionOnLocator(By.Id("1190256"), e => { e.Clear(); e.SendKeys(email); });
            Execute.ActionOnLocator(By.Id("1190257"), e => { e.Clear(); e.SendKeys(userName); });
            Execute.ActionOnLocator(By.Id("1190258"), e => { e.Clear(); e.SendKeys(password); });
            Execute.ActionOnLocator(By.Id("1190261"), e => { e.Clear(); e.SendKeys(password); });
            //isSSOUser -- Don't click
            //isAdmin
            Find.Element(By.Id("1190264")).Click();
            //*don't* showInContactsList
            //Find.Element(By.Id("1190259")).Click();

            Execute.ActionOnLocator(By.Id("1190263"), e => { e.Clear(); e.SendKeys(notes); });
            Execute.ActionOnLocator(By.Id("1190252"), e => { e.Clear(); e.SendKeys(title); });
            Execute.ActionOnLocator(By.Id("1190253"), e => { e.Clear(); e.SendKeys(company); });
            Execute.ActionOnLocator(By.Id("1190245"), e => { e.Clear(); e.SendKeys(street1); });
            Execute.ActionOnLocator(By.Id("1190247"), e => { e.Clear(); e.SendKeys(city); });

            //State
            IWebElement dropDownListBox = Find.Element(By.Id("1190249"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText(state);

            Execute.ActionOnLocator(By.Id("1190240"), e => { e.Clear(); e.SendKeys(businessPhone); });

            return userName;

        }
        public NewUserPage save()
        {
            Find.Element(By.LinkText("Save")).Click();
            return this;
        }
        public string createUser() {
            PleaseWait();
            String userName = addValues();
            save();
            //System.Threading.Thread.Sleep(2000);
            return userName;
        }
    }
}
