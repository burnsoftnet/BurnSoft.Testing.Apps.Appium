using BurnSoft.Testing.Apps.Appium.helpers;
using BurnSoft.Testing.Apps.Appium.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    public class JsonHandlingTests
    {
        private List<BatchCommandList> _testSequence;
        private string _errOut;
        private string _saveTo;
        private string _loadFrom;

        [SetUp]
        public void Setup()
        {
            _testSequence = helpers.GenerateTests.GetCommands();
            _errOut = "";
            _saveTo = Settings.Settings.JsonSaveTo;
            _loadFrom = Settings.Settings.JsonLoadFrom;
        }

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

        [Test, Category("BatchCommand JSON Tests")]
        public void ConvertTestSequenceToJsonFileTest()
        {
            if (JsonHandling.ConvertTestSequenceToJsonFile(_testSequence, _saveTo, out _errOut))
            {
                Console.WriteLine($"Data Saved to {_saveTo}");
            }
            if (_errOut.Length > 0)
            {
                Console.WriteLine($"ERROR: {_errOut}");
                Assert.Fail();
            }
        }
    }
}
