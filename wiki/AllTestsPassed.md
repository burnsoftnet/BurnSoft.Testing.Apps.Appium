# AllTestsPassed

The AllTestsPassed passed function works with the RunBatchCommands Function. It will take the results list from the results when you run the RunBatchCommand function and search through the results 
to look and pake sure that there are not DidPass=False values.  If all comes back true, then it will return true.

## Code Example

Code was taken from the unit test, no display results since it just returns true or false, but in this case it returned true

```

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

```