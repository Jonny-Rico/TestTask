using OpenQA.Selenium;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of 'Rock Paper Scissors' game page
    /// </summary>
    public class RockPaperScissorsGamePage : BaseGamePage
    {
        public Button RockPaperScissorsTabNavButton => new Button("Rock Paper Scissors tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='Rock Paper Scissors']]"));
        public Button CloseTutorialButtonNavButton => new Button("Close tutorial", CloseTutorialButtonBy);
        public readonly By CloseTutorialButtonBy = By.XPath("//button[.='Close tutorial']");
        private bool CloseTutorial;

        public RockPaperScissorsGamePage(bool closeTutorial = true) : base("Rock Paper Scissors")
        {
            CloseTutorial = closeTutorial;
        }

        protected override void WaitForLoadedCondition()
        {
            WaitForPossibleVisiblility(CloseTutorialButtonBy, Timeout.ThreeSec);

            if (CloseTutorial)
            {
                CloseTutorialButtonNavButton.Click();
                CloseTutorialButtonNavButton.WaitForDisappear();
            }

            WaitForPageStateComplete();
            RockPaperScissorsTabNavButton.WaitForVisible();
            WaitUntilAttributeIs(RockPaperScissorsTabNavButton.Find(), "class", "active", Timeout.TenSec);
            WaitUntilVisible(PlayingVideoContainerFrameBy);
        }
    }
}
