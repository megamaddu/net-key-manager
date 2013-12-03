@echo off
if "%~1" == "" (
	echo Please specify a .nupkg file to push.
) else (
	NuGet.exe push "%~1" -Source http://nuget.vjstage.com
)
pause
