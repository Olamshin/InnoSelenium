using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestStack.Seleno.PageObjects.Actions;
using OpenQA.Selenium.Support.UI;
using UnitTestProject1.Pages;
namespace UnitTestProject1
{
    public static class Helper
    {
        public static SearchPage GotoSearchPage()
        {
            return Host.Instance.NavigateToInitialPage<SearchPage>();
        }

        public static ImagesPage GotoImagesPage()
        {
            return GotoSearchPage().click_Images();
        }
        public static LandingPage GotoLandingPage()
        {
            return Host.Instance.NavigateToInitialPage<LandingPage>();
        }

        public static MainPage GotoAdminPage()
        {
            return GotoMainPage().clickAdmin();

        }
        public static LandingPage GotoLandingPage(string domain)
        {
            return Host.Instance.NavigateToInitialPage<LandingPage>("/" + domain);
        }

        public static MainPage GotoMainPage()
        {
            return Host.Instance.NavigateToInitialPage<MainPage>("/gisapi.dll?do=output&pageid=1032000&classid=1000000&objectid=1");
        }

        public static MainPage GotoUnitComp()
        {
            MainPage mainpage = GotoAdminPage();

            AdminComp a = (AdminComp)mainpage.Innerpage;
            a.clickUnit();

            mainpage.Innerpage = mainpage.GetComponent<UnitComp>();

            return mainpage;
        }
        public static MainPage GotoUserComp()
        {
            MainPage mainpage = GotoAdminPage();

            AdminComp a = (AdminComp)mainpage.Innerpage;
            a.PleaseWait();
            a.clickLink("Users");

            mainpage.Innerpage = mainpage.GetComponent<UserComp>();

            return mainpage;
        }

        public static MainPage GotoImportPage()
        {
            MainPage main = GotoMainPage();

            OpenQA.Selenium.Interactions.Actions builder =
                new OpenQA.Selenium.Interactions.Actions(Host.Instance.Application.Browser);
            builder.MoveToElement(Host.Instance.Application.Browser.FindElement(By.LinkText("Tools")))
                .Click().MoveByOffset(0,10).Build().Perform();

            builder.MoveToElement(Host.Instance.Application.Browser.FindElement(By.XPath("//table[contains(@id,'tblDynamicMenu')]/tbody/tr/td[text()='Import / Export']")))
                .Click().MoveByOffset(10,0).Build().Perform();

            builder.MoveToElement(Host.Instance.Application.Browser.FindElement(By.XPath("//table[contains(@id,'tblDynamicMenu')]/tbody/tr/td[text()='Import']")))
            .Click()
            .Build().Perform();

            main.Innerpage = main.GetComponent<ImportPage>();
            main.Innerpage.PleaseWait();
            return main;
        }

        /* public static MainPage GotoSearchHomePage()
         {
             MainPage main = GotoMainPage();
             return main.ClickNavTree("AD PRM Test Unit;MSTR_Test");
         }*/

        public static MainPage GotoSiteHomePage(String path)
        {
            MainPage main = GotoMainPage();
            main.Innerpage = main.ClickNavTree<SiteHomePage>(path);
            return main;
        }

        public static MainPage GotoProjectHomePage(String path)
        {
            MainPage main = GotoMainPage();
            main.Innerpage = main.ClickNavTree<ProjectHomePage>(path);
            return main;
        }

        public static MainPage GotoSearchRingHomePage(String path)
        {
            MainPage main = GotoMainPage();
            main.Innerpage = main.ClickNavTree<SearchRingHomePage>(path);
            return main;
        }

        public static MainPage GotoUnitHomePage(String path)
        {
            MainPage main = GotoMainPage();
            main.Innerpage = main.ClickNavTree<UnitHomePage>(path);
            return main;
        }

        public static MainPage GotoLeaseHomePage(String path)
        {
            MainPage main = GotoMainPage();
            main.Innerpage = main.ClickNavTree<LeaseHomePage>(path);
            return main;
        }

        public static MainPage GotoResponsibilityComp()
        {
            MainPage mainpage = GotoAdminPage();

            AdminComp a = (AdminComp)mainpage.Innerpage;
            a.PleaseWait();
            a.clickLink("Responsibility");

            mainpage.Innerpage = mainpage.GetComponent<ResponsibilityComp>();

            return mainpage;
        }
        public static MainPage GotoPortfolioComp()
        {
            MainPage m = GotoAdminPage();
            AdminComp a = (AdminComp)m.Innerpage;
            a.PleaseWait();
            a.clickLink("Portfolio");

            m.Innerpage = m.GetComponent<PortfolioComp>();

            return m;
        }
        public static MainPage GotoExtendedAttributesComp()
        {
            MainPage m = GotoAdminPage();
            AdminComp a = (AdminComp)m.Innerpage;
            a.PleaseWait();
            a.clickLink("Extended Attributes");

            m.Innerpage = m.GetComponent<ExtendedAttributesComp>();

            return m;
        }
        public static MainPage GotoLookUpTablesComp()
        {
            MainPage m = GotoAdminPage();
            AdminComp a = (AdminComp)m.Innerpage;
            a.PleaseWait();
            a.clickLink("Lookup Tables");

            m.Innerpage = m.GetComponent<LookupTablesComp>();

            return m;
        }


    }
}
