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
        private VendorSection _vendorsection;
        protected VendorSection vendorSection
        {
            get
            {
                if (_vendorsection == null)
                { _vendorsection = this.GetComponent<VendorSection>(); }
                return _vendorsection;
            }
        }

        private AssetSection _assetsection;
        protected AssetSection assetSection
        {
            get
            {
                if (_assetsection == null)
                { _assetsection = this.GetComponent<AssetSection>(); }
                return _assetsection;
            }
        }

        private IncidentSection _incidentsection;
        protected IncidentSection incidentSection
        {
            get
            {
                if (_incidentsection == null)
                { _incidentsection = this.GetComponent<IncidentSection>(); }
                return _incidentsection;
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

        public Boolean ExistsInVendorSection(String vendor_no)
        {
            return vendorSection.ExistsInGrid("Number", vendor_no);
        }

        public AssetPopup Add_Asset()
        {
            SwitchIn();
            //Find.Element(By.XPath("//a[contains(@onclick,'addAssetFromSite(')]")).Click();
            return Navigate.To<AssetPopup>(By.XPath("//a[contains(@onclick,'addAssetFromSite(')]"));

        }

        public Boolean ExistsInAssetSection(String asset_name)
        {
            return assetSection.ExistsInGrid("Name", asset_name);
        }

        public IncidentPopup Add_Incident()
        {
            SwitchIn();
            //Find.Element(By.XPath("//a[contains(@onclick,'addAssetFromSite(')]")).Click();
            return Navigate.To<IncidentPopup>(By.XPath("//a[contains(@onclick,'addIncident()')]"));

        }

        public Boolean ExistsInIncidentSection(String incident_problem)
        {
            return incidentSection.ExistsInGrid("Problem", incident_problem);
        }

        protected class VendorSection : SectionComponent
        {
            public VendorSection()
            {
                section_id = "114";
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

        protected class AssetSection : SectionComponent
        {
            public AssetSection()
            {
                section_id = "106";
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

        protected class IncidentSection : SectionComponent
        {
            public IncidentSection()
            {
                section_id = "107";
                grid_id = "705040100";
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
