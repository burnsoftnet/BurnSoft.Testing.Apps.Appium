# Developer Notes

This section is misc developer notes.  Things that might be needed for the project or helpers

## Required Software

* Visual Studio 2022
* .Net Framework 4.8.1
* Appium v3.2.1

### Appium Plugins

```
√ Listing available plugins
- execute-driver@5.0.3 [installed (npm)]
- images@4.0.4 [installed (npm)]
- inspector@2026.1.2 [installed (npm)]
- storage@1.0.4 [installed (npm)]
- relaxed-caps [not installed]
- universal-xml [not installed]
```

### Appium Drivers

```
√ Listing available drivers
- windows@5.1.5 [installed (npm)]
- uiautomator2@6.7.10 [installed (npm)]
- chromium@2.1.0 [installed (npm)]
- xcuitest [not installed]
- espresso [not installed]
- mac2 [not installed]
- safari [not installed]
- gecko [not installed]
```

## Other Helpers

* [xmldoc2md](https://charlesdevandiere.github.io/xmldoc2md/)

## xmldoc2md

'''cmd
xmldoc2md BurnSoft.Testing.Apps.Appium.dll --output docs --github-pages --back-button --index-page-name README
'''