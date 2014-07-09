using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Actions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestStack.Seleno.Configuration;

namespace UnitTestProject1
{
    [TestClass,Ignore]
    public class GoogleSearchTests
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Host.Instance = new SelenoHost();
            Host.Instance.Run(configure => configure
            .WithWebServer(new TestStack.Seleno.Configuration.WebServers.InternetWebServer("https://www.google.com"))
            .WithRemoteWebDriver(() => BrowserFactory.InternetExplorer()));
            Host.InitialCookies = Host.Instance.Application.Browser.Manage().Cookies.AllCookies;
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

        [TestCleanup]
        public void testCleanup()
        {
            //Host.Instance.Dispose();
        }


        [TestMethod]
        public void should_be_able_to_search()
        {
            var results = Helper.GotoSearchPage()
                .InputSearchTerm("JackAss")
                .Search();
            //System.Threading.Thread.Sleep(5000);
            results.Title.Should().Be("JackAss - Google Search");
        }

        [TestMethod]
        public void I_am_feeling_lucky()
        {
            var searchPage = Host.Instance.NavigateToInitialPage<SearchPage>();
            var resultsPage = searchPage
                .Feeling_lucky();
        }

        [TestMethod]
        public void gotoImages()
        {
            var searchPage = Host.Instance.NavigateToInitialPage<SearchPage>();
            var imagespage = searchPage
                   .click_Images();
            imagespage.Title.Should().Be("Google Images");
        }

        [TestMethod]
        public void search_panda()
        {
            var imagepage = Host.Instance.NavigateToInitialPage<SearchPage>()
               .click_Images()
               .InputSearchTerm("Panda")
               .Press_enter()
               .Title
               .Should().Be("Panda - Google Search");
        }
        [TestMethod]
        public void click_first_image()
        {
            var imgpage = Helper.GotoImagesPage()
                .InputSearchTerm("Panda")
                .Press_enter()
                .Click_Image(1);
            imgpage.Find.Element(By.Id("irc_bg")).GetCssValue("display").Should().NotBe("none");
        }

    }
}
