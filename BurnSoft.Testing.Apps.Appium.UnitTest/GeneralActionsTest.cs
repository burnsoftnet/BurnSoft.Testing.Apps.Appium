﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BurnSoft.Testing.Apps.Appium.Types;

namespace BurnSoft.Testing.Apps.Appium.UnitTest
{
    [TestClass]
    public class GeneralActionsTest
    {
        public TestContext TestContext { get; set; }
        private string _errOut;
        private GeneralActions _ga;
        private string _automationId;
        [TestInitialize]
        public void Init()
        {
            try
            {
                string SettingsScreenShotLocation = "ScreenShots";
                string FullExceptionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsScreenShotLocation);
                if (!Directory.Exists(FullExceptionPath)) Directory.CreateDirectory(FullExceptionPath);
                _errOut = "";
                _automationId = "AR-22";
                _ga = new GeneralActions();
                _ga.TestName = "UnitTest-Init";
                _ga.ApplicationPath = "c:\\Source\\Repos\\MyGunCollection\\BSMyGunCollection\\bin\\Debug\\BSMyGunCollection.exe";
                _ga.SettingsScreenShotLocation = FullExceptionPath;
                _ga.DoSleep = true;
                _ga.Initialize();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail(e.Message);
            }
        }
        [TestCleanup]
        public void CleanUp()
        {
            _ga.Dispose();
        }

        

        [TestMethod]
        public void PerformActionDoubleCLickElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.PerformAction(_automationId, "", GeneralActions.MyAction.DoubleClick,out _errOut, GeneralActions.AppAction.FindElementByName);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
            }
            Assert.IsTrue(value);
        }
        [TestMethod]
        public void PerformActionCLickElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.PerformAction(_automationId, "", GeneralActions.MyAction.Click, out _errOut, GeneralActions.AppAction.FindElementByName);
                if (_errOut.Length > 0) throw new Exception(_errOut);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
            }
            Assert.IsTrue(value);
        }

        [TestMethod]
        public void PerformActionVerifyElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.PerformAction(_automationId, "", GeneralActions.MyAction.Nothing, out _errOut, GeneralActions.AppAction.FindElementByName);
                if (_errOut.Length > 0) throw new Exception(_errOut);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
            }
            Assert.IsTrue(value);
        }

        [TestMethod]
        public void PerformActionSendTextElementTest()
        {
            bool value = false;
            try
            {
                if (!_ga.PerformAction("Search Gun Collection", "", GeneralActions.MyAction.Click, out _errOut,
                    GeneralActions.AppAction.FindElementByName)) throw new Exception(_errOut);
                Thread.Sleep(2000);
                if (!_ga.PerformAction("txtLookFor", "", GeneralActions.MyAction.Click, out _errOut, GeneralActions.AppAction.FindElementByAccessibilityId)) throw new Exception(_errOut);
                
                value = _ga.PerformAction("txtLookFor", "Glock", GeneralActions.MyAction.SendKeys, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                Thread.Sleep(2000);
                if (!_ga.PerformAction("btnSearch", "", GeneralActions.MyAction.Click, out _errOut)) throw new Exception(_errOut);
                Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
            }

            foreach (string s in _ga.ScreenShotLocation)
            {
                TestContext.WriteLine($"{s}");
            }
            foreach (string s in _ga.ErrorLists)
            {
                TestContext.WriteLine($"{s}");
            }
            Assert.IsTrue(value);
        }

        [TestMethod]
        public void BatchCommandTest()
        {
            
            try
            {
                List<BatchCommandList> cmd = new List<BatchCommandList>();
                cmd.Add(new BatchCommandList()
                {
                    TestName = "Search Gun Collection Button",
                    Actions = GeneralActions.MyAction.Click,
                    CommandAction = GeneralActions.AppAction.FindElementByName,
                    ElementName = "Search Gun Collection"
                });
                cmd.Add(new BatchCommandList()
                {
                    TestName = "Look For Textbox",
                    Actions = GeneralActions.MyAction.Click,
                    CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                    ElementName = "txtLookFor"
                });
                cmd.Add(new BatchCommandList()
                {
                    TestName = "Search for word Glock",
                    Actions = GeneralActions.MyAction.SendKeys,
                    CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                    ElementName = "txtLookFor",
                    SendKeys = "Glock"
                });
                cmd.Add(new BatchCommandList()
                {
                    TestName = "Start Search",
                    Actions = GeneralActions.MyAction.Click,
                    CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                    ElementName = "btnSearch"
                });
                List<BatchCommandList> value = _ga.RunBatchCommands(cmd, out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);

                int testNumber = 1;
                foreach (BatchCommandList v in value)
                {
                    string passfailed = v.PassedFailed ? "PASSED" : "FAILED";
                    TestContext.WriteLine($"{testNumber}.) {passfailed} - {v.TestName}");
                    TestContext.WriteLine(v.ReturnedValue);
                    testNumber++;
                }
                Assert.IsTrue(_ga.AllTestsPassed(value));
                TestContext.WriteLine("-----------START Generate Test Results Function-------------");
                TestContext.WriteLine(_ga.GenerateResults(value, out _errOut));
                TestContext.WriteLine("-----------END Generate Test Results Function-------------");
                if (_errOut.Length > 0) throw new Exception(_errOut);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail();
            }
            
        }
    }
}
