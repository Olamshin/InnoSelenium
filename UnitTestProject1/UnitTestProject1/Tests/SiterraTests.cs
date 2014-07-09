﻿using System;
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
        public void login()
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
        public void loginProfileURL()
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
        public void createSearchRing()
        {
            MainPage m = Helper.GotoMainPage()
                .leftNavBrowse()
                .Click_Search_Ring_Unit();

            UnitHomePage u = (UnitHomePage)m.Innerpage;
                u.Select_Search_Ring_Grid()
                .Create_Search_Ring();
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
        public void T035_createUser()
        {
            MainPage m = Helper.GotoUserComp();
            UserComp user = m.Innerpage as UserComp;
            NewUserPage newUser =  user.addUser();
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
            NewUserPage n= user.selectFirstUser();

            String userName = n.updateUser();

            System.Threading.Thread.Sleep(2000);

            user.findUser(userName, "User Name");

            m.InnerPageFindText(By.CssSelector("a[@id$='_DISPLAY_NAME']")).Should().NotBe("Sanity Test User");

        }
        [TestMethod]
        public void T037_createUserSubscriptions() {
            MainPage m = Helper.GotoUserComp();
            UserComp user = m.Innerpage as UserComp;
            user.PleaseWait();

            System.Threading.Thread.Sleep(4000);

            user.findUser("Sanity", "User Name");
            NewUserPage n = user.selectFirstUser();
            n.PleaseWait();
            UserSubscriptionsPopUp uspu = n.addNotifications();
            //uspu.addSubscription();

        }
		[TestMethod]
        public void gotoToDoList()
        {
            Helper.GotoMainPage()
                .Click_ToDoList_Link();
        }

        [TestMethod]
        public void T006_Update_Site()
        {
            MainPage m = Helper.GotoSiteHomePage();
            SiteHomePage s = m.Innerpage as SiteHomePage;
            SitePopup sp =s.click_edit();
            sp.PleaseWait();
            sp.address = "fail yeah";
            sp.Save();
            m.PleaseWait();
            m.Innerpage.PleaseWait();
            m.InnerPageFindText(By.Id("TXT_STREET")).Should().Be("fail yeah");
            //System.Threading.Thread.Sleep(5000);
        }

        [TestMethod]
        public void T007_AssignVendor2Site()
        {
            MainPage m = Helper.GotoSiteHomePage();

            SiteHomePage s = m.Innerpage as SiteHomePage;
            s.add_vendor()
                .select_vendor()
                .Select_Object<SiteHomePage>(s)
                .Save_Vendor();
           
        }

    }
}
