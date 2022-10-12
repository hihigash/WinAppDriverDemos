using System.Text.RegularExpressions;
using OpenQA.Selenium.Appium;

namespace CalcTests;

[TestClass]
public class CalcTests
{
    [TestMethod]
    public void NormalTest()
    {
        var options = new AppiumOptions();
        options.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");

        using var session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);
        session.FindElementByAccessibilityId("num1Button").Click();
        session.FindElementByAccessibilityId("plusButton").Click();
        session.FindElementByAccessibilityId("num2Button").Click();
        session.FindElementByAccessibilityId("equalButton").Click();

        var text = session.FindElementByAccessibilityId("CalculatorResults").Text;
        var actual = Regex.Match(text, @"Display is (\d+)").Groups[1].Value; // for English Version
        // var actual = Regex.Match(text, @"•\Ž¦‚Í (\d+) ‚Å‚·").Groups[1].Value; // for Japanese Version
        Assert.AreEqual("3", actual);
    }
}
