using OpenQA.Selenium;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of 'Bet On Poker' game page
    /// </summary>
    public class BetOnPokerGamePage : BaseGamePage
    {
        public Button BetOnPokerTabNavButton => new Button("Bet On Poker tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='Bet On Poker']]"));

        public BetOnPokerGamePage() : base("Bet On Poker")
        {
        }

        protected override void WaitForLoadedCondition()
        {
            WaitForPageStateComplete();
            BetOnPokerTabNavButton.WaitForVisible();
            WaitUntilAttributeIs(BetOnPokerTabNavButton.Find(), "class", "active", Timeout.TenSec);
            WaitUntilVisible(PlayingVideoContainerFrameBy);
        }
    }
}
