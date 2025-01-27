@echo off
setlocal EnableDelayedExpansion

echo -------------------------------------------------------------------------------
echo    Post-buid    ::     Move Plugins

set PlatformName=%2
set ProjectFileName=%3
set Platform=x64

set ProjectName=%1

if /I %PlatformName%==AnyCPU (
	for /F "delims=" %%i in ('type "%ProjectFileName%" ^| findstr /R /C:"<Prefer32Bit>true</Prefer32Bit>"') do (
		set Platform=x86
	)	
) else if /I %PlatformName%==x86 (
	set Platform=x86
)

robocopy "..\..\..\..\App\Bin\%Platform% " ".\Plugins\%Platform% " SQLite.Interop.dll /XO

robocopy ".\ " "Plugins\ " /XF *.exe *.config *.json *.manifest %ProjectName%.pdb /XD Plugins Data Deps /IS /MOV

if %errorlevel% leq 4 echo Post-buid ExitCode: %errorlevel%&echo:& exit 0 else exit %errorlevel%
