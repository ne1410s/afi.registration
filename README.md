### Test Coverage
```powershell
# obtain coverlet test coverage results
dotnet test --collect:"XPlat Code Coverage"`

# generate html summary report from coverlet coverage results
# PREREQ> dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -targetdir:coveragereport -reports:**/coverage.cobertura.xml -reporttypes:htmlsummary
```

### Entity Framework
```powershell
# add a new migration
# PREREQ> dotnet tool install -g dotnet-ef
dotnet ef migrations add <MigrationName> -p Afi.Registration.Persistence -s Afi.Registration.Api
```