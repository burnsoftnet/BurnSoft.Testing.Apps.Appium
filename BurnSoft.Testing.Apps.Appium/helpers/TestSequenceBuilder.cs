using BurnSoft.Testing.Apps.Appium.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnSoft.Testing.Apps.Appium.helpers
{
    /// <summary>
    /// Class TestSequenceBuilder is just a simple class that will have the 
    /// functions needed to append steps together and just pass the settings 
    /// you want to the function that will perform the function you want and 
    /// append it or add it to the Range of your test recipe.
    /// </summary>
    public class TestSequenceBuilder
    {
        /// <summary>
        /// Clicks the on element or verify that it exists.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="verify">if set to <c>true</c> [verify].</param>
        /// <param name="commandAction">The command action.</param>
        /// <example>
        /// List &lt;BatchCommandList&gt; cmd = new List&lt;BatchCommandList&gt;();
        /// cmd.AddRange(TestSequenceBuilder.ClickOnElement("Save Button", "btnSave"));
        /// </example>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> ClickOnElement(string testName, string element, bool verify = false,
            GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = verify ? "Verify" : "Click On";
            GeneralActions.MyAction action = verify ? GeneralActions.MyAction.Nothing : GeneralActions.MyAction.Click;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction
            });
            return cmd;
        }

        /// <summary>
        /// Doubles the click on element or verifies the contol.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="verify">if set to <c>true</c> [verify].</param>
        /// <param name="commandAction">The command action.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> DoubleClickOnElement(string testName, string element, bool verify = false,
            GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = verify ? "Verify" : "Double Click On";
            GeneralActions.MyAction action = verify ? GeneralActions.MyAction.Nothing : GeneralActions.MyAction.DoubleClick;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction
            });
            return cmd;
        }

        /// <summary>
        /// Sends the text to the control or verifies it.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        /// <param name="verify">if set to <c>true</c> [verify].</param>
        /// <param name="commandAction">The command action.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> SendText(string testName, string element, string value, bool verify = false,
            GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = verify ? "Verify" : "Send Text";
            GeneralActions.MyAction action = verify ? GeneralActions.MyAction.Nothing : GeneralActions.MyAction.SendKeys;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction,
                SendKeys = value
            });
            return cmd;
        }

        /// <summary>
        /// Clears the and send text to the control.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        /// <param name="verify">if set to <c>true</c> [verify].</param>
        /// <param name="commandAction">The command action.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> ClearAndSendText(string testName, string element, string value, bool verify = false,
            GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = verify ? "Verify" : "Clear And Send Text";
            GeneralActions.MyAction action = verify ? GeneralActions.MyAction.Nothing : GeneralActions.MyAction.ClearAndSendKeys;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction,
                SendKeys = value
            });
            return cmd;
        }

        /// <summary>
        /// Clicks the on element and tab over.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="tabCount">The tab count.</param>
        /// <param name="verify">if set to <c>true</c> [verify].</param>
        /// <param name="commandAction">The command action.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> ClickOnElementAndTabOver(string testName, string element, int tabCount, bool verify = false,
            GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = verify ? "Verify" : $"Click on {element} and tab over {tabCount} times";
            GeneralActions.MyAction action = verify ? GeneralActions.MyAction.Nothing : GeneralActions.MyAction.ClickOnElementAndTabOver;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction,
                TabCount = tabCount
            });
            return cmd;
        }

        /// <summary>
        /// Gets the name of the value from previous test and compare by test.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="fromTestName">Name of from test.</param>
        /// <param name="commandAction">The command action.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> GetValueFromPreviousTestAndCompareByTestName(string testName, string element, 
            string fromTestName, GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = $"Get value from {fromTestName} and Compare value in control {element}";
            GeneralActions.MyAction action = GeneralActions.MyAction.GetValueFromPreviousTestAndCompareByTestName;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction,
                TestNameLookUp = fromTestName
            });
            return cmd;
        }

        /// <summary>
        /// Gets the value from previous test and compare by test number.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="fromTestNumber">From test number.</param>
        /// <param name="commandAction">The command action.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> GetValueFromPreviousTestAndCompareByTestNumber(string testName, string element, 
            int fromTestNumber, GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = $"Get value from test step {fromTestNumber} and Compare value in control {element}";
            GeneralActions.MyAction action = GeneralActions.MyAction.GetValueFromPreviousTestAndCompareByTestNumber;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction,
                TestNumber = fromTestNumber
            });
            return cmd;
        }

        /// <summary>
        /// Sends the key down to control.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="repeatXTimes">The repeat x times.</param>
        /// <param name="commandAction">The command action.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> SendKeyDownToControl(string testName, string element,
            int repeatXTimes, GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = $"Send Key Down to control {element} {repeatXTimes} times.";
            GeneralActions.MyAction action = GeneralActions.MyAction.KeyDown;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction,
                RepeatXTimes = repeatXTimes
            });
            return cmd;
        }

        /// <summary>
        /// Sends the key up to control.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="repeatXTimes">The repeat x times.</param>
        /// <param name="commandAction">The command action.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> SendKeyUpToControl(string testName, string element,
            int repeatXTimes, GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = $"Send Key Up to control {element} {repeatXTimes} times.";
            GeneralActions.MyAction action = GeneralActions.MyAction.KeyUp;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction,
                RepeatXTimes = repeatXTimes
            });
            return cmd;
        }

        /// <summary>
        /// Sends the enter key to control.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="repeatXTimes">The repeat x times.</param>
        /// <param name="commandAction">The command action.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> SendEnterKeyToControl(string testName, string element,
            int repeatXTimes, GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = $"Send Enter Key to control {element} {repeatXTimes} times.";
            GeneralActions.MyAction action = GeneralActions.MyAction.KeyEnter;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction,
                RepeatXTimes = repeatXTimes
            });
            return cmd;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="fileNameAndPath">The file name and path.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> DeleteFile(string testName, string fileNameAndPath)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = $"Delete File {fileNameAndPath}";
            GeneralActions.MyAction action = GeneralActions.MyAction.DeleteFile;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                FilePath = fileNameAndPath
            });
            return cmd;
        }

        /// <summary>
        /// Fails if file exists.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="fileNameAndPath">The file name and path.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> FailIfFileExists(string testName, string fileNameAndPath)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = $"Checking to see if File {fileNameAndPath} doesn't exists.";
            GeneralActions.MyAction action = GeneralActions.MyAction.FailIfFileExists;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                FilePath = fileNameAndPath
            });
            return cmd;
        }

        /// <summary>
        /// Passes if file exists.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="fileNameAndPath">The file name and path.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> PassIfFileExists(string testName, string fileNameAndPath)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = $"Checking to see if File {fileNameAndPath} does exists.";
            GeneralActions.MyAction action = GeneralActions.MyAction.PassIfFileExists;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                FilePath = fileNameAndPath
            });
            return cmd;
        }

        /// <summary>
        /// Gets the focus on window.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="fileNameAndPath">The file name and path.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> GetFocusOnWindow(string testName, string fileNameAndPath)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = $"Get Focus on Window.";
            GeneralActions.MyAction action = GeneralActions.MyAction.GetFocusNewWindow;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                FilePath = fileNameAndPath
            });
            return cmd;
        }

        /// <summary>
        /// Reads the value.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="verify">if set to <c>true</c> [verify].</param>
        /// <param name="commandAction">The command action.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> ReadValue(string testName, string element, bool verify = false,
            GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = verify ? "Verify" : "Read Value";
            GeneralActions.MyAction action = verify ? GeneralActions.MyAction.Nothing : GeneralActions.MyAction.ReadValue;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction
            });
            return cmd;
        }

        /// <summary>
        /// Reads the value and compare.
        /// </summary>
        /// <param name="testName">Name of the test.</param>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        /// <param name="verify">if set to <c>true</c> [verify].</param>
        /// <param name="commandAction">The command action.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> ReadValueAndCompare(string testName, string element, string value, bool verify = false,
            GeneralActions.AppAction commandAction = GeneralActions.AppAction.FindElementById)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();
            string actionMs = verify ? "Verify" : $"Read Value And compare to {value}";
            GeneralActions.MyAction action = verify ? GeneralActions.MyAction.Nothing : GeneralActions.MyAction.ReadAndCompare;

            cmd.Add(new BatchCommandList()
            {
                Actions = action,
                TestName = $"{actionMs} {testName}",
                ElementName = element,
                CommandAction = commandAction,
                ExpectedReturnedValue = value
            });
            return cmd;
        }

        /// <summary>
        /// Sleep for 500ms this instance.
        /// </summary>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> Sleep500()
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();

            cmd.Add(new BatchCommandList()
            {
                Actions = GeneralActions.MyAction.Sleep,
                TestName = $"Sleep 500ms",
                SleepInterval = 500
            });
            return cmd;
        }

        /// <summary>
        /// Sleeps the specified interval.
        /// </summary>
        /// <param name="interval">The interval in ms.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        public static List<BatchCommandList> Sleep(int interval = 1000)
        {
            List<BatchCommandList> cmd = new List<BatchCommandList>();

            cmd.Add(new BatchCommandList()
            {
                Actions = GeneralActions.MyAction.Sleep,
                TestName = $"Sleep {interval}ms",
                SleepInterval = interval
            });
            return cmd;
        }
    }
}
