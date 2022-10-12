using OpenQA.Selenium.Support.UI;

namespace DesktopTests;

[TestClass]
public class NotepadTests : DesktopSession
{
    [Ignore]
    [TestMethod]
    public void LaunchNotepad_NoWait()
    {
        var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Session!);
        Session?.SendKey(Keys.Meta + "s" + Keys.Meta);
        Session?.SendKey("notepad" + Keys.Enter);

        var notepadWindow = Session?.FindElementByName("Untitled - Notepad");
        notepadWindow?.SendKeys(Keys.Alt + Keys.F4);
    }

    [TestMethod]
    public void LaunchNotepad_NotSmart()
    {
        Session?.SendKey(Keys.Meta + "s" + Keys.Meta);
        Thread.Sleep(TimeSpan.FromSeconds(5));
        Session?.SendKey("notepad");
        Session?.SendKey(Keys.Enter);

        Thread.Sleep(TimeSpan.FromSeconds(5));
        var notepadWindow = Session?.FindElementByName("Untitled - Notepad");
        notepadWindow?.SendKeys(Keys.Alt + Keys.F4);
    }

    [TestMethod]
    public void LaunchNotepad_Smart()
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
            var textbox = driver.FindElementByName("Search box");
            return textbox != null;
        });
        Session?.SendKey("notepad" + Keys.Enter);

        wait.Until(driver =>
        {
            var window = driver.FindElementByName("Untitled - Notepad");
            return window != null;
        });
        var notepadWindow = Session?.FindElementByName("Untitled - Notepad");
        notepadWindow?.SendKeys(Keys.Alt + Keys.F4);
    }
}
