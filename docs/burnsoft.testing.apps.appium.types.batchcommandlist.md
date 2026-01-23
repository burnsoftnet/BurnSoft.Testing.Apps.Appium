[`< Back`](./)

---

# BatchCommandList

Namespace: BurnSoft.Testing.Apps.Appium.Types

Batch Command List to Run a List of Selnium commands and hold all the elements , values and commands

```csharp
public class BatchCommandList
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BatchCommandList](./burnsoft.testing.apps.appium.types.batchcommandlist)

## Properties

### **TestName**

Gets or sets the name of the test.

```csharp
public string TestName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the test.

### **CommandAction**

Command Action, find by automation id, name, Class Name, etc

```csharp
public AppAction CommandAction { get; set; }
```

#### Property Value

[AppAction](./burnsoft.testing.apps.appium.generalactions.appaction)<br>

### **Actions**

Action to perform, send command, click, nothing, etc.

```csharp
public MyAction Actions { get; set; }
```

#### Property Value

[MyAction](./burnsoft.testing.apps.appium.generalactions.myaction)<br>

### **ElementName**

Element Name

```csharp
public string ElementName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **SendKeys**

Keys or text to send

```csharp
public string SendKeys { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PassedFailed**

Gets or sets a value indicating whether [passed failed].

```csharp
public bool PassedFailed { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if [passed failed]; otherwise, `false`.

### **ReturnedValue**

Gets or sets the returned value.

```csharp
public string ReturnedValue { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The returned value.

### **ExpectedReturnedValue**

Gets or sets the expected returned value. This is used when you have the action set to compare

```csharp
public string ExpectedReturnedValue { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The expected returned value.

### **ReturnedFoundValue**

Gets or sets the found returned value.

```csharp
public string ReturnedFoundValue { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The expected returned value.

### **ReturnedValueBlankOk**

Gets or sets a value indicating whether [returned value blank ok].

```csharp
public bool ReturnedValueBlankOk { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if [returned value blank ok]; otherwise, `false`.

### **SleepInterval**

Gets or sets the sleep interval.

```csharp
public int SleepInterval { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The sleep interval.

### **TabCount**

Gets or sets the tab count. This is used for Action ClickOnElementAndTabOver

```csharp
public int TabCount { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The tab count.

### **TestNumber**

Gets or sets the test number automatically as the results run.

```csharp
public int TestNumber { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The test number.

### **TestNameLookUp**

Gets or sets the test name look up.

```csharp
public string TestNameLookUp { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The test name look up.

## Constructors

### **BatchCommandList()**

```csharp
public BatchCommandList()
```

---

[`< Back`](./)
