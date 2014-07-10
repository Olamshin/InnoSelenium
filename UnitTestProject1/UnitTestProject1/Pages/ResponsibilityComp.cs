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
            //skins/common/images/icons/gif_buttons_solid/user_magnify.gif
            Browser.SwitchTo().DefaultContent();
            try //is browser already in mainframe?
            {
                Browser.SwitchTo().Frame("MainFrame");
                Host.Wait.Until<Boolean>((d) =>
                {
                    return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/user_magnify.gif");
                });
            }
            catch
            { //browser is already in mainframe
                Host.Wait.Until<Boolean>((d) =>
                {
                    return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/user_magnify.gif");
                });
            }

            Browser.SwitchTo().DefaultContent();
        }
        public void addValues(String display, String from, int roleNum)
        {
            //Display
            IWebElement dropDownListBox = Find.Element(By.Id("SearchObjectClassID"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText(display);

            //from
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
            PleaseWait();
            addValues(display, from, roleNum);
            search();
        }
    }
}
