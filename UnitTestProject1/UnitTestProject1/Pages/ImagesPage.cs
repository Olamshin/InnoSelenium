using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.Seleno.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace UnitTestProject1
{
    public class ImagesPage : Page
    {

        private void Wait()
        {
            Host.Wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(10));
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("gbq1"));
            });
        }

        public ImagesPage InputSearchTerm(string term)
        {
            Find
            .Element(By.Name("q"))
            .SendKeys(term);
            return this;
        }

        public ImagesPage Press_enter()
        {
            Find
            .Element(By.Name("q"))
            .SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            return this;
        }

        public ImagesPage Click_Image(int index)
        {
            Find
            .Element(By.Id("rg_s"))
            .FindElement(By.XPath("//div[@data-ri='"+index.ToString()+"']"))
            .Click();
            Wait();
            return this;
        }

        public SearchPage goHome()
        {
            return Navigate.To<SearchPage>(By.Id("gbq1"));

        }
    }
}
