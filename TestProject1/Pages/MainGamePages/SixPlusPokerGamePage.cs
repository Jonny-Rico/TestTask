using OpenQA.Selenium;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of '6+ Poker' game page
    /// </summary>
    public class SixPlusPokerGamePage : BaseGamePage
    {
        public Button SixPlusPokerTabNavButton => new Button("6+ Poker tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='6+ Poker']]"));

        public SixPlusPokerGamePage() : base("6+ Poker")
        {
        }

        protected override void WaitForLoadedCondition()
        {
            WaitForPageStateComplete();
            SixPlusPokerTabNavButton.WaitForVisible();
            WaitUntilAttributeIs(SixPlusPokerTabNavButton.Find(), "class", "active", Timeout.TenSec);
            WaitUntilVisible(PlayingVideoContainerFrameBy);
        }
    }
}
