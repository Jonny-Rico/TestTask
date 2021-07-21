using System;
using OpenQA.Selenium;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using TestProject1.Pages.Components;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;
using static TestProject1.Helpers.Logger;


namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of 'Rock Paper Scissors' game page
    /// </summary>
    public class RockPaperScissorsGamePage : BaseGamePage
    {
        public Button RockPaperScissorsTabNavButton => new Button("Rock Paper Scissors tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='Rock Paper Scissors']]"));
        public Button ScissorsActiveButton => new Button("Scissors active", By.XPath($"//div[@class='{BetButtonByTemplate} _1dG_UHv_llH7KeH10O2sfM']"));
        public Button RockActiveButton => new Button("Rock active", By.XPath($"//div[@class='{BetButtonByTemplate} _1pF4gXZ7SRHhRLpflbbpoA']"));
        public Button PaperActiveButton => new Button("Paper active", By.XPath($"//div[@class='{BetButtonByTemplate} _1jJBmkfQa5AiWpZ9akAiGJ']"));
        public Button CloseTutorialButtonNavButton => new Button("Close tutorial", CloseTutorialButtonBy);
        public Label BetAcceptedLabel => new Label("Bet accepted.", By.XPath("//div[text()='Bet accepted.']"));

        private readonly string AmountButtonByTemplate = "//button[@title='{0}']";

        private readonly By CloseTutorialButtonBy = By.XPath("//button[.='Close tutorial']");
        private readonly string BetButtonByTemplate = "_3-gGRUIEEQR2NCqMt8j0z9 _2_mGeTxlxB12fbSVfCVJQS";
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

        /// <summary>
        /// Place specific bet 
        /// </summary>
        /// <param name="bet">One of two bets:'Red' or 'Black'</param>
        public void PlaceBet(RockPaperScissorsBets bet, AmountOfBetButtons amount = AmountOfBetButtons.One)
        {
            Log.Info("Place one specified bet");
            switch (bet)
            {
                case RockPaperScissorsBets.Rock:
                    {
                        SetAmount(amount);
                        RockActiveButton.WaitForClickable(Timeout.OneMin);
                        RockActiveButton.Click();
                        break;
                    }
                case RockPaperScissorsBets.Paper:
                    {
                        SetAmount(amount);
                        PaperActiveButton.WaitForClickable(Timeout.OneMin);
                        PaperActiveButton.Click();
                        break;
                    }
                case RockPaperScissorsBets.Scissors:
                    {
                        SetAmount(amount);
                        ScissorsActiveButton.WaitForClickable(Timeout.OneMin);
                        ScissorsActiveButton.Click();
                        break;
                    }
                default:
                    throw new NotImplementedException($"Not implemented bet cannot be placed: {bet}");

            }
        }

        /// <summary>
        /// Add specific amount to Bet
        /// </summary>
        /// <param name="amount">Type of amount-button</param>
        public void SetAmount(AmountOfBetButtons amount)
        {
            Log.Info("Set one of specified amount");
            var amountValue = BetsPanel.AmountFromButton[amount];
            var amountButtonBy = By.XPath(string.Format(AmountButtonByTemplate, amountValue));
            var button = new Button($"'{amountValue}' amount", amountButtonBy);
            button.WaitForClickable(Timeout.ThirtySec);
            button.Click();
        }
    }

    public enum RockPaperScissorsBets
    {
        Rock,
        Paper,
        Scissors
    }
}

