@echo off
NuGet.exe pack -Build -IncludeReferencedProjects -Properties "Configuration=Release"
pause
