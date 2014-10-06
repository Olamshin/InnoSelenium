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
using OpenQA.Selenium.Remote;


namespace UnitTestProject1.Tests
{
    [TestClass]
    public class SiterraTests
    {
        public const string Default_SearchRing = "xSiterra;Search Rings;Test SEARCH RING (1144307)";
        public const string Default_Site = "xSiterra;Public;Test Site Creation (12345678915)";
        public const string Default_Lease = "xSiterra;Search Rings;Test SEARCH RING (1144307);Test Site (1251608);Leases;New Lease (1071114)";
        public const string Default_Project = "xSiterra;Search Rings;Test SEARCH RING (1144307);Projects;Search Ring Evaluation (10098810)";
        //[ClassInitialize]
        //public static void ClassInit(TestContext context)
        //{
        //    Host.Instance = new SelenoHost();
        //    Host.Instance.Run(configure => configure
        //    .WithWebServer(new TestStack.Seleno.Configuration.WebServers.InternetWebServer("https://develop-ci.siterra.com/"))
        //    .WithRemoteWebDriver(() => BrowserFactory.InternetExplorer()));
        //    Host.Wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Host.Instance.Application.Browser, TimeSpan.FromSeconds(15));

        //    Host.mainWindowHandle = Host.Instance.Application.Browser.CurrentWindowHandle;

        //}

        [TestInitialize]
        public void testInitialize()
        {
            if (Host.Instance == null)
            {
                lock (Host.sync)
                {
                    DesiredCapabilities capability = DesiredCapabilities.InternetExplorer();
                    Host.Instance = new SelenoHost();
                    //Host.Instance.Run(configure => configure
                    //.WithWebServer(new TestStack.Seleno.Configuration.WebServers.InternetWebServer("https://develop-ci.siterra.com/"))
                    //.WithRemoteWebDriver(() => new RemoteWebDriver(new Uri("http://10.6.90.5:4444/wd/hub"),capability)));
                    Host.Instance.Run(configure => configure
                        .WithWebServer(new TestStack.Seleno.Configuration.WebServers.InternetWebServer("https://develop-ci.siterra.com/"))
                        .WithRemoteWebDriver(() => BrowserFactory.InternetExplorer()));
                    //Host.Instance.Run(configure => configure.WithWebServer(new TestStack.Seleno.Configuration.WebServers.InternetWebServer("https://develop-ci.siterra.com/"))
                    //    .WithRemoteWebDriver(() => new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capability)));

                    Host.Wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Host.Instance.Application.Browser, TimeSpan.FromSeconds(15));

                    Host.mainWindowHandle = Host.Instance.Application.Browser.CurrentWindowHandle;
                    Host.Instance.Application.Browser.Manage().Cookies.DeleteAllCookies();
                }
            }
            Helper.GotoLandingPage().InnerLoginPage.Login().PleaseWait();
            //Host.Instance.Application.Browser.Manage().Cookies.DeleteAllCookies();
            //foreach (OpenQA.Selenium.Cookie cookie in Host.InitialCookies)
            //{
            //    Host.Instance.Application.Browser.Manage().Cookies.AddCookie(cookie);
            //}
        }

        [TestCleanup]
        public void testCleanup()
        {
            //try
            //{
            //    Host.Instance.Application.Browser.SwitchTo().DefaultContent();
            //    Host.Instance.Application.Browser.FindElement(By.LinkText("Log Off")).Click();
            //    //System.Threading.Thread.Sleep(1000);
            //}
            //catch { }
            Host.Instance.Application.Browser.Quit();
        }

        [TestMethod]
        public void T001_Login()
        {
            var page = Helper.GotoLandingPage();

            page.PleaseWait();

            page.InnerLoginPage.username = "admin_user";
            page.InnerLoginPage.password = "Potato3$";
            page.InnerLoginPage.domain = "Orbitel";

            page.InnerLoginPage.Click_Login();

            page.Title.Should().Contain("Orbitel");

        }

