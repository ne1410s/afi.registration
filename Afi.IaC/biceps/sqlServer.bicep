@minLength(6)
param sqlAdminUser string

@secure()
@minLength(8)
param sqlAdminPass string

param collation string = 'Latin1_General_CI_AS'

param prefix string
param location string
param suffix string
param tags object

resource sqlServer 'Microsoft.Sql/servers@2021-02-01-preview' = {
  name: '${prefix}-sqlserver-${suffix}'
  location: location
  tags: tags
  properties: {
    administratorLogin: sqlAdminUser
    administratorLoginPassword: sqlAdminPass
    minimalTlsVersion: '1.2'
    publicNetworkAccess: 'Disabled'
  }
  
  resource afiDb 'databases' = {
    name: 'afiDb'
    location: location
    tags: resourceGroup().tags
    sku: {
      capacity: 5
      name: 'Basic'
      size: '2,147,483,648'
      tier: 'Basic'
    }
    properties: {
      collation: collation
    }
  }
}
