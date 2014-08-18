﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.Seleno.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestStack.Seleno.PageObjects.Actions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace UnitTestProject1.Pages
{
    public class LeaseHomePage : SiterraComponent
    {
        private PaymentSection _paymentsection;
        private PaymentSection paymentSection
        {
            get
            {
                if (_paymentsection == null)
                { _paymentsection = this.GetComponent<PaymentSection>(); }
                _paymentsection.Show();
                return _paymentsection;
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
                return d.FindElement(By.Id("TXT_NAME"));
            });
            Host.Wait.Until<Boolean>((d) =>
            {
                return !d.PageSource.Contains("indicator_gray.gif");
                //return !d.FindElement(By.Id("NavContainer")).ToString().Contains("indicator_gray.gif");
            });
            SwitchOut();
        }

        private void SwitchIn()
        {
            Host.Instance.Application.Browser.SwitchTo().Frame("MainFrame");
        }

        private void SwitchOut()
        {
            Host.Instance.Application.Browser.SwitchTo().DefaultContent();
        }

        public LeasePopup Click_Edit()
        {
            SwitchIn();
            return Navigate.To<LeasePopup>(By.PartialLinkText("Edit"));
        }

        public PaymentPopup Add_Payment()
        {
            SwitchIn();
            //return Navigate.To<PaymentPopup>(By.XPath("//a[contains(@onclick, 'addPayment')]"));
            return paymentSection.Add_Payment();
        }

        private class PaymentSection : SectionComponent
        {
            public PaymentSection()
            {
                section_id = "114";
                grid_id = "723040100";
            }
            protected override void SwitchIn()
            {
                Browser.SwitchTo().Frame("MainFrame");
            }

            public PaymentSection SwitchFromPopup()
            {
                SwitchIn();
                return this;
            }

            protected override void SwitchOut()
            {
                Browser.SwitchTo().DefaultContent();
            }

            public PaymentPopup Add_Payment()
            {
                //SectionSearchByXPath("//a[contains(@onclick,'addPayment()')]").Click();
                //Find.Element(By.XPath("//a[contains(@onclick,'addPayment()')]")).Click();
                SwitchIn();
                return Navigate.To<PaymentPopup>(By.XPath("//a[contains(@onclick, 'addPayment')]"));
            }

            //public ObjectBrowserPage Payment_Select_Button()
            //{
            //    return Navigate.To<ObjectBrowserPage>(By.XPath("//a[contains(@onclick,'openVendorOBB(-1)')]"));
            //}

            //public VendorSection Submit_Vendor()
            //{
           //     Find.Element(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Click();
           //     return this;
            //}

            public PaymentSection PleaseWait()
            {
                Host.Wait.Until<Boolean>((d) =>
                {
                    return !d.FindElement(By.Id("PLACEHOLDER_" + grid_id)).Displayed;
                    //return !d.FindElement(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Displayed;
                });
                return this;
            }

        }

        private class AllocationSection : SectionComponent
        {
            private AllocationPlaceHolder _allocdialog;
            private AllocationPlaceHolder AllocDialog
            {
                get
                {
                    if (_allocdialog == null)
                    {
                        _allocdialog = this.GetComponent<AllocationPlaceHolder>();
                    }
                    return _allocdialog;
                }
            }
            public AllocationSection()
            {
                section_id = "112";
                grid_id = "203040100";
            }
            protected override void SwitchIn()
            {
                Browser.SwitchTo().Frame("MainFrame");
            }

            public AllocationSection SwitchFromPopup()
            {
                SwitchIn();
                return this;
            }

            protected override void SwitchOut()
            {
                Browser.SwitchTo().DefaultContent();
            }

            public AllocationSection Add_Allocation(string type, string costcenter)
            {
                SectionSearchByXPath("//a[contains(@onclick, 'addAllocation()')]").Click();
                //Find.Element(By.XPath("//a[contains(@onclick,'addPayment()')]")).Click();
                SwitchIn();
                AllocDialog.Type = type;
                AllocDialog.CostCenter = costcenter;
                AllocDialog.Click_Save();
                return this;
                //return Navigate.To<PaymentPopup>(By.XPath("//a[contains(@onclick, 'addAllocation()')]"));
            }

            public AllocationSection PleaseWait()
            {
                Host.Wait.Until<Boolean>((d) =>
                {
                    return !d.FindElement(By.Id("PLACEHOLDER_" + grid_id)).Displayed;
                    //return !d.FindElement(By.XPath("//input[contains(@onclick,'saveResponsibility(-1)')]")).Displayed;
                });
                return this;
            }

        }

        private class AllocationPlaceHolder : PlaceHolderComp
        {
            public string Type
            {
                set
                {
                    IWebElement dropDownListBox = Find.Element(By.Id("ALLOC_TYPE"));
                    SelectElement clickThis = new SelectElement(dropDownListBox);
                    clickThis.SelectByText(value);
                }
            }

            public string CostCenter
            {
                set
                {
                    IWebElement dropDownListBox = Find.Element(By.Id("ALLOC_COST_CENTER_ID"));
                    SelectElement clickThis = new SelectElement(dropDownListBox);
                    clickThis.SelectByText(value);
                }
            }

            public string CalculateBy
            {
                set
                {
                    IWebElement dropDownListBox = Find.Element(By.Id("ALLOC_CALC_TYPE"));
                    SelectElement clickThis = new SelectElement(dropDownListBox);
                    clickThis.SelectByText(value);
                }
            }

            public string Percent
            {
                set
                {
                    Execute.ActionOnLocator(By.Name("ALLOC_SHARE"), e => { e.Clear(); e.SendKeys(value); });
                }
            }

            public string Headcount
            {
                set
                {
                    Execute.ActionOnLocator(By.Name("ALLOC_HEADCOUNT"), e => { e.Clear(); e.SendKeys(value); });
                }
            }

            public string AllocUtilization
            {
                set
                {
                    IWebElement dropDownListBox = Find.Element(By.Id("ALLOC_UTILIZATION"));
                    SelectElement clickThis = new SelectElement(dropDownListBox);
                    clickThis.SelectByText(value);
                }
            }

            public string Description
            {
                set
                {
                    Execute.ActionOnLocator(By.Name("ALLOC_DESCRIPTION"), e => { e.Clear(); e.SendKeys(value); });
                }
            }

            internal AllocationPlaceHolder Click_Save()
            {
                IWebElement elem = Find.Element(By.XPath("//a[contains(@onclick,'saveAllocation(')]"));
                elem.Click();
                Host.Wait.Until<Boolean>((d) =>
                {
                    try
                    {
                        return !elem.Displayed;
                    }
                    catch (OpenQA.Selenium.StaleElementReferenceException e)
                    {
                        return true;
                    }
                });
                return this;
            }
        }
    }
}
