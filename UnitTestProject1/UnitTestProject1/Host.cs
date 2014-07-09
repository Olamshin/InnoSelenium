﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.Seleno;
using TestStack.Seleno.Configuration;
using TestStack.Seleno.Configuration.WebServers;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.Seleno.PageObjects;
namespace UnitTestProject1
{
    public static class Host
    {
        public static SelenoHost Instance;
        private static InternetExplorerOptions options = new InternetExplorerOptions();
        public static WebDriverWait Wait;
        public static string mainWindowHandle;
        public static System.Collections.ObjectModel.ReadOnlyCollection<Cookie> InitialCookies;
    }
    public class SiterraComponent : UiComponent
    {
        public virtual void PleaseWait(){}
    }

   /* public class SiterraComponent<T> : UiComponent where T : SiterraComponent
    {
       //private T _object;
       // public override void PleaseWait() { _object.PleaseWait(); }
    }*/
}
