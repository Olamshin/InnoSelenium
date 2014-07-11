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
    class PortfolioComp : SiterraComponent
    {
         public override void PleaseWait()
        {
            switchIn();
             try
            {
                Host.Wait.Until<Boolean>((d) =>
                {
                    return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/cross.gif");
                });
            }
            catch
            {
                System.Threading.Thread.Sleep(1000);
            }
             switchOut();
        }
        public Boolean PageLoaded()
         {
             switchIn();     
            return Browser.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/image_delete.gif");

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
