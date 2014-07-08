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
            a.clickLink("Users");

            mainpage.Innerpage = mainpage.GetComponent<UserComp>();

            return mainpage;
        }

    }
}
