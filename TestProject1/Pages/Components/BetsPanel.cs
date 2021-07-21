using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using TestProject1.Pages.MainGamePages;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages.Components
{
    /// <summary>
    /// Class contains elements and methods for 'Bets' panel
    /// </summary>
    public static class BetsPanel
    {
        public readonly static Dictionary<AmountOfBetButtons, string> AmountFromButton = new Dictionary<AmountOfBetButtons, string>
        {
            { AmountOfBetButtons.One,      "+1" },
            { AmountOfBetButtons.Three,    "+3" },
            { AmountOfBetButtons.Five,     "+5" },
            { AmountOfBetButtons.Ten,      "+10" },
            { AmountOfBetButtons.Fifty,    "+50" },
            { AmountOfBetButtons.Hundred,  "+100" },
        };

        private static TextBox AmountTextBox = new TextBox("Amount", By.Id("amount-input"));
        private static Button BetButton = new Button("Bet", By.ClassName("place-bet-button"));
        private static Button ClearAmountButton = new Button("Bet", By.XPath("//button[.='C']"));
        private static Button AddRandomButton = new Button("Add random", By.XPath("//button[text()='Add random']"));
        private static Image BetAcceptedImage = new Image("Bet accepted", By.XPath("//span[@class='notification-message'][.='Bet accepted.']"));
        private static Link BetHistoryLink = new Link("Bet history", By.ClassName("recent-bets-link"));
        private static By SelectedOddGameNameBy = By.ClassName("selected-odd-game-name");
        private static By SelectedOddInfoNameBy = By.CssSelector("div.selected-odd-info span");
        private readonly static string AmountButtonByTemplate = "//button[@class='bet-slip-amount-button'][.='{0}']";

        /// <summary>
        /// Check if odd is visible on Betslip
        /// </summary>
        /// <param name="gameName">Game name</param>
        /// <param name="oddTitle">Odd option title</param>
        /// <returns>true if odd is visible on Betslip panel; false otherwise</returns>
        public static bool IsOddOnBetslip(string gameName, string oddTitle)
        {
            var selectedOddGameName = Driver.FindElement(SelectedOddGameNameBy).Text;
            var selectedOddInfoName = FindElements(SelectedOddInfoNameBy).First().Text;

            return selectedOddGameName == gameName && selectedOddInfoName == oddTitle;
        }

        /// <summary>
        /// Check if any odd is added to  Betslip
        /// </summary>
        /// <returns>True if any Odd added to Betslip</returns>
        public static bool HasAddedOdd()
        {
            return IsElementVisible(SelectedOddInfoNameBy);
        }

        /// <summary>
        /// Set bet amount using existing betslip amount buttons
        /// </summary>
        /// <param name="amountButton">Type of amount-button</param>
        public static void SetAmount(AmountOfBetButtons amountButton)
        {
            var amountValue = AmountFromButton[amountButton];
            var amountButtonBy = By.XPath(string.Format(AmountButtonByTemplate, amountValue));
            var button = new Button($"'{amountValue}' amount", amountButtonBy);
            button.Click();
            BetButton.WaitForClickable();
            WaitUntilTrue(() => double.Parse(AmountTextBox.GetText()) == double.Parse(amountValue.Replace("+","")), Timeout.OneSec, "Amount was not set");
        }

        /// <summary>
        /// Set specific bet amount using amount textbox
        /// </summary>
        /// <param name="amountValue">Amount value</param>
        public static void SetAmount(string amountValue)
        {
            AmountTextBox.EnterText(amountValue);
            BetButton.WaitForClickable();
            WaitUntilTrue(() => double.Parse(AmountTextBox.GetText()) == double.Parse(amountValue), Timeout.OneSec, "Amount was not set");
        }

        /// <summary>
        /// Clear amount textbox
        /// </summary>
        public static void ClearAmount()
        {
            ClearAmountButton.Click();
            WaitUntilTrue(() => double.Parse(AmountTextBox.GetText()) == 0.00, Timeout.OneSec, "Amount was not cleared");
        }

        /// <summary>
        /// Place a bet to Betslip
        /// </summary>
        public static void PlaceBet()
        {
            switch (NavigationBar.GetActiveGame())
            {
                case Game.Speedy7:
                    {
                        var gamePage = new Speedy7GamePage();
                        var randomBet = RandomHelper.RandomEnumValue<Speedy7Bets>();
                        gamePage.PlaceBet(randomBet);
                    }

                    break;
                case Game.RockPaperScissors:
                    {
                        var gamePage = new RockPaperScissorsGamePage();
                        var randomBet = RandomHelper.RandomEnumValue<RockPaperScissorsBets>();
                        gamePage.PlaceBet(randomBet);
                    }

                    break;
                default:
                    {
                        BetButton.Click();
                        BetAcceptedImage.WaitForVisible();
                        break;
                    }
            }
        }

        /// <summary>
        /// Add random odd bet to Betslip using 'Add random' button
        /// </summary>
        public static void AddRandomOdd()
        {
            AddRandomButton.WaitForClickable(Timeout.OneMin);
            AddRandomButton.Click();
        }

        public static void OpenBetHistory()
        {
            BetHistoryLink.Click();
            new BetHistoryPage().WaitForLoading();
        }
    }

    public enum AmountOfBetButtons
    {
        One,
        Three,
        Five,
        Ten,
        Fifty,
        Hundred,
    }
}
