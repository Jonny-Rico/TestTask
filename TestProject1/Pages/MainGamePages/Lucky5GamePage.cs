using System.Collections.Generic;
using OpenQA.Selenium;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;
using static TestProject1.Helpers.Logger;
using static TestProject1.Pages.Components.OddsPanel;


namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of 'Lucky 5' game page
    /// </summary>
    public class Lucky5GamePage : BaseGamePage
    {
        public Button Lucky5TabNavButton => new Button("Lucky 5 tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='Lucky 5']]"));
        private readonly By GameInformationTableBy = By.CssSelector("div.game-information-header");

        public Lucky5GamePage() : base("Lucky 5")
        {
        }

        protected override void WaitForLoadedCondition()
        {
            WaitForPageStateComplete();
            Lucky5TabNavButton.WaitForVisible();
            WaitUntilAttributeIs(Lucky5TabNavButton.Find(), "class", "active", Timeout.TenSec);
            WaitForPossibleVisiblility(GameInformationTableBy, Timeout.ThreeSec);

            if (!IsElementVisible(GameInformationTableBy, Timeout.ThreeSec))
                WaitUntilVisible(PlayingVideoContainerFrameBy);
        }

        /// <summary>
        /// Add specific odd to Bet Slip
        /// </summary>
        /// <param name="oddTitle">Odd title</param>
        /// <param name="listOfNumbers">List of ball's numbers</param>
        public void AddOddToBetSlip(string oddTitle, List<string> listOfNumbers = null)
        {
            var oddItemBy = By.XPath(string.Format(OddItemByTemplate, oddTitle));
            var oddButton = new Button("Odd item", oddItemBy);
            Log.Info("Wait for odd buttons become enabled");
            WaitUntilTrue(() => !oddButton.Find().GetAttribute("class").Contains("disable"), Timeout.OneMin,
                $"Odd '{oddTitle}' is still cannot be selected after '{Timeout.OneMin}' seconds");
            oddButton.Click();

            Log.Info("Set balls to add to odd");
            if (listOfNumbers is null)
                RandomBallsButton.Click();
            else
            {
                foreach (var number in listOfNumbers)
                {
                    SetBallNumberToOdd(number);
                }
            }

            ConfirmBallsButton.Click();
        }
    }
}
