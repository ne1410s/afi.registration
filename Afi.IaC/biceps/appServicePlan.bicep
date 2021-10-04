param isFunctionApp bool

param prefix string
param location string
param suffix string
param tags object

var identifier = isFunctionApp ? 'plan' : 'asp'

resource appPlan 'Microsoft.Web/serverfarms@2021-01-01' = {
  name: '${prefix}-${identifier}-${suffix}'
  location: location
  tags: tags
  kind: isFunctionApp ? 'FunctionApp' : 'linux'
  sku: {
    name: isFunctionApp ? 'Y1' : 'F1'
    tier: isFunctionApp ? 'Dynamic' : 'Free'
  }
  properties: {
    reserved: isFunctionApp
  }
}

output appPlanId string = appPlan.id
