using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;

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
        /// The initialize passed
        /// </summary>
        private bool _initPassed;
        /// <summary>
        /// The win application driver process
        /// </summary>
        private Process _winAppDriverProcess;
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
                psi.Verb = "runas"; // run as administrator
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
            ErrorLists = new List<string>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralActions"/> class.
        /// </summary>
        public GeneralActions()
        {
            ErrorLists = new List<string>();
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (AppSession != null)
            {
                AppSession.Close();
                AppSession.Quit();
            }
            // Close the desktopSession
            if (DesktopSession != null)
            {
                DesktopSession.Close();
                DesktopSession.Quit();
            }

            StopWinappDriver();
        }
        /// <summary>
        /// Inititalizes this instance.
        /// </summary>
        /// <exception cref="System.Exception">AppSession is null, check your settings</exception>
        /// <exception cref="System.Exception">AppSession.SessionId is null, check your application path</exception>
        /// <exception cref="System.Exception">DesktopSession is null, please check your settings</exception>
        public void Inititalize()
        {
            try
            {
                _deviceName = Dns.GetHostName();
                StartWinAppDriver();
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", ApplicationPath);
                appiumOptions.AddAdditionalCapability("deviceName", _deviceName);
                appiumOptions.AddAdditionalCapability("ms:waitForAppLaunch", _waitForAppLaunch);
                this.AppSession = new WindowsDriver<WindowsElement>(new Uri(_windowsApplicationDriverUrl), appiumOptions);

                if (AppSession == null) throw new Exception("AppSession is null, check your settings");
                if (AppSession.SessionId == null) throw new Exception("AppSession.SessionId is null, check your application path");

                AppSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
                AppiumOptions optionsDesktop = new AppiumOptions();
                optionsDesktop.AddAdditionalCapability("app", "Root");
                optionsDesktop.AddAdditionalCapability("deviceName", _deviceName);
                DesktopSession = new WindowsDriver<WindowsElement>(new Uri(_windowsApplicationDriverUrl), optionsDesktop);

                if (DesktopSession == null) throw new Exception("DesktopSession is null, please check your settings");
                _initPassed = true;
            }
            catch (Exception e)
            {
                _initPassed = false;
                AddError(ErrorMessage("Inititalize", e));
            }
        }
        #endregion

        #region "Appinum Actions"        
        /// <summary>
        /// Enum AppAction
        /// </summary>
        public enum AppAction
        {
            FindElementByAccessibilityId,
            FindElementByName,
            FindElementByWindowsUiAutomation,
            FindElementByClassName,
            FindElementByCssSelector,
            FindElementById,
            FindElementByImage,
            FindElementByLinkText,
            FindElementByPartialLinkText,
            FindElementByTagName
        }
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
        /// Doubles the c lick element.
        /// </summary>
        /// <param name="automationId">The automation identifier.</param>
        /// <param name="errOut">The error out.</param>
        /// <param name="myAction">My action.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool DoubleCLickElement( string automationId, out string errOut, AppAction myAction = AppAction.FindElementByAccessibilityId)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                WindowsElement actionMenu = GetAction(automationId, myAction);
                Actions action = new Actions(DesktopSession);
                action.MoveToElement(actionMenu);
                action.DoubleClick();
                action.Perform();
                action.DoubleClick();
                action.Perform();
                bAns = true;
            }
            catch (Exception e)
            {
                errOut = $"ERROR: {e.Message}";
            }
            return bAns;
        }
        /// <summary>
        /// Clicks the on element.
        /// </summary>
        /// <param name="automationId">The automation identifier.</param>
        /// <param name="errOut">The error out.</param>
        /// <param name="myAction">My action.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ClickOnElement(string automationId, out string errOut, AppAction myAction = AppAction.FindElementByAccessibilityId)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                WindowsElement actionMenu = GetAction(automationId, myAction);
                Actions action = new Actions(DesktopSession);
                action.MoveToElement(actionMenu);
                action.Click();
                action.Perform();
                bAns = true;
            }
            catch (Exception e)
            {
                errOut = $"ERROR: {e.Message}";
            }
            return bAns;
        }
        /// <summary>
        /// Sends the text to element.
        /// </summary>
        /// <param name="automationId">The automation identifier.</param>
        /// <param name="value">The value.</param>
        /// <param name="errOut">The error out.</param>
        /// <param name="myAction">My action.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool SendTextToElement(string automationId,string value, out string errOut, AppAction myAction = AppAction.FindElementByAccessibilityId)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                WindowsElement actionMenu = GetAction(automationId, myAction);
                Actions action = new Actions(DesktopSession);
                action.MoveToElement(actionMenu);
                action.SendKeys(value);
                action.Perform();
                bAns = true;
            }
            catch (Exception e)
            {
                errOut = $"ERROR: {e.Message}";
            }
            return bAns;
        }

        #endregion
    }
}
