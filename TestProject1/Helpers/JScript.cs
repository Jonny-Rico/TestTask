using OpenQA.Selenium;
using static TestProject1.Helpers.BrowserHelper;

namespace TestProject1.Helpers
{
    /// <summary>
    /// Class for methods that are using JScript
    /// </summary>
    public class JScript
    {
        public static void ScrollToView(IWebElement element)
        {
            //this method will use javasript executor to move page to element specified
            var js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void ScrollToView(By by)
        {
            //this method will use javasript executor to move page to element specified
            ScrollToView(Driver.FindElement(by));
        }

        public static void ClickOn(IWebElement element)
        {
            //this method will use javasript executor to move page to element specified
            var js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].click();", element);
        }
    }
}
