# NorthwindCustomersAPI

### This is an API that provides CRUD (Create, Read, Update, and Delete) operations on the customers table of the Northwind database. This API is built using ASP.NET and C#.

<br>

## Getting Started
----------------
1. Clone this repository onto your local machine
2. Open the solution file in Visual Studio
3. Build the solution to install the necessary dependencies
4. Make sure you have access to the Northwind database
5. Update the appsettings.json file with your MySQL database credentials.
6. Start the API by running the project in Visual Studio.
7. The API will now be accessible at "http://localhost:****/customers"
<br></br>

# API Endpoints
## This API provides the following endpoints:
<br>
GET /customers

Retrieves a list of all customers from the database.
_____

GET /customers/{id}

Retrieves a customer by their id from the database.
_____

POST /customers

Creates a new customer in the database.
_____

PUT /customers/{id}

Updates an existing customer in the database or creates the customer if they do not already exist in the database.
_____

DELETE /customers/{id}

Deletes a customer from the database by their id.
<br></br>

# Credits and Special Thanks

This API was developed by Team 1 (Andrew, Danielle, Danyal, Jacob, Nooreen, Patrick and Talal) as part of a project for Tech211. Special thanks to Peter B for helping to work out some issues in early development.