using OpenQA.Selenium;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of 'War of Bets' game page
    /// </summary>
    public class WarOfBetsGamePage : BaseGamePage
    {
        public Button WarOfBetsTabNavButton => new Button("War of Bets tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='War of Bets']]"));

        public WarOfBetsGamePage() : base("War of Bets")
        {
        }

        protected override void WaitForLoadedCondition()
        {
            WaitForPageStateComplete();
            WarOfBetsTabNavButton.WaitForVisible();
            WaitUntilAttributeIs(WarOfBetsTabNavButton.Find(), "class", "active", Timeout.TenSec);
            WaitUntilVisible(PlayingVideoContainerFrameBy);
        }
    }
}
