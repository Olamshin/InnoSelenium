using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.Seleno;
using TestStack.Seleno.Configuration;
using TestStack.Seleno.Configuration.WebServers;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.Seleno.PageObjects;
using System.Collections.ObjectModel;
namespace UnitTestProject1
{
    
    public static class Host
    {
        public static SelenoHost Instance;
        private static InternetExplorerOptions options = new InternetExplorerOptions();
        public static WebDriverWait Wait;
        public static string mainWindowHandle;
        public static System.Collections.ObjectModel.ReadOnlyCollection<Cookie> InitialCookies;
    }
    public abstract class SiterraComponent : UiComponent
    {
        public abstract void PleaseWait();
    }

   public abstract class GridComponent : UiComponent
   {
       private String _gridid;
       public string grid_id
       {
           get
           {
               String idholder;
               if (_gridid == null) //object browser page
               {
                   idholder = Find.Element(By.XPath("//table[contains(@id, 'GRID_DATA_')]")).GetAttribute("id");
                   _gridid = idholder.Replace("GRID_DATA_", ""); ;
               }
               return _gridid;
           }
           set
           {
               _gridid = value;
           }
       }
       //public abstractstring grid_id { get; set; }
       public string search_input
       {
           set
           {
               SwitchIn();
               Execute.ActionOnLocator(By.XPath("//input[contains(@id,'TXT_SEARCH_')]"), e => { e.Clear(); e.SendKeys(value); });
               SwitchOut();
           }
       }

       protected abstract void SwitchIn();
       protected abstract void SwitchOut();

       public T Search_Object<T>(String searchinput, String searchtype, T s)
       {
           SwitchIn();
           SelectElement search_option = new SelectElement(Find.Element(By.XPath("//select[contains(@id,'CBO_SEARCH_')]")));
           search_option.SelectByText(searchtype);
           SwitchOut();
           search_input = searchinput;
           Click_Search();
           return s;
       }

       public void Select_Object(String columnname, String value)
       {
           IWebElement grid_table;
           grid_table = Find.Element(By.Id("GRID_DATA_"+grid_id));
           ReadOnlyCollection<IWebElement> grid_table_rows = grid_table.FindElements(By.XPath("//tr[@id='GRID_ROW']"));
           IEnumerable<IWebElement> grid_row = grid_table_rows.Where(row => row.Text.Contains(value));
           foreach(IWebElement r in grid_row)
           {
               IWebElement a = r.FindElement(By.XPath("//td[@class='Cell']/a[contains(@id,'"+columnname.ToUpper()+"']"));
           }
       }

       public GridComponent Select_FirstItem()
       {
           SwitchIn();
           Find.Element(By.XPath("//table[@id='GRID_DATA_"+grid_id+"']/tbody/tr[1]/td/a[@Class='ShortButton']"))
               .Click();
           Host.Instance.Application.Browser.SwitchTo().Window(Host.mainWindowHandle); //Implement stack for handles
           SwitchOut();
           System.Threading.Thread.Sleep(3000);
           return this;
       }

       private GridComponent Click_Search()
       {
           SwitchIn();
           Find.Element(By.XPath("//a[contains(@onclick,'getGridByID(') and contains(@onclick,'.ApplySearch()')]"))
               .Click();
           SwitchOut();
           return this;
       }

       public Boolean ExistsInGrid(String number)
       {
           IWebElement grid_table;
           SwitchIn();
           //s=Find.Element(By.XPath("//table[contains(@id, 'GRID_DATA_')]//tbody//tr//td//a//G_VALUE[. = 'Bescoby']")).Text;
           grid_table = Find.Element(By.Id("GRID_DATA_"+grid_id));
           ReadOnlyCollection<IWebElement> grid_table_rows = grid_table.FindElements(By.XPath("//tr[@id='GRID_ROW']"));
           IEnumerable<IWebElement> grid_row = grid_table_rows.Where(row => row.Text.Contains(number));
           if (grid_row.Count() > 0)
           {
               SwitchOut();
               return true;
           }
           else
           {
               SwitchOut();
               return false;
           }
       }
   }
}
