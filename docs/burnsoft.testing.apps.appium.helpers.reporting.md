[`< Back`](./)

---

# Reporting

Namespace: BurnSoft.Testing.Apps.Appium.helpers

Class containing Reporting Functions for the test Runs.

```csharp
public class Reporting
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Reporting](./burnsoft.testing.apps.appium.helpers.reporting)

## Constructors

### **Reporting()**

```csharp
public Reporting()
```

## Methods

### **GenerateResults(List&lt;BatchCommandList&gt;, String&)**

Generates the results from the Batch Command List to display the step number, testname, any returnedvalue results and
 if it failed, to return the element name that it failed at.

```csharp
public static string GenerateResults(List<BatchCommandList> cmdResults, String& errOut)
```

#### Parameters

`cmdResults` [List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
The command results.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
System.String.

### **AllTestsPassed(List&lt;BatchCommandList&gt;)**

Works through the results of the Batch Command list and looks to see if any of the tests where marked as failed,
 if some show up as failed then it will return false, else everything passed and it is true.

```csharp
public static bool AllTestsPassed(List<BatchCommandList> results)
```

#### Parameters

`results` [List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

---

[`< Back`](./)
