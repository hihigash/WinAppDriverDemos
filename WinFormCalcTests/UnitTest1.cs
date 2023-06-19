using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WinFormCalcTests
{
    // The target sample app is https://github.com/NeutronO/Calculator
    [TestClass]
    public class AdvancedCalculatorTests
    {
        [TestMethod]
        public void AdvancedCalculatorTest()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"C:\works\Calculator\Advanced Calculator.exe");
            using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), options))
            {
                session.FindElementByAccessibilityId("btn1").Click(); // 1
                session.FindElementByAccessibilityId("btn2").Click(); // 2
                session.FindElementByAccessibilityId("btn3").Click(); // 3

                session.FindElementByAccessibilityId("btnPlus").Click(); // +

                session.FindElementByAccessibilityId("btn3").Click(); // 3
                session.FindElementByAccessibilityId("btn6").Click(); // 6
                session.FindElementByAccessibilityId("btn9").Click(); // 9

                session.FindElementByAccessibilityId("btnPlus").Click(); // +

                session.FindElementByAccessibilityId("btn9").Click(); // 9
                session.FindElementByAccessibilityId("btn8").Click(); // 8
                session.FindElementByAccessibilityId("btn7").Click(); // 7

                session.FindElementByAccessibilityId("btnPlus").Click(); // +

                session.FindElementByAccessibilityId("btn7").Click(); // 7
                session.FindElementByAccessibilityId("btn4").Click(); // 4
                session.FindElementByAccessibilityId("btn1").Click(); // 1

                session.FindElementByAccessibilityId("btnEquals").Click(); // =

                var actual = session.FindElementByAccessibilityId("txtBox").Text;
                Assert.AreEqual(2220, int.Parse(actual));
            }
        }
    }
}
