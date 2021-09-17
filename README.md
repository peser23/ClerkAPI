# ClerkAPI
An api to query member data of One Hundred Seventeenth Congress

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
        <li><a href="#running-the-application">Running the App</a></li>
      </ul>
    </li>

  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project
  The ClerkAPI helps you query member data of the One Hundred Seventeenth Congress. Data is accurate as of 09/16/2021. 
  
  Note: Data from the XML file - https://clerk.house.gov/xml/lists/MemberData.xml was loaded to SQL Server database for this exercise
  
![image](https://user-images.githubusercontent.com/16979841/133715060-91b8324c-68d7-4708-87af-163c86379238.png)

### Built With

* []().NET Core 3.1 Web API
* []()SQL Server 2019
* []()Swagger
* []()Entity Framework Core

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.


### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/peser23/ClerkAPI.git
   ```
2. Database Setup
   * []()Connect to a SQL Server
   * []()Execute the SQL Query in file - ClerkData.SQL (file is in the Misc folder in the code repo). This would create the database - ClerkData and populate the data.
   * ![image](https://user-images.githubusercontent.com/16979841/133716224-03cd506d-4f3e-41e9-a04e-f60fa80f8cde.png)

 ### Running the application
 1. Open the solution file - Clerk.API.sln using Visual Studio 
 2. Modify the database connection string in the appsettings.json file to point to the database created in the "Database Setup" step.
 3. Press F5

