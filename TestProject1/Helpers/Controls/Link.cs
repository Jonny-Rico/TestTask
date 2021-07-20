using OpenQA.Selenium;

namespace TestProject1.Helpers.Controls
{
    /// <summary>
    /// Class for methods and elements of Link control
    /// </summary>
    public class Link : BaseControl
    {
        /// <summary>
        /// Create new link control
        /// </summary>
        /// <param name="name">Name of the link, used for logging</param>
        /// <param name="locator">Locator to the link control</param>
        public Link(string name, By locator) : base(name, ControlType.Link, locator) { }
    }
}
