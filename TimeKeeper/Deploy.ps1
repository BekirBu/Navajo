#$connectionString = $OctopusParameters["TimeKeeper"]
 
#Write-Host "Connection string is: $connectionString"
 
.\migrate.exe TimeKeeper.DAL.dll /startupConfigurationFile="TimeKeeper.DAL.dll.config" /force #/connectionString="$($connectionString)" /connectionProviderName="System.Data.SqlClient"