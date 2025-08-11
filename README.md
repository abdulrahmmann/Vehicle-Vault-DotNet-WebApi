The project is structured using the Vertical Slice Architecture (VSA) pattern, which organizes the codebase by features rather than technical layers, improving maintainability and scalability. Core design principles like SOLID are strictly followed, ensuring clean separation of concerns.
Vehicle Vault is a comprehensive backend solution built with .NET 8 Web API designed to manage and display detailed vehicle information. The system covers a wide range of entities such as vehicle makes, models, submodels, colors, fuel types, features, drive types, bodies, categories, transmission types, and associated images, providing a robust and scalable foundation for any automotive-related application.

Key Features and Architecture
Clean Modular Architecture:
The project is structured using the Vertical Slice Architecture (VSA) pattern, which organizes the codebase by features rather than technical layers, improving maintainability and scalability. Core design principles like SOLID are strictly followed, ensuring clean separation of concerns.

CQRS with MediatR:
Commands and queries are separated to optimize performance and clarity, using MediatR to handle requests and decouple business logic from controllers. This enables easier testing, maintenance, and evolution of features.

Data Access Layer:
The project uses Entity Framework Core with robust Entity Configurations to map domain entities to the database. The data access is abstracted through Repository, Generic Repository, and Unit of Work design patterns, promoting reusable and testable data operations.

Authentication and Authorization:
Implements ASP.NET Core Identity for managing users with features including registration, login, and role-based authorization. The API supports JWT authentication and uses API versioning to manage evolving endpoints smoothly.

Validation and Standardized Responses:
Input data is validated with FluentValidation to enforce business rules consistently. API responses are wrapped in custom BaseResponse and AuthResponse classes to provide clear, descriptive, and uniform outputs across all endpoints.

Technologies Used
.NET 8, ASP.NET Core Web API

MediatR (CQRS pattern)

Entity Framework Core

ASP.NET Core Identity with JWT

FluentValidation

SQL Server (can be adapted)
