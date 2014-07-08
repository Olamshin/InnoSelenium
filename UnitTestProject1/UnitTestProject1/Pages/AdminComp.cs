using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.Seleno.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestStack.Seleno.PageObjects.Actions;
using OpenQA.Selenium.Support.UI;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace UnitTestProject1.Pages
{
    public class AdminComp : SiterraComponent
    {
        public override void PleaseWait()
        {
            
            Browser.SwitchTo().Frame("MainFrame");
            Host.Wait.Until<Boolean>((d) =>
            {
                //System.Threading.Thread.Sleep(3000);
                //return d.FindElement(By.Id("DataDivContainer651847.7275465999"));
               // return d.PageSource.Contains("skins/common/images/icons/gif/folder_bug.gif");
                return d.PageSource.Contains("skins/common/images/icons/gif/group_key.gif");
            });
        }
        public AdminComp clickLink(String anchorText)
        {
            PleaseWait();
            Find.Element(By.LinkText(anchorText)).Click();
            return this;
            
        }
        /*public AdminPage clickNavHierarchy()
        {
            clickLink("Navigation / Hierarchy");
            this.Innerpage = this.GetComponent<UnitPage>();
            return this;
        }*/

        public void clickUnit()
        {
            PleaseWait();
            Find.Element(By.LinkText("Navigation / Hierarchy")).Click();
           
        }
        
        /*public AdminPage clickAddUser()
        {
            PleaseWait();
            Find.Element(By.LinkText("Add")).Click();
            return this;
        }*/

    }
}
