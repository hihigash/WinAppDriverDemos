namespace DesktopTests;

public class DesktopSession
{
    [TestInitialize]
    public void Initialize()
    {
        var options = new AppiumOptions();
        options.AddAdditionalCapability("app", "Root");
        Session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
    }

    [TestCleanup]
    public void Cleanup()
    {
        Session?.Quit();
        Session = null;
    }

    public WindowsDriver<WindowsElement>? Session { get; set; }
}
