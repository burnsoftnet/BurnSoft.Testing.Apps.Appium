﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BurnSoft.Testing.Apps.Appium.UnitTest
{
    [TestClass]
    public class GeneralActionsTest
    {
        public TestContext TestContext { get; set; }
        private GeneralActions _ga;
        [TestInitialize]
        public void Init()
        {
            _ga = new GeneralActions();
            _ga.ApplicationPath = "F:\\Source\\Repos\\MyGunCollection\\BSMyGunCollection\\bin\\Debug\\BSMyGunCollection.exe";
            _ga.Inititalize();
        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
