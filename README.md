[![Codacy Badge](https://app.codacy.com/project/badge/Grade/1f1959d4a297433fb4ab7d2d177a82c8)](https://www.codacy.com/gh/ne1410s/afi.registration/dashboard)
[![Build Status](https://app.travis-ci.com/ne1410s/afi.registration.svg?branch=main)](https://app.travis-ci.com/ne1410s/afi.registration)
[![Coverage Status](https://coveralls.io/repos/github/ne1410s/afi.registration/badge.svg)](https://coveralls.io/github/ne1410s/afi.registration?branch=main)

## Endpoint location
An endpoint is published [here](https://dev-registrationapp-afidemo-uks.azurewebsites.net/swagger/index.html).

Policies `AA-000001` to `AA-001000` are available for demo.

## Test Coverage
```powershell
# obtain coverlet test coverage results
dotnet test --settings coverlet.runsettings

# generate html summary report from coverlet coverage results
# PREREQ> dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -targetdir:coveragereport -reports:**/coverage.cobertura.xml -reporttypes:"html;htmlsummary" 
```

## Entity Framework
```powershell
# add a new migration
# PREREQ> dotnet tool install -g dotnet-ef
dotnet ef migrations add <MigrationName> -p Afi.Registration.Persistence -s Afi.Registration.Api

# obtains a script for production / SQL Server
$env:ASPNETCORE_ENVIRONMENT="Production";dotnet ef dbcontext script -p Afi.Registration.Persistence -s Afi.Registration.Api
```

## IaC
```bash
# create resource group
az group create -l uksouth -n dev-afidemo-uks --tags env=dev workload=afidemo

# deploy cloud infrastructure
az deployment group create -g dev-afidemo-uks -f .\Afi.IaC\biceps\all.bicep -p sqlAdminUser=afiadmin
```
