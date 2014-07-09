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
    public class SiteHomePage : SiterraComponent
    {
        private VendorGrid vendorGrid
        {
            get
            {
                return this.GetComponent<VendorGrid>();
            }
        }
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
                return d.FindElement(By.Id("TXT_NAME"));
            });
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.PageSource.Contains("indicator_gray.gif");
                //return !d.FindElement(By.Id("NavContainer")).ToString().Contains("indicator_gray.gif");
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
        public SitePopup click_edit()
        {
            SwitchIn();
            return Navigate.To<SitePopup>(By.PartialLinkText("Edit"));
        }
        public SiteHomePage add_vendor()
        {
            SwitchIn();
            Find.Element(By.XPath("//a[contains(@onclick,'addVendorResponsibility()')]")).Click();
            SwitchOut();
            return this;
        }
        public ObjectBrowserPage Select_Button()
        {
            SwitchIn();
            return Navigate.To<ObjectBrowserPage>(By.XPath("//a[contains(@onclick,'openVendorOBB(-1)')]"));
        }
        public SiteHomePage Submit_Vendor()
        {
            SwitchIn();
            Find.Element(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Click();
            SwitchOut();
            Host.Wait.Until<Boolean>((d) =>
            {
                try
                {
                    return !d.FindElement(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Displayed;
                }
                catch (Exception e)
                {
                    return true;
                }
            });
            return this;
        }

        public Boolean ExistsInVendorGrid(String vendor_no)
        {
            /*String s = null; ;
            IWebElement grid_table;
            SwitchIn();
            //s=Find.Element(By.XPath("//table[contains(@id, 'GRID_DATA_')]//tbody//tr//td//a//G_VALUE[. = 'Bescoby']")).Text;
            grid_table = Find.Element(By.Id("GRID_DATA_727040300"));
            ReadOnlyCollection<IWebElement> grid_table_rows = grid_table.FindElements(By.XPath("//tr[@id='GRID_ROW']"));
            IEnumerable<IWebElement> grid_row = grid_table_rows.Where(row => row.Text.Contains(vendor_no));
            if (grid_row.Count() > 0)
            {
                SwitchOut();
                return true;
            }
            else
            {
                SwitchOut();
                return false;
            }*/
            return vendorGrid.ExistsInGrid(vendor_no);
        }
    }

    public class VendorGrid : GridComponent
    {
       public VendorGrid()
        {
            grid_id = "727040300";
        }
        protected override void SwitchIn()
        {
            Browser.SwitchTo().Frame("MainFrame");
        }

        protected override void SwitchOut()
        {
            Browser.SwitchTo().DefaultContent();
        }

       
    }

}
