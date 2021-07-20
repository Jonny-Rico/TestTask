using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.Logger;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Pages
{
    /// <summary>
    /// Class for methods and elements of 'Bet history' page
    /// </summary>
    public class BetHistoryPage
    {
        private readonly By BetHistoryTableBy = By.ClassName("bets-history-table");
        private readonly By BetTableRowBy = By.XPath("//div[@class='bets-history-table']//tbody//tr");
        private readonly By PageHeaderTitleLabelBy = By.XPath("//div[@class='header-title']/*[text()='Bet history']");

        public void WaitForLoading()
        {
            Log.Info($"Wait for 'Bet History' page to be loaded...");
            try
            {
                WaitUntilVisible(PageHeaderTitleLabelBy);
            }
            catch (Exception e)
            {
                throw new Exception($"'Bet History' page is not loaded in time.", e);
            }
            Log.Info("-> page is loaded.");
        }

        /// <summary>
        /// Get bet
        /// </summary>
        /// <param name="index">Bet row index in table</param>
        /// <returns>Specified bet</returns>
        public BetItem GetBet(int index)
        {
            WaitUntilVisible(BetHistoryTableBy);
            var dataRow = GetBetHistoryTableData()[index - 1];

            return ParseBetRowDataToBetModel(dataRow);
        }

        /// <summary>
        /// Get bet table data
        /// </summary>
        /// <returns>Bet table data from all rows and columns</returns>
        public List<List<string>> GetBetHistoryTableData()
        {
            var data = new List<List<string>>();
            var rows = Driver.FindElements(BetTableRowBy);

            foreach (var row in rows)
            {
                var rowValues = row.FindElements(By.XPath("//td"));
                var rowData = new List<string>();
                rowData.AddRange(rowValues.Select(cell => cell.Text));
                data.Add(rowData);
            }

            return data;
        }

        /// <summary>
        /// Parse specific row of Bet history table to BetItem model
        /// </summary>
        /// <param name="dataRow">List of string values of each cell in row</param>
        /// <returns>BetItem model object instance</returns>
        private BetItem ParseBetRowDataToBetModel(List<string> dataRow)
        {
            var betItem = new BetItem() {
                Game = dataRow[1].Remove(dataRow[1].IndexOf(',')),
                Bet = dataRow[3],
                Amount = dataRow[6].Remove(dataRow[6].IndexOf('€')),
            };

            return betItem;
        }
    }

    /// <summary>
    /// Model of bet
    /// </summary>
    public class BetItem
    {
        public string Game;
        public string Bet;
        public string Amount;
    }
}
