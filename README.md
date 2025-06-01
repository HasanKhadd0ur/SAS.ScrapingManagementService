
# 📡 SAS Scraping Management Service

The **SAS Scraping Management Service** is a backend microservice within the broader **Situational Awareness System (SAS)**. It is responsible for managing and scheduling scraping tasks from platforms like Telegram and others, using a modular architecture based on Clean Architecture and Domain-Driven Design (DDD).

---

## 🏗️ Project Structure

```

src/
├── SAS.ScrapingManagementService.API/              # API contracts and models for external communication
├── SAS.ScrapingManagementService.Application/      # Application layer: use cases, DTOs, interfaces
├── SAS.ScrapingManagementService.Domain/           # Domain layer: entities, value objects, domain services, events
├── SAS.ScrapingManagementService.Infrastructure/   # Infrastructure logic (persistence, services)
│   ├── SAS.ScrapingManagementService.Infrastructure.Persistence/  # Repositories, EF Core, Unit of Work
│   └── SAS.ScrapingManagementService.Infrastructure.Services/     # Kafka integration, scheduling, background workers
├── SAS.ScrapingManagementService.Presentation/     # Web API controllers and endpoints
└── SAS.ScrapingManagementService.SharedKernel/     # Common logic: CQRS, base entities, errors, events, interfaces

````

---

## 🧩 Key Features

- ✅ Platform & Source Management (e.g., Telegram, Twitter)
- ✅ Scheduled Scraping Task Dispatcher
- ✅ Kafka Integration for event-based communication
- ✅ Clean Architecture with vertical slices and CQRS
- ✅ Modular services with domain encapsulation
- ✅ Background Services for continuous scheduling

---

## 🛠️ Technologies Used

- **.NET 9**
- **Kafka** for message passing
- **MongoDB** (optional: for storing session/token data)
- **Entity Framework Core** (if relational DB is used)
- **Clean Architecture & DDD**
- **CQRS Pattern**

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Kafka](https://kafka.apache.org/)
- MongoDB (if integrated with your scrapers)

### Run the Service

```bash
# Navigate to the project root
cd SAS.ScrapingManagementService

# Restore and build
dotnet restore
dotnet build

# Run the Presentation project (Web API)
dotnet run --project src/SAS.ScrapingManagementService.Presentation
````

---

## 📬 Kafka Setup

Kafka configuration is managed via `KafkaSettings.cs`:

```jsonc
{
  "Kafka": {
    "BootstrapServers": "localhost:9092",
    "ScrapingTopic": "scraping-tasks"
  }
}
```

Make sure your local Kafka server is running and the topic exists.

---

## 🌐 API Endpoints

| Controller                | Route                  | Description                     |
| ------------------------- | ---------------------- | ------------------------------- |
| PlatformsController       | `/api/platforms`       | Manage scraping platforms       |
| ScrapingDomainsController | `/api/scrapingdomains` | Manage data domains to scrape   |
| DataSourcesController     | `/api/datasources`     | Manage data sources and configs |

---

## 🧠 Architecture Overview

This service follows **Clean Architecture** and **Vertical Slice Architecture** principles:

* **Domain Layer** encapsulates business logic.
* **Application Layer** orchestrates use cases.
* **Infrastructure Layer** handles persistence and integrations (Kafka, schedulers).
* **Presentation Layer** exposes APIs.
* **Shared Kernel** provides base types, CQRS interfaces, errors, and events.

---

## 🔄 Background Services

Scraping tasks are scheduled via a hosted background service, which:

* Runs periodically
* Pulls enabled platforms/domains from the database
* Sends task messages to Kafka for consumption by agents

Settings are managed in `ScrapingSchedulerSettings`.

---

## 📂 Shared Kernel Highlights

* `ICommand`, `IQuery`, `ICommandHandler`, `IQueryHandler`
* `DomainEvent`, `DomainException`, `DomainError`
* `BaseEntity`, `IAggregateRoot`
* Standard error codes and messages

---

## 📌 To Do

* [ ] Add validation using FluentValidation
* [ ] Add authentication and role-based authorization
* [ ] Add Swagger/OpenAPI documentation
* [ ] Add integration and unit tests

---

## 🤝 Contributing

This project is part of a graduation project. Contributions are welcome for educational and collaborative purposes.

---

## 📄 License

TBD – Currently used for academic research and non-commercial development.

---
