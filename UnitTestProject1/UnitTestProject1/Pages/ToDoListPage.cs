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
    public class ToDoListPage : SiterraComponent
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

    }
}
