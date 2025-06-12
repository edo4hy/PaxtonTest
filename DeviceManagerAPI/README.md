Start API 
`dotnet restore`
`dotnet run`


API 
- GET - returns all devices 
- GET/{id} - returns device 
- POST - saves device to data store 

Stream - 
- Updated changes pushed SingalR stream


Simulate singal change config 
- Frequency up update AppSettings.LoopDelaySeconds in appsettings
- values are randomly generated based on AppSettings.PercentageChangeFactor 
- Offline online change facotr - AppSettings.OfflineChangeFactor


2 data stores - 
InMemory / SQL Server 
- Update in appsettings.json 
- SQL server requires connection - connectionString in appsetting.json

Spin up sql server 
`docker compose up -d`



Things to improve:
Simulate devices is a bit janky - especially the way its updating the repo
Forced in async a bit on the InMemory version 
Tests - just did a few for structure

Logging 
Exception handling 


To spin up the Angular app 

`ng serve`

Get data 3 ways - 
Fetch Now - call Get - returns all devices 
Start Polling - set a interval to call back every 15 seconds
Connect stream - SingalR data stream - updated on data change


Things to improve 
Style 
Structure 
Test
Really quite a lot 