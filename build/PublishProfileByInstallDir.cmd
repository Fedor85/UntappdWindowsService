SET installdir=E:\Services

Del /S /Q %installdir%\*
call PublishProfile.cmd
Xcopy /E /I publish %installdir%