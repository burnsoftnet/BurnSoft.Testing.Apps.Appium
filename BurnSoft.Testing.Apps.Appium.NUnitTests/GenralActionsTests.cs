using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BurnSoft.Testing.Apps.Appium.Types;

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
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            try
            {
                string SettingsScreenShotLocation = "ScreenShots";
                string fullExceptionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsScreenShotLocation);
                if (!Directory.Exists(fullExceptionPath)) Directory.CreateDirectory(fullExceptionPath);
                _errOut = "";
                _automationIdButton = "btnClickTest";
                _automationIdLabel = "lblClickStatus";
                _automationIdTextbox = "txtClickStatus";
                _ga = new GeneralActions();
                _ga.TestName = "UnitTest-Init";
                _ga.ApplicationPath = "C:\\Source\\Repos\\BurnSoft.Testing.Apps.Appium\\SampleUITestApp\\bin\\Debug\\SampleUITestApp.exe";
                _ga.SettingsScreenShotLocation = fullExceptionPath;
                _ga.DoSleep = true;
                _ga.ErrorCatcher += (ss, ee) =>
                {
                    TestContext.WriteLine(ee);
                };
                _ga.Initialize();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail(e.Message);
            }
        }
        [TearDown]
        public void Dispose()
        {
            _ga.Dispose();
        }

        [Test, Category("General Function Test")]
        public void PerformActionDoubleCLickElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.DoubleClick, out _errOut, GeneralActions.AppAction.FindElementByName);
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
        [Test, Category("General Function Test")]
        public void PerformActionReadElementTextboxTest()
        {
            bool value = false;
            try
            {
                bool myValue = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.DoubleClick, out _errOut, GeneralActions.AppAction.FindElementByName);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                Thread.Sleep(500);
                string status = _ga.PerformAction(_automationIdTextbox, out _errOut);
                TestContext.WriteLine($"Status Textbox: {status}");
                value = status.Length > 0;
                value = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.Click, out _errOut, GeneralActions.AppAction.FindElementByName);
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
        [Test, Category("General Function Test")]
        public void PerformActionReadElementLabelTest_expectFail()
        {
            bool value = false;
            try
            {
                bool myValue = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.DoubleClick, out _errOut, GeneralActions.AppAction.FindElementByName);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                Thread.Sleep(500);
                string status = _ga.PerformAction(_automationIdLabel, out _errOut);
                TestContext.WriteLine($"Status Label: {status}");
                value = status.Length > 0;
                value = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.Click, out _errOut, GeneralActions.AppAction.FindElementByName);
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
        [Test, Category("General Function Test")]
        public void PerformActionCLickElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.Click, out _errOut, GeneralActions.AppAction.FindElementByName);
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
        [Test, Category("General Function Test")]
        public void PerformActionVerifyElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.PerformAction(_automationIdButton, "", GeneralActions.MyAction.Nothing, out _errOut, GeneralActions.AppAction.FindElementByName);
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
        [Test, Category("General Function Test")]
        public void PerformActionSendTextElementTest()
        {
            bool value = false;
            try
            {
                string UseTab = "tabOther";
                string txt1 = "txtDatabaseServer";
                string txt2 = "txtUserName";
                string txt3 = "txtPassword";
                string saveBtn = "btnSave";
                if (!_ga.PerformAction(UseTab, "", GeneralActions.MyAction.Click, out _errOut,
                    GeneralActions.AppAction.FindElementByName)) throw new Exception(_errOut);
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

            foreach (string s in _ga.ScreenShotLocation)
            {
                TestContext.WriteLine($"{s}");
            }
            foreach (string s in _ga.ErrorLists)
            {
                TestContext.WriteLine($"{s}");
            }
            //Assert.IsTrue(value);
        }
        /// <summary>
        /// Gets the commands.
        /// </summary>
        /// <returns>List&lt;BatchCommandList&gt;.</returns>
        private List<BatchCommandList> GetCommands()
        {
            string UseTab = "Other";
            string txt1 = "txtDatabaseServer";
            string txt2 = "txtUserName";
            string txt3 = "txtPassword";
            string saveBtn = "btnSave";
            string nextTab = "Main";

            List<BatchCommandList> cmd = new List<BatchCommandList>();
            cmd.Add(new BatchCommandList()
            {
                TestName = "Click On Tab",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByName,
                ElementName = UseTab
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Verify Database Server Textbox exists",
                Actions = GeneralActions.MyAction.Nothing,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt1
            });
            cmd.Add(new BatchCommandList()
            {
                TestName = "Set Database Server",
                Actions = GeneralActions.MyAction.ClearAndSendKeys,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt1,
                SendKeys = "11.2.3.4"
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Verify Username Textbox exists",
                Actions = GeneralActions.MyAction.Nothing,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt2
            });
            cmd.Add(new BatchCommandList()
            {
                TestName = "Set Username",
                Actions = GeneralActions.MyAction.ClearAndSendKeys,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt2,
                SendKeys = "superman"
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Verify password Textbox exists",
                Actions = GeneralActions.MyAction.Nothing,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt3
            });
            cmd.Add(new BatchCommandList()
            {
                TestName = "Set Password",
                Actions = GeneralActions.MyAction.ClearAndSendKeys,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = txt3,
                SendKeys = "superangry"
            });


            cmd.Add(new BatchCommandList()
            {
                TestName = "Click Save Button",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = saveBtn
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Click on Main Tab",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = nextTab
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Click Button",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = _automationIdButton
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Read Textbox in Main after Click",
                Actions = GeneralActions.MyAction.ReadValue,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = _automationIdTextbox
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Read Label in Main after Click",
                Actions = GeneralActions.MyAction.ReadValue,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = _automationIdLabel
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Click File",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = "mnuFile"
            });

            cmd.Add(new BatchCommandList()
            {
                TestName = "Click Exit",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = "mnuExit"
            });

            return cmd;
        }
        /// <summary>
        /// Defines the test method BatchCommandTest.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        [Test, Category("Batch Testing")]
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
        [Test, Category("Batch Testing")]
        public void GenerateResultsTest()
        {
            try
            {
                List<BatchCommandList> value = _ga.RunBatchCommands(GetCommands(), out _errOut);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                TestContext.WriteLine(_ga.GenerateResults(value, out _errOut));
                if (_errOut.Length > 0) throw new Exception(_errOut);
                //Assert.IsTrue(value.Count > 0);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
                Assert.Fail(e.Message);
            }

        }
    }
}