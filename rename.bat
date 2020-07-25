REM See original at: https://stackoverflow.com/questions/52067917/how-to-replace-all-spaces-by-underscores-in-all-file-and-folder-names-recursivel

@echo off
setlocal EnableExtensions DisableDelayedExpansion
set "StartFolder=C:\Users\cex\source\repos\UnderstandingLanguageExt"

cd /D %SystemRoot%
set "RenameError="

rem Rename all files containing at least one space character in file name.
for /F "delims=" %%I in ('dir "%StartFolder%\* *" /A-D /B /S 2^>nul') do call :RenameFile "%%I"

rem Rename all folders containing at least one space character in folder name.
for /F "delims=" %%I in ('dir "%StartFolder%\* *" /AD /B /S 2^>nul') do call :RenameFolder "%%I"

if defined RenameError echo/& pause
rem Restore initial environment and exit this batch file.
endlocal
goto :EOF


:RenameFile
set "NewFileName=%~nx1"
set "NewFileName=%NewFileName: =_%"

set "FileAttributes=%~a1"
if "%FileAttributes:~3,1%" == "h" %SystemRoot%\System32\attrib.exe -h %1

ren %1 "%NewFileName%" 2>nul
if errorlevel 1 goto ErrorFileRename

if "%FileAttributes:~3,1%" == "h" %SystemRoot%\System32\attrib.exe +h "%~dp1%NewFileName%"
goto :EOF

:ErrorFileRename
echo Error renaming file %1
set "RenameError=1"
if "%FileAttributes:~3,1%" == "h" %SystemRoot%\System32\attrib.exe +h %1
goto :EOF


:RenameFolder
set "NewFolderName=%~nx1"
set "NewFolderName=%NewFolderName: =_%"

set "FolderPath=%~dp1"
if not exist "%FolderPath%" set "FolderPath=%FolderPath: =_%"
set "FullFolderName=%FolderPath%%~nx1"
if not exist "%FullFolderName%\" set "RenameError=1" & goto :EOF

for %%J in ("%FullFolderName%") do set "FolderAttributes=%%~aJ"
if "%FolderAttributes:~3,1%" == "h" %SystemRoot%\System32\attrib.exe -h "%FullFolderName%"

ren "%FullFolderName%" "%NewFolderName%" 2>nul
if errorlevel 1 goto ErrorFolderRename

if "%FolderAttributes:~3,1%" == "h" %SystemRoot%\System32\attrib.exe +h "%FolderPath%%NewFolderName%"
goto :EOF

:ErrorFolderRename
echo Error renaming folder "%FullFolderName%"
set "RenameError=1"
if "%FolderAttributes:~3,1%" == "h" %SystemRoot%\System32\attrib.exe +h "%FullFolderName%"
goto :EOF
