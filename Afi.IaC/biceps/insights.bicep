param prefix string
param location string
param suffix string
param tags object

resource insights 'Microsoft.Insights/components@2015-05-01' = {
  name: '${prefix}-insights-${suffix}'
  location: location
  tags: tags
  kind: 'web'
  properties:{
    Application_Type: 'web'
  }
}

output insightsId string = insights.id
output insightsKey string = insights.properties.InstrumentationKey
