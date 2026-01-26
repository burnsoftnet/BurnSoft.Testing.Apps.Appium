using BurnSoft.Testing.Apps.Appium.helpers;
using BurnSoft.Testing.Apps.Appium.Types;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
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
                List<BatchCommandList> list = JsonHandling.ConvertJsonToBatchCommand(_saveTo, out _errOut);
                PrintBatchCommand(list);
            }
            if (_errOut.Length > 0)
            {
                Console.WriteLine($"ERROR: {_errOut}");
                Assert.Fail();
            }
        }

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
                PrintBatchCommand(list);
            } else
            {
                Console.WriteLine("NOTHING LOADED FORM LIST");
                Assert.Fail();
            }
        }

        private void PrintBatchCommand(List<BatchCommandList> lst)
        {
            if (lst.Count > 0)
            {
                foreach (BatchCommandList l in lst)
                {
                    Console.WriteLine($"");
                    Console.WriteLine($"TestName: {l.TestName}");
                    Console.WriteLine($"CommandAction: {l.CommandAction}");
                    Console.WriteLine($"Actions: {l.Actions}");
                    Console.WriteLine($"ElementName: {l.ElementName}");
                    Console.WriteLine($"SendKeys: {l.SendKeys}");
                    Console.WriteLine($"PassedFailed: {l.PassedFailed}");
                    Console.WriteLine($"ReturnedValue: {l.ReturnedValue}");
                    Console.WriteLine($"ReturnedFoundValue: {l.ReturnedFoundValue}");
                    Console.WriteLine($"ReturnedValueBlankOk: {l.ReturnedValueBlankOk}");
                    Console.WriteLine($"SleepInterval: {l.SleepInterval}");
                    Console.WriteLine($"TabCount: {l.TabCount}");
                    Console.WriteLine($"TestNumber: {l.TestNumber}");
                    Console.WriteLine($"TestNameLookUp: {l.TestNameLookUp}");
                    Console.WriteLine($"");

                }
            }
        }
    }
}
