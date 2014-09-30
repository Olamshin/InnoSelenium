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
    public class ProjectEditSchedulePage : SiterraComponent
    {
        public ProjectEditSchedulePage()
        {
            /*this is due to the projectHomepage calling this page.
             * When ProjectHomepage calls this page, it has to switch in but cannot switch out
             * because of the return statement.
            */
            SwitchOut();
        }

        public enum TaskColumns
        {
            Milestone = 1,
            Lock = 2,
            Forecast = 3,
            Baseline = 4,
            Manual = 5,
            Completion = 6,
            Duration = 7,
            Predecessor = 8
        }
        Dictionary<int, string> dict =
        new Dictionary<int, string>() { 
            {2, "CHK_IS_LOCKED_"},
            {3, "TXT_PLANNED_DATE_"},
            {4, "TXT_REVISED_DATE_"},
            {5, "TXT_EXTERNAL_DATE_"},
            {6, "LBL_DISPLAY_DATE_"},
            {7, "TXT_DURATION_"}
        };

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


        private string getTaskID(string number)
        {
            string id = null;

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


        public ProjectEditSchedulePage Edit_Task(string tasknumber, TaskColumns t, string date)
        {
            string task_id = null;
            task_id = getTaskID(tasknumber);

            SwitchIn();
            IWebElement e = Find.Element(By.Id(dict[(int)t] + task_id));
            e.Clear();
            e.SendKeys(date);
            SwitchOut();
            return this;
        }

        public ProjectEditSchedulePage Commit_Changes()
        {
            SwitchIn();
            Find.Element(By.LinkText("Commit")).Click();
            SwitchOut();
            return this;
        }

        public Boolean Compare_Date(string tasknumber, TaskColumns t, string assertdate)
        {
            string task_id = null;
            task_id = getTaskID(tasknumber);
            Boolean flag;

            SwitchIn();
            string temp = Find.Element(By.Id(dict[(int)t] + task_id)).Text.Trim();
            DateTime d = Convert.ToDateTime(temp);
            DateTime a = Convert.ToDateTime(assertdate);
            if (d == a)
                flag = true;
            else
                flag = false;
            SwitchOut();

            return flag;
        }
    }
}
