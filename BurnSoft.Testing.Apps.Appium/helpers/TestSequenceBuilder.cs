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
            //cmd.AddRange(Sleep500());
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
