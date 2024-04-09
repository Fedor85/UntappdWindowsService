SET installdir=E:\Services

Del /S /Q %installdir%\*
call PublishService.cmd
Xcopy /E /I publish %installdir%