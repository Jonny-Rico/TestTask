using OpenQA.Selenium;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of 'Andar Bahar' game page
    /// </summary>
    public class AndarBaharGamePage : BaseGamePage
    {
        public Button AndarBaharTabNavButton => new Button("Andar Bahar tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='Andar Bahar']]"));
        public Button CloseTutorialButtonNavButton => new Button("Close tutorial", CloseTutorialButtonBy);
        private readonly By CloseTutorialButtonBy = By.XPath("//button[.='Close tutorial']");
        private bool CloseTutorial;

        public AndarBaharGamePage(bool closeTutorial = true) : base("Andar Bahar")
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
            AndarBaharTabNavButton.WaitForVisible();
            WaitUntilAttributeIs(AndarBaharTabNavButton.Find(), "class", "active", Timeout.TenSec);
            WaitUntilVisible(PlayingVideoContainerFrameBy);
        }
    }
}
