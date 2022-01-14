﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium.Appium.Windows;
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global

namespace BurnSoft.Testing.Apps.Appium
{
    /// <summary>
    /// The General Actions Class is the main class that will found to common functions used to communicate with appium.  Just like I did
    /// in the selenium helper library, this will be the main class in case there are other special classes that need to be created to have it work with
    /// other OS's or application types, etc.
    /// </summary>
    public class GeneralActions
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

        public GeneralActions(WindowsDriver<WindowsElement> desktopSession)
        {
            DesktopSession = desktopSession;
        }

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
        //End Snippet

    }
}
