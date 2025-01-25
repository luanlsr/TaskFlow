# TaskFlow Project

## Overview

TaskFlow is a sample project designed to showcase modern development practices using a layered architecture with Domain-Driven Design (DDD). It incorporates technologies like ASP.NET Core, Angular, NHibernate, FluentNHibernate, and various tools for CI/CD, testing, and observability.

This project serves as a learning tool and a starting point for building scalable and maintainable applications.

## Architecture

TaskFlow follows a clean architecture approach, separating responsibilities into multiple layers:

- **Apresentation**: Contains the API and UI projects (e.g., Angular for the frontend, ASP.NET Core for the backend).
- **Application**: Handles application logic, DTOs, and orchestrates use cases.
- **Domain**: Contains the core domain logic, including entities, aggregates, and domain services.
- **Domain.Core**: Holds shared contracts, base classes, and interfaces like IRepository, IService, and IUnitOfWork.
- **Infrastructure**: Implements the repository pattern, UnitOfWork, and integrates with external resources like the database and AWS services.
- **CrossCutting**: Provides shared services, like logging, dependency injection, and exception handling.

## Technologies Used

### Backend

- **ASP.NET Core**: Framework for building REST APIs.
- **NHibernate & FluentNHibernate**: ORM and fluent configuration for database interactions.
- **AutoMapper**: For mapping between domain entities and DTOs.
- **AWS Services**: Integration with S3, SQS, etc.
- **Dependency Injection**: Managed via ASP.NET Core's DI container.

### Frontend

- **Angular**: SPA framework for building user interfaces.

### Observability

- **New Relic**: Application performance monitoring.
- **Kibana & Grafana**: Visualization of logs and metrics.

### Testing

- **xUnit**: Unit testing framework.
- **FluentAssertions**: For expressive and readable assertions.
- **NSubstitute**: Mocking library for testing dependencies.
- **FizzWare**: Test data generation.

### CI/CD

- **Git & Bitbucket**: Version control and repository management.
- **Jenkins**: Automation server for CI/CD pipelines.

## Project Structure

TaskFlow
├── Apresentation
│   ├── TaskFlow.Api
│   ├── Configurations
│   ├── Controllers
│   ├── DTOs
│   ├── Extensions
│   ├── Middlewares
│   └── Properties
├── Application
│   ├── TaskFlow.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Mappings
│   └── UseCases
├── CrossCutting
│   ├── TaskFlow.CrossCutting.Logging
│   │   ├── Loggers
│   │   └── Configuration
│   ├── TaskFlow.CrossCutting.IoC
│   │   ├── Containers
│   │   └── Configurations
│   └── TaskFlow.CrossCutting.Utils
│       ├── Helpers
│       ├── Extensions
│       └── Constants
├── Domain
│   ├── TaskFlow.Domain
│   ├── Aggregates
│   ├── Entities
│   ├── Events
│   ├── Exceptions
│   ├── Interfaces
│   └── ValueObjects
├── Domain.Core
│   ├── TaskFlow.Domain.Core
│   ├── Aggregates
│   ├── Entities
│   ├── Events
│   ├── Interfaces
│   ├── Notifications
│   └── ValueObjects
├── Infrastructure
│   ├── TaskFlow.Infrastructure.Data
│   ├── AWS
│   ├── Persistence
│   ├── Repositories
│   └── Services
├── Tests
│   ├── TaskFlow.Tests.Unit
│   ├── TaskFlow.Tests.Integration
│   ├── Application
│   ├── Domain
│   ├── Infrastructure
│   └── Utilities


## Getting Started

### Prerequisites

**Development Tools:**

- Visual Studio 2022 or later
- Node.js (for Angular frontend)
- Docker (optional for containerized setups)

**Database:**

- PostgreSQL (default configuration)

### Setup Instructions

1. **Clone the Repository**

   ```bash
   git clone https://bitbucket.org/your-repo/taskflow.git
   cd taskflow
   ```

### Install Dependencies

**Backend**: Restore NuGet packages

```bash
dotnet restore
```

Frontend: Navigate to the Angular project and install npm packages
```bash cd TaskFlow/Apresentation/TaskFlow.Api
npm install
```

Configure Environment
Update the appsettings.json and environment variables as needed (e.g., database connection strings, AWS credentials).

Run Migrations
Apply the database migrations using FluentNHibernate.

```bash 
dotnet ef database update
```

Run the Application
Start the backend API and frontend application.

```bash 
dotnet run --project TaskFlow/Apresentation/TaskFlow.Api
```

Testing
Run the unit and integration tests to ensure the application is working as expected.

```bash
dotnet test
```

Contributing
1 - Fork the repository.
2 - Create a feature branch:

```bash
git checkout -b feature/your-feature
```

Commit your changes:
```bash
git commit -m 'Add your feature'
```

Push to the branch:
```bash
git push origin feature/your-feature
```

Open a Pull Request.


License
This project is licensed under the MIT License. See the LICENSE file for details.


Contact
For questions or support, contact [luan.ramalhosilva@gmail.com].

Agora tudo está dentro do bloco de código markdown. Se precisar de mais alguma coisa, é só avisar!








