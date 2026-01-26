[`< Back`](./)

---

# GeneralActions

Namespace: BurnSoft.Testing.Apps.Appium

The General Actions Class is the main class that will found to common functions used to communicate with appium. Just like I did
 in the selenium helper library, this will be the main class in case there are other special classes that need to be created to have it work with
 other OS's or application types, etc.

```csharp
public class GeneralActions : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [GeneralActions](./burnsoft.testing.apps.appium.generalactions)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Fields

### **InitPassed**

The initialize passed

```csharp
public bool InitPassed;
```

### **TestName**

The test name that will mostly be used for the screen capturing exception capturing

```csharp
public string TestName;
```

### **SettingsScreenShotLocation**

The settings screen shot location

```csharp
public string SettingsScreenShotLocation;
```

### **DoSleep**

Toggle the sleeping after a command was issue so you can see the results

```csharp
public bool DoSleep;
```

### **ScreenShotLocation**

The screen shot location/

```csharp
public List<string> ScreenShotLocation;
```

## Properties

### **DesktopSession**

Gets the desktop session.

```csharp
public WindowsDriver DesktopSession { get; private set; }
```

#### Property Value

WindowsDriver<br>
The desktop session.

### **SleepInterval**

Gets or sets the sleep interval.

```csharp
public int SleepInterval { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The sleep interval.

### **WaitForAppLaunch**

Gets or sets the wait for application launch.

```csharp
public int WaitForAppLaunch { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The wait for application launch.

### **ApplicationPath**

Gets or sets the application path.

```csharp
public string ApplicationPath { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The application path.

### **ErrorLists**

Gets or sets the error lists.

```csharp
public List<string> ErrorLists { get; set; }
```

#### Property Value

[List&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
The error lists.

### **NodeExecutable**

Gets or sets the node executable.

```csharp
public string NodeExecutable { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The node executable.

### **AppiumMainJs**

Gets or sets the appium main js.

```csharp
public string AppiumMainJs { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The appium main js.

## Constructors

### **GeneralActions()**

Initializes a new instance of the [GeneralActions](./burnsoft.testing.apps.appium.generalactions) class.

```csharp
public GeneralActions()
```

### **GeneralActions(String, String, Boolean)**

Initializes a new instance of the [GeneralActions](./burnsoft.testing.apps.appium.generalactions) class.

```csharp
public GeneralActions(string nodeExecutable, string appiumMainJs, bool debugMode)
```

#### Parameters

`nodeExecutable` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The node executable.

`appiumMainJs` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The appium main js.

`debugMode` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [debug mode].

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

### **Dispose()**

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### **Initialize(String)**

Initializes this instance.

```csharp
public void Initialize(string appUnderTest)
```

#### Parameters

`appUnderTest` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Exceptions

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
AppSession is null, check your settings

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
AppSession.SessionId is null, check your application path

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
DesktopSession is null, please check your settings

### **GetStringFromStep(List&lt;BatchCommandList&gt;, String, String&)**

Gets the string from step.

```csharp
public string GetStringFromStep(List<BatchCommandList> lst, string testName, String& errOut)
```

#### Parameters

`lst` [List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
The LST.

`testName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the test.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
System.String.

### **GetElements(String, String&, AppAction)**

Get Elemtns from item test

```csharp
public void GetElements(string automationId, String& errOut, AppAction myAction)
```

#### Parameters

`automationId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`myAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>

**Remarks:**

Might be able to delete later is not needed

### **PerformTabSelect(String, Int32, String&, AppAction)**

Performs the tab select, it will start at the automation id that you selected, 
 then you have the option to tab over x many times
 to another item then it will send a space key press to activate the final element.

```csharp
public bool PerformTabSelect(string automationId, int tabCount, String& errOut, AppAction myAction)
```

#### Parameters

`automationId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The automation identifier.

`tabCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The tab count.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

`myAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
My action.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if XXXX, `false` otherwise.

### **PerformAction(String, String, MyAction, String&, AppAction)**

Performs the action to execute on the application

```csharp
public bool PerformAction(string automationId, string value, MyAction action, String& errOut, AppAction myAction)
```

#### Parameters

`automationId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The automation identifier.

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value.

`action` [MyAction](./burnsoft.testing.apps.appium.generalactions.myaction)<br>
The action.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

`myAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
My action.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if XXXX, `false` otherwise.

### **PerformAction(String, String&, AppAction)**

Performs the action.

```csharp
public string PerformAction(string automationId, String& errOut, AppAction myAction)
```

#### Parameters

`automationId` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The automation identifier.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

`myAction` [AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>
My action.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
System.String.

### **RunBatchCommands(List&lt;BatchCommandList&gt;, String&)**

#### Caution

This was Replaced with the TestSequence.Run Function.  This will be removed later

---

Runs the batch commands.

```csharp
public List<BatchCommandList> RunBatchCommands(List<BatchCommandList> cmd, String& errOut)
```

#### Parameters

`cmd` [List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
The command.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

#### Exceptions

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
Error occured and the Driver is not active!

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
