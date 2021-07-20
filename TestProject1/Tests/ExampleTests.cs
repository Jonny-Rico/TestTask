using NUnit.Framework;
using System.Linq;
using TestProject1.Helpers;
using TestProject1.Pages;
using TestProject1.Pages.Components;
using static TestProject1.Pages.Components.NavigationBar;
using static TestProject1.Helpers.Logger;

namespace TestProject1.Tests
{
    [TestFixture]
    public class ExampleTests : BaseTest
    {
        [Test]
        public void AllGamesCanBeOpenedByMainTabsTest()
        {
            Log.Info("Step: Open game tabs for all existing games");
            foreach (var game in gameNames.Keys)
            {
                Log.Info($"Step: Open '{gameNames[game]}' game tab");
                NavigateToGame(game);
            }
        }

        [Test]
        public void OddCanBeAddedToRandomGameBetslipTest()
        {
            var game = RandomHelper.RandomEnumValue<Game>();

            Log.Info($"Step: Open '{gameNames[game]}' game");
            NavigateToGame(game);

            Log.Info("Step: Add last odd from list to Betslip");
            var oddTitle = OddsPanel.GetOddTitlesList().Last();
            OddsPanel.AddOddToBetSlip(game, oddTitle);

            Log.Info("Step: Verify odd was added to Betslip");
            var isOnBetSlip = BetsPanel.IsOddOnBetslip(gameNames[game], oddTitle);
            Assert.IsTrue(isOnBetSlip, "Odd is not added to Bet Slip");
        }

        [Test]
        public void BetCanBeStoredInBetHistoryTest()
        {
            var game = Game.BetOnPoker;
            var amount = "1.00";

            Log.Info($"Step: Open '{gameNames[game]}' game");
            NavigateToGame(game);

            Log.Info("Step: Add last odd from list to Betslip");
            var oddTitle = OddsPanel.GetOddTitlesList().Last();
            OddsPanel.AddOddToBetSlip(game, oddTitle);

            Log.Info($"Step: Set specified amount: '{amount}' and place a bet");
            BetsPanel.SetAmount(amount);
            BetsPanel.PlaceBet();

            Log.Info("Step: Open 'Bet history' page and verify previously placed bet appears in bet history");
            BetsPanel.OpenBetHistory();
            var lastBet = BetHistoryPage.GetBet(1);
            Assert.IsTrue(lastBet.Game == gameNames[game] && lastBet.Amount == amount && lastBet.Bet == oddTitle,
                "Placed bet is not visible in Bet history");
        }

        [Test]
        public void RandomOddCanBeAddedToRandomGameBetslipTest()
        {
            var game = Game.Wheel;

            Log.Info($"Step: Open '{gameNames[game]}' game");
            NavigateToGame(game);

            Log.Info("Step: Add random odd");
            BetsPanel.AddRandomOdd();

            Log.Info("Step: Verify any odd was added to Betslip");
            var hasOdd = BetsPanel.HasAddedOdd();
            Assert.IsTrue(hasOdd, "Any Odd was not added to Bet Slip");
        }
    }
}