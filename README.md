## ⛔Never push sensitive information such as client id's, secrets or keys into repositories including in the README file⛔

# Das-EarlyConnect-API

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

[![Build Status](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_apis/build/status/_projectname_?branchName=master)](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_build/latest?definitionId=_projectid_&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=_projectId_&metric=alert_status)](https://sonarcloud.io/dashboard?id=_projectId_)
[![Jira Project](https://img.shields.io/badge/Jira-Project-blue)](https://skillsfundingagency.atlassian.net/secure/RapidBoard.jspa?rapidView=564&projectKey=_projectKey_)
[![Confluence Project](https://img.shields.io/badge/Confluence-Project-blue)](https://skillsfundingagency.atlassian.net/wiki/spaces/_pageurl_)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg?longCache=true&style=flat-square)](https://en.wikipedia.org/wiki/MIT_License)

## Description

The Early Connect Project aims to bridge the gap between young people in education and apprenticeship opportunities by connecting them with employers and providers at an earlier stage. This initiative, in collaboration with UCAS, is part of a broader program that includes employer and school engagement.

Key Highlights:
Provides a new way for students in schools and colleges to explore apprenticeship options.
Collaborates with UCAS to enhance accessibility to apprenticeship pathways. (As of Nov 2024 UCAS are no longer part of this system)
Includes a digital workstream alongside employer and school engagement efforts.
Pilot launch in Autumn 2023, targeting three regions: North East, Lancashire, and London.
This project is designed to create stronger connections between education and employment, helping young people make informed career choices at the right time.`

## How It Works

Early connect: GAA

A collection of functions, API’s and micro-site that supports a triage form where users can enter their details and get sent 

## 🚀 Installation

### Pre-Requisites
```
* A clone of this repository
* A code editor that supports .Net8.0
* An Azure Service Bus instance
```
### Config

This utility uses the standard Apprenticeship Service configuration. All configuration can be found in the [das-employer-config repository](https://github.com/SkillsFundingAgency/das-employer-config).

AppSettings.Development.json file
```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "ConfigurationStorageConnectionString": "UseDevelopmentStorage=true;",
    "ConfigNames": "SFA.DAS.EarlyConnect.Api",
    "EnvironmentName": "LOCAL",
    "Version": "1.0"
}
```

Azure Table Storage config

Row Key: SFA.DAS.EarlyConnect.API_1.0

Partition Key: LOCAL

Data:

```json
{
  "EarlyConnectApi": {
    "DatabaseConnectionString": "Data Source=.;Initial Catalog=SFA.DAS.EarlyConnectApi.Database;Integrated Security=True",
    "BaseUrl": "https://at-earlyconnect.apprenticeships.education.gov.uk/",
    "NServiceBusConfiguration": {
      "SharedServiceBusEndpointUrl": "Endpoint=sb://das-at-shared-ns.servicebus.windows.net/",
      "NServiceBusLicense": " "
    }
  },
  "AzureAd": {
    "tenant": "*************.onmicrosoft.com",
    "identifier": "https://*********.onmicrosoft.com/das-test-*******"
  }
}
```
Check [das-employer-config repository](https://github.com/SkillsFundingAgency/das-employer-config) 
## 🔗 External Dependencies


## Technologies

```
* .NET 8.0  
* ASP.NET Core  
* Azure Identity  
* Application Insights  
* Microsoft Identity Client (MSAL)  
* Azure Table Storage  
* NUnit
* Moq
```

## 🐛 Known Issues


```

```
