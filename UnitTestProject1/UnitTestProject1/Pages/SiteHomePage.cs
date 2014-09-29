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
                {
                    _vendorsection = this.GetComponent<VendorSection>();
                    _vendorsection.init();
                }
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
                {
                    _assetsection = this.GetComponent<AssetSection>();
                    _assetsection.init();
                }
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
                {
                    _incidentsection = this.GetComponent<IncidentSection>();
                    _incidentsection.init();
                }
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
                {
                    _eventsection = this.GetComponent<EventSection>();
                    _eventsection.init();
                }
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
                {
                    _notesection = this.GetComponent<NoteSection>();
                    _notesection.init();
                }
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
                {
                    _projsummarysection = this.GetComponent<ProjectSummarySection>();
                    _projsummarysection.init();
                }
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
                {
                    _leftnavprojectsection = this.GetComponent<LeftNavProjectSection>();
                    _leftnavprojectsection.init();
                }
                _leftnavprojectsection.Show();
                return _leftnavprojectsection;
            }
        }

        private LeaseSection _leasesection;
        private LeaseSection leaseSection
        {
            get
            {
                if (_leasesection == null)
                {
                    _leasesection = this.GetComponent<LeaseSection>();
                    _leasesection.init();
                }
                _leasesection.Show();
                return _leasesection;
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
            Browser.SwitchTo().DefaultContent();
            Browser.SwitchTo().Frame("MainFrame");
        }

        private void SwitchOut()
        {
            Browser.SwitchTo().DefaultContent();
        }

        public SitePopup Click_Edit()
        {
            SwitchIn();
            return Navigate.To<SitePopup>(By.PartialLinkText("Edit"));
        }

        public SiteHomePage Assign_Vendor(string vendor_no)
        {
            VendorSection v = vendorSection;
            v.Assign_Vendor().Vendor_Select_Button()
            .Select_Object<VendorSection>(vendor_no, v)
            .SwitchFromPopup()
            .Submit_Vendor()
            .PleaseWait();
            return this;
        }

        public Boolean ExistsInVendorSection(String vendor_no)
        {
            Boolean flag;
            flag = vendorSection.ExistsInGrid("Number", vendor_no);
            return flag;
        }

        public SiteHomePage Add_Asset(string name)
        {
            assetSection.Add_Asset().Select_Type().Enter_Name(name).Save();
            return this;

        }

        public Boolean ExistsInAssetSection(String asset_name)
        {
            Boolean flag;
            flag = assetSection.ExistsInGrid("Name", asset_name);
            return flag;
        }

        public SiteHomePage Add_Incident(string problem, string assigned2User)
        {
            incidentSection.Add_Incident()
               .Select_Type()
               .Enter_Info(problem, assigned2User)
               .Save();
            return this;
        }

        public Boolean ExistsInIncidentSection(String incident_problem)
        {
            Boolean flag;
            flag = incidentSection.ExistsInGrid("Problem", incident_problem);
            return flag;
        }

        public SiteHomePage Add_Event(string name, string due_date, string action_start_date, string responsible_group, string description)
        {
            eventSection.Add_Event()
                .Enter_Info(name, due_date, action_start_date, responsible_group, description)
                .Save();
            return this;
        }

        public Boolean ExistsInEventSection(String event_label)
        {
            Boolean flag;
            flag = eventSection.ExistsInGrid("Event Label", event_label);
            return flag;
        }

        public SiteHomePage Add_Note(string subject, string message)
        {
            noteSection.Add_Note(subject, message);
            return this;
        }

        public Boolean ExistsInNoteSection(String subject)
        {
            Boolean flag;
            flag = noteSection.ExistsInGrid("Subject", subject);
            return flag;
        }

        public SiteHomePage Add_Project(String name, String start_date, String status)
        {
            leftnavprojectSection
                .Add_Project()
                .Select_Template()
                .Enter_Info(name, start_date, status)
                .Save();
            return this;
        }

        public SiteHomePage Add_Project(String template_name, String name, String start_date, String status)
        {
            SwitchIn();
            leftnavprojectSection
                .Add_Project()
                .Select_Template(template_name)
                .Enter_Info(name, start_date, status)
                .Save();
            return this;
        }

        public ProjectHomePage Goto_Project(String project_no)
        {
            return projsummarySection.Click_Project(project_no);
        }

        public Boolean ExistsInProjectSummarySection(String name)
        {
            Boolean flag;
            SwitchIn();
            flag = projsummarySection.ExistsInGrid("Name", name);
            SwitchOut();
            return flag;
        }

        public Boolean ExistsInLeftNavProjectSection(String name)
        {
            Boolean flag;
            flag = leftnavprojectSection.ExistsInGrid("Name", name);
            return flag;
        }

        public SiteHomePage Add_Lease(String name, String number, String description)
        {
            leaseSection.Add_Lease().Enter_Info(name, number, description).Save();
            return this;
        }

        public Boolean ExistsInLeaseSection(String name)
        {
            Boolean flag;
            flag = leaseSection.ExistsInGrid("Name", name);
            return flag;
        }

        //public 

        private class VendorSection : SectionComponent
        {
            public VendorSection()
            {

            }
            public void init()
            {
                section_id = "114";
                grid_id = "727040300";
            }
            protected override void SwitchIn()
            {
                Browser.SwitchTo().DefaultContent();
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
                int switched = 0;
                try
                {
                    //if it cant find the element, look into yourself
                    Find.Element(By.Id("PLACEHOLDER_" + grid_id));
                }
                catch
                {
                    SwitchIn();
                    switched = 1;
                }
                Find.Element(By.XPath("//a[contains(@onclick,'addVendorResponsibility()')]")).Click();
                if (switched == 1) SwitchOut();
                return this;
            }

            public ObjectBrowserPage Vendor_Select_Button()
            {
                SwitchIn();
                return Navigate.To<ObjectBrowserPage>(By.XPath("//a[contains(@onclick,'openVendorOBB(-1)')]"));
            }

            public VendorSection Submit_Vendor()
            {
                IWebElement e = Find.Element(By.Id("DataDivContainer" + section_id))
                    .FindElement(By.XPath(".//input[contains(@onclick,'saveResponsibility(-1)')]"));
                //System.Drawing.Point m = ((ILocatable)e).LocationOnScreenOnceScrolledIntoView;

                //var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
                //executor.ExecuteScript("window.scrollBy(0," + -1*(m.Y + 100) + ");");
                e.SendKeys(Keys.ArrowDown); // scrolling down becuase the Submit button hides behind the scoll bar

                Find.Element(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Click();
                return this;
            }

            public VendorSection PleaseWait()
            {
                int switched = 0;
                try
                {
                    //if it cant find the element, look into yourself
                    Find.Element(By.Id("PLACEHOLDER_" + grid_id));
                }
                catch
                {
                    SwitchIn();
                    switched = 1;
                }
                Host.Wait.Until<Boolean>((d) =>
                {
                    return !d.FindElement(By.Id("PLACEHOLDER_" + grid_id)).Displayed;
                    //return !d.FindElement(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Displayed;
                });
                if (switched == 1) SwitchOut();
                return this;
            }

        }

        private class AssetSection : SectionComponent
        {
            public void init()
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
                SwitchIn();
                return Navigate.To<AssetPopup>(By.XPath("//a[contains(@onclick,'addAssetFromSite(')]"));

            }
        }

        private class IncidentSection : SectionComponent
        {
            public void init()
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
                SwitchIn();
                return Navigate.To<IncidentPopup>(By.XPath("//a[contains(@onclick,'addIncident()')]"));

            }

        }

        private class EventSection : SectionComponent
        {
            public void init()
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
                SwitchIn();
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
            public void init()
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
                SwitchIn();
                Click_Note();
                NoteDialog.subject = subject;
                NoteDialog.message = message;
                NoteDialog.Click_Save();
                SwitchOut();
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
                        catch
                        {
                            return true;
                        }
                    });
                    return this;
                }
            }

        }

        private class ProjectSummarySection : SectionComponent
        {
            public void init()
            {
                section_id = "116";
                grid_id = "703041300";
            }
            protected override void SwitchIn()
            {
                Browser.SwitchTo().Frame("MainFrame");
            }

            public ProjectSummarySection SwitchFromPopup()
            {
                SwitchIn();
                return this;
            }

            protected override void SwitchOut()
            {
                Browser.SwitchTo().DefaultContent();
            }

            public ProjectHomePage Click_Project(String value)
            {
                return ClickObjInGrid<ProjectHomePage>("Number", value);
            }

        }

        private class LeftNavProjectSection : SectionComponent
        {
            public void init()
            {
                section_id = "703000000";
                grid_id = "703040400";
            }
            protected override void SwitchIn()
            {
                Browser.SwitchTo().Frame("MainFrame");
            }

            public LeftNavProjectSection SwitchFromPopup()
            {
                SwitchIn();
                return this;
            }

            protected override void SwitchOut()
            {
                Browser.SwitchTo().DefaultContent();
            }

            public ProjectPopup Add_Project()
            {
                SwitchIn();
                return Navigate.To<ProjectPopup>(By.XPath("//a[contains(@onclick,'addProject()')]"));
            }
        }

        private class LeaseSection : SectionComponent
        {
            public void init()
            {
                section_id = "105";
                grid_id = "716040100";
            }
            protected override void SwitchIn()
            {
                Browser.SwitchTo().Frame("MainFrame");
            }

            protected override void SwitchOut()
            {
                Browser.SwitchTo().DefaultContent();
            }

            public LeasePopup Add_Lease()
            {
                SwitchIn();
                return Navigate.To<LeasePopup>(By.XPath("//a[contains(@onclick,'addLease()')]"));
            }
        }
    }
}
