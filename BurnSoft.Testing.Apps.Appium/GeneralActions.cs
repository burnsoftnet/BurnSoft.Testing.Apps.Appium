using BurnSoft.Testing.Apps.Appium.Types;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
//using OpenQA.Selenium.Remote;
//using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Threading;

namespace BurnSoft.Testing.Apps.Appium
{
    /// <summary>
    /// The General Actions Class is the main class that will found to common functions used to communicate with appium.  Just like I did
    /// in the selenium helper library, this will be the main class in case there are other special classes that need to be created to have it work with
    /// other OS's or application types, etc.
    /// </summary>
    public class GeneralActions : IDisposable
    {
        #region "Private Variables"        
        /// <summary>
        /// The wait for application launch
        /// </summary>
        private int _waitForAppLaunch;
        /// <summary>
        /// The sleep interval
        /// </summary>
        private int _sleepInterval;
        /// <summary>
        /// Gets the desktop session.
        /// </summary>
        /// <value>The desktop session.</value>
        public WindowsDriver DesktopSession { get; private set; }
        /// <summary>
        /// The appium server
        /// </summary>
        private AppiumHelper appiumServer;
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
        #region "Public Variables"
        /// <summary>
        /// The initialize passed
        /// </summary>
        public bool InitPassed;
        /// <summary>
        /// The test name that will mostly be used for the screen capturing exception capturing
        /// </summary>
        public string TestName;
        /// <summary>
        /// The settings screen shot location
        /// </summary>
        public string SettingsScreenShotLocation;
        /// <summary>
        /// Toggle the sleeping after a command was issue so you can see the results
        /// </summary>
        public bool DoSleep;
        /// <summary>
        /// The screen shot location/
        /// </summary>
        public List<string> ScreenShotLocation;
        /// <summary>
        /// Gets or sets the sleep interval.
        /// </summary>
        /// <value>The sleep interval.</value>
        public int SleepInterval
        {
            get
            {
                if (_sleepInterval == 0)
                {
                    return 2000;
                }
                else
                {
                    return _sleepInterval;
                }
            }
            set => _sleepInterval = value;
        }
        /// <summary>
        /// Gets or sets the wait for application launch.
        /// </summary>
        /// <value>The wait for application launch.</value>
        public int WaitForAppLaunch
        {
            get
            {
                if (_waitForAppLaunch ==0)
                {
                    _waitForAppLaunch = 5;
                }

                return _waitForAppLaunch;
            }
            set => _waitForAppLaunch = value;
        }
        /// <summary>
        /// Gets or sets the application path.
        /// </summary>
        /// <value>The application path.</value>
        public string ApplicationPath { get; set; }
        /// <summary>
        /// Gets or sets the error lists.
        /// </summary>
        /// <value>The error lists.</value>
        public List<string> ErrorLists { get; set; }
        /// <summary>
        /// Gets or sets the node executable.
        /// </summary>
        /// <value>The node executable.</value>
        public string NodeExecutable { get; set; }
        /// <summary>
        /// Gets or sets the appium main js.
        /// </summary>
        /// <value>The appium main js.</value>
        public string AppiumMainJs { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [debug mode].
        /// </summary>
        /// <value><c>true</c> if [debug mode]; otherwise, <c>false</c>.</value>
        private bool DebugMode { get; set; }
        #endregion
        #region "Exception Error Handling"        
        /// <summary>
        /// The class location
        /// </summary>
        private static string _classLocation = "BurnSoft.Testing.Apps.Appium.GeneralActions";

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
        #region "Private init and cleanup functions"        
        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="error">The error.</param>
        private void AddError(string error)
        {
            SendError(error);
            if (ErrorLists == null) ErrorLists = new List<string>();
            ErrorLists.Add(error);
        }
        #endregion
        #region "Public Initalization and cleanup function"
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralActions"/> class.
        /// </summary>
        public GeneralActions()
        {
            //GeneralActionsInit();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralActions"/> class.
        /// </summary>
        /// <param name="nodeExecutable">The node executable.</param>
        /// <param name="appiumMainJs">The appium main js.</param>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        public GeneralActions(string nodeExecutable, string appiumMainJs,
            bool debugMode = false)
        {
            NodeExecutable = nodeExecutable;
            AppiumMainJs = appiumMainJs;
            DebugMode = debugMode;
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Close the desktopSession
            if (DesktopSession != null)
            {
                try
                {
                    DesktopSession?.Close();
                }
                catch (Exception e)
                {
                    SendDebug($"ERROR in Dispose while Closing DesktopSession: {e}");
                }
            }
            appiumServer.StopAppiumServer();
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <exception cref="System.Exception">AppSession is null, check your settings</exception>
        /// <exception cref="System.Exception">AppSession.SessionId is null, check your application path</exception>
        /// <exception cref="System.Exception">DesktopSession is null, please check your settings</exception>
        public void Initialize(string appUnderTest)
        {
            try
            {
                appiumServer = new AppiumHelper(nodeExecutable: NodeExecutable, appiumMainJs: AppiumMainJs);
                appiumServer.Errors += (ss, ee) =>
                {
                    SendError(ee);
                };
                var options = appiumServer.SetDesiredCapabilities(appUnderTest);
                if (!appiumServer.StartDriverConnection(options)) throw new Exception("Error Starting Connection!");
                DesktopSession = appiumServer.driver;
                //AppSession = DesktopSession;
            }
            catch (Exception e)
            {
                InitPassed = false;
                AddError(ErrorMessage("Initialize", e));
                ScreenShotIt();
            }
        }


        #endregion
        #region "Enumerators"
        /// <summary>
        /// Enum AppAction
        /// </summary>
        public enum AppAction
        {
            /// <summary>
            /// The find element by accessibility identifier
            /// </summary>
            FindElementByAccessibilityId,
            /// <summary>
            /// The find element by name
            /// </summary>
            FindElementByName,
            /// <summary>
            /// The find element by windows UI automation
            /// </summary>
            FindElementByWindowsUiAutomation,
            /// <summary>
            /// The find element by class name
            /// </summary>
            FindElementByClassName,
            /// <summary>
            /// The find element by CSS selector
            /// </summary>
            FindElementByCssSelector,
            /// <summary>
            /// The find element by identifier
            /// </summary>
            FindElementById,
            /// <summary>
            /// The find element by image
            /// </summary>
            FindElementByImage,
            /// <summary>
            /// The find element by link text
            /// </summary>
            FindElementByLinkText,
            /// <summary>
            /// The find element by partial link text
            /// </summary>
            FindElementByPartialLinkText,
            /// <summary>
            /// The find element by tag name
            /// </summary>
            FindElementByTagName,
            /// <summary>
            /// The nothing
            /// </summary>
            Nothing
        }
        /// <summary>
        /// Enum My Actions to do on the web page
        /// </summary>
        public enum MyAction
        {
            /// <summary>
            /// The nothing
            /// </summary>
            Nothing,
            /// <summary>
            /// The click
            /// </summary>
            Click,
            /// <summary>
            /// The double click
            /// </summary>
            DoubleClick,
            /// <summary>
            /// The send keys
            /// </summary>
            SendKeys,
            /// <summary>
            /// The clear and send keys
            /// </summary>
            ClearAndSendKeys,
            /// <summary>
            /// Read the value of the control
            /// </summary>
            ReadValue,
            /// <summary>
            /// The read and compare the text value
            /// </summary>
            ReadAndCompare,
            /// <summary>
            /// The sleep betwteen steps
            /// </summary>
            Sleep,
            /// <summary>
            /// The key down action
            /// </summary>
            KeyDown,
            /// <summary>
            /// The key up action
            /// </summary>
            KeyUp,
            /// <summary>
            /// The click on element and tab over
            /// </summary>
            ClickOnElementAndTabOver,
            /// <summary>
            /// The get value from previous test and compare value using test name to look up
            /// </summary>
            GetValueFromPreviousTestAndCompareByTestName,
            /// <summary>
            /// The get value from previous test and compare by test number
            /// </summary>
            GetValueFromPreviousTestAndCompareByTestNumber
        }
        #endregion
        #region "Appinum Actions"
        #region "Private/Internal Functions"
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <param name="automationId">The automation identifier.</param>
        /// <param name="myAction">My action.</param>
        /// <returns>WindowsElement.</returns>
        private AppiumElement GetAction(string automationId, AppAction myAction)
        {
            switch (myAction)
            {
                case AppAction.FindElementByAccessibilityId:
                    return DesktopSession.FindElement(by: MobileBy.Name(automationId));
                case AppAction.FindElementByName:
                    return DesktopSession.FindElement(by: MobileBy.Name(automationId));
                case AppAction.FindElementByClassName:
                    return DesktopSession.FindElement(by: MobileBy.ClassName(automationId)); 
                case AppAction.FindElementByCssSelector:
                    return DesktopSession.FindElement(by: MobileBy.CssSelector(automationId));
                case AppAction.FindElementById:
                    return DesktopSession.FindElement(by: MobileBy.Name(automationId));
                case AppAction.FindElementByImage:
                    return DesktopSession.FindElement(by: MobileBy.Name(automationId));
                case AppAction.FindElementByLinkText:
                    return DesktopSession.FindElement(by: MobileBy.LinkText(automationId));
                case AppAction.FindElementByPartialLinkText:
                    return DesktopSession.FindElement(by: MobileBy.PartialLinkText(automationId));
                case AppAction.FindElementByTagName:
                    return DesktopSession.FindElement(by: MobileBy.TagName(automationId));
                case AppAction.FindElementByWindowsUiAutomation:
                    return DesktopSession.FindElement(by: MobileBy.Name(automationId));
                default:
                    return DesktopSession.FindElement(by: MobileBy.Name(automationId));
            }
        }
        /// <summary>
        /// Screens the shot it.
        /// </summary>
        internal void ScreenShotIt()
        {
            if (DesktopSession != null)
            {
                ITakesScreenshot screenShotDriver = (ITakesScreenshot)DesktopSession;
                if (screenShotDriver.GetScreenshot() != null)
                {
                    if (TestName == null || TestName?.Length == 0) TestName = "UnMarked";
                    Screenshot screenShot = screenShotDriver.GetScreenshot();
                    string savePath = $"{SettingsScreenShotLocation}\\{TestName}-{DateTime.Now.Ticks}.png";
                    screenShot.SaveAsFile(savePath);
                    ScreenShotLocation?.Add(savePath);
                }
                else
                {
                    Debug.Print("The application is not active so we are unable to take a screen shot at this time.");
                }

            }
        }
        /// <summary>
        /// Gets the string from step.
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <param name="testName">Name of the test.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns>System.String.</returns>
        private string GetStringFromStep(List<BatchCommandList> lst, string testName, out string errOut)
        {
            string sAns = "";
            errOut = "";
            try
            {
                List<BatchCommandList> myResults = lst.Where(r => r.TestName.Equals(testName)).ToList();
                foreach (BatchCommandList m in myResults)
                {
                    if (m.ReturnedFoundValue != null)
                    {
                        sAns = m.ReturnedFoundValue;
                    }

                    if (m.ReturnedValue != null)
                    {
                        sAns = m.ReturnedValue;
                    }
                    
                }
            }
            catch (Exception e)
            {
                errOut = ErrorMessage("GetStringFromStep", e);
            }
            return sAns;
        }
        /// <summary>
        /// Gets the string from step.
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <param name="testNumber">The test number.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns>System.String.</returns>
        private string GetStringFromStep(List<BatchCommandList> lst, int testNumber, out string errOut)
        {
            string sAns = "";
            errOut = "";
            try
            {
                List<BatchCommandList> myResults = lst.Where(r => r.TestNumber.Equals(testNumber)).ToList();
                foreach (BatchCommandList m in myResults)
                {
                    if (m.ReturnedFoundValue != null)
                    {
                        sAns = m.ReturnedFoundValue;
                    }

                    if (m.ReturnedValue != null)
                    {
                        sAns = m.ReturnedValue;
                    }

                }
            }
            catch (Exception e)
            {
                errOut = ErrorMessage("GetStringFromStep", e);
            }
            return sAns;
        }
        #endregion

        /// <summary>
        /// Get Elemtns from item test
        /// </summary>
        /// <remarks>Might be able to delete later is not needed</remarks>
        /// <param name="automationId"></param>
        /// <param name="errOut"></param>
        /// <param name="myAction"></param>
        public void GetElements(string automationId, out string errOut, 
            AppAction myAction = AppAction.FindElementById)
        {
            errOut = "";
            try
            {

                //IEnumerable<AppiumWebElement> elementsOne = DesktopSession.FindElementsByAccessibilityId(automationId).ToList();
                IEnumerable<AppiumElement> elementsOne = DesktopSession.FindElements(By.Id(automationId)).ToList();
                var test = DesktopSession.FindElements(By.Id(automationId));
                Thread.Sleep(200);
            }
            catch (Exception e)
            {
                errOut = ErrorMessage("GetElements", e);
            }
        }
        /// <summary>
        /// Performs the tab select, it will start at the automation id that you selected, 
        /// then you have the option to tab over x many times
        /// to another item then it will send a space key press to activate the final element.
        /// </summary>
        /// <param name="automationId">The automation identifier.</param>
        /// <param name="tabCount">The tab count.</param>
        /// <param name="errOut">The error out.</param>
        /// <param name="myAction">My action.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool PerformTabSelect(string automationId, int tabCount, out string errOut, 
            AppAction myAction = AppAction.FindElementById)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                if (tabCount == 0) tabCount = 1;
                WebElement element = GetAction(automationId, myAction);

                OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(DesktopSession);
                action.MoveToElement(element);
                action.Perform();
                action.Click();
                action.Perform();

                if (tabCount > 1)
                {
                    for (int i = 1; i < tabCount + 1; i++)
                    {
                        element.SendKeys(Keys.Tab);
                        Thread.Sleep(200);
                    }
                }
                else
                {
                    element.SendKeys(Keys.Tab);
                    Thread.Sleep(200);
                }
                element.SendKeys(Keys.Enter);
                Thread.Sleep(200);


                bAns = true;
            }
            catch (Exception e)
            {
                errOut = $"{ErrorMessage("PerformAction", e)}";
                AddError(errOut);
                ScreenShotIt();
            }
            return bAns;
        }
        
