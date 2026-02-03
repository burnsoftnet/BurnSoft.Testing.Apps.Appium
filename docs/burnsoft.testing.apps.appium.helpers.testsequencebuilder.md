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
