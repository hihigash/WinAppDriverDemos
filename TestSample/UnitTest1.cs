using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;
using System.Drawing.Imaging;
using System.Threading;
using System.Xml.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class E2ETestSamples
    {
        public TestContext TestContext { get; set; } // 追加

        [TestMethod]
        public void AnalyzePageSource()
        {
var options = new AppiumOptions();
options.AddAdditionalCapability("app", "Root");
using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options, TimeSpan.FromMinutes(30)))
{
    XDocument document = XDocument.Parse(session.PageSource);
    foreach(var element in document.Element("Pane").Elements()) // Desktop Session 直下の要素を列挙
    {
        Console.WriteLine($"{element.Name} : {element.Attribute("Name").Value}");
    }
}
        }

        [TestMethod]
        public void PushButtons_Calculator()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), options))
            {
                var buttons = session.FindElementsByXPath("//Group[@AutomationId='NumberPad']/Button");
                foreach (var button in buttons)
                {
                    button.Click();
                }
            }
        }

        [TestMethod]
        public void Screenshot_Calculator()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), options))
            {
                // 計算の操作
                session.FindElementByAccessibilityId("num1Button").Click();
                session.FindElementByAccessibilityId("plusButton").Click();
                session.FindElementByAccessibilityId("num2Button").Click();
                session.FindElementByAccessibilityId("equalButton").Click();

                session.GetScreenshot().SaveAsFile("calc.jpg", ScreenshotImageFormat.Jpeg);
                TestContext.AddResultFile("calc.jpg");
            }
        }

        [TestMethod]
        public void ContextMenu_Explorer()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"C:\Windows\explorer.exe");
            options.AddAdditionalCapability("appArguments", @"C:\Windows");
            using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), options))
            {
                var element = session.FindElementByName("項目ビュー");
                var actions = new Actions(session);
                actions.ContextClick(element);
                actions.Build().Perform();

                Thread.Sleep(TimeSpan.FromSeconds(10)); // 目視用に 10 秒スリープ
            }
        }




        [TestMethod]
        public void PressWinKey_Desktop()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Root");
            using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), options))
            {
                // Win+S キーで検索ボックスを開く
                var actions1 = new Actions(session);
                actions1.SendKeys(Keys.Meta + "s" + Keys.Meta);
                actions1.Build().Perform();

                // 検索ボックスに "winver.exe" と入力する
                session.FindElementByAccessibilityId("SearchTextBox").SendKeys("winver.exe");

                // エンター キーを押す (実行）
                var actions2 = new Actions(session);
                actions2.SendKeys(Keys.Enter);
                actions2.Build().Perform();
            }
        }

        [TestMethod]
        public void SendKeys_Notepad()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"C:\Windows\System32\notepad.exe");
            options.AddAdditionalCapability("appArguments", @"C:\works\temp.txt");
            using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), options))
            {
                // 文字列を入力する
                var text = "こんにちは、世界！";
                session.FindElementByName("temp.txt - メモ帳").SendKeys(text);

                // ウィンドウを閉じて、"変更内容を保存しますか?" ダイアログで [保存しない] を選択する
                session.Close();
                session.FindElementByAccessibilityId("SecondaryButton").Click();
            }
        }

        [TestMethod]
        public void Click_WindowsAlarms()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");
            using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), options))
            {
                session.FindElementByAccessibilityId("AlarmButton").Click();
                session.FindElementByAccessibilityId("ClockButton").Click();
                session.FindElementByAccessibilityId("TimerButton").Click();
                session.FindElementByAccessibilityId("StopwatchButton").Click();
            }
        }

        [Ignore]
        [TestMethod]
        public void Launch_MicrosoftEdge()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Microsoft.MicrosoftEdge_8wekyb3d8bbwe!MicrosoftEdge");
            options.AddAdditionalCapability("appArguments", "https://github.com/microsoft/WinAppDriver");
            using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), options))
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }

        [TestMethod]
        public void Launch_Notepad()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", @"C:\Windows\System32\notepad.exe");
            options.AddAdditionalCapability("appArguments", @"C:\works\temp.txt");
            using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), options))
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }

        [TestMethod]
        public void DesktopSession()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", "Root");
            using (var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), options))
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
    }
}
