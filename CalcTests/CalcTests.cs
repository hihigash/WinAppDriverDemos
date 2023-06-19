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
        session.FindElementByAccessibilityId("num1Button").Click(); // 1
        session.FindElementByAccessibilityId("num2Button").Click(); // 2
        session.FindElementByAccessibilityId("num3Button").Click(); // 3

        session.FindElementByAccessibilityId("plusButton").Click(); // +

        session.FindElementByAccessibilityId("num3Button").Click(); // 3
        session.FindElementByAccessibilityId("num6Button").Click(); // 6
        session.FindElementByAccessibilityId("num9Button").Click(); // 9

        session.FindElementByAccessibilityId("plusButton").Click(); // +

        session.FindElementByAccessibilityId("num9Button").Click(); // 9
        session.FindElementByAccessibilityId("num8Button").Click(); // 8
        session.FindElementByAccessibilityId("num7Button").Click(); // 7

        session.FindElementByAccessibilityId("plusButton").Click(); // +

        session.FindElementByAccessibilityId("num7Button").Click(); // 7
        session.FindElementByAccessibilityId("num4Button").Click(); // 4
        session.FindElementByAccessibilityId("num1Button").Click(); // 1

        session.FindElementByAccessibilityId("equalButton").Click();

        var text = session.FindElementByAccessibilityId("CalculatorResults").Text;

        // var actual = Regex.Match(text, @"Display is ([\d,]+)").Groups[1].Value; // for English Edition
        var actual = Regex.Match(text, @"•\Ž¦‚Í ([\d,]+) ‚Å‚·").Groups[1].Value; // for Japanese Edition

        Assert.AreEqual(2220, int.Parse(actual, System.Globalization.NumberStyles.AllowThousands));
    }
}
