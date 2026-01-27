[`< Back`](./)

---

# JsonHandling

Namespace: BurnSoft.Testing.Apps.Appium.helpers

```csharp
public class JsonHandling
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [JsonHandling](./burnsoft.testing.apps.appium.helpers.jsonhandling)

## Constructors

### **JsonHandling()**

```csharp
public JsonHandling()
```

## Methods

### **ConvertTestSequenceToJson(List&lt;BatchCommandList&gt;, String&)**

Converts the test sequence to json.

```csharp
public static string ConvertTestSequenceToJson(List<BatchCommandList> lst, String& errOut)
```

#### Parameters

`lst` [List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
The BatchCommandList list test sequence that you created.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out, if error occurs.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
System.String json format.

### **ConvertTestSequenceToJsonFile(List&lt;BatchCommandList&gt;, String, String&)**

Converts the test sequence to json file and save to the file that you want to
 store the information at.

```csharp
public static bool ConvertTestSequenceToJsonFile(List<BatchCommandList> lst, string saveToPath, String& errOut)
```

#### Parameters

`lst` [List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
The BatchCommandList list test sequence that you created.

`saveToPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The save to path.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out, if error occurs.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
`true` if XXXX, `false` otherwise.

#### Exceptions

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>

### **ConvertJsonToBatchCommand(String, String&)**

Converts the json file to batch command to use in the test sequence.

```csharp
public static List<BatchCommandList> ConvertJsonToBatchCommand(string filePath, String& errOut)
```

#### Parameters

`filePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The file path.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>
The error out.

#### Returns

[List&lt;BatchCommandList&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
List&lt;BatchCommandList&gt;.

---

[`< Back`](./)
