using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BurnSoft.Testing.Apps.Appium.Types;
// ReSharper disable UseObjectOrCollectionInitializer

namespace BurnSoft.Testing.Apps.Appium.UnitTest
{
    [TestClass]
    public class GeneralActionsTest
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
        private string _automationId;
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            try
            {
                string SettingsScreenShotLocation = "ScreenShots";
                string fullExceptionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsScreenShotLocation);
                if (!Directory.Exists(fullExceptionPath)) Directory.CreateDirectory(fullExceptionPath);
                _errOut = "";
                _automationId = "AR-22";
                _ga = new GeneralActions();
                _ga.TestName = "UnitTest-Init";
                _ga.ApplicationPath = "c:\\Source\\Repos\\MyGunCollection\\BSMyGunCollection\\bin\\Debug\\BSMyGunCollection.exe";
                _ga.SettingsScreenShotLocation = fullExceptionPath;
                _ga.DoSleep = true;
                _ga.Initialize();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail(e.Message);
            }
        }
        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _ga.Dispose();
        }


        /// <summary>
        /// Defines the test method PerformActionDoubleCLickElementTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
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
        /// <summary>
        /// Defines the test method PerformActionReadElementTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        [TestMethod]
        public void PerformActionReadElementTest()
        {
            bool value = false;
            try
            {
                bool myValue = _ga.PerformAction(_automationId, "", GeneralActions.MyAction.DoubleClick, out _errOut, GeneralActions.AppAction.FindElementByName);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                Thread.Sleep(5000);
                string serial = _ga.PerformAction("txtSerial",out _errOut);
                TestContext.WriteLine($"Serial Number: {serial}");
                value = serial.Length > 0;
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
            }
            Assert.IsTrue(value);
        }
        /// <summary>
        /// Defines the test method PerformActionCLickElementTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
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
        /// <summary>
        /// Defines the test method PerformActionVerifyElementTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
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
        /// <summary>
        /// Defines the test method PerformActionSendTextElementTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        /// <exception cref="System.Exception"></exception>
        /// <exception cref="System.Exception"></exception>
        /// <exception cref="System.Exception"></exception>
        [TestMethod]
        public void PerformActionSendTextElementTest()
        {
            bool value = false;
            try
            {
                if (!_ga.PerformAction("Search Gun Collection", "", GeneralActions.MyAction.Click, out _errOut,
                    GeneralActions.AppAction.FindElementByName)) throw new Exception(_errOut);
                Thread.Sleep(2000);
                if (!_ga.PerformAction("txtLookFor", "", GeneralActions.MyAction.Click, out _errOut)) throw new Exception(_errOut);
                
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
        /// <summary>
        /// Gets the commands.
        /// </summary>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        private List<BatchCommandList> GetCommands()
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
                TestName = "Verify For Textbox exists",
                Actions = GeneralActions.MyAction.Nothing,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = "txtLookFor"
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
                TestName = "Verify Control Combo box Look in",
                Actions = GeneralActions.MyAction.Nothing,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = "cmbLookIn"
            });
            cmd.Add(new BatchCommandList()
            {
                TestName = "Get Control Combo Value box Look in",
                Actions = GeneralActions.MyAction.ReadValue,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = "cmbLookIn"
            });
            //cmd.Add(new BatchCommandList()
            //{
            //    TestName = "Click on Control Combo box Look in",
            //    Actions = GeneralActions.MyAction.Click,
            //    CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
            //    ElementName = "cmbLookIn"
            //});
            //cmd.Add(new BatchCommandList()
            //{
            //    TestName = "Click on Control Combo box Look in",
            //    Actions = GeneralActions.MyAction.Click,
            //    CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
            //    ElementName = "Model Name"
            //});
            cmd.Add(new BatchCommandList()
            {
                TestName = "Start Search",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = "btnSearch"
            });
            return cmd;
        }
        /// <summary>
        /// Defines the test method BatchCommandTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        [TestMethod]
        public void BatchCommandTest()
        {
            try
            {
                List<BatchCommandList> value = _ga.RunBatchCommands(GetCommands(), out _errOut);
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
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail();
            }
            
        }
        /// <summary>
        /// Defines the test method GenerateResultsTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        /// <exception cref="System.Exception"></exception>
        [TestMethod]
        public void GenerateResultsTest()
        {
            try
            {
                List<BatchCommandList> value = _ga.RunBatchCommands(GetCommands(), out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                TestContext.WriteLine(_ga.GenerateResults(value, out _errOut));
                if (_errOut.Length > 0) throw new Exception(_errOut);
                Assert.IsTrue(value.Count > 0);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail();
            }

        }
    }
}
