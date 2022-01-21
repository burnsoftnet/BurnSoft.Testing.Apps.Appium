#Batch Commands Functions

The batch commands functions will allow you to simply create a batch of commands to execute and the results
are stored in a list that can be passed to the Generateresults function to return a string report 
of the batch test.


## Code Example

The Code example below is from the unit test running a test against the My Gun Collection Application.
```
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

            cmd.Add(new BatchCommandList()
            {
                TestName = "Start Search",
                Actions = GeneralActions.MyAction.Click,
                CommandAction = GeneralActions.AppAction.FindElementByAccessibilityId,
                ElementName = "btnSearch"
            });


	try
            {
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
            }
            catch (Exception e)
            {
               TestContext.WriteLine($"ERROR: {e.Message}");
			                   Assert.Fail();
            }
```

### Passing Results Example

> 1.) PASSED - Search Gun Collection Button
> Was able to Click on Search Gun Collection using FindElementByName
> 
> 2.) PASSED - Verify For Textbox exists
> Was able to Verify Exists on txtLookFor using FindElementByAccessibilityId
> 
> 3.) PASSED - Look For Textbox
> Was able to Click on txtLookFor using FindElementByAccessibilityId
> 
> 4.) PASSED - Search for word Glock
> Was able to SendKeys Glock to txtLookFor using FindElementByAccessibilityId
> 
> 5.) PASSED - Verify Control Combo box Look in
> Was able to Verify Exists on cmbLookIn using FindElementByAccessibilityId
> 
> 6.) PASSED - Get Control Combo Value box Look in
> Was able to ReadValue on cmbLookIn using FindElementByAccessibilityIdReadValue on cmbLookIn using FindElementByAccessibilityId. Found value Display Name
> 
> 7.) PASSED - Start Search
> Was able to Click on btnSearch using FindElementByAccessibilityId

### Failed Results Example


> 1.) PASSED - Search Gun Collection Button
> Was able to Click on Search Gun Collection using FindElementByName
> 
> 2.) PASSED - Verify For Textbox exists
> Was able to Verify Exists on txtLookFor using FindElementByAccessibilityId 
> 
> 3.) PASSED - Look For Textbox
> Was able to Click on txtLookFor using FindElementByAccessibilityId
> 
> 4.) PASSED - Search for word Glock
> Was able to SendKeys Glock to txtLookFor using FindElementByAccessibilityId
> 
> 5.) PASSED - Verify Control Combo box Look in
> Was able to Verify Exists on cmbLookIn using FindElementByAccessibilityId
> 
> 6.) PASSED - Get Control Combo Value box Look in
> Was able to ReadValue on cmbLookIn using FindElementByAccessibilityIdReadValue on cmbLookIn using FindElementByAccessibilityId. Found value Display Name
> 
> 7.) PASSED - Click on Control Combo box Look in
> Was able to Click on cmbLookIn using FindElementByAccessibilityId
> 
> 8.) FAILED - Click on Control Combo box Look in
> C:\Source\Repos\BurnSoft.Testing.Apps.Appium\BurnSoft.Testing.Apps.Appium.UnitTest\bin\Debug\ScreenShots\UnitTest-Init-637782725471422018.png
> 
> 9.) PASSED - Start Search
> Was able to Click on btnSearch using FindElementByAccessibilityId
