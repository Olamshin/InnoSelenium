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
    public class UnitComp : UiComponent
    {
        public string unitName
        { 
            set{
                //Browser.SwitchTo().Frame("MainFrame");
                Execute.ActionOnLocator(By.Id("TXT_UNIT_NAME_-1_-1"), e => { e.Clear(); e.SendKeys(value); });
                Browser.SwitchTo().DefaultContent();
            }
        }
        public string allowedChildType
        {
            set
            {
                Browser.SwitchTo().DefaultContent();
                Browser.SwitchTo().Frame("MainFrame");
                IWebElement dropDownListBox = Find.Element(By.Id("TXT_CONTAINER_TYPE_-1_-1"));
                SelectElement clickThis = new SelectElement(dropDownListBox);
                clickThis.SelectByText(value);
            }
        }
        public void PleaseWait()
        {
            
            Host.Wait.Until<Boolean>((d) =>
            {
                //System.Threading.Thread.Sleep(3000);
                //return d.FindElement(By.Id("DataDivContainer651847.7275465999"));
                // return d.PageSource.Contains("skins/common/images/icons/gif/folder_bug.gif");
                return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/add.gif");
            });
        }
        public void buildOrgUnit(string unitType)
        {
            unitName = "xSiterra " + DateTime.Now.Day;
            allowedChildType = unitType;

        }
        public UnitComp clickSubmit()
        {
            Find.Element(By.LinkText("Submit")).Click();
            return this;
        }
        public UnitComp clickAddOrgUnit(string unitType)
        {
            PleaseWait();
            Find.Element(By.LinkText("Add")).Click();

            buildOrgUnit(unitType);
            clickSubmit();
           
            return this;
        }

        


    }
}
