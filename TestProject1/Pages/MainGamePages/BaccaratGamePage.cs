using OpenQA.Selenium;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of 'Baccarat' game page
    /// </summary>
    public class BaccaratGamePage : BaseGamePage
    {
        public Button BaccaratTabNavButton => new Button("Baccarat tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='Baccarat']]"));

        public BaccaratGamePage() : base("Baccarat")
        {
        }

        protected override void WaitForLoadedCondition()
        {
            WaitForPageStateComplete();
            BaccaratTabNavButton.WaitForVisible();
            WaitUntilAttributeIs(BaccaratTabNavButton.Find(), "class", "active", Timeout.TenSec);
            WaitUntilVisible(PlayingVideoContainerFrameBy);
        }
    }
}
