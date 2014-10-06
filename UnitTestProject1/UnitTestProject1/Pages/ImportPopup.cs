using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno.PageObjects;
using System.Windows.Forms;
using System.IO;

namespace UnitTestProject1.Pages
{
    public class ImportPopup : Page
    {
        private string handle;

        private string Name
        {
            set
            {
                Execute.ActionOnLocator(By.Id("TXT_NAME"), e => { e.Clear(); e.SendKeys(value); });
            }
        }

        private string Action
        {
            set
            {
                if (value.ToLower().Equals("insert"))
                {
                    Find.Element(By.Id("radActionInsert")).Click();
                }
                else if (value.ToLower().Equals("update"))
                {
                    Find.Element(By.Id("radActionUpdate")).Click();
                }
                else if (value.ToLower().Equals("delta"))
                {
                    Find.Element(By.Id("radActionDelta")).Click();
                }
            }
        }
        private string Process
        {
            set
            {
                if (value.ToLower().Equals("dry run"))
                {
                    Find.Element(By.Id("radProcessDRYRUN")).Click();
                }
                else if (value.ToLower().Equals("import"))
                {
                    Find.Element(By.Id("radProcessIMPORT")).Click();
                }
            }
        }

        public ImportPopup()
        {
            foreach (string a in Host.Instance.Application.Browser.WindowHandles)
            {
                if (!a.Equals(Host.mainWindowHandle))
                {
                    if (Host.Instance.Application.Browser.SwitchTo().Window(a).Url.Contains("PageID=902030200&ClassID=902000000"))
                        handle = a;
                }
            }
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
        }

        public ImportPopup Select_Type(String pmtType)
        {
            IWebElement dropDownListBox = Find.Element(By.Id("CMB_CLASSES"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText(pmtType);
            return this;
        }

        public ImportPopup Select_Format(String formatType)
        {
            IWebElement dropDownListBox = Find.Element(By.Id("CMB_IMPORT_FILE_FORMATS"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText(formatType);
            return this;
        }

        public ImportPopup Select_File(string filepath)
        {
            Browser.SwitchTo().Frame("UploadFrame");
            Find.Element(By.Id("uploadSelectBtn")).Click();
            System.Threading.Thread.Sleep(1000);
            SendKeys.SendWait(filepath);
            System.Threading.Thread.Sleep(1000);
            SendKeys.SendWait("{ENTER}"); //ENter Key
            System.Threading.Thread.Sleep(1000);
            Browser.SwitchTo().DefaultContent();
            return this;
        }

        public ImportPopup Select_Process(string process)
        {
            Process = process;
            return this;
        }

        public ImportPopup Enter_Info(string name, string type, string format, string action, string filepath)
        {
            System.Threading.Thread.Sleep(1000);
            if (name != null) Name = name;
            if (type != null) Select_Type(type);
            if (format != null) Select_Format(format);
            if (action != null) Action = action;
            if (filepath != null) Select_File(filepath);
            return this;
        }

        public ImportPopup Save()
        {
            Find.Element(By.Id("btnSave")).Click();

            Host.Wait.Until<Boolean>((d) =>
            {
                try
                {
                    return !d.FindElement(By.Id("trFileProgressBar")).Displayed;
                }
                catch { return true; }
            });
            System.Threading.Thread.Sleep(1000);
            Host.Wait.Until<Boolean>((d) =>
            {
                return d.FindElement(By.Id("FILE_INFO")).Displayed;
            });

            return this;
        }

        public ImportPopup Execute_Import()
        {
            Navigate.To<ModalDialog>(By.Id("btnExecute"));

            Find.Element(By.Id("OK")).Click();
            Host.Instance.Application.Browser.SwitchTo().Window(handle);
            return this;   
        }

        public void Close()
        {
            System.Threading.Thread.Sleep(3000);

            string temphandle = Browser.CurrentWindowHandle;
            Find.Element(By.Id("btnCancel")).Click();
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.WindowHandles.Contains(temphandle);
            });
            Host.Instance.Application.Browser.SwitchTo().Window(Host.mainWindowHandle); //Implement stack for handles
            Browser.SwitchTo().DefaultContent();
        }

        public static string generateUnitImportCSV()
        {
            string systemPath = System.Environment.
                             GetFolderPath(
                                 Environment.SpecialFolder.CommonApplicationData
                             );
            string filePath = Path.Combine(systemPath, "Sanity\\UnitImport.csv");

            (new FileInfo(filePath)).Directory.Create();
            File.Create(filePath).Close();

            string delimiter = ",";
            string[][] output = new string[][]{
            new string[]{"*Name - VARCHAR2(60)","**Number - VARCHAR2(30)","Description - VARCHAR2(255)","Parent Unit - Object number","Container Type - NUMBER(2)","Program - Object number","Unit Attribute - TRUE / FALSE","Program Budget Code - Text(60)"},
            new string[] { "UNIt1",null,null,null,null,null,null,null }
            };
            //output[1] = new string[] { "UNIt1",null,null,null,null,null,null,null };
            int length = output.GetLength(0);
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(delimiter, output[index]));
            File.AppendAllText(filePath, sb.ToString());
            return filePath;
        }

    }
}
