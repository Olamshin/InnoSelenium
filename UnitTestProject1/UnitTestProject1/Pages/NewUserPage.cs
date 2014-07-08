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
    class NewUserPage : Page
    {
        public void addValues() 
        {
            String firstName = "Sanity";
            String lastName = "User";
            String email = "noreply@siterra.com";
            String userName = "Sanity " + DateTime.Now.Date;
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
            //isSSOUser
            //isAdmin
            //showInContactsList

            Execute.ActionOnLocator(By.Id("1190263"), e => { e.Clear(); e.SendKeys(notes); });
            Execute.ActionOnLocator(By.Id("1190252"), e => { e.Clear(); e.SendKeys(title); });
            Execute.ActionOnLocator(By.Id("1190253"), e => { e.Clear(); e.SendKeys(company); });
            Execute.ActionOnLocator(By.Id("1190245"), e => { e.Clear(); e.SendKeys(street1); });
            Execute.ActionOnLocator(By.Id("1190247"), e => { e.Clear(); e.SendKeys(city); });

            //State
            Execute.ActionOnLocator(By.Id("1190240"), e => { e.Clear(); e.SendKeys(businessPhone); });

        }
        public NewUserPage save()
        {
            Find.Element(By.LinkText("Save")).Click();
            return this;
        }
        public NewUserPage createUser() {
            addValues();
            return this;
        }
    }
}
