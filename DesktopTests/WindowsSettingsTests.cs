using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace DesktopTests
{
    [TestClass]
    public class WindowsSettingsTests : DesktopSession
    {
        [TestMethod]
        public void OpenBluetoothSettings()
        {
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Session!)
            {
                Timeout = TimeSpan.FromSeconds(30),
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };
            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            Session?.SendKey(Keys.Meta + "i" + Keys.Meta);

            wait.Until(driver =>
            {
                var element = driver.FindElementByName("Settings");
                return element != null;
            });

            var settingsWindow = Session?.FindElementByName("Sessions");
            var listItem = settingsWindow?.FindElementByName("Bluetooth & devices");
            listItem?.Click();

            wait.Until(driver =>
            {
                var element = settingsWindow?.FindElementByXPath("//Button[@Name=\"Bluetooth & devices\"]");
                return element != null;
            });

            var items = settingsWindow?.FindElementsByXPath("//Window[@Name=\"Settings\"]/Custom/Pane/List/ListItem")
                .Select(x => x.Text);
            foreach (var item in items!)
            {
                Console.WriteLine(item);
            }
        }
    }
}
