# PerformAction

The PerformAction is the main function that will execute the command to perform an action, ie double click, click, read, send text, etc. 
By default it will have the GeneralAction.AppActions set to FindElementByAccessiblityId, but you can change it to something else if you needed to
when you call the function. 

There are two functions with the same name, one returns a true or false and the other read and returns the value of the control.

The order for them goes by the AutomationId/name/xpath/ etc, the value to send text ( bool version only ) amd the action (click, double click, etc), and the errout container just in case there was an exception.


## Code Example Bool

Code was taken from the unit test  Since it is true or false return, there is not results posted in the test if it passes.

'''

bool value = false;
            try
            {
                value = _ga.PerformAction("AR-22", "", GeneralActions.MyAction.DoubleClick,out _errOut, GeneralActions.AppAction.FindElementByName);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
            }
            Assert.IsTrue(value);

'''


## Code Example String

Code was taken from the unit test.

'''

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


'''

### Result from string return

'''
Serial Number: DS37241
'''