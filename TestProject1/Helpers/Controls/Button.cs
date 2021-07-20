using OpenQA.Selenium;

namespace TestProject1.Helpers.Controls
{
    /// <summary>
    /// Class for methods and elements of Button control
    /// </summary>
    public class Button : BaseControl
    {
        public Button(string name, By locator) : base(name, ControlType.Button, locator)
        {
        }
    }
}
