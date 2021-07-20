using NUnit.Framework;
using TestProject1.Pages;
using TestProject1.Pages.MainGamePages;
using static TestProject1.Helpers.BrowserHelper;

namespace TestProject1.Tests
{
    /// <summary>
    /// Base test class. All other test classes should be inherited from this class.
    /// </summary>
    [TestFixture]
    public class BaseTest
    {
        public WheelGamePage WhellGamePage => new WheelGamePage();
        public Speedy7GamePage Speedy7GamePage => new Speedy7GamePage();
        public BetHistoryPage BetHistoryPage => new BetHistoryPage();

        [SetUp]
        public void BeforeTest()
        {
            OpenBrowser();
            ClearBrowserCookies();
            GoToUrl("https://demo.betgames.tv/");
            WaitForPageStateComplete();
            SwitchToFrame(BaseGamePage.PageContainerFrameBy);
            WhellGamePage.WaitForLoading();
        }

        [TearDown]
        public void AfterTest()
        {
            CloseBrowser();
        }
    }
}
