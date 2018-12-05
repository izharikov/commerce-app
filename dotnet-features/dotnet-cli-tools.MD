# `dotnet` cli commands
## Create solution
```
dotnet new sln --name [Name Of Solution]
```
## Create classlib
```
dotnet new classlib --name [Project Name]
```
## Create web project
```
dotnet new web --name [Project Name]
```
## Add project to solution
```
dotnet sln [Name Of Solution] add [Path To .csproj file]
```
## Add project reference to another project
```
dotnet add [Path to .csproj] reference [Path to referenced .csproj file]
```
More information in [Microsoft Official Documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x).