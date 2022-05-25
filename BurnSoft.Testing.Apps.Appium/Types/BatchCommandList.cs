
// ReSharper disable UnusedMember.Global
namespace BurnSoft.Testing.Apps.Appium.Types
{
    /// <summary>
    /// Batch Command List to Run a List of Selnium commands and hold all the elements , values and commands
    /// </summary>
    public class BatchCommandList
    {
        /// <summary>
        /// Gets or sets the name of the test.
        /// </summary>
        /// <value>The name of the test.</value>
        public string TestName { get; set; }
        /// <summary>
        /// Command Action, find by automation id, name, Class Name, etc
        /// </summary>
        public GeneralActions.AppAction CommandAction { get; set; }
        /// <summary>
        /// Action to perform, send command, click, nothing, etc.
        /// </summary>
        public GeneralActions.MyAction Actions { get; set; }
        /// <summary>
        /// Element Name
        /// </summary>
        public string ElementName { get; set; }
        /// <summary>
        /// Keys or text to send 
        /// </summary>
        public string SendKeys { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [passed failed].
        /// </summary>
        /// <value><c>true</c> if [passed failed]; otherwise, <c>false</c>.</value>
        public bool PassedFailed { get; set; }
        /// <summary>
        /// Gets or sets the returned value.
        /// </summary>
        /// <value>The returned value.</value>
        public string ReturnedValue { get; set; }
        /// <summary>
        /// Gets or sets the expected returned value. This is used when you have the action set to compare
        /// </summary>
        /// <value>The expected returned value.</value>
        public string ExpectedReturnedValue { get; set; }
        /// <summary>
        /// Gets or sets the found returned value.
        /// </summary>
        /// <value>The expected returned value.</value>
        public string ReturnedFoundValue { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [returned value blank ok].
        /// </summary>
        /// <value><c>true</c> if [returned value blank ok]; otherwise, <c>false</c>.</value>
        public bool ReturnedValueBlankOk { get; set; }
        /// <summary>
        /// Gets or sets the sleep interval.
        /// </summary>
        /// <value>The sleep interval.</value>
        public int SleepInterval { get; set; }
        /// <summary>
        /// Gets or sets the tab count. This is used for Action ClickOnElementAndTabOver
        /// </summary>
        /// <value>The tab count.</value>
        public int TabCount { get; set; }
        /// <summary>
        /// Gets or sets the test number automatically as the results run.
        /// </summary>
        /// <value>The test number.</value>
        public int TestNumber { get; set; }

    }
}
