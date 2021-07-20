using OpenQA.Selenium;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of 'Dice Duel' game page
    /// </summary>
    public class DiceDuelGamePage : BaseGamePage
    {
        public Button DiceDuelTabNavButton => new Button("Dice Duel tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='Dice Duel']]"));

        public DiceDuelGamePage() : base("Dice Duel")
        {
        }

        protected override void WaitForLoadedCondition()
        {
            WaitForPageStateComplete();
            DiceDuelTabNavButton.WaitForVisible();
            WaitUntilAttributeIs(DiceDuelTabNavButton.Find(), "class", "active", Timeout.TenSec);
            WaitUntilVisible(PlayingVideoContainerFrameBy);
        }
    }
}
