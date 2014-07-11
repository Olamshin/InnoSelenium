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
    public class EventPopup : Page
    {
        private String handle;

        public string Name
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_NAME"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public string Due_date
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_DUE_DATE"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public string Action_start_date
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_START_DATE"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public string Responsible_group
        {
            set
            {

                SelectElement search_option = new SelectElement(Find.Element(By.Id("CMB_ASSIGNED_TO_GROUP_ID")));
                search_option.SelectByText(value);
            }
        }

        public string Description
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_DESCRIPTION"), e => { e.Clear(); e.SendKeys(value); });
            }
        }


        public EventPopup()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=706030100&ClassID=706000000"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }

        public EventPopup Enter_Info(string name, string due_date, string action_start_date,string responsible_group, string description)
        {
            Name = name;
            Due_date = due_date;
            Action_start_date = action_start_date;
            Responsible_group = responsible_group;
            Description = description;
            return this;
        }

        public void Save()
        {
            string temphandle = Browser.CurrentWindowHandle;
            Find.Element(By.LinkText("Save")).Click();
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.WindowHandles.Contains(temphandle);
            });
            Host.Instance.Application.Browser.SwitchTo().Window(Host.mainWindowHandle); //Implement stack for handles
            Browser.SwitchTo().DefaultContent();
        }
    }
}
