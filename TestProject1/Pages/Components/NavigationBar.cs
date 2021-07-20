using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using TestProject1.Helpers;
using TestProject1.Helpers.Controls;
using TestProject1.Pages.MainGamePages;

namespace TestProject1.Pages.Components
{
    /// <summary>
    /// Class with elements and methods for navigation between games using Main navigation bar
    /// </summary>
    public static class NavigationBar
    {
        public static readonly Dictionary<Game, string> gameNames = new Dictionary<Game, string>
        {
            { Game.AndarBahar,        "Andar Bahar"},
            { Game.Baccarat,          "Baccarat"},
            { Game.BetOnPoker,        "Bet On Poker"},
            { Game.DiceDuel,          "Dice Duel"},
            { Game.LuckyFive,         "Lucky 5"},
            { Game.LuckySeven,        "Lucky 7"},
            { Game.LuckySix,          "Lucky 6"},
            { Game.RockPaperScissors, "Rock Paper Scissors"},
            { Game.SixPlusPoker,      "6+ Poker"},
            { Game.Speedy7,           "Speedy 7"},
            { Game.WarOfBets,         "War of Bets"},
            { Game.Wheel,             "Wheel"},
        };

        private static string gameTabByTemplate = "//button[contains(@class,'tabs-bar-item') and .//span[text()='{0}']]";

        public static void NavigateToGame(Game game, bool closeTutorial = true)
        {
            var activeGame = GetActiveGame();

            if (activeGame == game)
                return;

            var gameTabButton = new Button(game.ToString(), By.XPath(string.Format(gameTabByTemplate, gameNames[game])));
            JScript.ScrollToView(gameTabButton.Find());
            gameTabButton.Click();

            //wait for specific game tab loaded
            switch (game)
            {
                case Game.Speedy7:
                    new Speedy7GamePage().WaitForLoading();
                    break;
                case Game.RockPaperScissors:
                    new RockPaperScissorsGamePage(closeTutorial).WaitForLoading();
                    break;
                case Game.AndarBahar:
                    new AndarBaharGamePage(closeTutorial).WaitForLoading();
                    break;
                case Game.WarOfBets:
                    new WarOfBetsGamePage().WaitForLoading();
                    break;
                case Game.SixPlusPoker:
                    new SixPlusPokerGamePage().WaitForLoading();
                    break;
                case Game.BetOnPoker:
                    new BetOnPokerGamePage().WaitForLoading();
                    break;
                case Game.Baccarat:
                    new BaccaratGamePage().WaitForLoading();
                    break;
                case Game.Wheel:
                    new WheelGamePage().WaitForLoading();
                    break;
                case Game.LuckySeven:
                    new Lucky7GamePage().WaitForLoading();
                    break;
                case Game.LuckySix:
                    new Lucky6GamePage().WaitForLoading();
                    break;
                case Game.LuckyFive:
                    new Lucky5GamePage().WaitForLoading();
                    break;
                case Game.DiceDuel:
                    new DiceDuelGamePage().WaitForLoading();
                    break;                    
                default:
                    throw new NotImplementedException("Not implemented page cannot be opened");
            }
        }

        /// <summary>
        /// Get type of current active game
        /// </summary>
        /// <returns>Type of game</returns>
        private static Game GetActiveGame()
        {
            var activeGame = Game.Undefined;

            foreach (var game in gameNames.Keys)
            {
                var currentGameName = gameNames[game];
                var gameTabButton = new Button(currentGameName, By.XPath(string.Format(gameTabByTemplate, currentGameName)));

                if (IsGameActive(game))
                {
                    activeGame = game;
                    break;
                }
            }

            return activeGame == Game.Undefined ? throw new Exception("Unable to identify active game application") : activeGame;
        }

        /// <summary>
        /// Check if specific game is active now
        /// </summary>
        /// <param name="game">Type of game</param>
        /// <returns>True if specified game is active; false otherwise</returns>
        public static bool IsGameActive(Game game)
        {
            var currentGameName = gameNames[game];
            var gameTabButton = new Button(currentGameName, By.XPath(string.Format(gameTabByTemplate, currentGameName)));

            return gameTabButton.Find().GetAttribute("class").Contains("active");
        }
    }
}
