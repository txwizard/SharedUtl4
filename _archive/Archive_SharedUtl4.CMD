@echo off
echo %0 Begin

:: 2012/08/25 DAG Replace hard coded paths with environment variables.

echo.
echo Project Assembly = %USERPROFILE%\My Documents\Visual Studio 2010\Projects\WizardWrx_Libs\SharedUtl4\SharedUtl4\bin\Release\WizardWrx.SharedUtl4.dll
echo Backup Directory = C:\COMMON_DATA\Assorted_Zips\program_source\Visual Studio 2010
echo.

"%USERPROFILE%\My Documents\Visual Studio 2010\Projects\Tools_for_Programmers\WWSourceCodeArchiver\WWSourceCodeArchiver\bin\Release\WWSourceCodeArchiver.exe" "-p%USERPROFILE%\My Documents\Visual Studio 2010\Projects\WizardWrx_Libs\SharedUtl4\SharedUtl4\bin\Release\WizardWrx.SharedUtl4.dll" "-tC:\COMMON_DATA\Assorted_Zips\program_source\Visual Studio 2010"

wwpause.exe
echo %0 Done!