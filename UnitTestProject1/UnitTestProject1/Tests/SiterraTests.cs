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
        public void T003_Create_Search_Ring()
        {
            MainPage m = Helper.GotoMainPage()
                .leftNavBrowse()
                .Click_Search_Ring_Unit();

            UnitHomePage u = (UnitHomePage)m.Innerpage;
                u.Create_Search_Ring();
        }

        [TestMethod]
        public void testAdminPage() 
        {
            AdminComp admin = (AdminComp)Helper.GotoAdminPage()
                .Innerpage;
                admin.clickLink("Library Templates");
             /*   admin.Find.Element(By.Id("SectionHeader100")).Should().NotBeNull();*/
            

        }
        /*[TestMethod]
        public void createUser()
        {
            AdminPage admin = (AdminPage)Helper.GotoAdminPage()
                                                .Innerpage;
            admin.clickUsers();
            admin.clickAddUser();

        }*/
        [TestMethod]
        public void createOrgUnit()
        {
            UnitComp unit = (UnitComp)Helper.GotoUnitComp()
                                            .Innerpage;
            unit.clickAddOrgUnit("Site");
        }

		[TestMethod]
        public void gotoToDoList()
        {
            Helper.GotoMainPage()
                .Click_ToDoList_Link();
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
            m.Innerpage.PleaseWait();
            string actual = m.InnerPageFindText(By.XPath("//a[contains(@title, '" + name + "')]"));
            string expected = name + " - " + number;
            //m.InnerPageFindText(By.CssSelector("table#TABLES_SITES a")).Should().Be("Selenium Site - WatirprufAutobots");
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void T006_Update_Site()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");
            SiteHomePage s = m.Innerpage as SiteHomePage;
            SitePopup sp =s.click_edit();
            sp.PleaseWait();
            sp.address = "paul test";
            sp.Save();
            m.PleaseWait();
            m.Innerpage.PleaseWait();
            m.InnerPageFindText(By.Id("TXT_STREET")).Should().Be("paul test");
            //System.Threading.Thread.Sleep(5000);
        }

        [TestMethod]
        public void T007_AssignVendor2Site()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Assign_Vendor()
                .Vendor_Select_Button()
                .Select_Object<SiteHomePage>("2648188", s)
                .Submit_Vendor();
            s.ExistsInVendorSection("2648188").Should().BeTrue();
           
        }

        [TestMethod]
        public void T008_AddAsset2Site()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.Add_Asset().Select_Type().Save();
            s.ExistsInAssetSection("New HVAC").Should().BeTrue();
        }

        [TestMethod]
        public void T009_AddIncident2Site()
        {
            MainPage m = Helper.GotoSiteHomePage("Denver;333 Easy Street (333)");

            SiteHomePage s = m.Innerpage as SiteHomePage;
            IncidentPopup i = s.Add_Incident().Select_Type();
            i.AssignedToUser = "Frederic Bottling";
            i.Problem = "Yes it is a Problem";
            i.Save();
            s.ExistsInIncidentSection("Yes it is a Problem").Should().BeTrue();
        }
        [TestMethod]
        public void T035_createUser()
        {
            MainPage m = Helper.GotoUserComp();
            UserComp user = m.Innerpage as UserComp;
            NewUserPage newUser = user.addUser();
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
        public void T080_responsibilities()
        {
            MainPage m = Helper.GotoResponsibilityComp();
            m.PleaseWait();
            ResponsibilityComp r = m.Innerpage as ResponsibilityComp;
            r.PleaseWait();

            r.searchResponsibility("Site","xSiterra", 5);

        }
    }
}
