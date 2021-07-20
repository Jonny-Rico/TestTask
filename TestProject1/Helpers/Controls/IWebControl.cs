namespace TestProject1.Helpers.Controls
{
    /// <summary>
    /// Interface to implement common UI control's methods
    /// </summary>
    public interface IWebControl
    {
        IWebControl Click(bool scrollTo = false);
        IWebControl JSClick();
        void WaitForVisible(int? timeout = null);
        void WaitForClickable(int? timeout = null);
        void WaitForDisappear(int? timeout = null);
    }
}
