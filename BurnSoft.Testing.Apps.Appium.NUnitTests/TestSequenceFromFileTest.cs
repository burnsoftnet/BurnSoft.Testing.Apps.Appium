using BurnSoft.Testing.Apps.Appium.NUnitTests.helpers;
using BurnSoft.Testing.Apps.Appium.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    public class TestSequenceFromFileTest
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
        /// The ts
        /// </summary>
        private TestSequence _ts;
        /// <summary>
        /// The node executable
        /// </summary>
        private string _nodeEXE;
        /// <summary>
        /// The appium NPM
        /// </summary>
        private string _appiumNpm;
        /// <summary>
        /// The aut
        /// </summary>
        private string _aut;
        /// <summary>
        /// The saved run report
        /// </summary>
        private List<BatchCommandList> _savedRunReport;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _errOut = "";
        }

        /// <summary>
        /// Called when [time setup].
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            string SettingsScreenShotLocation = "ScreenShots";
            string fullExceptionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsScreenShotLocation);
            if (!Directory.Exists(fullExceptionPath)) Directory.CreateDirectory(fullExceptionPath);
            _aut = Settings.Settings.ApplicationUnderTest;
            _nodeEXE = Settings.Settings.NodeExe;
            _appiumNpm = Settings.Settings.AppiumNpm;
            _ts = new TestSequence(nodeExecutable: _nodeEXE, appiumMainJs: _appiumNpm,
                settingsScreenShotLocation: fullExceptionPath, testName: "UnitTest-Init", 
                debugMode: true, breakOnFail: false);
            _ts.ErrorCatcher += (ss, ee) =>
            {
                TestContext.WriteLine($"ERROR: {ee}");
                _errOut += $"{ee}{Environment.NewLine}";
            };
            _ts.DebugLog += (ss, ee) =>
            {
                TestContext.WriteLine($"DEBUG: {ee}");
            };
            //_ts.Initialize(_aut);
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        [OneTimeTearDown]
        public void Dispose()
        {
            _ts.Dispose();
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
        /// Defines the test method RunTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        [Test, Category("Test Sequence Test - File"), Order(1)]
        public void RunTest()
        {
            try
            {
                string TestFile = Settings.Settings.JsonLoadFrom;
                List<BatchCommandList> value = _ts.Run(_aut, TestFile);
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
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        /// Defines the test method RunTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        [Test, Category("Test Sequence Test - File"), Order(1)]
        public void RunTestMGC()
        {
            string aut = "C:\\Source\\Repos\\MyGunCollection\\BSMyGunCollection\\bin\\Debug\\BSMyGunCollection.exe";
            string testFile = "c:\\test\\AddSimpleTest.json";
            bool didPass = true;
            try
            {
                string TestFile = Settings.Settings.JsonLoadFrom;
                                List<BatchCommandList> value = _ts.Run(aut, testFile);
                //if (_errOut.Length > 0) throw new Exception(_errOut);
                _savedRunReport = value;
                int testNumber = 1;
                foreach (BatchCommandList v in value)
                {
                    string passfailed = v.PassedFailed ? "PASSED" : "FAILED";
                    TestContext.WriteLine($"{testNumber}.) {passfailed} - {v.TestName}");
                    TestContext.WriteLine(v.ReturnedValue);
                    testNumber++;
                }
                //if (_errOut.Length > 0) throw new Exception(_errOut);
                didPass = _errOut.Length == 0;
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                didPass = false;
            }
            KillAppByName(aut);
            if (!didPass) Assert.Fail();
        }
    }
}
