[`< Back`](./)

---

# AppiumHelper

Namespace: BurnSoft.Testing.Apps.Appium

Class AppiumHelper class that contains functions to help start up and 
 communicate with the appium 3.x.x server

```csharp
public class AppiumHelper
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AppiumHelper](./burnsoft.testing.apps.appium.appiumhelper)

## Fields

### **driver**

The driver once the appium server is up and running and the 
 StartDriverConnection has been called and set

```csharp
public WindowsDriver driver;
```

## Constructors

### **AppiumHelper(String, Boolean)**

Initializes a new instance of the [AppiumHelper](./burnsoft.testing.apps.appium.appiumhelper) class.

```csharp
public AppiumHelper(string appiumApp, bool debugMode)
```

#### Parameters

`appiumApp` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The appium application.

`debugMode` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [debug mode].

### **AppiumHelper(String, String, Boolean)**

Initializes a new instance of the [AppiumHelper](./burnsoft.testing.apps.appium.appiumhelper) class.

```csharp
public AppiumHelper(string nodeExecutable, string appiumMainJs, bool debugMode)
```

#### Parameters

`nodeExecutable` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The node executable.

`appiumMainJs` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The appium main js.

`debugMode` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [debug mode].

### **AppiumHelper(FileInfo, FileInfo, Boolean)**

Initializes a new instance of the [AppiumHelper](./burnsoft.testing.apps.appium.appiumhelper) class.

```csharp
public AppiumHelper(FileInfo nodeExecutable, FileInfo appiumMainJs, bool debugMode)
```

#### Parameters

`nodeExecutable` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
The node executable.

`appiumMainJs` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
The appium main js.

`debugMode` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [debug mode].

### **AppiumHelper(String, String, Boolean, String, Boolean)**

Initializes a new instance of the [AppiumHelper](./burnsoft.testing.apps.appium.appiumhelper) class.

```csharp
public AppiumHelper(string appiumApp, string testApp, bool debugMode, string testAppParameters, bool fullReset)
```

#### Parameters

`appiumApp` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The appium application.

`testApp` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The test application.

`debugMode` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [debug mode].

`testAppParameters` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The test application parameters.

`fullReset` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [full reset].

## Methods

### **SendError(String)**

Sends the error.

```csharp
protected void SendError(string value)
```

#### Parameters

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value.

### **SendJunkError(String)**

Sends the junk error.

```csharp
protected void SendJunkError(string value)
```

#### Parameters

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The value.

### **StartAppium(String, Int32, Int32)**

Starts the appium process using the AppiumServiceBuilder class to help determin if the process is already running
 if not then spin an instance to start it up

```csharp
public bool StartAppium(string ip, int port, int startup_wait)
```

#### Parameters

`ip` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The ip of the appium server.

`port` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The port of the appium server.

`startup_wait` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The startup wait time for the process to come up.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if true, process is running or was already running, `false` if error occured.

### **StopAppiumServer()**

Stops the appium server.

```csharp
public bool StopAppiumServer()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if XXXX, `false` otherwise.

### **SetDesiredCapabilities(String, String, Boolean)**

"Capabilities" is the name given to the set of parameters used to start an Appium session. The information 
 in the set is used to describe what sort of "capabilities" you want your session to have, for example, a 
 certain mobile operating system or a certain version of a device. When you start your Appium session, 
 your Appium client will include the set of capabilities you've defined as an object in the 
 JSON-formatted body of the request. Capabilities are represented as key-value pairs, with values 
 allowed to be any valid JSON type, including other objects. Appium will then examine the capabilities 
 and make sure that it can satisfy them before proceeding to start the session and return an ID 
 representing the session to your client library.
 See Guide at https://appium.io/docs/en/3.1/guides/caps/

```csharp
public AppiumOptions SetDesiredCapabilities(string testApp, string testAppParameters, bool fullReset)
```

#### Parameters

`testApp` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The Application Under Test

`testAppParameters` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The test application parameters.

`fullReset` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if set to `true` [full reset].

#### Returns

AppiumOptions<br>
AppiumOptions.

### **StartDriverConnection(String, Int32, Int32, String)**

Starts the driver connection which will use the local application under test options
 that was set when you passed the aut to the init function in this class by startup

```csharp
public bool StartDriverConnection(string ip, int port, int wait, string httpProtocol)
```

#### Parameters

`ip` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The appium ip.

`port` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The appium port.

`wait` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The wait.

`httpProtocol` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The HTTP protocol.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if XXXX, `false` otherwise.

### **StartDriverConnection(AppiumOptions, String, Int32, Int32, String)**

Starts the driver connection. and the application under test when the options was created
 externaly and passed to this function to startup and test the application under test

```csharp
public bool StartDriverConnection(AppiumOptions options, string ip, int port, int wait, string httpProtocol)
```

#### Parameters

`options` AppiumOptions<br>
The appium aut options.

`ip` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The appium ip.

`port` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The appium port.

`wait` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The wait.

`httpProtocol` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The HTTP protocol.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if XXXX, `false` otherwise.

#### Exceptions

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
Error Starting Appium

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
AppSession is null, check your settings

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
AppSession.SessionId is null, check your application path

## Events

### **Errors**

Event handler for when exception errors occur in the functions

```csharp
public event EventHandler<string> Errors;
```

### **JunkErrors**

Occurs when [junk errors] are thrown, don't care but might be useful.

```csharp
public event EventHandler<string> JunkErrors;
```

---

[`< Back`](./)
