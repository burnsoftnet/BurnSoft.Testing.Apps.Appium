[`< Back`](./)

---

# TestSequence

Namespace: BurnSoft.Testing.Apps.Appium

Class TestSequence is the old BatchRun Function that was in the General Actions section to help grow
 this Batch Run with extra functions but still use the General Actions to run the tests.
 Implements the [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

```csharp
public class TestSequence : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TestSequence](./burnsoft.testing.apps.appium.testsequence)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Fields

### **DebugMode**

The debug mode toggle

```csharp
public bool DebugMode;
```

## Constructors

### **TestSequence(Boolean)**

Initializes a new instance of the [TestSequence](./burnsoft.testing.apps.appium.testsequence) class.

```csharp
public TestSequence(bool debugMode)
```

#### Parameters

`debugMode` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [debug mode].

### **TestSequence(String, String, Boolean, Boolean)**

Initializes a new instance of the [TestSequence](./burnsoft.testing.apps.appium.testsequence) class.

```csharp
public TestSequence(string nodeExecutable, string appiumMainJs, bool debugMode, bool breakOnFail)
```

#### Parameters

`nodeExecutable` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The node executable.

`appiumMainJs` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The appium main js.

`debugMode` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [debug mode].

`breakOnFail` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Stop the tests if a step fails

### **TestSequence(String, String, Boolean, String, Boolean, String, Boolean)**

Initializes a new instance of the [TestSequence](./burnsoft.testing.apps.appium.testsequence) class.

```csharp
public TestSequence(string nodeExecutable, string appiumMainJs, bool debugMode, string settingsScreenShotLocation, bool doSleep, string testName, bool breakOnFail)
```

#### Parameters

`nodeExecutable` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The node executable.

`appiumMainJs` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The appium main js.

`debugMode` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [debug mode].

`settingsScreenShotLocation` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The settings screen shot location.

`doSleep` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [do sleep].

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`breakOnFail` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Stop the tests if a step fails

## Methods

### **SendError(String)**

Sends the error the the event handler

```csharp
protected void SendError(string message)
```

#### Parameters

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The message.

### **SendDebug(String)**

Sends the debug messages only when the debug 
 flag is set to true

```csharp
protected void SendDebug(string message)
```

#### Parameters

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The message.

### **Run(String, String)**

Runs the specified application under test using the json command file

```csharp
public List<BatchCommandList> Run(string appUnderTest, string commandPath)
```

#### Parameters

`appUnderTest` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The application under test.

`commandPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The command path.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

#### Exceptions

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>

### **Run(String, List&lt;BatchCommandList&gt;, String&)**

Runs the specified application under test.

```csharp
public List<BatchCommandList> Run(string appUnderTest, List<BatchCommandList> cmd, String& errOut)
```

#### Parameters

`appUnderTest` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The application under test.

`cmd` [List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
The commands to run aginst the aut.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

#### Exceptions

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
Error occured and the Driver is not active!

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
Was Not able to {msg}{Environment.NewLine}{errOut}

### **Dispose()**

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

## Events

### **ErrorCatcher**

Occurs when En exception is caught

```csharp
public event EventHandler<string> ErrorCatcher;
```

### **DebugLog**

Occurs when [debug log].

```csharp
public event EventHandler<string> DebugLog;
```

---

[`< Back`](./)
