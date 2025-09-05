@echo off
setlocal EnableDelayedExpansion EnableExtensions

set "EventType=Post-build"
set "EventName=Move Plugins"

set "ProjectName=%1"
set "TargetDir=%2"
set "SolutionDir=%3"
set "PlatformName=%4"
set "ProjectFileName=%5"

set "Platform=x64"

echo ===============================================================================
echo    Project      ::     %ProjectName%
echo    Event        ::     Begin %EventType% -^> %EventName%
echo ===============================================================================

if /I %PlatformName%==AnyCPU (
	for /F "delims=" %%i in ('type "%ProjectFileName%" ^| findstr /R /C:"<Prefer32Bit>true</Prefer32Bit>"') do (
		set Platform=x86
	)
) else if /I %PlatformName%==x86 (
	set Platform=x86
)

robocopy "%SolutionDir%..\App\Bin\%Platform%\ " "%TargetDir%Plugins\%Platform%\ " System.Data.SQLite.dll /XO

robocopy "%TargetDir% " "%TargetDir%Plugins\ " /XF %ProjectName%.* *.json /IS /MOV /XD Plugins Data Deps

echo ===============================================================================
echo    Project      ::     %ProjectName%
echo    Event        ::     End %EventType% -^> %EventName%
echo    Result       ::     ExitCode: %errorlevel%
echo ===============================================================================
echo:

if %errorlevel% leq 4 (
	exit 0
) else (
	exit %errorlevel%
)