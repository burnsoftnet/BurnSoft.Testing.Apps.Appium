[`< Back`](./)

---

# TestSequenceBuilder

Namespace: BurnSoft.Testing.Apps.Appium.helpers

Class TestSequenceBuilder is just a simple class that will have the 
 functions needed to append steps together and just pass the settings 
 you want to the function that will perform the function you want and 
 append it or add it to the Range of your test recipe.

```csharp
public class TestSequenceBuilder
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TestSequenceBuilder](./burnsoft.testing.apps.appium.helpers.testsequencebuilder)

## Constructors

### **TestSequenceBuilder()**

```csharp
public TestSequenceBuilder()
```

## Methods

### **ClickOnElement(String, String, Boolean, AppAction)**

Clicks the on element or verify that it exists.

```csharp
public static List<BatchCommandList> ClickOnElement(string testName, string element, bool verify, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`verify` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [verify].

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **DoubleClickOnElement(String, String, Boolean, AppAction)**

Doubles the click on element or verifies the contol.

```csharp
public static List<BatchCommandList> DoubleClickOnElement(string testName, string element, bool verify, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`verify` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [verify].

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **SendText(String, String, String, Boolean, AppAction)**

Sends the text to the control or verifies it.

```csharp
public static List<BatchCommandList> SendText(string testName, string element, string value, bool verify, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value.

`verify` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [verify].

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **ClearAndSendText(String, String, String, Boolean, AppAction)**

Clears the and send text to the control.

```csharp
public static List<BatchCommandList> ClearAndSendText(string testName, string element, string value, bool verify, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value.

`verify` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [verify].

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **ClickOnElementAndTabOver(String, String, Int32, Boolean, AppAction)**

Clicks the on element and tab over.

```csharp
public static List<BatchCommandList> ClickOnElementAndTabOver(string testName, string element, int tabCount, bool verify, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`tabCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The tab count.

`verify` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [verify].

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **GetValueFromPreviousTestAndCompareByTestName(String, String, String, AppAction)**

Gets the name of the value from previous test and compare by test.

```csharp
public static List<BatchCommandList> GetValueFromPreviousTestAndCompareByTestName(string testName, string element, string fromTestName, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`fromTestName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of from test.

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **GetValueFromPreviousTestAndCompareByTestNumber(String, String, Int32, AppAction)**

Gets the value from previous test and compare by test number.

```csharp
public static List<BatchCommandList> GetValueFromPreviousTestAndCompareByTestNumber(string testName, string element, int fromTestNumber, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`fromTestNumber` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
From test number.

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **SendKeyDownToControl(String, String, Int32, AppAction)**

Sends the key down to control.

```csharp
public static List<BatchCommandList> SendKeyDownToControl(string testName, string element, int repeatXTimes, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`repeatXTimes` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The repeat x times.

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **SendKeyUpToControl(String, String, Int32, AppAction)**

Sends the key up to control.

```csharp
public static List<BatchCommandList> SendKeyUpToControl(string testName, string element, int repeatXTimes, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`repeatXTimes` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The repeat x times.

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **SendEnterKeyToControl(String, String, Int32, AppAction)**

Sends the enter key to control.

```csharp
public static List<BatchCommandList> SendEnterKeyToControl(string testName, string element, int repeatXTimes, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`repeatXTimes` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The repeat x times.

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **DeleteFile(String, String)**

Deletes the file.

```csharp
public static List<BatchCommandList> DeleteFile(string testName, string fileNameAndPath)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`fileNameAndPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file name and path.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **FailIfFileExists(String, String)**

Fails if file exists.

```csharp
public static List<BatchCommandList> FailIfFileExists(string testName, string fileNameAndPath)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`fileNameAndPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file name and path.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **PassIfFileExists(String, String)**

Passes if file exists.

```csharp
public static List<BatchCommandList> PassIfFileExists(string testName, string fileNameAndPath)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`fileNameAndPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file name and path.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **GetFocusOnWindow(String, String)**

Gets the focus on window.

```csharp
public static List<BatchCommandList> GetFocusOnWindow(string testName, string fileNameAndPath)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`fileNameAndPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file name and path.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **ReadValue(String, String, Boolean, AppAction)**

Reads the value.

```csharp
public static List<BatchCommandList> ReadValue(string testName, string element, bool verify, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`verify` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [verify].

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **ReadValueAndCompare(String, String, String, Boolean, AppAction)**

Reads the value and compare.

```csharp
public static List<BatchCommandList> ReadValueAndCompare(string testName, string element, string value, bool verify, AppAction commandAction)
```

#### Parameters

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`element` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The element.

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value.

`verify` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [verify].

`commandAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
The command action.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **Sleep500()**

Sleep for 500ms this instance.

```csharp
public static List<BatchCommandList> Sleep500()
```

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

### **Sleep(Int32)**

Sleeps the specified interval.

```csharp
public static List<BatchCommandList> Sleep(int interval)
```

#### Parameters

`interval` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The interval in ms.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

---

[`< Back`](./)
