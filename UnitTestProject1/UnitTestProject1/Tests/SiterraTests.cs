using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.Seleno.Configuration;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Actions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Net.Http;
using System.Threading.Tasks;
using UnitTestProject1.Pages;


namespace UnitTestProject1.Tests
{
    [TestClass]
    public class SiterraTests
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Host.Instance = new SelenoHost();
            Host.Instance.Run(configure => configure
            .WithWebServer(new TestStack.Seleno.Configuration.WebServers.InternetWebServer("https://apollo-test.siterra.com/"))
            .WithRemoteWebDriver(() => BrowserFactory.InternetExplorer()));
            Host.Wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Host.Instance.Application.Browser, TimeSpan.FromSeconds(15));
            Helper.GotoLandingPage().InnerLoginPage.Login().PleaseWait();
            Host.InitialCookies = Host.Instance.Application.Browser.Manage().Cookies.AllCookies;
            Host.mainWindowHandle = Host.Instance.Application.Browser.CurrentWindowHandle;

        }

        [TestInitialize]
        public void testInitialize()
        {
            Host.Instance.Application.Browser.Manage().Cookies.DeleteAllCookies();
            foreach (OpenQA.Selenium.Cookie cookie in Host.InitialCookies)
            {
                Host.Instance.Application.Browser.Manage().Cookies.AddCookie(cookie);
            }
        }

        [TestMethod]
        public void T001_Login()
        {
                var page = Helper.GotoLandingPage();

                page.PleaseWait();

                page.InnerLoginPage.username = "admin_user";
                page.InnerLoginPage.password = "Potato3$";
                page.InnerLoginPage.domain = "SMS";

                page.InnerLoginPage.Click_Login();

                page.Title.Should().Contain("SMS");

        }

        [TestMethod]
        public void T002_Login_Profile_URL()
        {
            var page = Helper.GotoLandingPage("Samsonite");

            page.PleaseWait();

            page.InnerLoginPage.username = "admin_user";
            page.InnerLoginPage.password = "Potato3$";

            page.InnerLoginPage.Click_Login();

            page.Title.Should().Contain("Samsonite");

        }

        //[TestMethod]
        //public void T003_Create_Search_Ring()
        //{
        //    string name = "Watirpruf";
        //    string number = "Autobot";
        //    string type = "Type A";
        //    MainPage m = Helper.GotoUnitHomePage("GG Test Unit;GG Search Rings");
        //    UnitHomePage u = m.Innerpage as UnitHomePage;
        //    SearchRingPopup s = u.Create_Search_Ring();
        //    s.select_type(type);
        //    s.PleaseWait();
        //    s.srName = name;
        //    s.srNumber = number;
        //    s.Save();
        //    u.PleaseWait();
        //    m.PleaseWait();
            
        //    m.AssertThatElements.Exist(By.LinkText(name));
        //   // m.AssertThatElements.Exist(By.XPath("//table[@id='GRID_DATA_702040100']/"));
        //   // m.InnerPageFindText(By.XPath("//a[contains(@title, '" + name + "')]"));
        //}

        [TestMethod]
        public void T004_Update_Search_Ring()
        {
            string address = "paul test";
            MainPage m = Helper.GotoSearchRingHomePage("GG Test Unit;GG Search Rings;Test Ring (1357213)");
            SearchRingHomePage s = m.Innerpage as SearchRingHomePage;
            SearchRingPopup p = s.edit_sr();
            p.PleaseWait();
            p.srAddress = address;
            p.Save();
            m.PleaseWait();
            s.PleaseWait();
            m.InnerPageFindText(By.Id("TXT_STREET")).Should().Be(address);
        }
        
		[TestMethod]
        public void T005_Create_Site()
        {
            string name = "Watirpruf";
            string number = "Autobot";
            string type = "Type B";
            MainPage m = Helper.GotoSearchRingHomePage("0Notify First Round;NPF Search Ring UStatus Changed (827349376)");
            SearchRingHomePage sr = m.Innerpage as SearchRingHomePage;
            SitePopup sp = sr.add_site();
            sp.select_type(type); //replace with type
            sp.PleaseWait();
            sp.siteName = name; //replace with name
            sp.siteNumber = number; //replace with number
            sp.Save();
            m.PleaseWait();
            sr.PleaseWait();
            string expected = name + " - " + number;
            m.InnerPageFindText(By.XPath("//a[contains(@title, '" + name + "')]")).Should().Be(expected);
        }

        [TestMethod]
        public void T006_Update_Site()
        {
            string address = "paul test";
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");
            SiteHomePage s = m.Innerpage as SiteHomePage;
            SitePopup sp =s.Click_Edit();
            sp.PleaseWait();
            sp.address = address;
            sp.Save();
            m.PleaseWait();
            m.Innerpage.PleaseWait();
            m.InnerPageFindText(By.Id("TXT_STREET")).Should().Be(address);
            //System.Threading.Thread.Sleep(5000);
        }

        [TestMethod]
        public void T007_AssignVendor2Site()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Assign_Vendor("2648188")
            .ExistsInVendorSection("2648188").Should().BeTrue();
        }

        [TestMethod]
        public void T008_AddAsset2Site()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Asset()
            .ExistsInAssetSection("New HVAC").Should().BeTrue();
        }

        [TestMethod]
        public void T009_AddIncident2Site()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Incident("Yes it is a Problem", "Frederic Bottling")
            .ExistsInIncidentSection("Yes it is a Problem").Should().BeTrue();
        }

        [TestMethod]
        public void T010_AddEvent2Site()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Event("Selenium Test", "12/12/2015", "12/12/2015", "GROUP C", "Selenium0")
                .ExistsInEventSection("Selenium Test").Should().BeTrue();
            
        }

        [TestMethod]
        public void T011_AddNote2Site()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Note("Sele Note","Go check your sele note")
                .ExistsInNoteSection("Sele Note")
                .Should().BeTrue();
        }

        [TestMethod]
        public void T012_AddLease2Site()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Lease("sele", "", "This is a Selenium test")
                .ExistsInLeaseSection("sele").Should().BeTrue();
               
        }

        [TestMethod]
        public void T013_Update_Lease()
        {
            string description = "sanity test";
            MainPage m = Helper.GotoLeaseHomePage("GG Test Unit;GG Sites;Paul Property (23421424353250);Leases (1);Paul Property Leases (382395966)");
            LeaseHomePage l = m.Innerpage as LeaseHomePage;
            LeasePopup p = l.Click_Edit();
            p.PleaseWait();
            p.ctrDescription = description;
            p.Save();
            m.PleaseWait();
            l.PleaseWait();
            m.InnerPageFindText(By.Id("TXT_STATUS_DESCRIPTION")).Should().Be(description);
        }

        [TestMethod]
        public void T014_createPayment()
        {
            string name = "Sanity Rent";
            string type = "Rent";
            string firstDate = "07/11/2014";
            string secondDate = "08/11/2014";
            string toDate = "06/10/2015";
            string amount = "1000";
            MainPage m = Helper.GotoLeaseHomePage("GG Test Unit;GG Sites;Paul Property (23421424353250);Leases (1);Paul Property Leases (382395966)");
            LeaseHomePage l = m.Innerpage as LeaseHomePage;
            PaymentPopup p = l.Add_Payment();
            p.pmtName = name;
            p.select_type(type);
            p.pmtFromDate = firstDate;
            p.pmtSecondDate = secondDate;
            p.pmtToDate = toDate;
            p.PleaseWait();
            p.pmtAmount = amount;
            p.Save();
            m.PleaseWait();
            l.PleaseWait();
            m.InnerPageFindText(By.PartialLinkText(name)).Should().Be(name);
        }


        [TestMethod]
        public void T035_createUser()
        {
            MainPage m = Helper.GotoUserComp();
            UserComp user = m.Innerpage as UserComp;
            NewUserPage newUser = user.addUser();
            newUser.switchFocusToUserPage();
            String userName = newUser.createUser();
            //String userName = "Sanity";
            System.Threading.Thread.Sleep(2000);

            user.findUser(userName, "User Name");

            m.InnerPageFindText(By.CssSelector("a[id$='_DISPLAY_NAME']")).Should().Be("Sanity Test User");
        }

        [TestMethod]
        public void T036_updateUser()
        {
            //WARNING: NOT CURRENTLY WORKING 100% OF THE TIME//
            MainPage m = Helper.GotoUserComp();
            UserComp user = m.Innerpage as UserComp;
            user.PleaseWait();

            System.Threading.Thread.Sleep(4000);

            user.findUser("Sanity", "User Name");
            NewUserPage n = user.selectFirstUser();
            n.switchFocusToUserPage();

            String userName = n.updateUser();

            System.Threading.Thread.Sleep(2000);

            user.findUser(userName, "User Name");

            m.InnerPageFindText(By.CssSelector("a[@id$='_DISPLAY_NAME']")).Should().NotBe("Sanity Test User");
        }

        [TestMethod]
        public void T037_createUserSubscriptions()
        {
            MainPage m = Helper.GotoUserComp();
            m.PleaseWait();
            UserComp user = m.Innerpage as UserComp;
            user.PleaseWait();

            System.Threading.Thread.Sleep(4000);

            user.findUser("Sanity Test", "Display Name");
            NewUserPage n = user.selectFirstUser();

            UserSubscriptionsPopUp uspu = n.addNotifications();

            uspu.addSubscription("Project", "Created", "xSiterra");

            
            n.switchFocusToUserPage();

            UserSubscriptionsPopUp uspu2 = n.addNotifications();
            uspu2.switchFocus2SubscriptionsPopUp();
            uspu2.addSubscription("Documents", "Uploaded", "xSiterra");

            n.switchFocusToUserPage();
            n.saveAndClose();
            NewUserPage n2 = user.selectFirstUser();

            //Assert!
           /* n2.ExistsInSubscriptionSection("Created").Should().BeTrue();
            n2.ExistsInSubscriptionSection("Documents").Should().BeTrue();*/
        }

        [TestMethod]
        public void T038_CreateProjectSchedule()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Project("Sele Project3", "12/12/2015", "Active")
            .ExistsInLeftNavProjectSection("Sele Project3").Should().BeTrue();      
        }

        [TestMethod]
        public void T039_ChangeProjectDates()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Goto_Project("11200400").PleaseWait();
        }

        [TestMethod]
        public void T080_responsibilities()
        {
            /***NOT DONE***/
            MainPage m = Helper.GotoResponsibilityComp();
            m.PleaseWait();
            ResponsibilityComp r = m.Innerpage as ResponsibilityComp;
            r.PleaseWait();

            r.searchResponsibility("Site","xSiterra", 5);
        }

        [TestMethod]
        public void T081_portfolio()
        {
            MainPage m = Helper.GotoPortfolioComp();
            m.PleaseWait();
            PortfolioComp p = m.Innerpage as PortfolioComp;
            p.PleaseWait();

            p.PageLoaded().Should().BeTrue();
        }

        [TestMethod]
        public void T082_extendedAttributes()
        {
            MainPage m = Helper.GotoExtendedAttributesComp();
            m.PleaseWait();
            ExtendedAttributesComp ea = m.Innerpage as ExtendedAttributesComp;
            ea.PleaseWait();

            ea.PageLoaded().Should().BeTrue();
        }

        [TestMethod]
        public void T083_lookupTables()
        {
            MainPage m = Helper.GotoLookUpTablesComp();
            m.PleaseWait();
            LookupTablesComp l = (LookupTablesComp)m.Innerpage;
            l.PleaseWait();

            l.PageLoaded().Should().BeTrue();
        }
		
        [TestMethod]
        public void T084_deactivateUser()
        {
            MainPage m = Helper.GotoUserComp();
            m.PleaseWait();
            UserComp u = (UserComp)m.Innerpage;
            u.PleaseWait();

            u.findUser("Sanity Test", "Display Name");

            u.deactivateUser();

        }

        [TestMethod]
        public void leaseLeftNav()
        {
            Helper.GotoMainPage()
                .clickLeaseLeftNav()
                .Find.Element(By.Id("tdSetGlobalNavSites"))
                .GetCssValue("background-position-x")
                .Equals("-10px");
        }

        [TestMethod]
        public void siteLeftNav()
        {
            Helper.GotoMainPage()
                .clickSiteLeftNav()
                .Find.Element(By.Id("tdSetGlobalNavSites"))
                .GetCssValue("background-position-x")
                .Should().Equals("-10px");
            System.Threading.Thread.Sleep(3000);
        }

        [TestMethod]
        public async Task CheckFlashDownload()
        {
            var downloadlink = Helper.GotoLandingPage().Click_Downloads().CheckFileDownload();

            using (var client = new HttpClient())
            {

                //client.BaseAddress = new Uri(downloadlink);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/octet-stream"));

                HttpResponseMessage response = await client.GetAsync(new Uri(downloadlink));

                response.IsSuccessStatusCode.Should().BeTrue();
            }
        }

        [TestMethod]
        public void gotobrowse()
        {
            MainPage m = Helper.GotoMainPage()
                .clickBrowseLeftNav();
            m.ClickNavTree<SiteHomePage>("Amcknight;Site;New Site (SiteNumber)");
            //System.Threading.Thread.Sleep(3000);
            //m.Innerpage.
        }

        [TestMethod]
        public void gotoToDoList()
        {
            Helper.GotoMainPage()
                .Click_ToDoList_Link();
        }
    }
}
