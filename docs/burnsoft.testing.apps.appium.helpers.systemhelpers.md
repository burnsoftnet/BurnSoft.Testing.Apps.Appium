[`< Back`](./)

---

# SystemHelpers

Namespace: BurnSoft.Testing.Apps.Appium.helpers

Class SystemHelpers is just that functions that are system related that help with 
 the testing you want to do and the unit tests

```csharp
public class SystemHelpers
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SystemHelpers](./burnsoft.testing.apps.appium.helpers.systemhelpers)

## Constructors

### **SystemHelpers()**

```csharp
public SystemHelpers()
```

## Methods

### **FindExePath(String)**

Locates the full path of an executable file by searching the environment's PATH.

```csharp
public static string FindExePath(string exeName)
```

#### Parameters

`exeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the executable file (e.g., "cmd.exe").

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The fully qualified path to the file, or null if not found.

### **KillAppByName(String, String&)**

Kills the name of the application by.

```csharp
public static bool KillAppByName(string appName, String& errOut)
```

#### Parameters

`appName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the application.

`errOut` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

---

[`< Back`](./)
