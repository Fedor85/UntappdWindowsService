Client by UntappdWindowsService
To change the URL of connection to the service
Add to Startup project in App.config:

  <appSettings>
    <add key="UntappdWCFServiceUrlBase" value="" />
  </appSettings>
 
 Or this project add file UntappdWindowsService.Extension.dll.config (Copy Always), example content include in package :

 <?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="UntappdWCFServiceUrlBase" value="" />
  </appSettings>
</configuration>

