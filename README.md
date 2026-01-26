# BurnSoft.Testing.Apps.Appium

![](https://img.shields.io/badge/license-MIT-blue.svg?maxAge=3600) 

The BurnSoft.Testing.Apps.Appium Library was created to help simplify the Appium library functions to help build tests for your applications quick and easy.
Currently this project is IN PROGRESS, and as soon as it starts coming together, more documentation will be presented.

## Older WinDriver Source

The Older WinDriver ( pre appium ) test library can still be found in the [release/v1.0.0.12](https://github.com/burnsoftnet/BurnSoft.Testing.Apps.Appium/tree/release/v1.0.0.12) branch.
The Master Branch was merged with an updated, ( STILL WORKING ON ) appium version with updated libraries to get rid of the dependabot pull requests and complaints that the library was not up to date
So if you are looking for the WinDriver Version visit the release branch listed above.

## Documentation

Details about the [API Dcoumentation](docs/README.md) are available to view, also the Help file is also available in the package
[Developer Notes](docs/DeveloperNotes.md) Is avavilabe for more information about what was installed on the developer machine.

## Resources
- [BurnSoft.Universal](https://github.com/burnsoftnet/BurnSoft.Universal)
- [Appium](https://appium.io/)

## Requirements

- Windows 10,11
- Appium installed via npm
- Developer Mode Enabled on Windows 10/11 Machine.

[![Donate](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=JSW8XEMQVH4BE)]


## Release Log

### v3.1.2.44-beta

* Updated to use .net framework 4.8.1
* Changed Unit Test to NUnit
* Updated The library to use the appium application instead of the old winDriver function
* Updated to Work with Appium Server 3.1.2
* Updated Version to match the support for the appium Server, so this will start with 3.1.2
* REFACTOR GeneralActions to use the New Appium helper to manage the appium helper and the AUT.
* Added Functions to Convert the List BatchCommands to json to save to file and use later.
* Added function to load the saved json file be used in the List BatchCommands to use in the Test Sequencer.

### v1.x

- Initial Release