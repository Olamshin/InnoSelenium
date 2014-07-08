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
        private SiterraComponent _Innerpage;
        private TreeSelection _tree;

        public SiterraComponent Innerpage
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

        private TreeSelection tree
        {
            get
            {
                if (_tree == null)
                {
                    _tree = this.GetComponent<TreeSelection>();
                }
                return _tree;
            }

            set
            {
                _tree = value;
            }
        }

        public T ClickNavTree<T>(string node_path) where T : SiterraComponent, new()
        {
            clickBrowseLeftNav();
            tree.Click_Node(node_path);
            Innerpage = this.GetComponent<T>();
            Innerpage.PleaseWait();
            return Innerpage as T;
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

        public String InnerPageFindText(By findExpression)
        {
            Browser.SwitchTo().Frame("MainFrame");
            String e = Find.Element(findExpression).Text.Trim();
            Browser.SwitchTo().DefaultContent();
            return e;

        }

        public MainPage clickLeaseLeftNav()
        {
            PleaseWait();
            Find.Element(By.Id("imgSetGlobalNavLeases")).Click();
            return this;
        }

        public MainPage clickBrowseLeftNav()
        {
            PleaseWait();
            Find.Element(By.Id("tdSetGlobalNavBrowse")).Click();
            this.tree = GetComponent<TreeSelection>();
            return this;
        }

        public MainPage clickSiteLeftNav()
        {
            PleaseWait();
            Find.Element(By.Id("imgSetGlobalNavSites")).Click();
            return this;
        }

        public MainPage leftNavBrowse()
        {
            PleaseWait();
            Find.Element(By.Id("tdSetGlobalNavBrowse")).Click();
            return this;
        }


        public MainPage Click_Filter_Button()
        {
            PleaseWait();
            Find.Element(By.Id("BtnGlobalNavFilter")).Click();
            return this;
        }

        public MainPage Click_ToDoList_Link()
        {
            Browser.SwitchTo().Frame("MainFrame");
            Find.Element(By.LinkText("To-Dos / Activity")).Click();
            Browser.SwitchTo().DefaultContent();
            this.Innerpage = this.GetComponent<ToDoListPage>();
            return this;
        }
        public TreeSelection Click_TreeSelection()
        {
            PleaseWait();
            //Actions builder = new Actions(Browser);
            //builder.MoveToElement(Find.Element(By.Id("TreeSelection"))).Build().Perform();
            Find.Element(By.Id("TreeSelection")).Click();
            return this.GetComponent<TreeSelection>();
        }
        public MainPage Click_Search_Ring_Unit()
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Browser.SwitchTo().Frame("frameGlobalNavBrowse");
            Find.Element(By.PartialLinkText("0Notify First Round")).Click();
            Browser.SwitchTo().DefaultContent();
            this.Innerpage = this.GetComponent<UnitHomePage>();
            return this;
        }
        public MainPage clickAdmin()
        {
            PleaseWait();
            Find.Element(By.LinkText("Admin")).Click();
            this.Innerpage = this.GetComponent<AdminComp>();
            return this;
        }
    }
    public class TreeSelection : UiComponent
    {
        public void PleaseWait()
        {
            Browser.SwitchTo().DefaultContent();
            Browser.SwitchTo().Frame("MainFrame");
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("tabsContainer"));
            });
            Browser.SwitchTo().DefaultContent();
        }
        private void NodeWait()
        {
            Host.Wait.Until<Boolean>((d) =>
            {
                //return !d.FindElement(By.Id("GRID_INDICATOR_716040300")).GetCssValue("display").Equals("none");
                return d.FindElement(By.XPath("//table[@class ='GridNavigator']/tbody/tr/td/img")).GetCssValue("display").Equals("none");
            });
        }

        public TreeSelection Click_Node(string node_path)
        {
            IWebElement node;
            string[] s = node_path.Split(';');
            if (s.Length == 1)
            {
                var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
                // Browser.SwitchTo().Frame(Find.Element(By.Id("divGlobalNavBrowse")).FindElement(By.Id("frameGlobalNavBrowse")));
                Browser.SwitchTo().Frame(Find.Element(By.Id("frameGlobalNavBrowse")));

                node = Find.Element(By.PartialLinkText(" " + s[0] + " "));
                node.Click();

                Browser.SwitchTo().DefaultContent();
                NodeWait();
            }
            else
            {
                Browser.SwitchTo().Frame(Find.Element(By.Id("frameGlobalNavBrowse")));
                foreach (string node_name in s)
                {
                    node = Find.Element(By.PartialLinkText(" " + node_name + " "));
                    var dynamicnodeid = node.GetAttribute("id");
                    System.Threading.Thread.Sleep(3000);
                    if (s.Last().Equals(node_name))
                    {
                        Find.Element(By.Id(dynamicnodeid)).Click();
                    }
                    else
                    {
                        Find.Element(By.Id("ImageNode" + dynamicnodeid)).Click();
                    }

                    Host.Wait.Until<Boolean>((d) =>
                    {
                        return !d.PageSource.Contains("loadingSM.gif");
                        //return !d.FindElement(By.Id("NavContainer")).ToString().Contains("indicator_gray.gif");
                    });
                }
                Browser.SwitchTo().DefaultContent();
            }
            //System.Threading.Thread.Sleep(2000);
            return this;
            // System.Threading.Thread.Sleep(1000);

        }
    }
}
