using OpenQA.Selenium;
using System.Collections.Generic;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using TestProject1.Pages.Components;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;
using static TestProject1.Helpers.Logger;

namespace TestProject1.Pages.MainGamePages
{
    /// <summary>
    /// Class contains elements and methods of 'Speedy 7' game page
    /// </summary>
    public class Speedy7GamePage : BaseGamePage
    {
        public Button Speedy7TabNavButton => new Button("Speedy 7 tab", By.XPath("//button[contains(@class,'tabs-bar-item') and .//span[text()='Speedy 7']]"));
        public Button RedBetButton => new Button("Red", By.XPath("//button//span[.='Red']"));
        public Button BlackBetButton => new Button("Red", By.XPath("//button//span[.='Black']"));
        public Label BetAcceptedLabel => new Label("Bet accepted.", By.XPath("//div[text()='Bet accepted.']"));

        private readonly string AmountButtonByTemplate = "//button[@title='{0}']";
        private readonly string BetButtonByTemplate = "//button//span[.='{0}']";

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

        /// <summary>
        /// Place specific bet 
        /// </summary>
        /// <param name="bet">One of two bets:'Red' or 'Black'</param>
        public void PlaceBet(Speedy7Bets bet, AmountOfBetButtons amount = AmountOfBetButtons.One)
        {
            Log.Info("Set amount");
            SetAmount(amount);
            Log.Info("Place one of two specified bet");
            var betButton = new Button($"'{bet}' amount", By.XPath(string.Format(BetButtonByTemplate, bet)));
            betButton.WaitForClickable(Timeout.OneMin);
            betButton.Click();
            BetAcceptedLabel.WaitForVisible();
        }

        /// <summary>
        /// Add specific amount to Bet
        /// </summary>
        /// <param name="amount">Type of amount-button</param>
        public void SetAmount(AmountOfBetButtons amount)
        {
            Log.Info("Set one of specified amount");
            var amountValue = BetsPanel.AmountFromButton[amount] = "€";
            var amountButtonBy = By.XPath(string.Format(AmountButtonByTemplate, amountValue));
            var button = new Button($"'{amountValue}' amount", amountButtonBy);
            button.WaitForClickable(Timeout.ThirtySec);
            button.Click();
        }
    }

    public enum Speedy7Bets
    {
        Red,
        Black
    }
}
