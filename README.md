Install and uninstall Windows services

Install using PowerShell:
New-Service -Name "UntappdWindowsService" -BinaryPathName C:\Services\UntappdWindowsService\UntappdWindowsService.exe

Uninstall using PowerShell:
Remove-Service -Name "UntappdWindowsService"
