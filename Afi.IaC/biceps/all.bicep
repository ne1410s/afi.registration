@minLength(6)
param sqlAdminUser string

@secure()
@minLength(8)
param sqlAdminPass string

@allowed([
  'dev'
  'test'
  'prod'
])
param prefix string = first(split(resourceGroup().name, '-'))

@minLength(3)
param location string = resourceGroup().location
param workload string = split(resourceGroup().name, '-')[1]
param extraTags object = {}

var suffix = '${workload}-${substring(location, 0, 3)}'
var tags = union(extraTags, resourceGroup().tags)

module insightsDeploy 'insights.bicep' = {
  name: 'insightsDeploy'
  params: {
    prefix: prefix
    location: location
    suffix: suffix
    tags: tags
  }
}

module storageAccountDeploy 'storageAccount.bicep' = {
  name: 'storageAccountDeploy'
  params: {
    prefix: prefix
    location: location
    suffix: suffix
    tags: tags
  }
}

module keyVaultDeploy 'keyVault.bicep' = {
  name: 'keyVaultDeploy'
  params: {
    secretKvps: [
      {
        key: 'storageConnection'
        value: storageAccountDeploy.outputs.storageConnection1
      }
    ]
    prefix: prefix
    location: location
    suffix: suffix
    tags: tags
  }
  dependsOn: [
    storageAccountDeploy
  ]
}

module sqlServerDeploy 'sqlServer.bicep' = {
  name: 'sqlServerDeploy'
  params: {
    sqlAdminPass: sqlAdminPass
    sqlAdminUser: sqlAdminUser
    prefix: prefix
    location: location
    suffix: suffix
    tags: tags
  }
  dependsOn: [
    storageAccountDeploy
  ]
}

module webAppPlanDeploy 'appServicePlan.bicep' = {
  name: 'webAppPlanDeploy'
  params: {
    isFunctionApp: false
    prefix: prefix
    location: location
    suffix: suffix
    tags: tags
  }
}

module webAppDeploy 'appService.bicep' = {
  name: 'webAppDeploy'
  params: {
    shortName: 'registration'
    isFunctionApp: false
    appPlanId: webAppPlanDeploy.outputs.appPlanId
    insightsKey: insightsDeploy.outputs.insightsKey
    prefix: prefix
    location: location
    suffix: suffix
    tags: tags
  }
  dependsOn: [
    webAppPlanDeploy
    insightsDeploy
  ]
}
