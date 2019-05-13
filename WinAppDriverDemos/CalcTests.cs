using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Text.RegularExpressions;
using Xunit;

namespace WinAppDriverDemos
{
    public class CalcTests
    {
        [Fact]
        public void TestMethod1()
        {
            var capabilities = new DesiredCapabilities();
            capabilities.SetCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");

            using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), capabilities))
            {
                session.FindElementByAccessibilityId("num1Button").Click();
                session.FindElementByAccessibilityId("plusButton").Click();
                session.FindElementByAccessibilityId("num2Button").Click();
                session.FindElementByAccessibilityId("equalButton").Click();

                var text = session.FindElementByAccessibilityId("CalculatorResults").Text;
                var actual = Regex.Match(text, @"表示は (\d+) です").Groups[1].Value;
                Assert.Equal("3", actual);
            }
        }
    }
}
