@echo off
setlocal EnableDelayedExpansion EnableExtensions

set "EventType=Pre-build"
set "EventName=Clean Plugins"

set "ProjectName=%1"
set "TargetDir=%2"

echo ===============================================================================
echo    Project      ::     %ProjectName%
echo    Event        ::     Begin %EventType% -^> %EventName%
echo ===============================================================================

rd /s /q "%TargetDir%Plugins\"

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