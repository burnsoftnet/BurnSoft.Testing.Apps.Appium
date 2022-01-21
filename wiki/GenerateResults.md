# GenerateResults

The  function works in conjuntion with the BatchCommand Function.  The Batch Command function will return all the results back in a list format, the GenerateResults function willsort through that
list to make a nice print of the test results.  This way you can get a nice little report batch from that batch command to see the results from passing or failed tests.

This is why in the batch command example contained a TestName field, this way you can adda description to that test for when you print out the results.

## Code Example

this code example is from the unit test

'''
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

'''

## Results

This is the results from the unit test code above.

'''
1.)  PASSED! Search Gun Collection Button  Was able to Click on Search Gun Collection using FindElementByName

2.)  PASSED! Look For Textbox  Was able to Click on txtLookFor using FindElementByAccessibilityId

3.)  PASSED! Search for word Glock  Was able to SendKeys Glock to txtLookFor using FindElementByAccessibilityId

4.)  PASSED! Start Search  Was able to Click on btnSearch using FindElementByAccessibilityId

'''