Install and uninstall Windows services

Install using PowerShell:
New-Service -Name "Untappd Windows Service" -BinaryPathName C:\Services\UntappdWindowsService\UntappdWindowsService.exe

Uninstall using PowerShell:
Remove-Service -Name "Untappd Windows Service"
