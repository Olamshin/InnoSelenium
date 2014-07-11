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
        private TreeSelector _tree;

        private TreeSelector tree
        {
            get
            {
                if (_tree == null)
                {
                    _tree = this.GetComponent<TreeSelector>();
                }
                return _tree;
            }

            set
            {
                _tree = value;
            }
        }
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
            /*Find.Element(By.Id("3121681"));

            IWebElement dropDownListBox2 = Find.Element(By.Id("AdminTreeSelection"));
            SelectElement clickThis2 = new SelectElement(dropDownListBox2);
            clickThis2.SelectByText(from);*/

            //Find.Element(By.Id("AdminTreeSelection")).Click();
            tree.clickNode(from);

            //include child objects
            Find.Element(By.Id("chkChildObjects")).Click();

            /*//select the role
            Find.Element(By.Id("lnkRoleFilter")).Click();
            System.Threading.Thread.Sleep(2000);
            String cssNum="select[id='selRoleFilter'] *:nth-child("+roleNum+")";
            Find.Element(By.CssSelector(cssNum)).Click();*/
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
            //search();
            //switchOut();
        }
        private class TreeSelector: UiComponent
        {
            public void pleaseWait() 
            {
                Host.Wait.Until<Boolean>((d) =>
                {
                    return d.PageSource.Contains("skins/common/images/icons/gif/folder.gif");
                });
            }
            public void switchIn()
            {
                Browser.SwitchTo().DefaultContent();
                Browser.SwitchTo().Frame("ifrmTree");
            }
            public void switchToMainFrame()
            {
                Browser.SwitchTo().DefaultContent();
                Browser.SwitchTo().Frame("MainFrame");
            }
            public void nodeWait() { }
            public TreeSelector clickNode(String nodePath) {
                //switchIn();
                
                IWebElement node;
                string[] s = nodePath.Split(';');
                if (s.Length == 1)
                {
                    var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
                    // Browser.SwitchTo().Frame(Find.Element(By.Id("divGlobalNavBrowse")).FindElement(By.Id("frameGlobalNavBrowse")));
                    //switchIn();
                    Find.Element(By.Id("AdminTreeSelection")).Click();
                    node = Find.Element(By.PartialLinkText(s[0]));
                    node.Click();

                    

                    /**/Browser.SwitchTo().DefaultContent();
                    //NodeWait();
                }
                /*else
                {
                    switchIn();
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
                }*/
                //System.Threading.Thread.Sleep(2000);
                return this;
            }
        }
    }
}
