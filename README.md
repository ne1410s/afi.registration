[![Build Status](https://app.travis-ci.com/ne1410s/afi.registration.svg?branch=main)](https://app.travis-ci.com/ne1410s/afi.registration)

## Test Coverage
```powershell
# obtain coverlet test coverage results
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings

# generate html summary report from coverlet coverage results
# PREREQ> dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -targetdir:coveragereport -reports:**/coverage.cobertura.xml -reporttypes:"html;htmlsummary" 
```

## Entity Framework
```powershell
# add a new migration
# PREREQ> dotnet tool install -g dotnet-ef
dotnet ef migrations add <MigrationName> -p Afi.Registration.Persistence -s Afi.Registration.Api
```

## IaC
```bash
# create resource group
az group create -l uksouth -n dev-afidemo-uks --tags env=dev workload=afidemo

# deploy cloud infrastructure
az deployment group create -g dev-afidemo-uks -p sqlAdminUser=afiadmin
```