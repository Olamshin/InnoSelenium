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
        private VendorSection vendorSection
        {
            get
            {
                if (_vendorsection == null)
                { _vendorsection = this.GetComponent<VendorSection>(); }
                _vendorsection.Show();
                return _vendorsection;
            }
        }

        private AssetSection _assetsection;
        private AssetSection assetSection
        {
            get
            {
                if (_assetsection == null)
                { _assetsection = this.GetComponent<AssetSection>(); }
                _assetsection.Show();
                return _assetsection;
            }
        }

        private IncidentSection _incidentsection;
        private IncidentSection incidentSection
        {
            get
            {
                if (_incidentsection == null)
                { _incidentsection = this.GetComponent<IncidentSection>(); }
                _incidentsection.Show();
                return _incidentsection;
            }
        }

        private EventSection _eventsection;
        private EventSection eventSection
        {
            get
            {
                if (_eventsection == null)
                { _eventsection = this.GetComponent<EventSection>(); }
                _eventsection.Show();
                return _eventsection;
            }
        }

        private NoteSection _notesection;
        private NoteSection noteSection
        {
            get
            {
                if (_notesection == null)
                { _notesection = this.GetComponent<NoteSection>(); }
                _notesection.Show();
                return _notesection;
            }
        }

        private ProjectSummarySection _projsummarysection;
        private ProjectSummarySection projsummarySection
        {
            get
            {
                if (_projsummarysection == null)
                { _projsummarysection = this.GetComponent<ProjectSummarySection>(); }
                _projsummarysection.Show();
                return _projsummarysection;
            }
        }

        private LeftNavProjectSection _leftnavprojectsection;
        private LeftNavProjectSection leftnavprojectSection
        {
            get
            {
                if (_leftnavprojectsection == null)
                { _leftnavprojectsection = this.GetComponent<LeftNavProjectSection>(); }
                _leftnavprojectsection.Show();
                return _leftnavprojectsection;
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

        public SitePopup Click_Edit()
        {
            SwitchIn();
            return Navigate.To<SitePopup>(By.PartialLinkText("Edit"));
        }

        public SiteHomePage Assign_Vendor(string vendor_no)
        {
            SwitchIn();
            vendorSection.Assign_Vendor()
                .Vendor_Select_Button()
                .Select_Object<VendorSection>(vendor_no, vendorSection)
                .SwitchFromPopup()
                .Submit_Vendor()
                .PleaseWait();
            SwitchOut();
            return this;
        }

        public Boolean ExistsInVendorSection(String vendor_no)
        {
            Boolean flag;
            SwitchIn();
            flag = vendorSection.ExistsInGrid("Number", vendor_no);
            SwitchOut();
            return flag;
        }

        public SiteHomePage Add_Asset()
        {
            SwitchIn();
            assetSection.Add_Asset().Select_Type().Save();
            SwitchOut();
            return this;

        }

        public Boolean ExistsInAssetSection(String asset_name)
        {
            Boolean flag;
            SwitchIn();
            flag = assetSection.ExistsInGrid("Name", asset_name);
            SwitchOut();
            return flag;
        }

        public SiteHomePage Add_Incident(string problem, string assigned2User)
        {
            SwitchIn();
            incidentSection.Add_Incident()
               .Select_Type()
               .Enter_Info(problem, assigned2User)
               .Save();
            SwitchOut();
            return this;
        }

        public Boolean ExistsInIncidentSection(String incident_problem)
        {
            Boolean flag;
            SwitchIn();
            flag = incidentSection.ExistsInGrid("Problem", incident_problem);
            SwitchOut();
            return flag;
        }

        public SiteHomePage Add_Event(string name, string due_date, string action_start_date, string responsible_group, string description)
        {
            SwitchIn();
            eventSection.Add_Event()
                .Enter_Info(name,due_date,action_start_date,responsible_group,description)
                .Save();
            SwitchOut();
            return this;
        }

        public Boolean ExistsInEventSection(String event_label)
        {
            Boolean flag;
            SwitchIn();
            flag = eventSection.ExistsInGrid("Event Label", event_label);
            SwitchOut();
            return flag;
        }

        public SiteHomePage Add_Note(string subject, string message)
        {
            SwitchIn();
            noteSection.Add_Note(subject, message);
            SwitchOut();
            return this;
        }

        public Boolean ExistsInNoteSection(String subject)
        {
            Boolean flag;
            SwitchIn();
            flag =  noteSection.ExistsInGrid("Subject", subject);
            SwitchOut();
            return flag;
        }



        private class VendorSection : SectionComponent
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

            public VendorSection SwitchFromPopup()
            {
                SwitchIn();
                return this;
            }

            protected override void SwitchOut()
            {
                Browser.SwitchTo().DefaultContent();
            }

            public VendorSection Assign_Vendor()
            {
                Find.Element(By.XPath("//a[contains(@onclick,'addVendorResponsibility()')]")).Click();
                return this;
            }

            public ObjectBrowserPage Vendor_Select_Button()
            {
                return Navigate.To<ObjectBrowserPage>(By.XPath("//a[contains(@onclick,'openVendorOBB(-1)')]"));
            }

            public VendorSection Submit_Vendor()
            {
                Find.Element(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Click();
                return this;
            }

            public VendorSection PleaseWait()
            {
                Host.Wait.Until<Boolean>((d) =>
                {
                    return !d.FindElement(By.Id("PLACEHOLDER_" + grid_id)).Displayed;
                    //return !d.FindElement(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Displayed;
                });
                return this;
            }

        }

        private class AssetSection : SectionComponent
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

            public AssetPopup Add_Asset()
            {
                return Navigate.To<AssetPopup>(By.XPath("//a[contains(@onclick,'addAssetFromSite(')]"));

            }
        }

        private class IncidentSection : SectionComponent
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

            public IncidentPopup Add_Incident()
            {
                //Find.Element(By.XPath("//a[contains(@onclick,'addAssetFromSite(')]")).Click();
                return Navigate.To<IncidentPopup>(By.XPath("//a[contains(@onclick,'addIncident()')]"));

            }

        }

        private class EventSection : SectionComponent
        {
            public EventSection()
            {
                section_id = "108";
                grid_id = "706040100";
            }
            protected override void SwitchIn()
            {
                Browser.SwitchTo().Frame("MainFrame");
            }

            protected override void SwitchOut()
            {
                Browser.SwitchTo().DefaultContent();
            }

            public EventPopup Add_Event()
            {
                return Navigate.To<EventPopup>(By.XPath("//a[contains(@onclick,'addEvent()')]"));
            }

        }

        private class NoteSection : SectionComponent
        {
            private NotePlaceHolder _notedialog;
            private NotePlaceHolder NoteDialog
            {
                get
                {
                    if (_notedialog == null)
                    {
                        _notedialog = this.GetComponent<NotePlaceHolder>();
                    }
                    return _notedialog;
                }
            }
            public NoteSection()
            {
                section_id = "103";
                grid_id = "166040200";
            }

            private void PleaseWait()
            {
                System.Threading.Thread.Sleep(1000);//implement for the future
            }

            public NoteSection Add_Note(string subject, string message)
            {
                Click_Note();
                NoteDialog.subject = subject;
                NoteDialog.message = message;
                NoteDialog.Click_Save();
                return this;
            }

            public void Click_Note()
            {
                PleaseWait();
                Find.Element(By.XPath("//a[contains(@onclick,'addNote()')]")).Click();
            }

            protected override void SwitchIn()
            {
                Browser.SwitchTo().Frame("MainFrame");
            }

            protected override void SwitchOut()
            {
                Browser.SwitchTo().DefaultContent();
            }

            private class NotePlaceHolder : PlaceHolderComp
            {
                public string subject
                {
                    set
                    {
                        Execute.ActionOnLocator(By.Id("NOTE_SUBJECT_-1"), e => { e.Clear(); e.SendKeys(value); });
                    }
                }

                public string message
                {
                    set
                    {
                        Execute.ActionOnLocator(By.Id("NOTE_MESSAGE_-1"), e => { e.Clear(); e.SendKeys(value); });
                    }
                }

                internal NotePlaceHolder Click_Save()
                {
                    //SwitchIn();
                    IWebElement elem = Find.Element(By.XPath("//a[contains(@onclick,'saveNote(')]"));
                    elem.Click();
                    //SwitchOut();
                    Host.Wait.Until<Boolean>((d) =>
                    {
                        try
                        {
                            return !elem.Displayed;
                        }
                        catch (OpenQA.Selenium.StaleElementReferenceException e)
                        {
                            return true;
                        }
                    });
                    return this;
                }
            }

        }
    }
}
