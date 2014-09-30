using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Pages
{
    public class ImportPage : SiterraComponent
    {
        private ImportSection _importsection;
        private ImportSection importSection
        {
            get
            {
                if (_importsection == null)
                {
                    _importsection = this.GetComponent<ImportSection>();
                    _importsection.init();
                }
                _importsection.Show();
                return _importsection;
            }
        }

        public override void PleaseWait()
        {
            var executor = Host.Instance.Application.Browser as IJavaScriptExecutor;
            Host.Wait.Until<Boolean>((Browser) =>
            {
                return executor.ExecuteScript("return document.readyState").Equals("complete");
            });

            SwitchIn();
            Host.Wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("SectionHeader100"));
            });
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.PageSource.Contains("indicator_gray.gif");
                //return !d.FindElement(By.Id("NavContainer")).ToString().Contains("indicator_gray.gif");
            });
            System.Threading.Thread.Sleep(1000);
            SwitchOut();
        }

        private void SwitchIn()
        {
            Browser.SwitchTo().Frame("MainFrame");
        }

        private void SwitchOut()
        {
            Browser.SwitchTo().DefaultContent();
        }

        public ImportPopup Add_Import()
        {
            return importSection.Add_Import();
        }

        private class ImportSection : SectionComponent
        {
            public ImportSection()
            {

            }
            public void init()
            {
                section_id = "100";
                grid_id = "142040100";
            }

            protected override void SwitchIn()
            {
                Browser.SwitchTo().Frame("MainFrame");
            }

            protected override void SwitchOut()
            {
                Browser.SwitchTo().DefaultContent();
            }
            public ImportPopup Add_Import()
            {
                SwitchIn();
                return Navigate.To<ImportPopup>(By.XPath("//a[contains(@onclick,'addImport();')]"));
            }
        }
    }
}
