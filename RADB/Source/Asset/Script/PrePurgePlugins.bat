@echo off
setlocal EnableDelayedExpansion

echo -------------------------------------------------------------------------------
echo    Pre-buid     ::     Clean Plugins

robocopy "..\..\Plugins\ " "Plugins\ " /PURGE /XF .gitkeep

if %errorlevel% leq 4 echo Pre-buid ExitCode: %errorlevel%&echo:& exit 0 else exit %errorlevel%