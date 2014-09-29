using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.Seleno.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestStack.Seleno.PageObjects.Actions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace UnitTestProject1.Pages
{
    public class ProjectHomePage : SiterraComponent
    {
        public override void PleaseWait()
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Host.Wait.Until<Boolean>((Browser) =>
            {
                return executor.ExecuteScript("return document.readyState").Equals("complete");
            });

            SwitchIn();
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("DataDivContainerview"));
            });
            SwitchOut();
        }

        private void SwitchIn()
        {
            Host.Instance.Application.Browser.SwitchTo().Frame("MainFrame");
        }

        private void SwitchOut()
        {
            Host.Instance.Application.Browser.SwitchTo().DefaultContent();
        }

        public ProjectHomePage Edit_Task()
        {
            return this;
        }
        private void gotoEditScheduleTab()
        {
            SwitchIn();
            try
            {
                Find.OptionalElement(By.Id("DIV_EDIT_BUTTONS"));
            }
            catch
            {
                Find.Element(By.Id("ScheduleEditTab")).Click();
                SwitchOut();
            }

            SwitchOut();
        }
        private string getTaskID(string number)
        {
            string id = null;

            gotoEditScheduleTab();
            SwitchIn();
            IEnumerable<IWebElement> elems = Find.Elements(By.CssSelector(".ScheduleTable tbody tr[id*=\"Task\"]"));
            foreach (IWebElement e in elems)
            {
                IWebElement z = e.FindElement(By.CssSelector("td:nth-child(1)"));
                if (number == z.Text.Trim())
                {
                    id = e.GetAttribute("id").Replace("Task", "");
                    break;
                }
            }
            SwitchOut();
            return id;

        }
        public ProjectHomePage Edit_Baseline(string tasknumber, string date)
        {
            string task_id = null;
            task_id = getTaskID(tasknumber);
            gotoEditScheduleTab();
            SwitchIn();
            IWebElement e = Find.Element(By.Id("TXT_REVISED_DATE_" + task_id));
            e.Clear();
            e.SendKeys(date);
            SwitchOut();
            return this;
        }

        public ProjectHomePage Edit_Forcast(string tasknumber, string date)
        {
            string task_id = null;
            task_id = getTaskID(tasknumber);
            gotoEditScheduleTab();
            SwitchIn();
            IWebElement e = Find.Element(By.Id("TXT_PLANNED_DATE_" + task_id));
            e.Clear();
            e.SendKeys(date);
            SwitchOut();
            return this;
        }


        public ProjectHomePage Edit_Manual(string tasknumber, string date)
        {
            string task_id = null;
            task_id = getTaskID(tasknumber);
            gotoEditScheduleTab();
            SwitchIn();
            IWebElement e = Find.Element(By.Id("TXT_EXTERNAL_DATE_" + task_id));
            e.Clear();
            e.SendKeys(date);
            SwitchOut();
            return this;
        }
        public ProjectHomePage Edit_Duration(string tasknumber, string duration)
        {
            string task_id = null;
            task_id = getTaskID(tasknumber);
            gotoEditScheduleTab();
            SwitchIn();
            IWebElement e = Find.Element(By.Id("TXT_DURATION_" + task_id));
            e.Clear();
            e.SendKeys(duration);
            SwitchOut();
            return this;
        }
        public ProjectHomePage Commit_Changes()
        {
            SwitchIn();
            Find.Element(By.LinkText("Commit")).Click();
            System.Threading.Thread.Sleep(3000);
            SwitchOut();
            return this;
        }

    }
}
