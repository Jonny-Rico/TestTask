using OpenQA.Selenium;
using TestProject1.Helpers.Controls;
using TestProject1.Helpers;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of 'Whell' game page
    /// </summary>
    public class WheelGamePage : BaseGamePage
    {
        public Button WheelTabNavButton => new Button("Wheel tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='Wheel']]"));

        public WheelGamePage() : base("Wheel")
        {
        }

        protected override void WaitForLoadedCondition()
        {
            WaitForPageStateComplete();
            WheelTabNavButton.WaitForVisible();
            WaitUntilAttributeIs(WheelTabNavButton.Find(), "class", "active", Timeout.TenSec);
            WaitUntilVisible(PlayingVideoContainerFrameBy);
        }
    }
}
