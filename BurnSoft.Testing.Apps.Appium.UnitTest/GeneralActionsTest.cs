using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
                _errOut = "";
                _automationId = "AR-22";
                _ga = new GeneralActions();
                _ga.ApplicationPath = "c:\\Source\\Repos\\MyGunCollection\\BSMyGunCollection\\bin\\Debug\\BSMyGunCollection.exe";
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
        public void ClickOnFirstObjectInList()
        {
            bool value = false;
            try
            {
                value = _ga.ClickOnElement(_automationId, out _errOut, GeneralActions.AppAction.FindElementByName);
                if (_errOut.Length > 0) throw new Exception(_errOut);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
            }
            Assert.IsTrue(value);
        }
        [TestMethod]
        public void DoubleCLickElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.DoubleCLickElement(_automationId, out _errOut, GeneralActions.AppAction.FindElementByName);
                if (_errOut.Length > 0) throw new Exception(_errOut);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
            }
            Assert.IsTrue(value);
        }

        [TestMethod]
        public void PerformActionDoubleCLickElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.PerformAction(_automationId, "", GeneralActions.MyAction.DoubleClick,out _errOut, GeneralActions.AppAction.FindElementByName);
                if (_errOut.Length > 0) throw new Exception(_errOut);
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
    }
}
