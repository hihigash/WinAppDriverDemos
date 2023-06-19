using OpenQA.Selenium.Support.UI;

namespace DesktopTests;

[TestClass]
public class WordPadTests : DesktopSession
{
    [Ignore]
    [TestMethod]
    public void LaunchWordpad_NoWait()
    {
        var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Session!);
        Session?.SendKey(Keys.Meta + "s" + Keys.Meta);
        Session?.SendKey("wordpad" + Keys.Enter);

        //var notepadWindow = Session?.FindElementByName("Document - WordPad"); // for English Edition
        var notepadWindow = Session?.FindElementByName("ドキュメント - ワードパッド"); // for Japanese Edition
        notepadWindow?.SendKeys(Keys.Alt + Keys.F4);
    }

    [TestMethod]
    public void LaunchWordpad_NotSmart()
    {
        Session?.SendKey(Keys.Meta + "s" + Keys.Meta);
        Thread.Sleep(TimeSpan.FromSeconds(5));
        Session?.SendKey("wordpad");
        Session?.SendKey(Keys.Enter);

        Thread.Sleep(TimeSpan.FromSeconds(10));
        //var notepadWindow = Session?.FindElementByName("Document - WordPad"); // for English Edition
        var notepadWindow = Session?.FindElementByName("ドキュメント - ワードパッド"); // for Japanese Edition
        notepadWindow?.SendKeys(Keys.Alt + Keys.F4);
    }

    [TestMethod]
    public void LaunchWordpad_Smart()
    {
        var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Session!)
        {
            Timeout = TimeSpan.FromSeconds(30),
            PollingInterval = TimeSpan.FromMilliseconds(100)
        };
        wait.IgnoreExceptionTypes(typeof(WebDriverException));

        Session?.SendKey(Keys.Meta + "s" + Keys.Meta);
        wait.Until(driver =>
        {
            var textbox = driver.FindElementByAccessibilityId("SearchTextBox");
            return textbox != null;
        });
        Session?.SendKey("wordpad" + Keys.Enter);

        wait.Until(driver =>
        {
            //var window = Session?.FindElementByName("Document - WordPad"); // for English Edition
            var window = Session?.FindElementByName("ドキュメント - ワードパッド"); // for Japanese Edition
            return window != null;
        });
        //var notepadWindow = Session?.FindElementByName("Document - WordPad"); // for English Edition
        var notepadWindow = Session?.FindElementByName("ドキュメント - ワードパッド"); // for Japanese Edition
        notepadWindow?.SendKeys(Keys.Alt + Keys.F4);
    }
}
