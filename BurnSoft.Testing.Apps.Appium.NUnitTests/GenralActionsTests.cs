using BurnSoft.Testing.Apps.Appium.helpers;
using BurnSoft.Testing.Apps.Appium.NUnitTests.helpers;
using BurnSoft.Testing.Apps.Appium.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    public class GenralActionsTests
    {
        /// <summary>
         /// Gets or sets the test context.
         /// </summary>
         /// <value>The test context.</value>
        public TestContext TestContext { get; set; }
        /// <summary>
        /// The error out
        /// </summary>
        private string _errOut;
        /// <summary>
        /// The ga
        /// </summary>
        private GeneralActions _ga;
        /// <summary>
        /// The automation identifier
        /// </summary>
        private string _automationIdButton;
        private string _automationIdLabel;
        private string _automationIdTextbox;
        private string _nodeEXE;
        private string _appiumNpm;
        private string _appiumEXE;
        private string _aut;
        private List<BatchCommandList> _savedRunReport;
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            try
            {
                _errOut = "";
                _automationIdButton = "btnClickTest";
                _automationIdLabel = "lblClickStatus";
                _automationIdTextbox = "txtClickStatus";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail(e.Message);
            }
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //_savedRunReport = new List<BatchCommandList>();
            string SettingsScreenShotLocation = "ScreenShots";
            string fullExceptionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsScreenShotLocation);
            if (!Directory.Exists(fullExceptionPath)) Directory.CreateDirectory(fullExceptionPath);
            _aut = Settings.Settings.ApplicationUnderTest;
            _nodeEXE = Settings.Settings.NodeExe;
            _appiumNpm = Settings.Settings.AppiumNpm;
            _appiumEXE = Settings.Settings.AppiumServerExe;
            _ga = new GeneralActions(nodeExecutable: _nodeEXE, appiumMainJs: _appiumNpm);
            _ga.TestName = "UnitTest-Init";
            //_ga.ApplicationPath = "C:\\Source\\Repos\\BurnSoft.Testing.Apps.Appium\\SampleUITestApp\\bin\\Debug\\SampleUITestApp.exe";
            _ga.SettingsScreenShotLocation = fullExceptionPath;
            _ga.DoSleep = true;
            _ga.ErrorCatcher += (ss, ee) =>
            {
                TestContext.WriteLine($"ERROR: {ee}");
            };
            _ga.Initialize(_aut);
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _ga.Dispose();
            KillAppByName(_aut);
        }

        public void KillAppByName(string appName)
        {
            // The process name is typically the executable name without the .exe extension
            // For "notepad.exe", the process name is "notepad"
            appName = Path.GetFileName(appName);
            string processName = Path.GetFileNameWithoutExtension(appName);

            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process proc in processes)
            {
                try
                {
                    proc.Kill();
                    // Optional: wait for the process to exit to ensure it's fully terminated
                    proc.WaitForExit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not kill process {proc.Id}: {ex.Message}");
                }
            }
        }

        [Test, Category("General Function Test"), Order(3)]
        public void PerformActionDoubleCLickElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.DoubleClick, 
                    out _errOut, GeneralActions.AppAction.FindElementById);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
            }
            //Assert.IsTrue(value);
        }

        /// <summary>
        /// Defines the test method PerformActionReadElementTest for textbox.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        [Test, Category("General Function Test"), Order(5)]
        public void PerformActionReadElementTextboxTest()
        {
            bool value = false;
            try
            {
                bool myValue = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.DoubleClick, 
                    out _errOut, GeneralActions.AppAction.FindElementById);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                Thread.Sleep(500);
                string status = _ga.PerformAction(_automationIdTextbox, out _errOut);
                TestContext.WriteLine($"Status Textbox: {status}");
                value = status.Length > 0;
                value = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.Click, 
                    out _errOut, GeneralActions.AppAction.FindElementById);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                status = _ga.PerformAction(_automationIdTextbox, out _errOut);
                TestContext.WriteLine($"Status Textbox: {status}");
                value = status.Length > 0;
                if (!value || !status.Equals("Clicked"))
                {
                    throw new Exception("Value read is not what is expected");
                }
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail(e.Message);
            }
            //Assert.IsTrue(value);
        }

        /// <summary>
        /// Defines the test method PerformActionReadElementTest for textbox.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        [Test, Category("General Function Test"), Order(4)]
        public void PerformActionReadElementLabelTest_expectFail()
        {
            bool value = false;
            try
            {
                bool myValue = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.DoubleClick,
                    out _errOut, GeneralActions.AppAction.FindElementById);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                Thread.Sleep(500);
                string status = _ga.PerformAction(_automationIdLabel, out _errOut);
                TestContext.WriteLine($"Status Label: {status}");
                value = status.Length > 0;
                value = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.Click, 
                    out _errOut, GeneralActions.AppAction.FindElementById);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                status = _ga.PerformAction(_automationIdLabel, out _errOut);
                TestContext.WriteLine($"Status Label: {status}");
                value = status.Length > 0;
                if (!value || status.Equals("Clicked"))
                {
                    throw new Exception("Value read is not what is expected");
                }
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail(e.Message);
            }
            //Assert.IsTrue(value);
        }
        /// <summary>
        /// Defines the test method PerformActionCLickElementTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        [Test, Category("General Function Test"), Order(2)]
        public void PerformActionCLickElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.Click, 
                    out _errOut, GeneralActions.AppAction.FindElementById);
                if (_errOut.Length > 0) throw new Exception(_errOut);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail(e.Message);
            }
            //Assert.IsTrue(value);
        }
        /// <summary>
        /// Defines the test method PerformActionVerifyElementTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        [Test, Category("General Function Test"), Order(1)]
        public void PerformActionVerifyElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.Nothing, 
                    out _errOut, GeneralActions.AppAction.FindElementById);
                if (_errOut.Length > 0) throw new Exception(_errOut);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail(e.Message);
            }
            //Assert.IsTrue(value);
        }
        /// <summary>
        /// Defines the test method PerformActionSendTextElementTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        /// <exception cref="System.Exception"></exception>
        /// <exception cref="System.Exception"></exception>
        /// <exception cref="System.Exception"></exception>
        [Test, Category("General Function Test"), Order(6)]
        public void PerformActionSendTextElementTest()
        {
            bool value = false;
            try
            {
                string UseTab = "Other";
                string txt1 = "txtDatabaseServer";
                string txt2 = "txtUserName";
                string txt3 = "txtPassword";
                string saveBtn = "btnSave";

                Thread.Sleep(2000);
                if (!_ga.PerformAction(UseTab, "", GeneralActions.MyAction.Click, out _errOut,
                    GeneralActions.AppAction.FindElementById)) throw new Exception(_errOut);
                Thread.Sleep(1000);

                if (!_ga.PerformAction(txt1, "", GeneralActions.MyAction.Click, out _errOut)) throw new Exception(_errOut);
                value = _ga.PerformAction(txt1, "11.1.1.90", GeneralActions.MyAction.SendKeys, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);

                if (!_ga.PerformAction(txt2, "", GeneralActions.MyAction.Click, out _errOut)) throw new Exception(_errOut);
                value = _ga.PerformAction(txt2, "superuser", GeneralActions.MyAction.SendKeys, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);

                if (!_ga.PerformAction(txt3, "", GeneralActions.MyAction.Click, out _errOut)) throw new Exception(_errOut);
                value = _ga.PerformAction(txt3, "supersecret", GeneralActions.MyAction.SendKeys, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);

                Thread.Sleep(1000);
                if (!_ga.PerformAction(saveBtn, "", GeneralActions.MyAction.Click, out _errOut)) throw new Exception(_errOut);
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail(e.Message);
            }

            if (_ga.ScreenShotLocation != null)
            {
                foreach (string s in _ga.ScreenShotLocation)
                {
                    TestContext.WriteLine($"{s}");
                }
            }
            if (_ga.ErrorLists != null)
            {
                foreach (string s in _ga.ErrorLists)
                {
                    TestContext.WriteLine($"{s}");
                }
            }
        }
        [Test, Category("General Function Test"), Order(7)]
        public void DropDownBoxTest()
        {
            bool value = false;
            try
            {
                Thread.Sleep(1000);

                value = _ga.PerformAction(Mappings.TestAppMap.AutomationIds.DebugModeDropDown, "Ve", GeneralActions.MyAction.SendKeys, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);

                string svalue = _ga.PerformAction(Mappings.TestAppMap.AutomationIds.DebugModeDropDown, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);

                TestContext.WriteLine(svalue);

                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail(e.Message);
            }

            if (_ga.ScreenShotLocation != null)
            {
                foreach (string s in _ga.ScreenShotLocation)
                {
                    TestContext.WriteLine($"{s}");
                }
            }
            if (_ga.ErrorLists != null)
            {
                foreach (string s in _ga.ErrorLists)
                {
                    TestContext.WriteLine($"{s}");
                }
            }
        }
    }
}