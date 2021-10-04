param shortName string
param appPlanId string
param insightsKey string
param isFunctionApp bool
param extraAppSettings array = []

param prefix string
param location string
param suffix string
param tags object

var globalAppSettings = [
  {
    name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
    value: insightsKey
  }
]

resource appService 'Microsoft.Web/sites@2021-01-01' = {
  name: '${prefix}-${toLower(shortName)}app-${suffix}'
  location: location
  tags: tags
  kind: isFunctionApp ? 'functionapp' : 'app'
  properties: {
    serverFarmId: appPlanId
    reserved: isFunctionApp
    httpsOnly: true
    siteConfig: {
      appSettings: union(globalAppSettings, extraAppSettings)
      ftpsState: 'Disabled'
    }
  }
}

output appName string = appService.name
output appUrl string = 'https://${appService.properties.defaultHostName}/api'
