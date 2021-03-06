﻿using System;
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
        public static object sync = new object();

        [ThreadStatic]
        public static SelenoHost Instance;

        [ThreadStatic]
        public static WebDriverWait Wait;
        [ThreadStatic]
        public static string mainWindowHandle;

        public static void mouseMoveNClick(By ByObject)
        {
            OpenQA.Selenium.Interactions.Actions builder =
                new OpenQA.Selenium.Interactions.Actions(Host.Instance.Application.Browser);
            builder.MoveToElement(Host.Instance.Application.Browser.FindElement(ByObject)).Build().Perform();
            Host.Instance.Application.Browser.FindElement(ByObject).Click();
        }

        public static void mouseMoveNClick(IWebElement e)
        {
            OpenQA.Selenium.Interactions.Actions builder =
                new OpenQA.Selenium.Interactions.Actions(Host.Instance.Application.Browser);
            builder.MoveToElement(e).Build().Perform();
            e.Click();
        }

        public static void executeJavaScript(string script)
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            executor.ExecuteScript(script);
        }

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
                    _gridid = idholder.Replace("GRID_DATA_", "");
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
            System.Threading.Thread.Sleep(1000);
            return this;
        }

        private GridComponent Click_Search()
        {
            SwitchIn();
            Find.Element(By.XPath("//a[contains(@onclick,'getGridByID(') and contains(@onclick,'.ApplySearch()')]"))
                .Click();
            SwitchOut();
            //PleaseWaitForSearch();
            return this;
        }

        public void PleaseWaitForSearch()
        {
            System.Threading.Thread.Sleep(1500); //implement for the future
            SwitchIn();
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("CHK_MATCH_WHOLE_" + grid_id));
            });
            SwitchOut();
        }

        public T ClickObjInGrid<T>(String columnname, String value) where T : SiterraComponent, new()
        {
            T holder;
            IWebElement elem = null;
            ExistsInGrid(columnname, value, ref elem);
            elem.Click();
            holder = this.GetComponent<T>();
            SwitchOut();
            return holder;
        }

        public Boolean ExistsInGrid(String columnname, String value)
        {
            IWebElement elem = null;
            return ExistsInGrid(columnname, value, ref elem);
        }

        public Boolean ExistsInGrid(String columnname, String value, ref IWebElement elem)
        {
            int column_index = -1;
            IWebElement grid_table, grid_table_header;
            IWebElement a = null;
            Boolean isCheckBox;
            Boolean isLeftNavGrid;
            ReadOnlyCollection<IWebElement> grid_header, grid_table_rows, grid_row_data;
            PleaseWaitForSearch();

            SwitchIn();
            //s=Find.Element(By.XPath("//table[contains(@id, 'GRID_DATA_')]//tbody//tr//td//a//G_VALUE[. = 'Bescoby']")).Text;
            grid_table = Find.Element(By.Id("GRID_DATA_" + grid_id));
            try
            {
                grid_table_header = grid_table.FindElement(By.XPath("./thead/tr[@class='GridHeader']"));
                isLeftNavGrid = false;
            }
            catch
            {
                //it must be a left nav grid
                grid_table_header = grid_table.FindElement(By.XPath("./thead/tr[@id='GRID_TR_HEADER_" + grid_id + "']"));
                isLeftNavGrid = true;
            }

            grid_header = grid_table_header.FindElements(By.XPath("./td[@class='HeaderCell']"));

            //if grid has checkbox
            try
            {
                isCheckBox = grid_table_header.FindElement(By.Id("CHECKBOX_HEADER")).Displayed;
            }
            catch
            {
                isCheckBox = false;
            }

            var grid_header_enumerator = grid_header.GetEnumerator();
            for (int i = 0; grid_header_enumerator.MoveNext(); i++)
            {

                if (!isLeftNavGrid && grid_header_enumerator.Current.Text.Equals(columnname))
                {
                    column_index = i;
                    break;
                }
                else if (isLeftNavGrid && grid_header_enumerator.Current.GetAttribute("title").Equals(columnname))
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

            string cell_path = isLeftNavGrid ? "./td[contains(@class,'LeftNavGridCell')]" : "./td[@class!='HIDDEN']";
            for (int i = 0; grid_row_enum.MoveNext(); i++)
            {
                //Get all cells from the row
                grid_row_data = grid_row_enum.Current.FindElements(By.XPath(cell_path));
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

            if (a == null) { SwitchOut(); return false; }
            if (a.Text.Equals(value))
            {
                SwitchOut();
                //elem = a.FindElement(By.XPath("./a")); //get Parent element
                return true;
            }
            else
            {
                SwitchOut();
                return false;
            }
        }
    }

    public abstract class SectionComponent : GridComponent
    {
        private String _sectionid;
        private IWebElement sectionElement;
        public string section_id
        {
            get
            {
                String idholder;
                if (_sectionid == null) //object browser page
                {
                    if (sectionElement == null)
                        sectionElement = Find.Element(By.XPath("//table[contains(@id, 'GRID_DATA_')]"));

                    idholder = sectionElement.GetAttribute("id");
                    _sectionid = idholder.Replace("GRID_DATA_", ""); ;
                }
                return _sectionid;
            }
            set
            {
                _sectionid = value;
                sectionElement = Find.Element(By.XPath("//table[contains(@id, 'GRID_DATA_')]"));
            }
        }

        public void Show()
        {

            IWebElement elem = null;
            try
            {
                SwitchIn();
                elem = Find.Element(By.Id("DataDivContainer" + _sectionid));
            }
            catch
            {
                SwitchOut();
                elem = Find.Element(By.Id("DataDivContainer" + _sectionid));
            }

            if (elem != null)
            {
                if (!elem.Displayed)
                {
                    //Delete Please!
                    //try
                    //{
                    Find.Element(By.Id("SectionHeader" + _sectionid)).Click();
                    //}
                    //catch {
                    //    SwitchIn();
                    //    Find.Element(By.Id("SectionHeader" + _sectionid)).Click();
                    //}
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
            SwitchOut();
        }

        public IWebElement SectionSearchByXPath(string path)
        {
            return sectionElement.FindElement(By.XPath(path));
        }
    }

    public abstract class PlaceHolderComp : UiComponent
    {

    }

    public class ModalDialog : UiComponent
    {
        private string handle;
        public ModalDialog()
        {
            /*for some reason, the windowHandles procedure doesnt 
             * update the number of handles to include the modal dialog. Setting the 
             * focus to the active element allows selenium to recognize the modal dialog window.
             * */
            
            //Host.Instance.Application.Browser.WindowHandles;
            System.Threading.Thread.Sleep(3000);
            Host.Instance.Application.Browser.SwitchTo().ActiveElement();
            string han = Host.Instance.Application.Browser.Manage().Window.ToString() ;
            System.Collections.ObjectModel.ReadOnlyCollection<string> handles = Host.Instance.Application.Browser.WindowHandles;
            foreach (string a in handles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("CustomMessage.htm"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().ActiveElement();
            //Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }
    }
}