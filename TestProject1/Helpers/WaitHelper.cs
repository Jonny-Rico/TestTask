using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static TestProject1.Helpers.BrowserHelper;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;
using static TestProject1.Helpers.Logger;
using static TestProject1.Helpers.Timeout;

namespace TestProject1.Helpers
{
    /// <summary>
    /// Helper class with methods related to wait for different WebElement conditions
    /// </summary>
    public static class WaitHelper
    {
        public static int DefaultTimeout = 10 * WaitFact;

        /// <summary>
        /// Wait for element to be visible using locator.
        /// Throws NoSuchElementExeption if element is not visible.
        /// </summary>
        /// <param name="locator">locator to element</param>
        /// <param name="timeout">time to wait in seconds</param>
        public static void WaitUntilVisible(By locator, int? timeout = null)
        {
            timeout = GetValueOrDefault(timeout);
            try
            {
                var wait = new WebDriverWait(Driver, GetTimeSpan(timeout));
                wait.Until(ElementIsVisible(locator));
            }
            catch (WebDriverTimeoutException e)
            {
                throw new WebDriverTimeoutException($"'{locator}' doesn't appear in {timeout} seconds.", e);
            }
        }

        /// <summary>
        /// Wait for element to disappear using locator.
        /// </summary>
        /// <param name="locator">Locator to element</param>
        /// <param name="timeout">Time to wait in seconds</param>
        public static void WaitUntilDisappear(By locator, int? timeout = null)
        {
            timeout = GetValueOrDefault(timeout);
            try
            {
                var wait = new WebDriverWait(Driver, GetTimeSpan(timeout));
                wait.Until(InvisibilityOfElementLocated(locator));
            }
            catch (WebDriverTimeoutException e)
            {
                throw new WebDriverTimeoutException($"'{locator}' doesn't disappear in {timeout} seconds.", e);
            }
        }

        /// <summary>
        /// Wait for element to be clickable using locator.
        /// Throws NoSuchElementExeption if element is not clickable.
        /// </summary>
        /// <param name="locator">Locator to element</param>
        /// <param name="timeout">Time to wait in seconds</param>
        public static void WaitUntilClickable(By locator, int? timeout = null)
        {
            var wait = new WebDriverWait(Driver, GetTimeSpan(timeout));
            wait.Until(ElementToBeClickable(locator));
        }

        /// <summary>
        /// Wait until the value of the input function equals to True. If the function() != True before timeout - the TimeoutException with the given message will be thrown
        /// </summary>
        /// <param name="boolFunction">Function to wait</param>
        /// <param name="timeout">Timeout in seconds</param>
        /// <param name="exceptionMessage">Exception message written in log if the timeout is reached</param>
        /// <param name="sleepTimeoutMilliseconds">Optional, sleep timeout in seconds between check-iteration</param>
        public static void WaitUntilTrue(Func<bool> boolFunction, long timeout, string exceptionMessage, int sleepTimeoutMilliseconds = 100)
        {
            var isTrue = IsTrue(boolFunction, timeout, sleepTimeoutMilliseconds);

            if (!isTrue)
            {
                throw new TimeoutException(exceptionMessage);
            }
        }

        /// <summary>
        /// Wait for element's attribute becomes specified value.
        /// Throws NoSuchAttributeException if attribute has not defined value.
        /// </summary>
        /// <param name="webElement">target web element</param>
        /// <param name="attribute">Attribute name</param>
        /// <param name="value">Expected attribute value</param>
        /// <param name="timeout">Time to wait in seconds</param>
        public static void WaitUntilAttributeIs(IWebElement webElement, string attribute, string value, int? timeout = null)
        {
            var wait = new WebDriverWait(Driver, GetTimeSpan(timeout));
            wait.Until(x => webElement.GetAttribute(attribute) != null && webElement.GetAttribute(attribute).Contains(value));
        }

        /// <summary>
        /// Wait for possible visibility. If the element doesn't appear - no exception is thrown.
        /// </summary>
        /// <param name="secondsToWait">Max time to wait in seconds</param>
        public static void WaitForPossibleVisiblility(By locator, int? secondsToWait = null)
        {
            Log.Info($"Waiting {secondsToWait} seconds for possible visibility of element");
            try
            {
                WaitUntilVisible(locator, secondsToWait);
            }
            catch (WebDriverTimeoutException)
            { }
        }

        /// <summary>
        /// Check whether the value of the boolean function becomes true until timeout.
        /// </summary>
        /// <param name="boolFunction">Boolean function</param>
        /// <param name="timeout">Timeout in seconds</param>
        /// <param name="sleepTimeoutMilliseconds">Optional, sleep timeout in seconds</param>
        /// <returns>True - if boolean function returns true until timeout; false otherwise</returns>
        public static bool IsTrue(Func<bool> boolFunction, long timeout, int sleepTimeoutMilliseconds = 100)
        {
            long startTime = GetCurrentTimestampInSec();

            while (!boolFunction() && (GetCurrentTimestampInSec() - startTime < timeout))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(sleepTimeoutMilliseconds));
            }

            return boolFunction();
        }

        /// <summary>
        /// Check for element to be visible using locator.
        /// Returns true if success. Return false if element not visible.
        /// </summary>
        /// <param name="locator">Locator to element</param>
        /// <param name="timeout">Time to wait in seconds</param>
        public static bool IsElementVisible(By locator, int? timeout = null)
        {
            try
            {
                WaitUntilVisible(locator, timeout);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check for element to be clickable (visible and enabled) using locator.
        /// Returns true if success. Return false if element not clickable.
        /// </summary>
        /// <param name="locator">Locator to element</param>
        /// <param name="timeout">Time to wait in seconds</param>
        public static bool IsElementClickable(By locator, int? timeout = null)
        {
            try
            {
                WaitUntilClickable(locator, timeout);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get vaue or returns default wait time if the input value is null
        /// </summary>
        /// <param name="timeout">Seconds to wait int value</param>
        /// <returns>Default selenium timeout if the value is null; otherwise - input value</returns>
        public static int GetValueOrDefault(int? timeout)
        {
            return timeout.GetValueOrDefault(DefaultTimeout);
        }

        /// <summary>
        /// Convert seconds to TimeSpan object. If the input value is null - the default selenium timeout is used.
        /// </summary>
        /// <param name="timeout">Seconds to convert</param>
        /// <returns>TimeOut object representing the input seconds value</returns>
        private static TimeSpan GetTimeSpan(int? timeout)
        {
            return TimeSpan.FromSeconds(GetValueOrDefault(timeout));
        }

        /// <summary>
        /// Get current Timestamp (converted to seconds)
        /// </summary>
        /// <returns>Current timestamp in seconds</returns>
        public static long GetCurrentTimestampInSec()
        {
            return ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
        }
    }
}
