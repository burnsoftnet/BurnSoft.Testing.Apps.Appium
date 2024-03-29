﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using BurnSoft.Testing.Apps.Appium.Types;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantCast
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable UnusedVariable

// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global

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
        /// The windows application driver URL
        /// </summary>
        private string _windowsApplicationDriverUrl;
        /// <summary>
        /// The win application driver path
        /// </summary>
        private string _winAppDriverPath;
        /// <summary>
        /// The device name
        /// </summary>
        private string _deviceName = "";
        /// <summary>
        /// The wait for application launch
        /// </summary>
        private int _waitForAppLaunch;
        /// <summary>
        /// The win application driver process
        /// </summary>
        private Process _winAppDriverProcess;
        /// <summary>
        /// The sleep interval
        /// </summary>
        private int _sleepInterval;
        /// <summary>
        /// Gets the application session.
        /// </summary>
        /// <value>The application session.</value>
        public WindowsDriver<WindowsElement> AppSession { get; private set; }
        /// <summary>
        /// Gets the desktop session.
        /// </summary>
        /// <value>The desktop session.</value>
        public WindowsDriver<WindowsElement> DesktopSession { get; private set; }
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
        /// Gets or sets the windows application driver URL. If not set, it will default to http://127.0.0.1:4723
        /// </summary>
        /// <value>The windows application driver URL.</value>
        public string WindowsApplicationDriverUrl
        {
            get
            {
                if (_windowsApplicationDriverUrl == null) return "http://127.0.0.1:4723";
                if (_windowsApplicationDriverUrl.Length == 0)
                {
                    return "http://127.0.0.1:4723";
                }
                else
                {
                    return _windowsApplicationDriverUrl;
                }
            }
            set => _windowsApplicationDriverUrl = value;
        }
        /// <summary>
        /// Gets or sets the win application driver path. If not set, it will
        /// default to C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe
        /// </summary>
        /// <value>The win application driver path.</value>
        public string WinAppDriverPath
        {
            get {
                if (_winAppDriverPath == null) return @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
                if (_winAppDriverPath.Length == 0)
                {
                    return @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
                }
                else
                {
                    return _winAppDriverPath;
                }
            }
            set => _winAppDriverPath = value;
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
        /// Gets or sets a value indicating whether to run the test in admin mode.
        /// </summary>
        /// <value><c>true</c> if [run in admin mode]; otherwise, <c>false</c>.</value>
        public bool RunInAdminMode { get; set; }

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
            if (ErrorLists == null) ErrorLists = new List<string>();
            ErrorLists.Add(error);
        }
        /// <summary>
        /// Starts the win application driver.
        /// </summary>
        private void StartWinAppDriver()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(WinAppDriverPath);
                psi.UseShellExecute = true;
                if (RunInAdminMode) psi.Verb = "runas"; // run as administrator
                _winAppDriverProcess = Process.Start(psi);
            }
            catch (Exception e)
            {
                AddError(ErrorMessage("StartWinAppDriver", e));
            }
        }
        /// <summary>
        /// Stops the winapp driver.
        /// </summary>
        private void StopWinappDriver()
        {
            // Stop the WinAppDriverProcess
            if (_winAppDriverProcess != null)
            {
                foreach (var process in Process.GetProcessesByName("WinAppDriver"))
                {
                    process.Kill();
                }
            }
        }
        #endregion
        #region "Public Initalization and cleanup function"
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralActions"/> class.
        /// </summary>
        /// <param name="desktopSession">The desktop session.</param>
        public GeneralActions(WindowsDriver<WindowsElement> desktopSession)
        {
            DesktopSession = desktopSession;
            GeneralActionsInit();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralActions"/> class.
        /// </summary>
        public GeneralActions()
        {
            GeneralActionsInit();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralActions"/> class.
        /// </summary>
        /// <param name="runInAdminMode">if set to <c>true</c> [run in admin mode].</param>
        public GeneralActions(bool runInAdminMode)
        {
            GeneralActionsInit(runInAdminMode);
        }
        /// <summary>
        /// Generals the actions initialize.
        /// </summary>
        /// <param name="runInAdminMode">if set to <c>true</c> [run in admin mode].</param>
        private void GeneralActionsInit(bool runInAdminMode = false) 
        {
            ErrorLists = new List<string>();
            ScreenShotLocation = new List<string>();
            RunInAdminMode = runInAdminMode;
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (AppSession != null)
            {
                AppSession.CloseApp();
                //AppSession.Close();
                //AppSession.Quit();
            }
            // Close the desktopSession
            if (DesktopSession != null)
            {
                DesktopSession.CloseApp();
                //DesktopSession.Close();
                //DesktopSession.Quit();
            }

            StopWinappDriver();
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <exception cref="System.Exception">AppSession is null, check your settings</exception>
        /// <exception cref="System.Exception">AppSession.SessionId is null, check your application path</exception>
        /// <exception cref="System.Exception">DesktopSession is null, please check your settings</exception>
        public void Initialize()
        {
            try
            {
                _deviceName = Dns.GetHostName();
                StartWinAppDriver();
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", ApplicationPath);
                appiumOptions.AddAdditionalCapability("deviceName", _deviceName);
                appiumOptions.AddAdditionalCapability("ms:waitForAppLaunch", WaitForAppLaunch);
                AppSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);

                if (AppSession == null) throw new Exception("AppSession is null, check your settings");
                if (AppSession.SessionId == null) throw new Exception("AppSession.SessionId is null, check your application path");

                AppSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
                AppiumOptions optionsDesktop = new AppiumOptions();
                optionsDesktop.AddAdditionalCapability("app", "Root");
                optionsDesktop.AddAdditionalCapability("deviceName", _deviceName);
                DesktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), optionsDesktop);

                if (DesktopSession == null) throw new Exception("DesktopSession is null, please check your settings");
                InitPassed = true;
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
        private WindowsElement GetAction(string automationId, AppAction myAction)
        {
            switch (myAction)
            {
                case AppAction.FindElementByAccessibilityId:
                    return DesktopSession.FindElementByAccessibilityId(automationId);
                case AppAction.FindElementByName:
                    return DesktopSession.FindElementByName(automationId);
                case AppAction.FindElementByClassName:
                    return DesktopSession.FindElementByClassName(automationId);
                case AppAction.FindElementByCssSelector:
                    return DesktopSession.FindElementByCssSelector(automationId);
                case AppAction.FindElementById:
                    return DesktopSession.FindElementById(automationId);
                case AppAction.FindElementByImage:
                    return DesktopSession.FindElementByImage(automationId);
                case AppAction.FindElementByLinkText:
                    return DesktopSession.FindElementByLinkText(automationId);
                case AppAction.FindElementByPartialLinkText:
                    return DesktopSession.FindElementByPartialLinkText(automationId);
                case AppAction.FindElementByTagName:
                    return DesktopSession.FindElementByTagName(automationId);
                case AppAction.FindElementByWindowsUiAutomation:
                    return DesktopSession.FindElementByWindowsUIAutomation(automationId);
                default:
                    return DesktopSession.FindElementByAccessibilityId(automationId);
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
                    screenShot.SaveAsFile(savePath, ScreenshotImageFormat.Png);
                    ScreenShotLocation.Add(savePath);
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
        #region "Reporting"
        /// <summary>
        /// Generates the results from the Batch Command List to display the step number, testname, any returnedvalue results and
        /// if it failed, to return the element name that it failed at.
        /// </summary>
        /// <param name="cmdResults">The command results.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns>System.String.</returns>
        public string GenerateResults(List<BatchCommandList> cmdResults, out string errOut)
        {
            string sAns = "";
            errOut = "";
            try
            {
                int stepNumber = 1;
                foreach (BatchCommandList c in cmdResults)
                {
                    if (c.TestName != null)
                    {
                        if (c.TestName?.Length > 0)
                        {
                            string passFailed = c.PassedFailed ? "PASSED!" : "FAILED!";
                            sAns += $"{Environment.NewLine}{stepNumber}.)  {passFailed} {c.TestName}";
                            if (c.ReturnedValue.Length > 0) sAns += $"  {c.ReturnedValue}";
                            if (!c.PassedFailed) sAns += $"{Environment.NewLine} Failed at line: {c.ElementName}";
                            stepNumber++;
                        }
                    }
                }

                if (sAns.Length > 0) sAns += $"{Environment.NewLine}";
            }
            catch (Exception e)
            {
                errOut = e.Message;
            }
            return sAns;
        }

        /// <summary>
        /// Works through the results of the Batch Command list and looks to see if any of the tests where marked as failed,
        /// if some show up as failed then it will return false, else everything passed and it is true.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public bool AllTestsPassed(List<BatchCommandList> results)
        {
            return results.All(r => r.PassedFailed);
        }

        #endregion
        /// <summary>
        /// Get Elemtns from item test
        /// </summary>
        /// <remarks>Might be able to delete later is not needed</remarks>
        /// <param name="automationId"></param>
        /// <param name="errOut"></param>
        /// <param name="myAction"></param>
        public void GetElements(string automationId, out string errOut, AppAction myAction = AppAction.FindElementByAccessibilityId)
        {
            errOut = "";
            try
            {
               
                IEnumerable<AppiumWebElement> elementsOne = DesktopSession.FindElementsByAccessibilityId(automationId).ToList();
                var test = DesktopSession.FindElements(By.Id(automationId));
                Thread.Sleep(200);
            }
            catch (Exception e)
            {
                errOut = ErrorMessage("GetElements", e);
            }
        }
        /// <summary>
        /// Performs the tab select, it will start at the automation id that you selected, then you have the option to tab over x many times
        /// to another item then it will send a space key press to activate the final element.
        /// </summary>
        /// <param name="automationId">The automation identifier.</param>
        /// <param name="tabCount">The tab count.</param>
        /// <param name="errOut">The error out.</param>
        /// <param name="myAction">My action.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool PerformTabSelect(string automationId, int tabCount, out string errOut, AppAction myAction = AppAction.FindElementByAccessibilityId)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                if (tabCount == 0) tabCount = 1;
                WindowsElement element = GetAction(automationId, myAction);

                Actions action = new Actions(DesktopSession);
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
        public bool PerformAction(string automationId, string value, MyAction action, out string errOut, AppAction myAction = AppAction.FindElementByAccessibilityId)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                WindowsElement actionMenu = GetAction(automationId, myAction);

                if (action.Equals(MyAction.Nothing))
                {
                    bAns = actionMenu.Displayed;
                }
                else
                {
                    Actions runAction = new Actions(DesktopSession);
                    runAction.MoveToElement(actionMenu);
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
                        case MyAction.DoubleClick:
                            runAction.DoubleClick();
                            break;
                        case MyAction.KeyDown:
                            runAction.KeyDown(value);
                            break;
                        case MyAction.KeyUp:
                            runAction.KeyUp(value);
                            break;
                        case MyAction.Sleep:
                            Thread.Sleep(Convert.ToInt32(value));
                            break;
                    }

                    runAction.Perform();
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
        public string PerformAction(string automationId, out string errOut, AppAction myAction = AppAction.FindElementByAccessibilityId)
        {
            string sAns = "";
            errOut = "";
            try
            {
                WindowsElement actionMenu = GetAction(automationId, myAction);
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
                    theReturned.Add(new BatchCommandList() { SleepInterval = c.SleepInterval, 
                        Actions = c.Actions, ElementName = c.ElementName, SendKeys = c.SendKeys, 
                        PassedFailed = didpass,ReturnedValue = result, TestName = c.TestName, ReturnedFoundValue = foundValue, TestNumber = testNumber
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
