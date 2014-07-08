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
        public static LandingPage GotoLandingPage(string domain)
        {
            return Host.Instance.NavigateToInitialPage<LandingPage>("/" + domain);
        }

        public static MainPage GotoMainPage()
        {
            return Host.Instance.NavigateToInitialPage<MainPage>("/gisapi.dll?do=output&pageid=1032000&classid=1000000&objectid=1");
        }

    }
}