        [TestMethod]
        public void T002_Login_Profile_URL()
        {
            var page = Helper.GotoLandingPage("Orbitel");

            page.PleaseWait();

            page.InnerLoginPage.username = "admin_user";
            page.InnerLoginPage.password = "Potato3$";

            page.InnerLoginPage.Click_Login();

            page.Title.Should().Contain("Orbitel");

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
            string address = "Update Search Ring " + DateTime.Now.ToString("s");
            MainPage m = Helper.GotoSearchRingHomePage(Default_SearchRing);
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
            string name = "Sanity";
            string number = DateTime.Now.ToString("s");
            string type = "Type B";
            MainPage m = Helper.GotoSearchRingHomePage(Default_SearchRing);
            SearchRingHomePage sr = m.Innerpage as SearchRingHomePage;
            SitePopup sp = sr.add_site();
            sp.Enter_Info(type, name, number, null);
            m.PleaseWait();
            sr.PleaseWait();
            string expected = name + " - " + number;
            m.InnerPageFindText(By.XPath("//a[contains(@title, '" + name + " - " + number + "')]")).Should().Be(expected);
        }

        [TestMethod]
        public void T006_Update_Site()
        {
            string address = "Sanity " + DateTime.Now.ToString("s");
            MainPage m = Helper.GotoSiteHomePage(Default_Site);
            SiteHomePage s = m.Innerpage as SiteHomePage;
            SitePopup sp = s.Click_Edit();
            sp.Update_Info(null, null, address);
            m.PleaseWait();
            m.Innerpage.PleaseWait();
            m.InnerPageFindText(By.Id("TXT_STREET")).Should().Be(address);
            //System.Threading.Thread.Sleep(5000);
        }

        [TestMethod]
        public void T007_AssignVendor2Site()
        {
            string testVendor = "2569317";
            MainPage m = Helper.GotoSiteHomePage(Default_Site);

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Assign_Vendor(testVendor)
            .ExistsInVendorSection(testVendor).Should().BeTrue();
        }

        [TestMethod]
        public void T008_AddAsset2Site()
        {
            string testAsset = "Sanity" + DateTime.Now.ToString("s");
            MainPage m = Helper.GotoSiteHomePage(Default_Site);

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Asset(testAsset)
            .ExistsInAssetSection(testAsset).Should().BeTrue();
        }

        [TestMethod]
        public void T009_AddIncident2Site()
        {
            string testProblem = "Sanity" + DateTime.Now.ToString("s"); ;
            MainPage m = Helper.GotoSiteHomePage(Default_Site);

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Incident(testProblem, "Laith dahiyat")
            .ExistsInIncidentSection(testProblem).Should().BeTrue();
        }

        [TestMethod]
        public void T010_AddEvent2Site()
        {
            string testName = "Sanity" + DateTime.Now.ToString("s");
            MainPage m = Helper.GotoSiteHomePage(Default_Site);

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Event(testName, "12/12/2015", "12/12/2015", "BM Group", "Selenium0")
                .ExistsInEventSection(testName).Should().BeTrue();

        }

        [TestMethod]
        public void T011_AddNote2Site()
        {
            string testNote = "Sanity Note" + DateTime.Now.ToString("s");
            MainPage m = Helper.GotoSiteHomePage(Default_Site);

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Note(testNote, "Go check your sele note")
                .ExistsInNoteSection(testNote)
                .Should().BeTrue();
        }

        [TestMethod]
        public void T012_AddLease2Site()
        {
            string testLease = "Sanity Lease" + DateTime.Now.ToString("s");
            MainPage m = Helper.GotoSiteHomePage(Default_Site);

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Lease(testLease, null, "This is a Selenium test")
                .ExistsInLeaseSection(testLease).Should().BeTrue();

        }

