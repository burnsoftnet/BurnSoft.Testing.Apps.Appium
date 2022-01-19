@echo off
REM ******************************************************
REM * VS Buid Script when using GhostScript for chm file *
REM * and building nuget packages from Solution 	 *
REM ******************************************************
REM ******************************************************
REM USE Build Script below for Solution and upload to local server
REM $(SolutionDir)postbuild.bat $(SolutionDir) $(ProjectDir) $(ConfigurationName) $(ProjectName)
REM
REM USE Build Script below for Solution and upload to GitHub
REM $(SolutionDir)postbuild.bat $(SolutionDir) $(ProjectDir) $(ConfigurationName) $(ProjectName) -g
REM 
REM USE Build Script below for Project Only and upload to local server
REM $(ProjectDir)postbuild.bat $(SolutionDir) $(ProjectDir) $(ConfigurationName) $(ProjectName)
REM 
REM USE Build Script below for Project Only and upload to GitHub
REM $(ProjectDir)postbuild.bat $(SolutionDir) $(ProjectDir) $(ConfigurationName) $(ProjectName) -g

REM ******************************************************
SET SolutionDir=%1
SET ProjectDir=%2
SET ConfigurationName=%3
SET HELPFILENAME=%4
SET DEBUG="Debug"
SET RELEASE="Release"
SET NUGETEXT=nupkg
SET USENUGETSERVER=""
cd "%ProjectDir%"
copy /Y "%SolutionDir%Help\%HELPFILENAME%.chm" "%ProjectDir%bin\%ConfigurationName%\%HELPFILENAME%.chm"
cd "%ProjectDir%"
del /Q %ProjectDir%*.nupkg
SET LOCALNUGET=nuget.burnsoft.prod
SET USENUGETSERVER="http://%LOCALNUGET%"

if "%ConfigurationName%" == %DEBUG% (
	echo "nuget Dev packing"
	nuget pack
)

if "%ConfigurationName%" == %RELEASE% (
	echo "nuget Production Packing"
	nuget pack -Prop Configuration=Release
)

SET nupak=""
for /R "%CD%" %%f in (*.%NUGETEXT%) do (
	SET nupak=%%~nf
	echo %%~nf
)
echo "removing %HELPFILENAME%. from %nupak% "

FOR /F "tokens=* USEBACKQ" %%F IN (`%SolutionDir%StringReplace %nupak% %HELPFILENAME%.`) DO (
	SET ver=%%F
)
echo "PackageVersion: %ver%"

SET targetstatus=
for /f "skip=3 tokens=6" %%g in ('ping %LOCALNUGET%^|find /i "TTL"') do @set targetstatus=%%g
if "%targetstatus%"=="" echo %LOCALNUGET% is not reachable & goto GITHUB

echo "deleting %HELPFILENAME% %ver%"
nuget delete %HELPFILENAME% %ver% burnsoft -Source %USENUGETSERVER% -NonInteractive
echo "Uploading %nupak%.%NUGETEXT%"
nuget push %nupak%.%NUGETEXT% burnsoft -Source %USENUGETSERVER%

:GITHUB

if NOT "%~5"=="-g" goto END
echo "nuget guthub push"
nuget push %nupak%.%NUGETEXT% -source "github"

:NUGET

if NOT "%~5"=="-n" goto END
echo "Push to Nuget.org"
nuget push %nupak%.%NUGETEXT% -Source https://api.nuget.org/v3/index.json
:END
cd ..