using BurnSoft.Testing.Apps.Appium.helpers;
using BurnSoft.Testing.Apps.Appium.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
                _errOut += $"{ee}{Environment.NewLine}";
            };
            _ts.DebugLog += (ss, ee) =>
            {
                TestContext.WriteLine($"DEBUG: {ee}");
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
        [Test, Category("Test Sequence Test - File"), Order(1)]
        public void RunTest()
        {
            try
            {
                string TestFile = Settings.Settings.JsonLoadFrom;
                List<BatchCommandList> value = _ts.Run(_aut, TestFile);
                if (_errOut.Length > 0) throw new Exception(_errOut);
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
        /// MGCs the adjustment.
        /// </summary>
        /// <param name="newfile">The newfile.</param>
        private void MGCAdjustment(string newfile)
        {
            string testFile = "c:\\test\\AddSimpleTest.json";
            List<BatchCommandList> org = JsonHandling.ConvertJsonToBatchCommand(testFile, out _errOut);
            List<BatchCommandList> newList = new List<BatchCommandList>();
            newList.AddRange(TestSequenceBuilder.DumpPageSourceToFile("Dump Main XML", "c:\\test\\MGCDump\\mainDump.xml"));
            //newList.Add(new BatchCommandList()
            //{
            //    TestName = "Sleep to Allow app to load",
            //    Actions = GeneralActions.MyAction.Sleep,
            //    CommandAction = GeneralActions.AppAction.Nothing,
            //    ElementName = "",
            //    SleepInterval = 10000,
            //});

            //newList.Add(new BatchCommandList()
            //{
            //    TestName = "Focus on New window",
            //    Actions = GeneralActions.MyAction.GetFocusNewWindow,
            //    CommandAction = GeneralActions.AppAction.Nothing,
            //    ElementName = ""
            //});
            newList.AddRange(org);
            JsonHandling.ConvertTestSequenceToJsonFile(newList, newfile, out _errOut);
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
            //string testFile = "c:\\test\\AddSimpleTestNew.json";
            //MGCAdjustment(testFile);
            bool didPass = true;
            try
            {
                string TestFile = Settings.Settings.JsonLoadFrom;
                List<BatchCommandList> value = _ts.Run(aut, testFile);
                int testNumber = 1;
                foreach (BatchCommandList v in value)
                {
                    string passfailed = v.PassedFailed ? "PASSED" : "FAILED";
                    TestContext.WriteLine($"{testNumber}.) {passfailed} - {v.TestName}");
                    TestContext.WriteLine(v.ReturnedValue);
                    testNumber++;
                }
                didPass = _errOut.Length == 0;
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                didPass = false;
            }
            if (!didPass) Assert.Fail();
        }
    }
}
