namespace DesktopTests;

public static class WindowsDriverExtensions
{
    public static void SendKey(this WindowsDriver<WindowsElement> driver, string keysToSend)
    {
        var actions = new Actions(driver);
        actions.SendKeys(keysToSend);
        actions.Build().Perform();
    }
}
