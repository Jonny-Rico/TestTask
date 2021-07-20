using System;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static TestProject1.Helpers.Logger;

namespace TestProject1.Helpers
{
    /// <summary>
    /// Class to initialize webdriver and open/close browser.
    /// </summary>
    public static class BrowserHelper
    {
        [ThreadStatic]
        private static IWebDriver driver;

        public static IWebDriver Driver
        {
            get
            {
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        /// <summary>
        /// Set Chrome Options
        /// </summary>
        /// <returns>Set of chrome options</returns>
        public static ChromeOptions SetChromeOptions()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");

            return chromeOptions;
        }

        /// <summary>
        /// Initialize selenium driver and Open browser. 
        /// </summary>
        public static void OpenBrowser()
        {
            var browser = "Chrome";
            Log.Info($"Open {browser} browser:");

            Log.Info("Initiate webdriver...");
            InitiateDriver();

            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        /// <summary>
        /// Initialize selenium driver
        /// </summary>
        public static void InitiateDriver()
        {
            try
            {
                Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, SetChromeOptions());
            }
            catch (Exception)
            {
                Log.Warning("ChromeDriver failed to initialize. Retrying the connection...");
                Thread.Sleep(5000);
                Driver = new ChromeDriver(SetChromeOptions());
            }
        }

        /// <summary>
        /// Close browser instance and dispose resources
        /// </summary>
        public static void CloseBrowser()
        {
            Log.Info("Close browser instance:");
            Driver.Quit();
            Driver.Dispose();
            Driver = null;
            Log.Info("-> browser instance closed.");
        }

        public static void ClearBrowserCookies()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
        }

        public static void GoToUrl(string url)
        {
            Log.Info($"Open {url}");
            Driver.Navigate().GoToUrl(url);
        }

        public static void WaitForPageStateComplete(int timeout = 20)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        /// <summary>
        /// Finds all IWebElement for the given locator.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>all IWebElement matching the current criteria, or an empty list if nothing matches</returns>
        public static ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            ReadOnlyCollection<IWebElement> elements;
            try
            {
                elements = Driver.FindElements(locator);
            }
            catch (StaleElementReferenceException)
            {
                // sometimes the page is changed during FindElement(). That's why we need to handle this exception
                Log.Warning("StaleElementReferenceException while executing FindElements(). Wait and try again.");
                Thread.Sleep(3000);
                elements = Driver.FindElements(locator);
            }

            return elements;
        }

        /// <summary>
        /// Switch driver to frame by locator
        /// </summary>
        /// <param name="locator">Frame locator</param>
        public static void SwitchToFrame(By locator)
        {
            WaitHelper.WaitUntilVisible(locator);

            var frameElement = Driver.FindElement(locator);
            Driver.SwitchTo().Frame(frameElement);
        }

        /// <summary>
        /// Switch driver to the main driver window. Used after switching to frame
        /// </summary>
        public static void SwitchToDefaultContent()
        {
            Driver.SwitchTo().DefaultContent();
        }
    }
}