        [TestMethod]
        public void T013_Update_Lease()
        {
            string description = "Sanity Desc."+ DateTime.Now.ToString("s");
            MainPage m = Helper.GotoLeaseHomePage(Default_Lease);
            LeaseHomePage l = m.Innerpage as LeaseHomePage;
            LeasePopup p = l.Click_Edit();
            p.PleaseWait();
            p.Enter_Info(null, null, description).Save();
            m.PleaseWait();
            l.PleaseWait();
            m.InnerPageFindText(By.Id("TXT_STATUS_DESCRIPTION")).Should().Be(description);
        }

        [TestMethod]
        public void T014_createPayment()
        {
            string name = "Sanity Rent" + DateTime.Now.ToString("s");
            string type = "Rent";
            string firstDate = "07/11/2014";
            string secondDate = "08/11/2014";
            string toDate = "06/10/2015";
            string amount = "1000";
            MainPage m = Helper.GotoLeaseHomePage(Default_Lease);
            LeaseHomePage l = m.Innerpage as LeaseHomePage;
            PaymentPopup p = l.Add_Payment();
            p.Enter_Info(name, type, firstDate, secondDate, toDate, amount);
            p.Save();
            m.PleaseWait();
            l.PleaseWait();
            m.InnerPageFindText(By.PartialLinkText(name)).Should().Be(name);
        }

        [TestMethod]
        public void T015_addAllocation2Lease()
        {
            MainPage m = Helper.GotoLeaseHomePage(Default_Lease);
            LeaseHomePage l = m.Innerpage as LeaseHomePage;
        }

        [TestMethod]
        public void T015_createAllocation()
        {
            MainPage m = Helper.GotoLeaseHomePage("Denver;333 Easy Street (333);Leases (2);333 Easy Street Leases (333)");

            LeaseHomePage s = m.Innerpage as LeaseHomePage;
        }

        [TestMethod]
        public void T016_createOffset()
        {
            MainPage m = Helper.GotoLeaseHomePage("Denver;333 Easy Street (333);Leases (2);333 Easy Street Leases (333)");

            LeaseHomePage s = m.Innerpage as LeaseHomePage;


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
            string testProject = "Sanity" + DateTime.Now.ToString("s");
            MainPage m = Helper.GotoSiteHomePage(Default_Site);

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Project(testProject, "12/12/2015", "Active")
            .ExistsInLeftNavProjectSection(testProject).Should().BeTrue();
        }

        [TestMethod]
        public void T039_ChangeProjectDates()
        {
            string testDate = DateTime.Now.ToString("d");
            MainPage m = Helper.GotoProjectHomePage(Default_Project);

            ProjectHomePage p = m.Innerpage as ProjectHomePage;
            p.Goto_Edit_Schedule().Edit_Task("1.0",ProjectEditSchedulePage.TaskColumns.Baseline, "12/05/2015")
                .Edit_Task("1.0", ProjectEditSchedulePage.TaskColumns.Duration, "2")
                .Edit_Task("1.0",ProjectEditSchedulePage.TaskColumns.Manual, testDate)
                .Commit_Changes()
                .Compare_Date("1.0", ProjectEditSchedulePage.TaskColumns.Completion, testDate).Should().BeTrue();

        }

        [TestMethod]
        public void T079_Import()
        {
            string testPath=ImportPopup.generateUnitImportCSV();
            string testimport="Sanity" + DateTime.Now.ToString("s");
            MainPage m = Helper.GotoImportPage();
            ImportPage i = m.Innerpage as ImportPage;
            i.Add_Import()
                .Enter_Info(testimport, "Unit", "CSV", "Insert", @testPath)
                .Save()
                .Select_Process("import")
                .Execute_Import()
                .Close();
            System.Threading.Thread.Sleep(1000);
            i.ExistsInImportSection(testimport).Should().BeTrue();
        }

        [TestMethod]
        public void T080_responsibilities()
        {
            /***NOT DONE***/
            MainPage m = Helper.GotoResponsibilityComp();
            m.PleaseWait();
            ResponsibilityComp r = m.Innerpage as ResponsibilityComp;
            r.PleaseWait();

            r.searchResponsibility("Site", "xSiterra", 5);
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
    }
}
