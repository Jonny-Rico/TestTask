using OpenQA.Selenium;
using System;
using TestProject1.Helpers.Controls;
using TestProject1.Helpers;
using static TestProject1.Helpers.Logger;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages
{
    /// <summary>
    /// Base class for all pages in Platform
    /// </summary>
    public abstract class BaseGamePage
    {
        public static readonly By PageContainerFrameBy = By.Name("betgames_iframe_1");
        protected readonly string OddItemByTemplate = "//div[@class='odds-list']/div[contains(@class,'odd-item ') and .//span[@class='odd-title'][.='{0}']]";
        protected readonly string SelectedOddByTemplate = "//div[@class='selected-odd-info']/span[.='{0}']";
        protected readonly By PlayingVideoContainerFrameBy = By.ClassName("video-iframe");

        protected readonly string name;

        /// <summary>
        /// Initialize page
        /// </summary>
        /// <param name="name">page name, used for logging</param>
        public BaseGamePage(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Wait for page to be loaded and ready for operation
        /// </summary>
        public void WaitForLoading()
        {
            Log.Info($"Wait for '{name}' page to be loaded...");
            try
            {
                WaitForLoadedCondition();
            }
            catch (Exception e)
            {
                throw new Exception($"'{name}' page is not loaded in time.", e);
            }
            Log.Info("-> page is loaded");
        }

        /// <summary>
        /// Wait until loaded conditions for page are finished. Should be implemented for each page
        /// </summary>
        protected abstract void WaitForLoadedCondition();

        /// <summary>
        /// Add specific odd to Bet Slip
        /// </summary>
        /// <param name="oddTitle">Odd title</param>
        public virtual void AddOddToBetSlip(string oddTitle)
        {
            var oddItemBy = By.XPath(string.Format(OddItemByTemplate, oddTitle));
            var oddButton = new Button("Odd item", oddItemBy);
            Log.Info("Wait for odd buttons become enabled");
            WaitUntilTrue(() => !oddButton.Find().GetAttribute("class").Contains("disable"), Timeout.OneMin,
                $"Odd '{oddTitle}' is still cannot be selected after '{Timeout.OneMin}' seconds");
            oddButton.Click();
        }
    }

    public enum Game
    {
        Speedy7,
        RockPaperScissors,
        AndarBahar,
        WarOfBets,
        SixPlusPoker,
        BetOnPoker,
        Baccarat,
        Wheel,
        LuckySeven,
        LuckySix,
        LuckyFive,
        DiceDuel,
        Undefined
    }
}
