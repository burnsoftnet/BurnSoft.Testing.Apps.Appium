using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium.Windows;

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
        private string _WinAppDriverPath;
        /// <summary>
        /// The device name
        /// </summary>
        private string DeviceName = "";
        /// <summary>
        /// The wait for application launch
        /// </summary>
        private int _WaitForAppLaunch;
        /// <summary>
        /// The initialize passed
        /// </summary>
        private bool InitPassed;
        /// <summary>
        /// The win application driver process
        /// </summary>
        private static Process _winAppDriverProcess;
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
                if (_WinAppDriverPath == null) return @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
                if (_WinAppDriverPath.Length == 0)
                {
                    return @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
                }
                else
                {
                    return _WinAppDriverPath;
                }
            }
            set => _WinAppDriverPath = value;
        }
        /// <summary>
        /// Gets or sets the wait for application launch.
        /// </summary>
        /// <value>The wait for application launch.</value>
        public int WaitForAppLaunch
        {
            get
            {
                if (_WaitForAppLaunch ==0)
                {
                    _WaitForAppLaunch = 5;
                }

                return _WaitForAppLaunch;
            }
            set => _WaitForAppLaunch = value;
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

    }
}
