using OpenQA.Selenium;

namespace TestProject1.Helpers.Controls
{
    /// <summary>
    /// Class for methods and elements of Label control
    /// </summary>
    public class Label : BaseControl
    {
        public Label(string name, By locator) : base(name, ControlType.Label, locator)
        {
        }
    }
}
