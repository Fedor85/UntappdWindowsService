SET installdir=E:\Services\UntappdWindowsService

Del /S /Q %installdir%\*
call PublishService.cmd
Xcopy /E /I publish %installdir%