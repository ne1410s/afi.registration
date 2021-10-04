param secretKvps array = []

param prefix string
param location string
param suffix string
param tags object

resource keyVault 'Microsoft.KeyVault/vaults@2019-09-01' = {
  name: '${prefix}-kv-${suffix}'
  location: location
  tags: tags
  properties: {
    tenantId: subscription().tenantId
    accessPolicies: []
    sku: {
      family: 'A'
      name: 'standard'
    }
  }

  resource secret 'secrets' = [for kvp in secretKvps: {
    name: kvp.key
    properties: {
      value: kvp.value
    }
  }]
}

output vaultName string = keyVault.name
