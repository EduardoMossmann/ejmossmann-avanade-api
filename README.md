# ejmossmann-avanade-api
This repository is dedicated to the requested API for Avanade Technical Interview

This is a Blog system developed using .NET Core 9 and following principles of Domain-Driven Design, Clean Architecture and Clean Code.

## Authentication/Authorization

The authentication and authorization is currently done by in login endpoint using credentials defined in the appsettings, using JWT Token and role claims for User and Admin.

## Controllers

Two controllers were defined - LoginController and PostController

Login Controller handle the token generation only

Post Controller handle operations for:
- Create a Post
- Get List of Posts
- Create Comment to a Post
- Get a Post by Id

## Logging

Logging is currently done and stored in Logs/ folder inside **AvanadeBlog.Api** project

## Database

Database used was local SQL Server and configured using Entity Framework Core. 

Mappings, migrations and Database context are present in **AvanadeBlog.Infrastructure.Data** project

## Model Validation

There is model validation done using Fluent Validation library on the application side for the request view models.
This model validation reflects rules implied by Entity Framework Core mapping definitions