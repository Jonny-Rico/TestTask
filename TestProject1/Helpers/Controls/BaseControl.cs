using OpenQA.Selenium;
using System;
using static TestProject1.Helpers.BrowserHelper;
using static TestProject1.Helpers.Logger;
using static TestProject1.Helpers.WaitHelper;

namespace TestProject1.Helpers.Controls
{
    /// <summary>
    /// Base class for all controls
    /// </summary>
    public abstract class BaseControl : IWebControl
    {
        public string Name { get; }
        protected ControlType type;
        public By Locator;

        protected BaseControl(string name, ControlType type, By locator)
        {
            Name = name;
            this.type = type;
            Locator = locator;
        }

        public IWebElement Find()
        {
            try
            {
                return Driver.FindElement(Locator);
            }
            catch (NoSuchElementException e)
            {
                throw new Exception($"{this} not found on page.", e);
            }
        }

        /// <summary>
        /// Click on control
        /// </summary>
        /// <param name="scrollTo">whether to scroll to this control or not</param>
        /// <returns>this control</returns>
        public IWebControl Click(bool scrollTo = false)
        {
            Log.Info($"Click {this}.");

            WaitUntilClickable(Locator);

            if (scrollTo)
                JScript.ScrollToView(Locator);

            Find().Click();

            return this;
        }

        public IWebControl JSClick()
        {
            Log.Info($"Click {this}, using JS.");
            WaitUntilClickable(Locator);
            JScript.ClickOn(Find());

            return this;
        }

        /// <summary>
        /// Wait for control to be visible on the page
        /// </summary>
        /// <param name="timeout">Time to wait in seconds; if null is passed - default timeout is used</param>
        public void WaitForVisible(int? timeout = null)
        {
            timeout = GetValueOrDefault(timeout);

            Log.Info($"Waiting {this} for visible state within {timeout} seconds.");
            try
            {
                WaitUntilVisible(Locator, timeout: timeout);
            }
            catch (Exception e)
            {
                throw new Exception($"{this} not visible on page in {timeout} seconds.", e);
            }
        }

        /// <summary>
        /// Wait for control to be clickable
        /// </summary>
        /// <param name="timeout">Time to wait in seconds; if null is passed - default timeout is used</param>
        public virtual void WaitForClickable(int? timeout = null)
        {
            timeout = GetValueOrDefault(timeout);

            Log.Info($"Waiting {this} for clickable state within {timeout} seconds.");
            try
            {
                WaitUntilClickable(Locator, timeout: timeout);
            }
            catch (Exception e)
            {
                throw new Exception($"{this} not clickable in {timeout} seconds.", e);
            }
        }

        /// <summary>
        /// Wait for control to disappear from the page
        /// </summary>
        /// <param name="timeout">Time to wait in seconds; if null is passed - default timeout is used</param>
        public void WaitForDisappear(int? timeout = null)
        {
            timeout = GetValueOrDefault(timeout);

            Log.Info($"Waiting {this} for disappear within {GetValueOrDefault(timeout)} seconds.");
            try
            {
                WaitUntilDisappear(Locator, timeout: timeout);
            }
            catch (Exception e)
            {
                throw new Exception($"{this} still present on page in {GetValueOrDefault(timeout)} seconds.", e);
            }
        }

        /// <summary>
        /// Check if control is clickable
        /// </summary>
        /// <param name="timeout">time to wait in seconds; if null is passed - default selenium timeout is used</param>
        /// <returns>true if element becomes clickable until timeout; false otherwise</returns>
        public bool IsClickable(int? timeout = null)
        {
            Log.Info($"Check if {this} is clickable within {GetValueOrDefault(timeout)} seconds.");

            return IsElementClickable(Locator, timeout: timeout);
        }

        /// <summary>
        /// Gets the text value
        /// </summary>
        /// <returns>String text if element exists</returns>
        public virtual string GetText()
        {
            Log.Info($"Getting text of {this}");
            WaitUntilVisible(Locator);

            return Find().GetAttribute("innerText");
        }
    }
}
