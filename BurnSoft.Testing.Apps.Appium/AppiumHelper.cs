using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private string _appiumApp;
        private bool _buggerme;
        private AppiumOptions _options;
        public AppiumLocalService AppiumDriver;
        private AppiumLocalService appiumServer;
        public AppiumHelper(string appiumApp, bool debugMode = false)
        {
            _appiumApp = appiumApp;
            _buggerme = debugMode;
            //AppiumDriver = AppiumLocalService.BuildDefaultService();
        }

        public AppiumHelper(string appiumApp, string testApp, bool debugMode = false, string testAppParameters = "", bool fullReset = true)
        {
            _appiumApp = appiumApp;
            _buggerme = debugMode;
            //AppiumDriver = AppiumLocalService.BuildDefaultService();
            _options = SetDesiredCapabilities(testApp, testAppParameters, fullReset);
        }

        public AppiumOptions SetDesiredCapabilities(string testApp, string testAppParameters = "", bool fullReset = true)
        {
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalAppiumOption("platform", "Windows");
            options.AddAdditionalAppiumOption("automationName", "Windows");
            options.AddAdditionalAppiumOption("appium:app", testApp);
            options.AddAdditionalAppiumOption("appium:deviceName", Environment.MachineName);
            options.AddAdditionalAppiumOption("appium:fullReset", "");
            if (testAppParameters.Length > 0)
            {
                options.AddAdditionalAppiumOption("appium:appArguments", testAppParameters);
            }
            return options;
        }

        public bool StartAppium(string ip = "127.0.0.1", int port = 4723)
        {
            bool bAns = false;
            try
            {
                var nodeExecutable = new FileInfo(@"C:\nvm4w\nodejs\node.exe");
                var appiumMainJs = new FileInfo(@"C:\Users\burnsoft\AppData\Roaming\npm\node_modules\appium\build\lib\main.js");

                //var appiumExe = new FileInfo(_appiumApp);
                appiumServer = new AppiumServiceBuilder()
                    .WithIPAddress(ip)
                    .UsingAnyFreePort() // Use any available port
                                        //.UsingDriverExecutable(appiumExe)
                                        // .UsingPort(4723)         // Or use a specific port
                    .UsingDriverExecutable(nodeExecutable) // Specify Node.js path
                    .WithAppiumJS(appiumMainJs)
                    .WithStartUpTimeOut(TimeSpan.FromMinutes(2))
                    .Build();
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

        public bool StartDriverConnection(AppiumOptions options, string ip = "127.0.0.1",
            int port = 4723, int wait = 10, string httpProtocol = "http")
        {
            bool bAns = false;
            try
            {
                if (!StartAppium(ip, port)) throw new Exception("Error Starting Appium");

                bAns = true;
            }
            catch (Exception ex)
            {
                SendError(ErrorMessage("StartAppium", ex));
            }
            return bAns;

        }
    }
}
