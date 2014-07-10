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
    public class UserSubscriptionsPopUp : Page
    {
        private String handle;

        public UserSubscriptionsPopUp()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=726030100&ClassID=726000000"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
            //PleaseWait for instantiation
            Host.Instance.Application.Browser.SwitchTo().DefaultContent();

            Host.Wait.Until<Boolean>((d) =>
            {
                return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/disk_check.gif");
            });
        }
         public void PleaseWait()
        {
            Browser.SwitchTo().DefaultContent();

            Host.Wait.Until<Boolean>((d) =>
            {
                return d.PageSource.Contains("skins/common/images/icons/gif_buttons_solid/disk_check.gif");
            });
        }
        public void addValues(String notifyFor, String when, String scope)
         {
            //Notification For
             IWebElement dropDownListBox = Find.Element(By.Id("CMB_CLASS_ID"));
             SelectElement clickThis = new SelectElement(dropDownListBox);
             clickThis.SelectByText(notifyFor);

             System.Threading.Thread.Sleep(3000);
            //When
             IWebElement dropDownListBox2 = Find.Element(By.Id("CMB_EVENT_TYPE_ID"));
             SelectElement clickThis2 = new SelectElement(dropDownListBox2);
             clickThis.SelectByText(when);

            //scope
            String cssScope = "option[value='"+scope+"']";
            Find.Element(By.CssSelector(cssScope));
         }
        public UserSubscriptionsPopUp saveAndClose() {
            Find.Element(By.LinkText("Save & Close")).Click();
            Host.Instance.Application.Browser.SwitchTo().Window(Host.mainWindowHandle); //Implement stack for handles
            Browser.SwitchTo().DefaultContent();
            return this;
        }
        public void addSubscription()
        {
            PleaseWait();
            Browser.SwitchTo().DefaultContent();
            addValues("Project", "Created", "xSiterra");
        }

    }
}
