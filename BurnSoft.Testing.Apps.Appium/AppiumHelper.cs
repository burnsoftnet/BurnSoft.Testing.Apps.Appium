using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BurnSoft.Testing.Apps.Appium
{
    /// <summary>
    /// Class AppiumHelper class that contains functions to help start up and 
    /// communicate with the appium 3.x.x server
    /// </summary>
    public class AppiumHelper
    {
        #region "Exception Error Handling"        
        /// <summary>
        /// The class location
        /// </summary>
        private static string _classLocation = "BurnSoft.Testing.Apps.Appium.AppiumHelper";

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
        #region "Event Handler"        
        /// <summary>
        /// Event handler for when exception errors occur in the functions
        /// </summary>
        public event EventHandler<string> Errors;
        /// <summary>
        /// Occurs when [junk errors] are thrown, don't care but might be useful.
        /// </summary>
        public event EventHandler<string> JunkErrors;
        /// <summary>
        /// Sends the error.
        /// </summary>
        /// <param name="value">The value.</param>
        protected virtual void SendError(string value)
        {
            Errors?.Invoke(this, value);
        }
        /// <summary>
        /// Sends the junk error.
        /// </summary>
        /// <param name="value">The value.</param>
        protected virtual void SendJunkError(string value)
        {
            JunkErrors?.Invoke(this, value);
        }
        #endregion
        #region "Private Variables and constants"        
        /// <summary>
        /// The appium application direct
        /// </summary>
        private string _appiumApp;
        /// <summary>
        /// The buggerme Debugging options
        /// </summary>
        private bool _buggerme;
        /// <summary>
        /// The private internal options for the application under test
        /// </summary>
        private AppiumOptions _options;       
        /// <summary>
        /// The appium server that uses the AppiumServiceBuilder To set 
        /// the server parameters and start the process
        /// </summary>
        private AppiumLocalService appiumServer;
        /// <summary>
        /// The node executable file name and path
        /// </summary>
        private FileInfo _nodeExecutable;
        /// <summary>
        /// The appium main js file name and path
        /// </summary>
        private FileInfo _appiumMainJs;
        /// <summary>
        /// The implicit timeout sec. Change this to a more reasonable value
        /// </summary>
        private static TimeSpan IMPLICIT_TIMEOUT_SEC = TimeSpan.FromSeconds(10);
        #endregion
        #region "Public Variables"        
        /// <summary>
        /// The driver once the appium server is up and running and the 
        /// StartDriverConnection has been called and set
        /// </summary>
        public WindowsDriver driver;
        #endregion

        #region "AppiumHelper Init"        
        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumHelper"/> class.
        /// </summary>
        /// <param name="appiumApp">The appium application.</param>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        public AppiumHelper(string appiumApp, bool debugMode = false)
        {
            _appiumApp = appiumApp;
            _buggerme = debugMode;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumHelper"/> class.
        /// </summary>
        /// <param name="nodeExecutable">The node executable.</param>
        /// <param name="appiumMainJs">The appium main js.</param>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        public AppiumHelper(string nodeExecutable, string appiumMainJs,  
            bool debugMode = false)
        {
            _nodeExecutable = new FileInfo(nodeExecutable);
            _appiumMainJs = new FileInfo(appiumMainJs);
            _buggerme = debugMode;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumHelper"/> class.
        /// </summary>
        /// <param name="nodeExecutable">The node executable.</param>
        /// <param name="appiumMainJs">The appium main js.</param>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        public AppiumHelper(FileInfo nodeExecutable, FileInfo appiumMainJs,
            bool debugMode = false)
        {
            _nodeExecutable = nodeExecutable;
            _appiumMainJs = appiumMainJs;
            _buggerme = debugMode;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumHelper"/> class.
        /// </summary>
        /// <param name="appiumApp">The appium application.</param>
        /// <param name="testApp">The test application.</param>
        /// <param name="debugMode">if set to <c>true</c> [debug mode].</param>
        /// <param name="testAppParameters">The test application parameters.</param>
        /// <param name="fullReset">if set to <c>true</c> [full reset].</param>
        public AppiumHelper(string appiumApp, string testApp, bool debugMode = false, 
            string testAppParameters = "", bool fullReset = true)
        {
            _appiumApp = appiumApp;
            _buggerme = debugMode;
            _options = SetDesiredCapabilities(testApp, testAppParameters, fullReset);
        }
        #endregion
        #region "Startup, Close and Set Desired Capabilities"        
        /// <summary>
        /// Starts the appium process using the AppiumServiceBuilder class to help determin if the process is already running
        /// if not then spin an instance to start it up
        /// </summary>
        /// <param name="ip">The ip of the appium server.</param>
        /// <param name="port">The port of the appium server.</param>
        /// <param name="startup_wait">The startup wait time for the process to come up.</param>
        /// <returns><c>true</c> if true, process is running or was already running, <c>false</c> if error occured.</returns>
        public bool StartAppium(string ip = "127.0.0.1", int port = 4723, int startup_wait = 2)
        {
            bool bAns = false;
            try
            {
                var nodeExecutable = _nodeExecutable;
                var appiumMainJs = _appiumMainJs;
                if (port == 0)
                {
                    appiumServer = new AppiumServiceBuilder()
                    .WithIPAddress(ip)
                    .UsingAnyFreePort() // Use any available port
                    .UsingDriverExecutable(nodeExecutable) // Specify Node.js path
                    .WithAppiumJS(appiumMainJs)
                    .WithStartUpTimeOut(TimeSpan.FromMinutes(2))
                    .Build();
                } else
                {
                    appiumServer = new AppiumServiceBuilder()
                    .WithIPAddress(ip)
                    .UsingPort(port) // Use any available port
                    .UsingDriverExecutable(nodeExecutable) // Specify Node.js path
                    .WithAppiumJS(appiumMainJs)
                    .WithStartUpTimeOut(TimeSpan.FromMinutes(startup_wait))
                    .Build();
                }

                    appiumServer.Start();
                bAns = true;
            }
            catch (Exception ex)
            {
                SendError(ErrorMessage("StartAppium", ex));
            }
            return bAns;
        }
        /// <summary>
        /// Stops the appium server.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool StopAppiumServer()
        {
            bool bAns = false;
            try
            {
                if (appiumServer != null && appiumServer.IsRunning)
                {
                    appiumServer.Dispose();
                    driver = null;
                    Console.WriteLine("Appium server stopped.");
                }
                bAns = true;
            }
            catch (Exception ex)
            {
                SendError(ErrorMessage("StopAppiumServer", ex));
            }
            return bAns;
        }
        #endregion
        #region "Application Under Test Handling COnfig and Startup"
        /// <summary>
        /// "Capabilities" is the name given to the set of parameters used to start an Appium session. The information 
        /// in the set is used to describe what sort of "capabilities" you want your session to have, for example, a 
        /// certain mobile operating system or a certain version of a device. When you start your Appium session, 
        /// your Appium client will include the set of capabilities you've defined as an object in the 
        /// JSON-formatted body of the request. Capabilities are represented as key-value pairs, with values 
        /// allowed to be any valid JSON type, including other objects. Appium will then examine the capabilities 
        /// and make sure that it can satisfy them before proceeding to start the session and return an ID 
        /// representing the session to your client library.
        /// See Guide at  https://appium.io/docs/en/3.1/guides/caps/
        /// </summary>
        /// <param name="testApp">The Application Under Test</param>
        /// <param name="testAppParameters">The test application parameters.</param>
        /// <param name="fullReset">if set to <c>true</c> [full reset].</param>
        /// <returns>AppiumOptions.</returns>
        public AppiumOptions SetDesiredCapabilities(string testApp, string testAppParameters = "", bool fullReset = true)
        {
            AppiumOptions options = new AppiumOptions();
            try
            {
                string deivceName = Environment.MachineName;
                string platform = "Windows";
                string automationName = platform;

                options.App = testApp;
                options.DeviceName = deivceName;
                options.AutomationName = automationName;
                options.PlatformName = platform;
                //options.AddAdditionalOption("app", testApp);
                //options.AddAdditionalOption("deviceName", deivceName);
                //options.AddAdditionalOption("platformName", platform);
                ////options.AddAdditionalOption("automationName", automationName);

                //options.AddAdditionalAppiumOption("platform", platform);
                //options.AddAdditionalAppiumOption("AutomationName", automationName);
                //options.AddAdditionalAppiumOption("app", testApp);
                //options.AddAdditionalAppiumOption("deviceName", deivceName); 
                //options.AddAdditionalAppiumOption("fullReset", fullReset);

                if (testAppParameters.Length > 0)
                {
                    options.AddAdditionalAppiumOption("appium:appArguments", testAppParameters);
                }
            }
            catch (Exception ex)
            {
                SendError(ErrorMessage("SetDesiredCapabilities", ex));
            }
            return options;
        }
        /// <summary>
        /// Starts the driver connection which will use the local application under test options
        /// that was set when you passed the aut to the init function in this class by startup
        /// </summary>
        /// <param name="ip">The appium ip.</param>
        /// <param name="port">The appium port.</param>
        /// <param name="wait">The wait.</param>
        /// <param name="httpProtocol">The HTTP protocol.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool StartDriverConnection(string ip = "127.0.0.1",
           int port = 4723, int wait = 2, string httpProtocol = "http")
        {
            return StartDriverConnection(_options, ip, port, wait, httpProtocol);
        }
        /// <summary>
        /// Starts the driver connection. and the application under test when the options was created
        /// externaly and passed to this function to startup and test the application under test
        /// </summary>
        /// <param name="options">The appium aut options.</param>
        /// <param name="ip">The appium ip.</param>
        /// <param name="port">The appium port.</param>
        /// <param name="wait">The wait.</param>
        /// <param name="httpProtocol">The HTTP protocol.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.Exception">Error Starting Appium</exception>
        /// <exception cref="System.Exception">AppSession is null, check your settings</exception>
        /// <exception cref="System.Exception">AppSession.SessionId is null, check your application path</exception>
        public bool StartDriverConnection(AppiumOptions options, string ip = "127.0.0.1",
            int port = 4723, int wait = 2, string httpProtocol = "http")
        {
            bool bAns = false;
            try
            {
                if (!StartAppium(ip, port, startup_wait: wait)) throw new Exception("Error Starting Appium");
                string url = $"{httpProtocol}://{ip}:{port}";
                WindowsDriver AppSession = new WindowsDriver(new Uri(url), options);
                AppSession.Manage().Timeouts().ImplicitWait = IMPLICIT_TIMEOUT_SEC;
                if (AppSession == null) throw new Exception("AppSession is null, check your settings");
                if (AppSession.SessionId == null) throw new Exception("AppSession.SessionId is null, check your application path");

                driver = AppSession;
                bAns = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                SendError(ErrorMessage("StartDriverConnection", ex));
            }
            return bAns;

        }
        #endregion
    }
}
