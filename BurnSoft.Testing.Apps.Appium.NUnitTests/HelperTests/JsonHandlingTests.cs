using BurnSoft.Testing.Apps.Appium.helpers;
using BurnSoft.Testing.Apps.Appium.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    /// <summary>
    /// Class JsonHandlingTests.
    /// </summary>
    public class JsonHandlingTests
    {
        /// <summary>
        /// The test sequence
        /// </summary>
        private List<BatchCommandList> _testSequence;
        /// <summary>
        /// The error out
        /// </summary>
        private string _errOut;
        /// <summary>
        /// The save to
        /// </summary>
        private string _saveTo;
        /// <summary>
        /// The load from
        /// </summary>
        private string _loadFrom;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _testSequence = helpers.GenerateTests.GetCommands();
            _errOut = "";
            _saveTo = Settings.Settings.JsonSaveTo;
            _loadFrom = Settings.Settings.JsonLoadFrom;
        }
        /// <summary>
        /// Defines the test method ConvertTestSequenceToJsonTest.
        /// </summary>
        [Test, Category("BatchCommand JSON Tests")]
        public void ConvertTestSequenceToJsonTest()
        {
            string json = JsonHandling.ConvertTestSequenceToJson(_testSequence, out _errOut);
            if (_errOut.Length > 0)
            {
                Console.WriteLine($"ERROR: {_errOut}");
                Assert.Fail();
            }
            if (json == null) Assert.Fail();
            if (json.Length == 0)
            {
                Assert.Fail();
            } else
            {
                Console.WriteLine($"JSON: {json}");
            }
        }
        /// <summary>
        /// Defines the test method ConvertTestSequenceToJsonFileTest.
        /// </summary>
        [Test, Category("BatchCommand JSON Tests")]
        public void ConvertTestSequenceToJsonFileTest()
        {
            if (JsonHandling.ConvertTestSequenceToJsonFile(_testSequence, _saveTo, out _errOut))
            {
                Console.WriteLine($"Data Saved to {_saveTo}");
                List<BatchCommandList> list = JsonHandling.ConvertJsonToBatchCommand(_saveTo, out _errOut);
                PrintLists.PrintBatchCommandToConsole(list);
            }
            if (_errOut.Length > 0)
            {
                Console.WriteLine($"ERROR: {_errOut}");
                Assert.Fail();
            }
        }
        /// <summary>
        /// Defines the test method ConvertJsonToBatchCommandTest.
        /// </summary>
        [Test, Category("BatchCommand JSON Tests")]
        public void ConvertJsonToBatchCommandTest()
        {
            if (_loadFrom.Length == 0)
            {
                Console.WriteLine("LOAD FROM LOCATION IS BLANK!");
                Assert.Fail();
            }
            List<BatchCommandList> list = JsonHandling.ConvertJsonToBatchCommand(_loadFrom, out _errOut);
            if (_errOut.Length > 0)
            {
                Console.WriteLine($"ERROR: {_errOut}");
                Assert.Fail();
            }
            if (list.Count > 0)
            {
                Console.WriteLine($"LIST LOADED WITH: {list.Count} items");
                PrintLists.PrintBatchCommandToConsole(list);
            } else
            {
                Console.WriteLine("NOTHING LOADED FORM LIST");
                Assert.Fail();
            }
        }
    }
}
