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
    public class ProjectPopup :Page
    {
        private String handle;

        private string Name
        {
            set
            {
                Execute.ActionOnLocator(By.Id("1190210"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        private string Start_date
        {
            set
            {
                Execute.ActionOnLocator(By.Id("1190211"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        private string Status
        {
            set
            {
                SelectElement search_option = new SelectElement(Find.Element(By.Id("1190209")));
                search_option.SelectByText(value);
            }
        }

        public ProjectPopup()
        {
            System.Threading.Thread.Sleep(1000); //implement for the future
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=703030200&ClassID=703000000"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }

        public ProjectPopup Select_Template()
        {

            SelectElement template_option = new SelectElement(Find.Element(By.Id("CMB_TEMPLATE_ID")));
            foreach (IWebElement elem in template_option.Options)
            {
                if(!elem.GetAttribute("value").Equals("-1"))
                {
                    template_option.SelectByValue(elem.GetAttribute("value"));
                    break;
                }
            }
            return Navigate.To<ProjectPopup>(By.LinkText("Next")); ;
        }

        public ProjectPopup Select_Template(string value)
        {

            SelectElement template_option = new SelectElement(Find.Element(By.Id("CMB_TEMPLATE_ID")));
            template_option.SelectByText(value);
            return Navigate.To<ProjectPopup>(By.LinkText("Next")); ;
        }

        public ProjectPopup Enter_Info(String name, String start_date, String status)
        {
            Name = name;
            Start_date = start_date;
            Status = status;
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
