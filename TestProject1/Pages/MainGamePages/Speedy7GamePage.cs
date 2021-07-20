using OpenQA.Selenium;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of 'Speedy 7' game page
    /// </summary>
    public class Speedy7GamePage : BaseGamePage
    {
        public Button Speedy7TabNavButton => new Button("Speedy 7 tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='Speedy 7']]"));

        public Speedy7GamePage() : base("Speedy 7")
        {
        }

        protected override void WaitForLoadedCondition()
        {
            WaitForPageStateComplete();
            Speedy7TabNavButton.WaitForVisible();
            WaitUntilAttributeIs(Speedy7TabNavButton.Find(), "class", "active", Timeout.TenSec);
            WaitUntilVisible(PlayingVideoContainerFrameBy);
        }
    }
}
