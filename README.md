# Asset Tracker v2.0EF

## Overview

Asset Tracker v2.0EF is a console-based application for tracking and managing computer and phone assets. The application allows users to add, list, update, and delete assets while displaying relevant asset details. It is built using Entity Framework Core for data management. This project was developed as part of an advanced postgraduate training course in Information Technology at Lexicon.
## Features

- **Add Asset**: Add new computer or phone assets.
- **List Assets**: View a list of all assets with detailed information.
- **Update Asset**: Update properties of existing assets.
- **Delete Asset**: Remove assets from the system.

## Installation

1. **Clone the Repository**
   ```sh
   git clone https://github.com/yourusername/asset-tracker.git
   cd asset-tracker
2. **Install NuGet Packages**
  Install the required NuGet packages using the .NET CLI
  ```sh
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  dotnet add package Microsoft.EntityFrameworkCore.Tools
  ```
3. **Setup Database**
  Update the connection string in MyDbContext.cs to point to your database.

5. **Run the Application**
  ```sh
  dotnet run
  ```
## Usage
Main Menu
  ```ruby
    █████  ███████ ███████ ███████ ████████ ████████ ██████   █████   ██████ ██   ██ ███████ ██████ v.2.0
   ██   ██ ██      ██      ██         ██       ██    ██   ██ ██   ██ ██      ██  ██  ██      ██   ██
   ███████ ███████ ███████ █████      ██       ██    ██████  ███████ ██      █████   █████   ██████ 
   ██   ██      ██      ██ ██         ██       ██    ██   ██ ██   ██ ██      ██  ██  ██      ██   ██
   ██   ██ ███████ ███████ ███████    ██       ██    ██   ██ ██   ██  ██████ ██   ██ ███████ ██   ██
  
  >> Pick an option:
  >> (1) Add an Asset
  >> (2) List Assets
  >> (3) Update Asset
  >> (4) Delete an Asset
  >> (0) Quit
  
  Enter a Number: 
  ```
## Adding an Asset

1. Select option 1 from the main menu.
2. Choose the type of asset (Computer or Phone).
3. Enter the asset details as prompted.

## Listing Assets

1. Select option 2 from the main menu.
2. View the list of all assets, including details such as Id, Type, Brand, Model, Office, Purchase Date, Price USD, Currency, and Local Price.

## Updating an Asset

1. Select option 3 from the main menu.
2. Choose the type of asset to update (Computer or Phone).
3. Enter the Id of the asset you want to update.
4. Specify the property to update and enter the new value.

## Deleting an Asset

1. Select option 4 from the main menu.
2. Choose the type of asset to delete (Computer or Phone).
3. Enter the Id of the asset you want to delete.
4. Confirm the deletion.

## Code Structure
**Program.cs** 
Handles the main logic for displaying menus and processing user inputs.

**Asset.cs** 
Defines the Asset class along with Computer and Phone classes inheriting from Asset.

**MyDbContext.cs** 
Configures the Entity Framework Core context for interacting with the database.

## Contributions
Contributions are welcome! Please submit a pull request or open an issue to discuss your ideas.

## License
This project is licensed under the MIT License.

## Acknowledgments
Thank you for using Asset Tracker v2.0EF. If you encounter any issues or have suggestions for improvement, feel free to contact us or submit an issue on GitHub.
