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
        private VendorGrid _vendorgrid;
        protected VendorGrid vendorGrid
        {
            get
            {
                if (_vendorgrid == null)
                { _vendorgrid = this.GetComponent<VendorGrid>(); }
                return _vendorgrid;
            }
        }

        private AssetGrid _assetgrid;
        protected AssetGrid assetGrid
        {
            get
            {
                if (_assetgrid == null)
                { _assetgrid = this.GetComponent<AssetGrid>(); }
                return _assetgrid;
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

        public SiteHomePage Assign_Vendor()
        {
            SwitchIn();
            Find.Element(By.XPath("//a[contains(@onclick,'addVendorResponsibility()')]")).Click();
            SwitchOut();
            return this;
        }

        public ObjectBrowserPage Vendor_Select_Button()
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
            return vendorGrid.ExistsInGrid("Number", vendor_no);
        }

        public AssetPopup Add_Asset()
        {
            SwitchIn();
            //Find.Element(By.XPath("//a[contains(@onclick,'addAssetFromSite(')]")).Click();
            return Navigate.To<AssetPopup>(By.XPath("//a[contains(@onclick,'addAssetFromSite(')]"));

        }

        public Boolean ExistsInAssetGrid(String asset_no)
        {
            return assetGrid.ExistsInGrid("Number", asset_no);
        }

        protected class VendorGrid : GridComponent
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

        protected class AssetGrid : GridComponent
        {
            public AssetGrid()
            {
                grid_id = "711040100";
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
}
