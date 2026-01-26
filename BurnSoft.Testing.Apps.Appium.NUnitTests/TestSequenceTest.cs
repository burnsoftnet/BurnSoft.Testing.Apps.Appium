using BurnSoft.Testing.Apps.Appium.helpers;
using BurnSoft.Testing.Apps.Appium.NUnitTests.helpers;
using BurnSoft.Testing.Apps.Appium.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    public class TestSequenceTest
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

        private TestSequence _ts;
        private string _automationIdButton;
        private string _automationIdLabel;
        private string _automationIdTextbox;
        private string _nodeEXE;
        private string _appiumNpm;
        private string _appiumEXE;
        private string _aut;
        private List<BatchCommandList> _savedRunReport;

        [SetUp]
        public void Setup()
        {
            _errOut = "";
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
            _ts = new TestSequence(nodeExecutable: _nodeEXE, appiumMainJs: _appiumNpm, 
                settingsScreenShotLocation: fullExceptionPath, testName: "UnitTest-Init");
            _ts.ErrorCatcher += (ss, ee) =>
            {
                TestContext.WriteLine($"ERROR: {ee}");
            };
            //_ts.Initialize(_aut);
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _ts.Dispose();
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

        [Test, Category("Test Sequence Test"), Order(1)]
        public void RunTest()
        {
            try
            {
                List<BatchCommandList> value = _ts.Run(_aut ,GenerateTests.GetCommands(), out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                _savedRunReport = value;
                int testNumber = 1;
                foreach (BatchCommandList v in value)
                {
                    string passfailed = v.PassedFailed ? "PASSED" : "FAILED";
                    TestContext.WriteLine($"{testNumber}.) {passfailed} - {v.TestName}");
                    TestContext.WriteLine(v.ReturnedValue);
                    testNumber++;
                }
                //Assert.IsTrue(_ga.AllTestsPassed(value));
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Defines the test method GenerateResultsTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        /// <exception cref="System.Exception"></exception>
        [Test, Category("Test Sequence Test"), Order(2)]
        public void GenerateResultsTest()
        {
            try
            {
                List<BatchCommandList> value = new List<BatchCommandList>();
                if (_savedRunReport == null)
                {
                    value = _ts.Run(_aut ,GenerateTests.GetCommands(), out _errOut);
                    if (_errOut.Length > 0) throw new Exception(_errOut);
                }
                else
                {
                    value = _savedRunReport;
                }

                TestContext.WriteLine(Reporting.GenerateResults(value, out _errOut));
                if (_errOut.Length > 0) throw new Exception(_errOut);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail(e.Message);
            }

        }
    }
}
