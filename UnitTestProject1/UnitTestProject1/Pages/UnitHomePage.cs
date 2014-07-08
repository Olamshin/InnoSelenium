﻿using System;
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
    public class UnitHomePage : UiComponent
    {

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

        public UnitHomePage Select_Search_Ring_Grid()
        {
            PleaseWait();
            Browser.SwitchTo().Frame("MainFrame");
            Find.Element(By.Id("btnContentSelector")).Click();
            Find.Element(By.PartialLinkText("Search Ring")).Click();
            return this;
        }

        public SearchRingPopup Create_Search_Ring()
        {
            PleaseWait();
            return Navigate.To<SearchRingPopup>(By.PartialLinkText("Add"));
        }
    }
}