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
            PleaseWaitForSearch();
            return s;
        }

        public void Select_Object(String columnname, String value)
        {
            SwitchIn();
            IWebElement grid_table, a = null;
            grid_table = Find.Element(By.Id("GRID_DATA_" + grid_id));
            ReadOnlyCollection<IWebElement> grid_table_rows = grid_table.FindElements(By.XPath("//tr[@id='GRID_ROW']"));
            IEnumerable<IWebElement> grid_row = grid_table_rows.Where(row => row.Text.Contains(value));

            foreach (IWebElement r in grid_row)
            {
                a = r.FindElement(By.XPath("//td[@class='Cell']/a[contains(@id,'" + columnname.ToUpper() + "')]"));
            }

            a.Click();
            SwitchOut();

        }

        public GridComponent Select_FirstItem()
        {
            SwitchIn();
            Find.Element(By.XPath("//table[@id='GRID_DATA_" + grid_id + "']/tbody/tr[1]/td/a[@Class='ShortButton']"))
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
            PleaseWaitForSearch();
            return this;
        }

        public void PleaseWaitForSearch()
        {
            System.Threading.Thread.Sleep(1500); //implement for the future
            //SwitchIn();
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("CHK_MATCH_WHOLE_" + grid_id));
            });
            //SwitchOut();
        }

        public Boolean ExistsInGrid(String columnname, String value)
        {
            int column_index = -1;
            IWebElement grid_table;
            IWebElement a = null;
            Boolean isCheckBox;
            ReadOnlyCollection<IWebElement> grid_header, grid_table_rows, grid_row_data;
            PleaseWaitForSearch();

            //SwitchIn();
            //s=Find.Element(By.XPath("//table[contains(@id, 'GRID_DATA_')]//tbody//tr//td//a//G_VALUE[. = 'Bescoby']")).Text;
            grid_table = Find.Element(By.Id("GRID_DATA_" + grid_id));
            IWebElement grid_table_header = grid_table.FindElement(By.XPath("./thead/tr[@class='GridHeader']"));
            grid_header = grid_table_header.FindElements(By.XPath("./td[@class='HeaderCell']"));
            try
            {
                isCheckBox = grid_table_header.FindElement(By.Id("CHECKBOX_HEADER")).Displayed;
            }
            catch
            {
                isCheckBox = false;
            }
            var grid_header_enumerator = grid_header.GetEnumerator();
            for (int i = (isCheckBox ? 0 : -1); grid_header_enumerator.MoveNext(); i++)
            {
                if (grid_header_enumerator.Current.Text.Equals(columnname))
                {
                    column_index = i;
                    break;
                }
            }

            if (column_index == -1)
            {
                Console.WriteLine("Column Does Not Exists");
                return false;
            }
            grid_table_rows = grid_table.FindElements(By.XPath("./tbody/tr[@id='GRID_ROW']"));
            IEnumerable<IWebElement> grid_row = grid_table_rows.Where(row => row.Text.Contains(value));
            var grid_row_enum = grid_row.GetEnumerator();
            for (int i = 0; grid_row_enum.MoveNext(); i++)
            {
                //Get all cells from the row
                grid_row_data = grid_row_enum.Current.FindElements(By.XPath("./td[@class='Cell']"));
                var grid_data_enum = grid_row_data.GetEnumerator();

                for (int j = 0; grid_data_enum.MoveNext(); j++)
                {
                    if (j == column_index)
                    {
                        a = grid_data_enum.Current;
                        break;
                    }
                }
            }

            if (a.Text.Equals(value))
            {
                //SwitchOut();
                return true;
            }
            else
            {
                //SwitchOut();
                return false;
            }
        }
    }

    public abstract class SectionComponent : GridComponent
    {
        private String _sectionid;
        public string section_id
        {
            get
            {
                String idholder;
                if (_sectionid == null) //object browser page
                {
                    idholder = Find.Element(By.XPath("//table[contains(@id, 'GRID_DATA_')]")).GetAttribute("id");
                    _sectionid = idholder.Replace("GRID_DATA_", ""); ;
                }
                return _sectionid;
            }
            set
            {
                _sectionid = value;
            }
        }

        public void Show()
        {

            IWebElement elem = null;
            try
            {
                elem = Find.Element(By.Id("DataDivContainer" + _sectionid));
            }
            catch
            {

            }

            if (elem != null)
            {
                if(!elem.Displayed)
                {
                    try
                    {
                        Find.Element(By.Id("SectionHeader" + _sectionid)).Click();
                    }
                    catch { }
                }
            }
            else
            {
                try
                {
                    Find.Element(By.Id("SectionHeader" + _sectionid)).Click();
                }
                catch { }
            }

        }
    }

    public abstract class PlaceHolderComp : UiComponent
    {

    }
}
