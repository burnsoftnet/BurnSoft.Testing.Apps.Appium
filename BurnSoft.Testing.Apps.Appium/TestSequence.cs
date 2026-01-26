using BurnSoft.Testing.Apps.Appium.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static BurnSoft.Testing.Apps.Appium.GeneralActions;

namespace BurnSoft.Testing.Apps.Appium
{
    public class TestSequence
    {
        private GeneralActions generalActions;
        public bool DebugMode;

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
        public TestSequence(bool debugMode = false)
        {
            generalActions = new GeneralActions();
            DebugMode = debugMode;
        }
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
    }
}
