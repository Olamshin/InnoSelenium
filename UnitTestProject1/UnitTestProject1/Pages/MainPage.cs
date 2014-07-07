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
    public class MainPage : Page
    {
        private UiComponent _Innerpage;

        public UiComponent Innerpage
        {
            get
            {
                if (_Innerpage == null)
                {
                    _Innerpage = this.GetComponent<MyHomePage>();
                }
                return _Innerpage;
            }

            set
            {
                _Innerpage = value;
            }
        }

        public void PleaseWait()
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Host.Wait.Until<Boolean>((Browser) =>
            {
                return executor.ExecuteScript("return document.readyState").Equals("complete");
            });

            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.PageSource.Contains("indicator_gray.gif");
                //return !d.FindElement(By.Id("NavContainer")).ToString().Contains("indicator_gray.gif");
            });
        }

        public MainPage clickLeaseLeftNav()
        {
            PleaseWait();
            Find.Element(By.Id("imgSetGlobalNavLeases")).Click();
            return this;
        }

        public MainPage clickSiteLeftNav()
        {
            PleaseWait();
            Find.Element(By.Id("imgSetGlobalNavSites")).Click();
            return this;
        }
        public MainPage Click_Filter_Button()
        {
            PleaseWait();
            Find.Element(By.Id("BtnGlobalNavFilter")).Click();
            return this;
        }
        public TreeSelection Click_TreeSelection()
        {
            PleaseWait();
            Find.Element(By.Id("TreeSelection")).Click();
            return this.GetComponent<TreeSelection>();
        }
    }
    public class TreeSelection : UiComponent
    {
        public void PleaseWait()
        {
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.FindElement(By.Id("ifrmTree")).GetCssValue("display").Equals("none");
            });
        }
        private void NodeWait()
        {
            Host.Wait.Until<Boolean>((d) =>
            {
                //return !d.FindElement(By.Id("GRID_INDICATOR_716040300")).GetCssValue("display").Equals("none");
                return d.FindElement(By.XPath("//table[@class ='GridNavigator']/tbody/tr/td/img")).GetCssValue("display").Equals("none");
            });
        }

        public TreeSelection Click_Node(string org_name)
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Browser.SwitchTo().Frame(Find.Element(By.Id("divTreeContainer")).FindElement(By.Id("ifrmTree")));

            executor.ExecuteScript(Find.Element(By.Id("TableNode1000056"))
                .FindElement(By.XPath("//table[@title = '"+org_name+"']//a[@class ='TreeLink']"))
                .GetAttribute("onclick")+";");

            Browser.SwitchTo().DefaultContent();
            NodeWait();
            return this;
           // System.Threading.Thread.Sleep(1000);

        }
    }
}
