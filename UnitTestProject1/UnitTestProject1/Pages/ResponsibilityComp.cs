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
    class ResponsibilityComp : SiterraComponent
    {
        public override void PleaseWait()
        {
            try
            {
                Host.Wait.Until<Boolean>((d) =>
                   {
                       return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/user_magnify.gif");
                   });
            }
            catch
            {
                System.Threading.Thread.Sleep(1000);
            }

        }
        public void switchOut()
        {
            Browser.SwitchTo().DefaultContent();
        }
        public void switchIn()
        {
            Browser.SwitchTo().Frame("MainFrame");
        }
        public void addValues(String display, String from, int roleNum)
        {
            //Display
            IWebElement dropDownListBox = Find.Element(By.Id("SearchObjectClassID"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText(display);

            //from
            Find.Element(By.Id("3121681"));

            IWebElement dropDownListBox2 = Find.Element(By.Id("AdminTreeSelection"));
            SelectElement clickThis2 = new SelectElement(dropDownListBox2);
            clickThis2.SelectByText(from);

            //include child objects
            Find.Element(By.Id("chkChildObjects")).Click();

            //select the role
            String cssNum="select[id='selRoleFilter'] *:nth-child("+roleNum+")";
            Find.Element(By.CssSelector(cssNum));
        }
        public void search()
        {
            Find.Element(By.Id("btnSearch")).Click();
        }
        public void searchResponsibility(String display, String from, int roleNum)
        {
            switchOut();
            switchIn();
            PleaseWait();
            addValues(display, from, roleNum);
            search();
            switchOut();
        }
    }
}
