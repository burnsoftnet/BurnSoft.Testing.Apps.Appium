using BurnSoft.Testing.Apps.Appium.helpers;
using BurnSoft.Testing.Apps.Appium.NUnitTests.helpers;
using BurnSoft.Testing.Apps.Appium.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
                debugMode: Settings.Settings.Debug, breakOnFail: Settings.Settings.BreakOnFail);
            _ts.ErrorCatcher += (ss, ee) =>
            {
                TestContext.WriteLine($"ERROR: {ee}");
            };
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        [OneTimeTearDown]
        public void Dispose()
        {
            _ts.Dispose();
        }

        /// <summary>
        /// Defines the test method RunTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
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
