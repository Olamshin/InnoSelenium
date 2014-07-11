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
    public class IncidentPopup: Page
    {
        private String handle;

        public string Problem
        {
            set
            {
                Execute.ActionOnLocator(By.Name("1190161"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        public string AssignedToUser
        {
            set
            {
                
                SelectElement search_option = new SelectElement(Find.Element(By.Id("1190157")));
                search_option.SelectByText(value);
            }
        }

        public IncidentPopup()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=705030100&ClassID=705000000"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }

        public IncidentPopup Enter_Info(string problem, string assigned2User)
        {
            AssignedToUser = assigned2User;
            Problem = problem;
            return this;
        }

        public IncidentPopup Select_Type()
        {
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("SectionForm"));
            });
            return Navigate.To<IncidentPopup>(By.LinkText("Next"));
            //Find.Element(By.LinkText("Next")).Click();
            //System.Threading.Thread.Sleep(6000);
        }

        public void Save()
        {
            string temphandle = Browser.CurrentWindowHandle;
            Find.Element(By.LinkText("Finish")).Click();
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.WindowHandles.Contains(temphandle);
            });
            Host.Instance.Application.Browser.SwitchTo().Window(Host.mainWindowHandle); //Implement stack for handles
            Browser.SwitchTo().DefaultContent();
        }
    }
}
