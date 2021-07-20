using OpenQA.Selenium;
using static TestProject1.Helpers.WaitHelper;
using static TestProject1.Helpers.Logger;


namespace TestProject1.Helpers.Controls
{
    /// <summary>
    /// Class for methods and elements of Textbox control
    /// </summary>
    public class TextBox : BaseControl
    {
        public TextBox(string name, By locator) : base(name, ControlType.TextBox, locator) { }

        /// <summary>
        /// Enters text into a text box
        /// </summary>
        /// <param name="input">The string value to enter</param>
        /// <param name="clear">Clear or not clear the text box before entering</param>
        /// <param name="waitForElement">Еrue - wait for text box to be ready for interaction; false - used to operate with invisible text boxes (file uploads)</param>
        /// <param name="forceSet">if true - set the value into text box in any case; if false - set the value ONLY if it differs from the current text box value</param>
        public void EnterText(string input, bool clear = true, bool waitForElement = true, bool forceSet = true)
        {
            // wait for element ready to interaction
            if (waitForElement)
                WaitUntilClickable(Locator);

            if (!forceSet && GetText() == input)
            {
                Log.Info($"Skipped entering value into {this} because it's current value is already = '{input}'");
                return;
            }

            if (clear)
            {
                ClearText(waitForElement: false);
            }

            Log.Info($"Entering text '{input}' in {this}");
            var textBox = Find();
            textBox.SendKeys(input);
        }

        /// <summary>
        /// Clears text box of text
        /// </summary>
        /// <param name="waitForElement">whether code whould wait for element ready for interaction</param>
        /// <param name="checkIsCleared">whether to check element is cleared or not</param>
        public void ClearText(bool waitForElement = true, bool checkIsCleared = true)
        {
            Log.Info($"Clearing text in {this}.");

            //wait for element ready to interaction
            if (waitForElement)
                WaitUntilClickable(Locator);

            var element = Find();
            element.Clear();

            if (checkIsCleared)
            {
                //if not cleared (React JS controls or so), clear with key combination
                if (!string.IsNullOrWhiteSpace(GetText()))
                {
                    element = Find();
                    element.Click();
                    element.SendKeys(Keys.Control + "a");
                    element.SendKeys(Keys.Delete);
                }
            }
        }

        /// <summary>
        /// Gets the text value of textbox
        /// </summary>
        /// <returns> string if element exists</returns>
        public override string GetText()
        {
            Log.Info($"Getting text of {this}.");
            WaitUntilVisible(Locator);

            return Find().GetAttribute("value");
        }
    }
}
