using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BurnSoft.Testing.Apps.Appium
{
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
        public event EventHandler<string> Errors;
        public event EventHandler<string> JunkErrors;
        protected virtual void SendError(string value)
        {
            Errors?.Invoke(this, value);
        }
        protected virtual void SendJunkError(string value)
        {
            JunkErrors?.Invoke(this, value);
        }
        #endregion
        private string _appiumApp;
        private bool _buggerme;
        private AppiumOptions _options;
        public AppiumLocalService AppiumDriver;
        private AppiumLocalService appiumServer;
        private FileInfo _nodeExecutable;
        private FileInfo _appiumMainJs;
        public WindowsDriver driver;

        private static TimeSpan INIT_TIMEOUT_SEC = TimeSpan.FromSeconds(180); /* Change this to a more reasonable value */
        private static TimeSpan IMPLICIT_TIMEOUT_SEC = TimeSpan.FromSeconds(10); /* Change this to a more reasonable value */

        #region "AppiumHelper Init"
        public AppiumHelper(string appiumApp, bool debugMode = false)
        {
            _appiumApp = appiumApp;
            _buggerme = debugMode;
        }

        public AppiumHelper(string nodeExecutable, string appiumMainJs,  
            bool debugMode = false)
        {
            _nodeExecutable = new FileInfo(nodeExecutable);
            _appiumMainJs = new FileInfo(appiumMainJs);
            _buggerme = debugMode;
        }

        public AppiumHelper(FileInfo nodeExecutable, FileInfo appiumMainJs,
            bool debugMode = false)
        {
            _nodeExecutable = nodeExecutable;
            _appiumMainJs = appiumMainJs;
            _buggerme = debugMode;
        }

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

                options.AddAdditionalOption("app", testApp);
                options.AddAdditionalOption("deviceName", deivceName);
                options.AddAdditionalOption("platformName", "Windows");
                options.AddAdditionalOption("automationName", automationName);

                options.AddAdditionalAppiumOption("platform", "Windows");
                options.AddAdditionalAppiumOption("AutomationName", automationName);
                options.AddAdditionalAppiumOption("app", testApp);
                options.AddAdditionalAppiumOption("deviceName", deivceName); 
                options.AddAdditionalAppiumOption("fullReset", fullReset);

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

        public bool StopAppiumServer()
        {
            bool bAns = false;
            try
            {
                if (appiumServer != null && appiumServer.IsRunning)
                {
                    appiumServer.Dispose();
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

        public bool StartDriverConnection(AppiumOptions options, string ip = "127.0.0.1",
            int port = 4723, int wait = 2, string httpProtocol = "http")
        {
            bool bAns = false;
            //AppiumDriver<IWebElement> driver = null;
            try
            {
                if (!StartAppium(ip, port, startup_wait: wait)) throw new Exception("Error Starting Appium");
                string url = $"{httpProtocol}://{ip}:{port}";
                WindowsDriver AppSession = new WindowsDriver(new Uri(url), options);
                //AppiumDriver driver = appiumDriver;
                AppSession.Manage().Timeouts().ImplicitWait = IMPLICIT_TIMEOUT_SEC;
                if (AppSession == null) throw new Exception("AppSession is null, check your settings");
                if (AppSession.SessionId == null) throw new Exception("AppSession.SessionId is null, check your application path");

                driver = AppSession;
                // Build the service
                //var appiumLocalService = builder.Build();

                // Start the service
                //appiumLocalService.Start();
                bAns = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                SendError(ErrorMessage("StartDriverConnection", ex));
            }
            return bAns;

        }
    }
}
