## This project is an Authentication and Authorization API built using ASP.NET Core customizable way. It provides user registration and login functionality using JWT (JSON Web Tokens) for authentication. :notebook: C#

### Technologies Used
- ASP.NET Core 8
- Entity Framework Core 8
- JWT (JSON Web Token)
- SQL Server
- Postman (Swagger optional, for testing the API)
  
### Costumizable Parts

```javascript
"ConnectionStrings": {
  "default": "server=yourservername; database=databasename; Trusted_Connection=True;TrustServerCertificate=True;" // (Last two parts for sql server security)
}
```

```javascript
"Jwt": {
  "SecretKey": "your-secret-key",
  "Issuer": "your-issuer",
  "Audience": "your-audience",
  "ExpireMinutes": "120" // => this depends on your project expectations, security etc.
}
```
### Steps
- Create migrations and update database
```terminal
dotnet ef migrations add InitialCreate // (this name is customizable but best practise for first migration using 'Initial', 'InitialCreate' etc.)
dotnet ef database update
```
- Running, testing api on Swagger or postman what you prefer
```terminal
dotnet run
```
- Register User -> POST /api/v1/auth/register
```javascript
//Request body
{
  "email": "user@example.com",
  "password": "UserPassword123"
}
// 200 OK if registration is successful.
// 400 Bad Request if there is an error.
```


- Login User -> POST /api/v1/auth/login
```javascript
//Request body
{
  "email": "user@example.com",
  "password": "UserPassword123"
}
// 200 OK with a JSON response containing the JWT token.
// 400 Bad Request if login fails.
```


### :incoming_envelope: Contact Information :incoming_envelope:

For any questions or further information, please don't hesitate to contact me :pray:

Email: merttopcu.dev@gmail.com

LinkedIn: https://www.linkedin.com/in/mert-topcu/