        /// <summary>
        /// Performs the action to execute on the application
        /// </summary>
        /// <param name="automationId">The automation identifier.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        /// <param name="errOut">The error out.</param>
        /// <param name="myAction">My action.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool PerformAction(string automationId, string value, MyAction action, out string errOut, 
            AppAction myAction = AppAction.FindElementById)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                var actionMenu = GetAction(automationId, myAction);
                //var actionMenu = DesktopSession.FindElement(MobileBy.Name(automationId));

                if (action.Equals(MyAction.Nothing))
                {
                    bAns = actionMenu.Displayed;
                }
                else
                {
                    var runAction = actionMenu;
                    //OpenQA.Selenium.Interactions.Actions runAction = new OpenQA.Selenium.Interactions.Actions(DesktopSession);
                    //runAction.MoveToElement(actionMenu);
                    switch (action)
                    {
                        case MyAction.Click:
                            runAction.Click();
                            break;
                        case MyAction.SendKeys:
                            runAction.SendKeys(value);
                            break;
                        case MyAction.ClearAndSendKeys:
                            actionMenu.Clear();
                            runAction.SendKeys(value);
                            break;
                        //case MyAction.DoubleClick:
                        //    runAction.DoubleClick();
                        //    break;
                        //case MyAction.KeyDown:
                        //    runAction.KeyDown(value);
                        //    break;
                        //case MyAction.KeyUp:
                        //    runAction.KeyUp(value);
                        //    break;
                        case MyAction.Sleep:
                            Thread.Sleep(Convert.ToInt32(value));
                            break;
                    }

                    //runAction.Perform();
                    bAns = true;
                }

            }
            catch (Exception e)
            {
                errOut = $"ACTION: {action} - {ErrorMessage("PerformAction", e)}";
                AddError(errOut);
                ScreenShotIt();
            }
            return bAns;
        }

        /// <summary>
        /// Performs the action.
        /// </summary>
        /// <param name="automationId">The automation identifier.</param>
        /// <param name="errOut">The error out.</param>
        /// <param name="myAction">My action.</param>
        /// <returns>System.String.</returns>
        public string PerformAction(string automationId, out string errOut, 
            AppAction myAction = AppAction.FindElementById)
        {
            string sAns = "";
            errOut = "";
            try
            {
                var actionMenu = GetAction(automationId, myAction);
                sAns = actionMenu.Text;
            }
            catch (Exception e)
            {
                errOut = $"ACTION: Read Value - {ErrorMessage("PerformAction", e)}";
                AddError(errOut);
                ScreenShotIt();
            }
            return sAns;
        }
        /// <summary>
        /// Runs the batch commands.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        /// <exception cref="System.Exception">Error occured and the Driver is not active!</exception>
        /// <example>
        /// private List&lt;BatchCommandList&gt; GetCommands() <br/>
        /// { <br/>
        /// List&lt;BatchCommandList&gt; cmd = new List&lt;BatchCommandList&gt;(); <br/>
        /// cmd.Add(new BatchCommandList() <br/>
        /// { <br/>
        /// TestName = "Search Gun Collection Button", <br/>
        /// Actions = GeneralActions.MyAction.Click, <br/>
        /// CommandAction = GeneralActions.AppAction.FindElementByName, <br/>
        /// ElementName = "Search Gun Collection" <br/>
        /// }); <br/>
        /// cmd.Add(new BatchCommandList() <br/>
        /// { <br/>
        /// TestName = "Verify For Textbox exists", <br/>
        /// Actions = GeneralActions.MyAction.Nothing, <br/>
        /// CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId, <br/>
        /// ElementName = "txtLookFor" <br/>
        /// }); <br/>
        /// cmd.Add(new BatchCommandList() <br/>
        /// { <br/>
        /// TestName = "Look For Textbox", <br/>
        /// Actions = GeneralActions.MyAction.Click, <br/>
        /// CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId, <br/>
        /// ElementName = "txtLookFor" <br/>
        /// }); <br/>
        /// cmd.Add(new BatchCommandList() <br/>
        /// { <br/>
        /// TestName = "Search for word Glock", <br/>
        /// Actions = GeneralActions.MyAction.SendKeys, <br/>
        /// CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId, <br/>
        /// ElementName = "txtLookFor", <br/>
        /// SendKeys = "Glock" <br/>
        /// }); <br/>
        /// cmd.Add(new BatchCommandList() <br/>
        /// { <br/>
        /// TestName = "Verify Control Combo box Look in", <br/>
        /// Actions = GeneralActions.MyAction.Nothing, <br/>
        /// CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId, <br/>
        /// ElementName = "cmbLookIn" <br/>
        /// }); <br/>
        /// cmd.Add(new BatchCommandList() <br/>
        /// { <br/>
        /// TestName = "Get Control Combo Value box Look in", <br/>
        /// Actions = GeneralActions.MyAction.ReadValue, <br/>
        /// CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId, <br/>
        /// ElementName = "cmbLookIn" <br/>
        /// }); <br/>
        /// //cmd.Add(new BatchCommandList() <br/>
        /// //{ <br/>
        /// //    TestName = "Click on Control Combo box Look in", <br/>
        /// //    Actions = GeneralActions.MyAction.Click, <br/>
        /// //    CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId, <br/>
        /// //    ElementName = "cmbLookIn" <br/>
        /// //}); <br/>
        /// //cmd.Add(new BatchCommandList() <br/>
        /// //{ <br/>
        /// //    TestName = "Click on Control Combo box Look in", <br/>
        /// //    Actions = GeneralActions.MyAction.Click, <br/>
        /// //    CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId, <br/>
        /// //    ElementName = "Model Name" <br/>
        /// //}); <br/>
        /// cmd.Add(new BatchCommandList() <br/>
        /// { <br/>
        /// TestName = "Start Search", <br/>
        /// Actions = GeneralActions.MyAction.Click, <br/>
        /// CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId, <br/>
        /// ElementName = "btnSearch" <br/>
        /// }); <br/>
        /// return cmd; <br/>
        /// } <br/>
        /// /// &lt;summary&gt; <br/>
        /// /// Defines the test method BatchCommandTest. <br/>
        /// /// &lt;/summary&gt; <br/>
        /// /// &lt;exception cref="System.Exception"&gt;&lt;/exception&gt; <br/>
        /// [TestMethod] <br/>
        /// public void BatchCommandTest() <br/>
        /// { <br/>
        /// try <br/>
        /// { <br/>
        /// List&lt;BatchCommandList&gt; value = _ga.RunBatchCommands(GetCommands(), out _errOut); <br/>
        /// if (_errOut.Length &gt; 0) throw new Exception(_errOut); <br/>
        ///  <br/>
        /// int testNumber = 1; <br/>
        /// foreach (BatchCommandList v in value) <br/>
        /// { <br/>
        /// string passfailed = v.PassedFailed ? "PASSED" : "FAILED"; <br/>
        /// TestContext.WriteLine($"{testNumber}.) {passfailed} - {v.TestName}"); <br/>
        /// TestContext.WriteLine(v.ReturnedValue); <br/>
        /// testNumber++; <br/>
        /// } <br/>
        /// Assert.IsTrue(_ga.AllTestsPassed(value)); <br/>
        /// } <br/>
        /// catch (Exception e) <br/>
        /// { <br/>
        /// TestContext.WriteLine($"ERROR: {e.Message}"); <br/>
        /// Assert.Fail(); <br/>
        /// } <br/>
        ///  <br/>
        /// } <br/>
        /// </example>
        public List<BatchCommandList> RunBatchCommands(List<BatchCommandList> cmd, out string errOut)
        {
            List<BatchCommandList> theReturned = new List<BatchCommandList>();
            errOut = @"";
            try
            {
                int testNumber = 1;
                foreach (BatchCommandList c in cmd)
                {
                    bool didpass = false;
                    string result;
                    string sendkeys = @"";
                    string foundValue = "";
                    try
                    {
                        if (DesktopSession == null) throw new Exception("Error occured and the Driver is not active!");
                        if (c.SendKeys != null) sendkeys = c.SendKeys;
                        string msg = $"{c.Actions} on {c.ElementName} using {c.CommandAction}";
                        if (sendkeys.Length > 0) msg = $"{c.Actions} {sendkeys} to {c.ElementName} using {c.CommandAction}";
                        if (c.Actions.Equals(MyAction.Nothing)) msg = msg.Replace("Nothing", "Verify Exists");

                        switch (c.Actions)
                        {
                            case MyAction.ReadValue:
                                foundValue = PerformAction(c.ElementName, out errOut, c.CommandAction);
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");
                                msg += $"{msg}. Found value {foundValue}";
                                if (!didpass) didpass = true;
                                break;
                            case MyAction.ReadAndCompare:
                                foundValue = PerformAction(c.ElementName, out errOut, c.CommandAction);
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
                                didpass = PerformTabSelect(c.ElementName, c.TabCount, out errOut, c.CommandAction);
                                string actionMsg = didpass ? "Was" : "Was Not";
                                msg += $"{actionMsg} to click on {c.ElementName} and tab over {c.TabCount} to select element at tab.";
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");
                                break;
                            case MyAction.GetValueFromPreviousTestAndCompareByTestName:
                                string ExpectedReturnedValue = GetStringFromStep(theReturned, c.TestNameLookUp, out errOut);
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");
                                foundValue = PerformAction(c.ElementName, out errOut, c.CommandAction);
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
                                string ExpectedReturnedValueNum = GetStringFromStep(theReturned, c.TestNumber, out errOut);
                                if (errOut.Length > 0)
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");
                                foundValue = PerformAction(c.ElementName, out errOut, c.CommandAction);
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
                                if (!PerformAction(c.ElementName, sendkeys, c.Actions, out errOut, c.CommandAction))
                                    throw new Exception($"Was Not able to {msg}{Environment.NewLine}{errOut}");
                                if (!didpass) didpass = true;
                                break;
                        }
                        result = $"Was able to {msg}{Environment.NewLine}";

                    }
                    catch (Exception e)
                    {
                        didpass = false;
                        if (ScreenShotLocation.Count > 0)
                        {
                            result = $"{e.Message}{Environment.NewLine}";
                            foreach (string s in ScreenShotLocation)
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


        #endregion
    }
}
