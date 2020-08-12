# AspNetCoreSimpleCookieAuthenticationExample
*This project (startup.cs and account controller codes) is created for who want's to create CookieAuthentication based .net core MVC project infrastructure easly  with minimum effort. This is not complete project/solution, this project just includes "Startup.cs" and "AccountController" file codes for example.



*Lets Start for some requirements.

*You should install EF Tools globally with this command (You can check commands from microsoft site: https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)

dotnet tool install --global dotnet-ef

*If you installed EF Tools before please update it first

dotnet tool update --global dotnet-ef 

*We are ready for DB First Or Code First approach. Lets Install Microsoft.EntityFrameworkCore Packages :)
*We need theese packages from nuget package manager.

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
*Optionally you need
Newtonsoft.Json
Microsoft.AspNetCore.Mvc.NewtonsoftJson
Automapper
Automapper.Extensions.Microsoft.DependencyInjection

*After installation complate, you should use DB or Code First approach to build your context. Lets example DB First Approach

dotnet ef dbcontext scaffold "Server=localhost;Database=ApplicationDB;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer -c "ApplicationContextName" --force --project Application.ProjectName --startup-project Application.StartupProjectName


it is finished. You can apply "Startup.cs" and "AccountController.cs" file solutions
