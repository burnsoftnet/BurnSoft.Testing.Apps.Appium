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
        /// <summary>
        /// The automation identifier label
        /// </summary>
        private string _automationIdLabel;
        /// <summary>
        /// The automation identifier textbox
        /// </summary>
        private string _automationIdTextbox;
        /// <summary>
        /// The node executable
        /// </summary>
        private string _nodeEXE;
        /// <summary>
        /// The appium NPM
        /// </summary>
        private string _appiumNpm;
        /// <summary>
        /// The appium executable
        /// </summary>
        private string _appiumEXE;
        /// <summary>
        /// The aut ( application under test )
        /// </summary>
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

        /// <summary>
        /// Called when [time setup].
        /// </summary>
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
            _ga = new GeneralActions(nodeExecutable: _nodeEXE, appiumMainJs: _appiumNpm, 
                debugMode: Settings.Settings.Debug);
            _ga.TestName = "UnitTest-Init";
            _ga.SettingsScreenShotLocation = fullExceptionPath;
            _ga.DoSleep = true;
            _ga.ErrorCatcher += (ss, ee) =>
            {
                TestContext.WriteLine($"ERROR: {ee}");
            };
            _ga.Initialize(_aut);
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        [OneTimeTearDown]
        public void Dispose()
        {
            _ga.Dispose();
            KillAppByName(_aut);
        }

        /// <summary>
        /// Kills the name of the application by.
        /// </summary>
        /// <param name="appName">Name of the application.</param>
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
        /// <summary>
        /// Defines the test method PerformActionDoubleCLickElementTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
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
                if (!_ga.PerformAction(Mappings.TestAppMap.AutomationIds.TabMain, "", GeneralActions.MyAction.Click, out _errOut,
                    GeneralActions.AppAction.FindElementById)) throw new Exception(_errOut);
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
        /// <summary>
        /// Defines the test method DropDownBoxSendKeysTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        [Test, Category("General Function Test"), Order(7)]
        public void DropDownBoxSendKeysTest()
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
        /// <summary>
        /// Defines the test method DropDownBoxSendArrowKeys.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        [Test, Category("General Function Test"), Order(8)]
        public void DropDownBoxSendArrowKeys()
        {
            bool value = false;
            try
            {
                Thread.Sleep(1000);

                value = _ga.PerformAction(Mappings.TestAppMap.AutomationIds.DebugModeDropDown, "", GeneralActions.MyAction.Click, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);

                value = _ga.PerformAction(Mappings.TestAppMap.AutomationIds.DebugModeDropDown, "", GeneralActions.MyAction.KeyDown, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);

                value = _ga.PerformAction(Mappings.TestAppMap.AutomationIds.DebugModeDropDown, "", GeneralActions.MyAction.KeyDown, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);

                value = _ga.PerformAction(Mappings.TestAppMap.AutomationIds.DebugModeDropDown, "", GeneralActions.MyAction.KeyDown, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);

                value = _ga.PerformAction(Mappings.TestAppMap.AutomationIds.DebugModeDropDown, "", GeneralActions.MyAction.KeyUp, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);

                value = _ga.PerformAction(Mappings.TestAppMap.AutomationIds.DebugModeDropDown, "", GeneralActions.MyAction.KeyEnter, out _errOut);
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
        /// <summary>
        /// Defines the test method DeleteFileTest.
        /// </summary>
        [Test, Category("General Function Test"), Order(9)]
        public void DeleteFileTest()
        {
            if (!_ga.PerformAction(GeneralActions.MyAction.DeleteFile, Settings.Settings.DeletedFile, out _errOut))
            {
                Console.WriteLine(_errOut);
                Assert.Fail();
            }
        }
        /// <summary>
        /// Defines the test method FailFileTest.
        /// </summary>
        [Test, Category("General Function Test"), Order(10)]
        public void FailFileTest()
        {
            if (_ga.PerformAction(GeneralActions.MyAction.FailIfFileExists, Settings.Settings.FailFile, out _errOut))
            {
                Console.WriteLine(_errOut);
                Assert.Fail();
            }
        }
        /// <summary>
        /// Defines the test method PassFileTest.
        /// </summary>
        [Test, Category("General Function Test"), Order(11)]
        public void PassFileTest()
        {
            if (!_ga.PerformAction(GeneralActions.MyAction.PassIfFileExists, Settings.Settings.PassFile, out _errOut))
            {
                Console.WriteLine(_errOut);
                Assert.Fail();
            }
        }
    }
}