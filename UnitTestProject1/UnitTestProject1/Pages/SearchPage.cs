using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.Seleno.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestStack.Seleno.PageObjects.Actions;
using OpenQA.Selenium.Support.UI;


namespace UnitTestProject1
{
    public class SearchPage : Page
    {
        private void Wait()
        {
            Host.Wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(10));
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("hdtbSum"));
            });
        }

        public SearchPage InputSearchTerm(string term)
        {
            Find
            .Element(By.Name("q"))
            .SendKeys(term);
            return this;
        }

        public ResultsPage Search()
        {
            var rpage= Navigate.To<ResultsPage>(By.Name("btnG"));
            Wait();
            return rpage;
        }

        public ResultsPage Feeling_lucky()
        {
            return Navigate.To<ResultsPage>(By.Name("btnI"));
           
        }

        public ImagesPage click_Images()
        {
            return Navigate.To<ImagesPage>(By.LinkText("Images"));
        }
    }
}
