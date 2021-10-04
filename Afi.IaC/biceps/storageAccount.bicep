param privateBlobContainerNames array = []
param publicBlobContainerNames array = []

param prefix string
param location string
param suffix string
param tags object

resource storageAccount 'Microsoft.Storage/storageAccounts@2021-04-01' = {
  name: '${prefix}store${replace(suffix, '-', '')}'
  location: location
  tags: tags
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
  properties: {
    minimumTlsVersion: 'TLS1_2'
    networkAcls: {
      bypass: 'AzureServices'
      defaultAction: 'Allow'
    }
    supportsHttpsTrafficOnly: true
    encryption: {
      services: {
        blob: {
          enabled: true
        }
      }
      keySource: 'Microsoft.Storage'
    }
  }

  resource blobservice 'blobServices' = {
    name: 'default'

    resource privateContainer 'containers' = [for privateBlobContainerName in privateBlobContainerNames: {
      name: privateBlobContainerName
      properties: {
        publicAccess: 'None'
      }
    }]

    resource publicContainer 'containers' = [for publicBlobContainerName in publicBlobContainerNames: {
      name: publicBlobContainerName
      properties: {
        publicAccess: 'Container'
      }
    }]
  }
}

var storageAccountKeys = listKeys(storageAccount.id, storageAccount.apiVersion).keys
var endpointSuffix = environment().suffixes.storage
var connectionPrefix = 'DefaultEndpointsProtocol=https;AccountName=${storageAccount.name}'

output storageConnection1 string = '${connectionPrefix};AccountKey=${storageAccountKeys[0].value};EndpointSuffix=${endpointSuffix}'
output storageConnection2 string = '${connectionPrefix};AccountKey=${storageAccountKeys[1].value};EndpointSuffix=${endpointSuffix}'
output host string = storageAccount.name
output endpoint string = 'https://${storageAccount.name}.${environment().suffixes.storage}'
