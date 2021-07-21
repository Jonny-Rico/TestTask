using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using TestProject1.Pages.MainGamePages;
using TestProject1.Helpers.Controls;
using static TestProject1.Helpers.BrowserHelper;

namespace TestProject1.Pages.Components
{
    /// <summary>
    /// Class contains elements and methods for left 'Odd list' panel
    /// </summary>
    public static class OddsPanel
    {
        private static By OddTitleBy = By.CssSelector("span.odd-title");
        private static string OddBallItemByTemplate = "//div[@class='game-items-selection']//span[text()='{0}']";
        
        public static Button RandomBallsButton = new Button("Random ball numbers", By.CssSelector("div.game-items-selected button"));
        public static Button ConfirmBallsButton = new Button("Confirm ball numbers", By.ClassName("odd-item-dropdown-confirm"));

        /// <summary>
        /// Add specific odd to Bet Slip according specified game exclude 'Speedy 7' and 'Rock Paper Scissors';
        /// </summary>
        /// <param name="oddTitle">Odd text value</param>
        /// <param name="game">Specified game from game list</param>
        public static void AddOddToBetSlip(Game game, string oddTitle)
        {
            //add specific odd for specific game
            switch (game)
            {
                case Game.Speedy7:
                    throw new NotImplementedException("There is no any Odds for 'Speedy 7' game. Bet need to be placed instead");
                case Game.RockPaperScissors:
                    throw new NotImplementedException("There is no any Odds for 'Rock Paper Scissors' game. Bet need to be placed instead");
                case Game.AndarBahar:
                    new AndarBaharGamePage().AddOddToBetSlip(oddTitle);
                    break;
                case Game.WarOfBets:
                    new WarOfBetsGamePage().AddOddToBetSlip(oddTitle);
                    break;
                case Game.SixPlusPoker:
                    new SixPlusPokerGamePage().AddOddToBetSlip(oddTitle);
                    break;
                case Game.BetOnPoker:
                    new BetOnPokerGamePage().AddOddToBetSlip(oddTitle);
                    break;
                case Game.Baccarat:
                    new BaccaratGamePage().AddOddToBetSlip(oddTitle);
                    break;
                case Game.Wheel:
                    new WheelGamePage().AddOddToBetSlip(oddTitle);
                    break;
                case Game.LuckySeven:
                    new Lucky7GamePage().AddOddToBetSlip(oddTitle);
                    break;
                case Game.LuckySix:
                    new Lucky6GamePage().AddOddToBetSlip(oddTitle);
                    break;
                case Game.LuckyFive:
                    new Lucky5GamePage().AddOddToBetSlip(oddTitle);
                    break;
                case Game.DiceDuel:
                    new DiceDuelGamePage().AddOddToBetSlip(oddTitle);
                    break;
                case Game.Undefined:
                    throw new NotImplementedException("Not implemented game cannot be interactable");
                default:
                    break;
            }
        }

        /// <summary>
        /// Get list of odds
        /// </summary>
        /// <returns>List of odds titles</returns>
        public static List<string> GetOddTitlesList()
        {
            switch (NavigationBar.GetActiveGame())
            {
                case Game.Speedy7:
                    throw new NotImplementedException("There is no any Odds for 'Speedy 7' game. Bet need to be placed instead");
                case Game.RockPaperScissors:
                    throw new NotImplementedException("There is no any Odds for 'Rock Paper Scissors' game. Bet need to be placed instead");
                default:
                    return FindElements(OddTitleBy).Select(x => x.Text).ToList();
            }
        }

        /// <summary>
        /// Set ball number for odds that contains several balls to choose
        /// </summary>
        /// <param name="number">Ball number</param>
        public static void SetBallNumberToOdd(string number)
        {
            var ballItemBy = By.XPath(string.Format(OddBallItemByTemplate, number));
            new Button($"Ball '{number}'", ballItemBy).Click();
        }
    }
}
