# ASP.NET Project for Asset Management

This is a C# project that allows registration and login of users. After logging in, users can manage their assets. Each asset has an Id, Title, and Data field where Data is represented as a JSON string.

## Prerequisites

- Visual Studio 2022 or later
- .NET 6 SDK
- SQL Lite

## Getting Started

1. Clone this repository to your local machine.
2. Open the solution file `SolveX.AssetManagment.sln` in Visual Studio.
3. In the `appsettings.json` file, update the `DefaultConnection` with desired sqlite database name (Default is `SolveXDatabase.db`).
4. The database and its migrations will be created automaticlly 
5. Build and run the application.

## Functionality

### User Registration and Login

- Users can register with a unique username and a password.
- Users can login with their username and password and will be provided with JWT token.
- Passwords are securely hashed before being stored in the database.

### Asset Management

- Users can view a list of their assets by id, title or property value thats stored in the data field.
- Users can add a new asset by providing a title, id and a JSON string for the Data field.
- Users can view assets that are linked to their asset
- Users with admin privalge can export properties of asset to excel

### Endpoints
Once the application is started the endpoints will be available at https://localhost:7048/swagger/index.html. <br>
For user authentication you will first need to login and then add JWT token to requests by:
1. Clicking the authorize button on the top right corner
![authorize](https://user-images.githubusercontent.com/81408310/219330950-5ef652c9-9b8c-4474-9a28-484ace53b9cf.png)
2. Inserting the token prefixed by `Bearer {token}`
![authorizeStep2](https://user-images.githubusercontent.com/81408310/219331229-f884ef90-8039-473f-aaf1-401a305394ad.png)
3. Finally click the `Authorize` button

## Technologies Used

- ASP.NET 6
- Entity Framework Core 7.0.3
- SQL Lite
