
# Web Service Development - C# Rest Azure by Matthias Pöttler

## Install/Run instructions 
- clone the Project from https://github.com/mpoettler/WebServiceAzuePoettler.git or https://mpoettler@webservicepoettler.scm.azurewebsites.net/WebservicePoettler.git .
- The Project can be opend in Visual Studio or Visual Studio Code.
- To open the swagger Site you can just run the Project or just open https://webservicepoettler.azurewebsites.net/swagger/index.html in your Browser.

## Description 
This App is a simpler Version to store some values AdmaModel(GNSS System) into an Azure Database. The Application is also deployed in Micrsoft Azrue. You can use HTTP Methods do add, change,delete or show values from the Azure SQL Database. The Technology Stack is C# with Azure.

## Documentation about technology and project 

### Azure 
- Azure is Cloud Computing Service from Mirosoft
- Azure provicdes many Services like SQL Databases
- Azure is not Free

### Swagger
- Swagger synchronizes API documentation with server and client 
- Allwos Interaction with the Rest Api 
- Provides Responses in Json or Xaml

## Known Issues
- Testing is missing
- Authentication is missing
- more Error Handling
- Frontend is missing

## Sources

- https://learn.microsoft.com/de-de/azure/app-service/app-service-
web-tutorial-rest-api
- https://learn.microsoft.com/en-us/azure/app-service/tutorial-
dotnetcore-sqldb-app?tabs=azure-portal%2Cvisualstudio-
deploy%2Cdeploy-instructions-azure-portal%2Cazure-portal-
logs%2Cazure-portal-resources#4---connect-the-app-to-the-database
- https://learn.microsoft.com/en-us/azure/active-directory-
b2c/enable-authentication-web-api?tabs=csharpclient
- https://hevodata.com/learn/azure-rest-apis/