# Butter Mountain
## description
This was a final project made for an intermediate Web Development course at Lane Community College.

## tools used
 - MySql 8
 - .NET 6
 - ASP.NET MVC
 - EntityFramework
 - Microsoft Identity
   
## how to run
 - clone or download the repository
 - change directory to ButterMtn-296 ```cd ButterMtn-296```
 ### Add appsettings file
  - create appsettings.json in root directory
  - copy this template and replace USERNAME and PASSWORD with the credentials for your local database
   ```
   {
     "Logging": {
        "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
      "MySqlConnection": "server=127.0.0.1;uid=USERNAME;pwd=PASSWORD;database=ButterMtn-296",
    }
   }
```

### Use Entity framework to initialize the schema
 - make a new migration using 
   ```dotnet ef migrations add Initial```
 - update the database with ```dotnet ef database update```

### Run the code 
 - In Visual Studio, start debugging by clicking the play button
