## How to add migration
```
 dotnet ef migrations add [Migration Name] --context [DbContext Class Name]
```
## How to apply migrations
```
dotnet ef database update --context [DbContext Class Name]
```
