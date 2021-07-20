using OpenQA.Selenium;

namespace TestProject1.Helpers.Controls
{
    /// <summary>
    /// Class for methods and elements of Image control
    /// </summary>
    public class Image : BaseControl
    {
        public Image(string name, By locator) : base(name, ControlType.Image, locator)
        {
        }
    }
}
