using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.Seleno.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestStack.Seleno.PageObjects.Actions;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1.Pages
{
    class LookupTablesComp: SiterraComponent
    {
        public override void PleaseWait()
        {
            switchIn();
            Host.Wait.Until<Boolean>((d) =>
            {
                return d.PageSource.Contains("skins/common/images/icons/gif_grey/resultset_last_disabled.gif");
            });

            switchOut();
        }
        public Boolean PageLoaded()
        {
            switchIn();
            return Browser.PageSource.Contains("Table Name");
        }
        public void switchIn()
        {
            Browser.SwitchTo().Frame("MainFrame");
        }
        public void switchOut()
        {
            Browser.SwitchTo().DefaultContent();
        }
    }
}
