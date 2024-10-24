Project was developed on Visual Studio 2022

1. Integration with Finatachart service is located in Asset.Infrastructure project 
		-> ClientServices folder 
		-> Fintachart folder
   To test Finatachart Integration service APIs you can lounch integration tests are located in Tests project.

2. a) To lounch project in docker container first install Docker Desktop
   b) To test WebApi that using Finatachart service you must just type Ctrl+F5 in VS 2022
   c) You will see swagger interface opened at browser and use them to test APIs

   GetAssetInfo Api to receive data online assets  you can do it only if call it by 
    any client program that create stream and will read step by step responses 
	or just will receive batch of data

	To receive token you should use GetToken API. I hardcoded request data to easy test API

Database Sqlite is used to store data and located in Asset.Infrastructure project
