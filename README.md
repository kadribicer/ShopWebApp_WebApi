<h2>The Retail Store Discounts</h2>
<details>
  <summary>Expand for detail</summary>
<br>
  
-	If the user is an employee of the store, he gets a 30% discount
-	If the user is an affiliate of the store, he gets a 10% discount
- If the user has been a customer for over 2 years, he gets a 5% discount.
-	For every $100 on the bill, there would be a $ 5 discount (e.g. for $ 990, you get $ 45 as a discount).
-	The percentage based discounts do not apply on groceries.
-	A user can get only one of the percentage based discounts on a bill.
- Create a RESTful API that returns the final invoice amount including discount when an invoice is issued.

</details>

#
<h3>Instructions and General Information</h3>
  
A small web application created to simulate store discounts.

- .NET Class Libraries
- WebUI - Asp .NET Core
- RESTful API - Asp .NET Core
- Layer Architecture
- MVC Desing Pattern
- Repository Desing Pattern
- UnitofWork Design Pattern
- Dependency Injection Design Pattern
- Entity Framework - Code First

#
<h3>Materials used for the test</h3>

- XUnit Unit tests
- SonarQube (Screenshot added)
- Postman (API Tests) (Screenshot added.)
- Swagger (API Tests) (Screenshot added.)
- VS Code Analyzer

#
<h3>General Information regarding the solution</h3>

- The scenario was simulated using the database.
- In order to run the project, you need to fill the Context field in the Data Layer or simulate the database.
- Unit tests run with the Visual Studio test explorer.
- Tested with server. (Screenshot added)
- You can see the application live.

#
<h3>Application endpoint</h3>

You can access the application from this link. [Web Application](http://kadribicer.shop).

#
<h3>API Endpoint</h3>

- Http Method - GET
- You can access the api endpoint here. [API Endpoint](http://kadribicer.com).

Example request

```
  {
    "subTotalPrice": 2514,
    "totalPrice": 1679,
    "discountPrice": 835,
    "invoiceDate": "2022-05-04T15:37:20.3345451",
    "userId": 1,
    "user": null,
    "invoiceDetails": [
      {
        "productName": "Strawberry",
        "productUnitPrice": 3,
        "productCategory": "Groceries",
        "productTotalQuantity": 4,
        "productTotalPrice": 12,
        "invoiceId": 52,
        "id": 193,
        "status": true
      },
      {
        "productName": "Avocado",
        "productUnitPrice": 2,
        "productCategory": "Groceries",
        "productTotalQuantity": 1,
        "productTotalPrice": 2,
        "invoiceId": 52,
        "id": 194,
        "status": true
      },
      {
        "productName": "Notebook",
        "productUnitPrice": 350,
        "productCategory": "Electronic",
        "productTotalQuantity": 4,
        "productTotalPrice": 1400,
        "invoiceId": 52,
        "id": 195,
        "status": true
      },
      {
        "productName": "Phone",
        "productUnitPrice": 275,
        "productCategory": "Electronic",
        "productTotalQuantity": 4,
        "productTotalPrice": 1100,
        "invoiceId": 52,
        "id": 196,
        "status": true
      }
    ],
    "id": 52,
    "status": true
  }

```

<h3>Other Information</h3>


* The given scenario is developed not only as a web application but also as a console application.
- You can access the console application from the link here. [Console Application](https://github.com/kadribicer/ShopConsoleApp_WebApi).


