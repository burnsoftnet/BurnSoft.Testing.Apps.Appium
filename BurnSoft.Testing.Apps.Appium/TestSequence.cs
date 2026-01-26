using BurnSoft.Testing.Apps.Appium.Types;
using System;
using System.Collections.Generic;
using System.Threading;
using static BurnSoft.Testing.Apps.Appium.GeneralActions;

namespace BurnSoft.Testing.Apps.Appium
{
    /// <summary>
    /// Class TestSequence is the old BatchRun Function that was in the General Actions section to help grow
    /// this Batch Run with extra functions but still use the General Actions to run the tests.
    /// Implements the <see cref="IDisposable" />
    /// </summary>
    /// <seealso cref="IDisposable" />
    public class TestSequence : IDisposable
    {
        /// <summary>
        /// The general actions link to use function
        /// </summary>
        private GeneralActions generalActions;
        /// <summary>
        /// The debug mode toggle
        /// </summary>
        public bool DebugMode;
        #region "Exception Error Handling"        
        /// <summary>
        /// The class location
        /// </summary>
        private static string _classLocation = "BurnSoft.Testing.Apps.Appium.TestSequence";

        /// <summary>
        /// Errors the message for regular Exceptions
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string functionName, Exception e) => $"{_classLocation}.{functionName} - {e.Message}";
        /// <summary>
        /// Errors the message for access violations
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string functionName, AccessViolationException e) => $"{_classLocation}.{functionName} - {e.Message}";
        /// <summary>
        /// Errors the message for invalid cast exception
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string functionName, InvalidCastException e) => $"{_classLocation}.{functionName} - {e.Message}";
        /// <summary>
        /// Errors the message argument exception
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string functionName, ArgumentException e) => $"{_classLocation}.{functionName} - {e.Message}";
        /// <summary>
        /// Errors the message for argument null exception.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="e">The e.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string functionName, ArgumentNullException e) => $"{_classLocation}.{functionName} - {e.Message}";
        #endregion
        #region "Event Handlers"        
        /// <summary>
        /// Occurs when En exception is caught 
        /// </summary>
        public event EventHandler<string> ErrorCatcher;
        /// <summary>
        /// Sends the error the the event handler
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual void SendError(string message)
        {
            ErrorCatcher?.Invoke(this, message);
        }
        /// <summary>
        /// Occurs when [debug log].
        /// </summary>
        public event EventHandler<string> DebugLog;
        /// <summary>
        /// Sends the debug messages only when the debug 
        /// flag is set to true
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>virtualvoid.</returns>
        protected virtual void SendDebug(string message)
        {
            if (DebugMode) DebugLog?.Invoke(this, message);
        }
        #endregion        
        /// <summary>
        /// Initializes a new instance of the <see cref="TestSequence"/> class.
        /// </summary>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        public TestSequence(bool debugMode = false)
        {
            generalActions = new GeneralActions();
            DebugMode = debugMode;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TestSequence"/> class.
        /// </summary>
        /// <param name="nodeExecutable">The node executable.</param>
        /// <param name="appiumMainJs">The appium main js.</param>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        public TestSequence(string nodeExecutable, string appiumMainJs,
            bool debugMode = false)
        {
            generalActions = new GeneralActions(nodeExecutable: nodeExecutable, 
                appiumMainJs: appiumMainJs, debugMode: debugMode);
            generalActions.TestName = "GenericTestSequence";
            generalActions.SettingsScreenShotLocation = "";
            generalActions.DoSleep = true;
            DebugMode = debugMode;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TestSequence"/> class.
        /// </summary>
        /// <param name="nodeExecutable">The node executable.</param>
        /// <param name="appiumMainJs">The appium main js.</param>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        /// <param name="settingsScreenShotLocation">The settings screen shot location.</param>
        /// <param name="doSleep">if set to <c>true</c> [do sleep].</param>
        /// <param name="testName">Name of the test.</param>
        public TestSequence(string nodeExecutable, string appiumMainJs,
            bool debugMode = false, string settingsScreenShotLocation = "", 
            bool doSleep = true, string testName = "GenericTestSequence")
        {
            generalActions = new GeneralActions(nodeExecutable: nodeExecutable,
                appiumMainJs: appiumMainJs, debugMode: debugMode);
            generalActions.TestName = testName;
            generalActions.SettingsScreenShotLocation = settingsScreenShotLocation;
            generalActions.DoSleep = doSleep;
            DebugMode = debugMode;
        }
        /// <summary>
        /// Runs the specified application under test.
        /// </summary>
        /// <param name="appUnderTest">The application under test.</param>
        /// <param name="cmd">The commands to run aginst the aut.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        /// <exception cref="System.Exception">Error occured and the Driver is not active!</exception>
        /// <exception cref="System.Exception">Was Not able to {msg}{Environment.NewLine}{errOut}</exception>
        public List<BatchCommandList> Run(string appUnderTest, List<BatchCommandList> cmd, out string errOut)
        {
            List<BatchCommandList> theReturned = new List<BatchCommandList>();
            errOut = @"";
            try
            {
                generalActions.ErrorCatcher += (ss, ee) =>
                {
                    SendError($"GeneralActions {ee}");
                };
                generalActions.Initialize(appUnderTest);
                int testNumber = 1;
                foreach (BatchCommandList c in cmd)
                {
                    bool didpass = false;
                    string result;
                    string sendkeys = @"";
                    string foundValue = "";
                    try
                    {
                        if (generalActions.DesktopSession == null) throw new Exception("Error occured and the Driver is not active!");
                        if (c.SendKeys != null) sendkeys = c.SendKeys;
                        string msg = $"{c.Actions} on {c.ElementName} using {c.CommandAction}";
                        if (sendkeys.Length > 0) msg = $"{c.Actions} {sendkeys} to {c.ElementName} using {c.CommandAction}";
                        if (c.Actions.Equals(MyAction.Nothing)) msg = msg.Replace("Nothing", "Verify Exists");

                        switch (c.Actions)
                        {
                            case MyAction.ReadValue:
                                foundValue = generalActions.PerformAction(c.ElementName, out errOut, c.CommandAction);
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");
                                msg += $"{msg}. Found value {foundValue}";
                                if (!didpass) didpass = true;
                                break;
                            case MyAction.ReadAndCompare:
                                foundValue = generalActions.PerformAction(c.ElementName, out errOut, c.CommandAction);
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");

                                if (foundValue.Equals(c.ExpectedReturnedValue))
                                {
                                    if (!didpass) didpass = true;
                                    msg += $"{msg}. Found value {foundValue}, and expected {c.ExpectedReturnedValue}";
                                }
                                else
                                {
                                    msg += $"{msg}. Found value {foundValue}, but expected {c.ExpectedReturnedValue}";
                                }
                                break;
                            case MyAction.Sleep:
                                Thread.Sleep(c.SleepInterval);
                                msg += $"Was able to Sleep for {c.SleepInterval} ms.";
                                if (!didpass) didpass = true;
                                break;
                            case MyAction.ClickOnElementAndTabOver:
                                didpass = generalActions.PerformTabSelect(c.ElementName, c.TabCount, out errOut, c.CommandAction);
                                string actionMsg = didpass ? "Was" : "Was Not";
                                msg += $"{actionMsg} to click on {c.ElementName} and tab over {c.TabCount} to select element at tab.";
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");
                                break;
                            case MyAction.GetValueFromPreviousTestAndCompareByTestName:
                                string ExpectedReturnedValue = generalActions.GetStringFromStep(theReturned, c.TestNameLookUp, out errOut);
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");
                                foundValue = generalActions.PerformAction(c.ElementName, out errOut, c.CommandAction);
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");

                                if (foundValue.Equals(ExpectedReturnedValue))
                                {
                                    if (!didpass) didpass = true;
                                    msg += $"{msg}. Found value {foundValue}, and expected {ExpectedReturnedValue}";
                                }
                                else
                                {
                                    msg += $"{msg}. Found value {foundValue}, but expected {ExpectedReturnedValue}";
                                }
                                break;
                            case MyAction.GetValueFromPreviousTestAndCompareByTestNumber:
                                string ExpectedReturnedValueNum = generalActions.GetStringFromStep(theReturned, c.TestNumber.ToString(), out errOut);
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");
                                foundValue = generalActions.PerformAction(c.ElementName, out errOut, c.CommandAction);
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");

                                if (foundValue.Equals(ExpectedReturnedValueNum))
                                {
                                    if (!didpass) didpass = true;
                                    msg += $"{msg}. Found value {foundValue}, and expected {ExpectedReturnedValueNum}";
                                }
                                else
                                {
                                    msg += $"{msg}. Found value {foundValue}, but expected {ExpectedReturnedValueNum}";
                                }
                                break;
                            case MyAction.KeyDown:
                                didpass = ClickOnControlSendKeyDown(c.ElementName, c.RepeatXTimes, out errOut);
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg} {c.RepeatXTimes} times{Environment.NewLine}{errOut}");
                                msg += $"{msg} {c.RepeatXTimes} times.";
                                break;
                            case MyAction.KeyUp:
                                didpass = ClickOnControlSendKeyUp(c.ElementName, c.RepeatXTimes, out errOut);
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg} {c.RepeatXTimes} times{Environment.NewLine}{errOut}");
                                msg += $"{msg} {c.RepeatXTimes} times.";
                                break;
                            default:
                                if (!generalActions.PerformAction(c.ElementName, sendkeys, c.Actions, out errOut, c.CommandAction))
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");
                                if (!didpass) didpass = true;
                                break;
                        }
                        result = $"Was able to {msg}{Environment.NewLine}";

                    }
                    catch (Exception e)
                    {
                        didpass = false;
                        if (generalActions.ScreenShotLocation.Count > 0)
                        {
                            result = $"{e.Message}{Environment.NewLine}";
                            foreach (string s in generalActions.ScreenShotLocation)
                            {
                                result = $"{s}{Environment.NewLine}";
                            }
                        }
                        else
                        {
                            result = e.Message;
                        }

                    }
                    theReturned.Add(new BatchCommandList()
                    {
                        SleepInterval = c.SleepInterval,
                        Actions = c.Actions,
                        ElementName = c.ElementName,
                        SendKeys = c.SendKeys,
                        PassedFailed = didpass,
                        ReturnedValue = result,
                        TestName = c.TestName,
                        ReturnedFoundValue = foundValue,
                        TestNumber = testNumber
                    });
                    testNumber++;
                }
            }
            catch (Exception e)
            {
                errOut = e.Message;
            }

            return theReturned;
        }

        /// <summary>
        /// Clicks the on control send key down.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="sendXTimes">The send x times.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.Exception"></exception>
        private bool ClickOnControlSendKeyDown(string control, int sendXTimes, out string errOut)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                bool value = generalActions.PerformAction(control, "", GeneralActions.MyAction.Click, out errOut);
                if (errOut.Length > 0) throw new Exception(errOut);

                for (int i = 0; i > sendXTimes; i++)
                {
                    value = generalActions.PerformAction(control, "", GeneralActions.MyAction.KeyDown, out errOut);
                    if (errOut.Length > 0) throw new Exception(errOut);
                }
                bAns = true;
            }
            catch (Exception e)
            {
                errOut = ErrorMessage("ClickOnControlSendKeyDown", e);
            }
            return bAns;
        }

        /// <summary>
        /// Clicks the on control send key up.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="sendXTimes">The send x times.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.Exception"></exception>
        private bool ClickOnControlSendKeyUp(string control, int sendXTimes, out string errOut)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                bool value = generalActions.PerformAction(control, "", GeneralActions.MyAction.Click, out errOut);
                if (errOut.Length > 0) throw new Exception(errOut);

                for (int i = 0; i > sendXTimes; i++)
                {
                    value = generalActions.PerformAction(control, "", GeneralActions.MyAction.KeyUp, out errOut);
                    if (errOut.Length > 0) throw new Exception(errOut);
                }
                bAns = true;
            }
            catch (Exception e)
            {
                errOut = ErrorMessage("ClickOnControlSendKeyUp", e);
            }
            return bAns;
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            generalActions.Dispose();
        }
    }
}
