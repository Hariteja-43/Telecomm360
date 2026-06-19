# Telecomm360 - Telecom Operations Management System

## Overview
Telecomm360 is a comprehensive ASP.NET Core Web API for managing telecom operations including customer management, billing, network resources, analytics, and compliance. Built for scalability and maintainability with clean architecture principles.

## Technology Stack
- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server LocalDB
- **ORM**: Entity Framework Core
- **Authentication**: JWT Bearer Tokens
- **Testing**: NUnit + Moq
- **API Documentation**: Swagger/OpenAPI

## Architecture Diagram

```
┌─────────────────────────────────────────────────────────────────────────┐
│                         PRESENTATION LAYER                               │
│  ┌─────────────────────────────────────────────────────────────────┐   │
│  │                    20 API Controllers                            │   │
│  │  Auth│Products│Orders│Customers│Subscribers│Network│Alarms│...  │   │
│  └─────────────────────────────────────────────────────────────────┘   │
│                              ↓ HTTP/JSON ↓                              │
└─────────────────────────────────────────────────────────────────────────┘
                                    ↓
┌─────────────────────────────────────────────────────────────────────────┐
│                          MIDDLEWARE LAYER                                │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐                 │
│  │ JWT Auth     │  │ Exception    │  │ CORS         │                 │
│  │ Middleware   │  │ Handling     │  │ Policy       │                 │
│  └──────────────┘  └──────────────┘  └──────────────┘                 │
└─────────────────────────────────────────────────────────────────────────┘
                                    ↓
┌─────────────────────────────────────────────────────────────────────────┐
│                         BUSINESS LOGIC LAYER                             │
│  ┌─────────────────────────────────────────────────────────────────┐   │
│  │                    20 Service Classes                            │   │
│  │  • AuthService          • ProductService                         │   │
│  │  • CustomerService      • SubscriberService                      │   │
│  │  • OrderService         • NetworkResourceService                 │   │
│  │  • BillingService       • AnalyticsService                       │   │
│  │  • AlarmService         • IncidentService                        │   │
│  │  • ComplianceService    • AuditService                           │   │
│  └─────────────────────────────────────────────────────────────────┘   │
│                       Business Rules & Validation                        │
└─────────────────────────────────────────────────────────────────────────┘
                                    ↓
┌─────────────────────────────────────────────────────────────────────────┐
│                         DATA ACCESS LAYER                                │
│  ┌─────────────────────────────────────────────────────────────────┐   │
│  │                   20 Repository Classes                          │   │
│  │  • UserRepository       • CustomerRepository                     │   │
│  │  • OrderRepository      • ProductRepository                      │   │
│  │  • AlarmRepository      • InvoiceRepository                      │   │
│  │  • NetworkResourceRepository  • AnalyticsRepository              │   │
│  └─────────────────────────────────────────────────────────────────┘   │
│                         AppDbContext (EF Core)                           │
└─────────────────────────────────────────────────────────────────────────┘
                                    ↓
┌─────────────────────────────────────────────────────────────────────────┐
│                           DATABASE LAYER                                 │
│  ┌─────────────────────────────────────────────────────────────────┐   │
│  │              SQL Server LocalDB (Telecomm360DB)                  │   │
│  │                                                                   │   │
│  │  Tables (20):                                                     │   │
│  │  Users, Roles, Customers, Subscribers, Products, Orders          │   │
│  │  NetworkResources, ProvisioningTasks, Invoices, Payments         │   │
│  │  Alarms, Incidents, Notifications, UsageRecords                  │   │
│  │  KPIReports, AnalyticsDatasets, ComplianceReports                │   │
│  │  RetentionPolicies, AuditLogs, Dashboards                        │   │
│  └─────────────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────────────┘
```

## Domain Model & Relationships

```
┌─────────────┐         ┌─────────────┐         ┌─────────────┐
│   Roles     │────1:N──│   Users     │────1:N──│  AuditLogs  │
└─────────────┘         └─────────────┘         └─────────────┘
                               │
                               │ 1:N
                               ↓
                        ┌─────────────┐
                        │  Customers  │
                        └─────────────┘
                               │
                               │ 1:N
                               ↓
                        ┌─────────────┐         ┌─────────────┐
                        │ Subscribers │────1:N──│Notifications│
                        └─────────────┘         └─────────────┘
                               │                        
                               │ N:1                    
                               ↓                        
┌─────────────┐         ┌─────────────┐         ┌─────────────┐
│  Products   │────1:N──│   Orders    │────1:N──│ Provisioning│
└─────────────┘         └─────────────┘         │   Tasks     │
                               │                 └─────────────┘
                               │ 1:N
                               ↓
                        ┌─────────────┐         ┌─────────────┐
                        │  Invoices   │────1:N──│  Payments   │
                        └─────────────┘         └─────────────┘

┌─────────────┐         ┌─────────────┐         ┌─────────────┐
│   Alarms    │────1:N──│  Incidents  │         │Usage Records│
└─────────────┘         └─────────────┘         └─────────────┘

┌─────────────┐         ┌─────────────┐         ┌─────────────┐
│ KPIReports  │         │ Analytics   │         │ Compliance  │
│             │         │  Datasets   │         │  Reports    │
└─────────────┘         └─────────────┘         └─────────────┘
```

## System Workflow

### 1. Authentication Flow
```
┌──────────┐         ┌──────────┐         ┌──────────┐         ┌──────────┐
│  Client  │────────▶│Auth      │────────▶│Auth      │────────▶│User      │
│          │  Login  │Controller│ Validate│Service   │  Query  │Repository│
│          │         │          │         │          │         │          │
│          │◀────────│          │◀────────│          │◀────────│          │
│          │  JWT    │          │ User DTO│          │  User   │          │
└──────────┘  Token  └──────────┘         └──────────┘  Entity └──────────┘
```

**Steps**:
1. Client sends POST /api/auth/login with credentials
2. AuthController validates and forwards to AuthService
3. AuthService verifies credentials via UserRepository
4. Generate JWT token with user claims (UserID, Email, Role)
5. Return token with 3-hour expiry
6. Client includes token in Authorization header for subsequent requests

### 2. Order Management Flow
```
┌──────────┐    ┌──────────┐    ┌──────────┐    ┌──────────┐    ┌──────────┐
│ Customer │───▶│  Order   │───▶│  Order   │───▶│Provision │───▶│ Network  │
│          │    │Controller│    │ Service  │    │  Task    │    │ Resource │
└──────────┘    └──────────┘    └──────────┘    └──────────┘    └──────────┘
   Creates           ↓               ↓               ↓               ↓
   Order         Validates      Creates          Assigns         Allocates
                 Request        Order          Provisioning      Capacity
                                  ↓
                            ┌──────────┐
                            │ Invoice  │
                            │ Generated│
                            └──────────┘
```

**Steps**:
1. POST /api/orders - Create order with ProductID, SubscriberID
2. Validates product exists and subscriber exists
3. Creates order with status "Pending"
4. POST /api/orders/{id}/submit - Submit order
5. Creates provisioning task linked to order
6. Allocates network resources
7. POST /api/orders/{id}/fulfill - Complete provisioning
8. Generates invoice for billing
9. Updates order status to "Fulfilled"

### 3. Billing & Usage Flow
```
┌──────────┐         ┌──────────┐         ┌──────────┐         ┌──────────┐
│Subscriber│────────▶│  Usage   │────────▶│ Invoice  │────────▶│ Payment  │
│          │  Uses   │  Record  │ Triggers│ Service  │ Process │ Service  │
│          │ Service │          │         │          │         │          │
└──────────┘         └──────────┘         └──────────┘         └──────────┘
                          ↓                     ↓                     ↓
                     Track Usage          Create Invoice        Record Payment
                     (Data/Minutes)       (Amount/DueDate)      (Status/Method)
```

**Steps**:
1. POST /api/usage - Record usage (SubscriberID, DataUsed, Minutes)
2. Usage service aggregates usage data
3. POST /api/invoices - Generate invoice based on usage
4. Calculate amount based on pricing model
5. POST /api/payments - Process payment
6. Update invoice status to "Paid"
7. Generate notification to subscriber

### 4. Network Monitoring Flow
```
┌──────────┐         ┌──────────┐         ┌──────────┐         ┌──────────┐
│ Network  │────────▶│  Alarm   │────────▶│ Incident │────────▶│Notification│
│ Resource │ Detects │ Service  │ Creates │ Service  │ Alerts  │  Service   │
│          │  Issue  │          │         │          │         │            │
└──────────┘         └──────────┘         └──────────┘         └──────────┘
                          ↓                     ↓                     ↓
                     Log Alarm           Assign Priority        Notify Staff/
                   (Severity/Status)     Track Resolution       Subscribers
```

**Steps**:
1. POST /api/alarms - System detects network issue
2. Create alarm with severity (Critical, High, Medium, Low)
3. POST /api/incidents - Convert alarm to incident
4. Assign incident to support team
5. POST /api/notifications - Send notifications
6. Track resolution and update incident status
7. Close alarm when resolved

### 5. Analytics & Reporting Flow
```
┌──────────┐         ┌──────────┐         ┌──────────┐         ┌──────────┐
│  Raw     │────────▶│Analytics │────────▶│  KPI     │────────▶│Dashboard │
│  Data    │ Process │ Dataset  │ Analyze │ Reports  │ Display │ Service  │
│          │         │ Service  │         │ Service  │         │          │
└──────────┘         └──────────┘         └──────────┘         └──────────┘
Orders/Usage/              ↓                     ↓                     ↓
Alarms/Revenue      Aggregate Data        Calculate KPIs        Visualize
                    Transform/Clean       (Revenue/Churn)        Metrics
```

**Steps**:
1. POST /api/datasets - Ingest raw data
2. Transform and clean data
3. POST /api/reports - Generate KPI reports
4. Calculate metrics (Revenue, Churn Rate, ARPU)
5. GET /dashboard - Retrieve dashboard data
6. Display aggregated metrics and trends

## API Endpoints Reference

### Authentication APIs
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/auth/register | Register new user |
| POST | /api/auth/login | Login and get JWT token |
| POST | /api/auth/forgot-password | Request password reset |
| POST | /api/auth/reset-password | Reset password with token |

### Customer Management APIs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/customers | Get all customers |
| GET | /api/customers/{id} | Get customer by ID |
| POST | /api/customers | Create new customer |
| PUT | /api/customers/{id} | Update customer (partial) |
| DELETE | /api/customers/{id} | Delete customer |

### Subscriber Management APIs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/subscribers | Get all subscribers |
| GET | /api/subscribers/{id} | Get subscriber by ID |
| POST | /api/subscribers | Create new subscriber |
| PUT | /api/subscribers/{id} | Update subscriber (partial) |
| DELETE | /api/subscribers/{id} | Delete subscriber |

### Product & Order APIs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /products | Get all products |
| GET | /products/{id} | Get product by ID |
| POST | /products | Create new product |
| PUT | /products/{id} | Update product |
| DELETE | /products/{id} | Delete product |
| GET | /api/orders | Get all orders |
| GET | /api/orders/{id} | Get order by ID |
| POST | /api/orders | Create new order |
| POST | /api/orders/{id}/submit | Submit order |
| POST | /api/orders/{id}/cancel | Cancel order |
| POST | /api/orders/{id}/fulfill | Fulfill order |

### Network Management APIs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/networkresources | Get all resources |
| GET | /api/networkresources/{id} | Get resource by ID |
| POST | /api/networkresources | Create resource |
| PUT | /api/networkresources/{id} | Update resource |
| DELETE | /api/networkresources/{id} | Delete resource |
| GET | /api/provisioningtasks | Get all tasks |
| GET | /api/provisioningtasks/{id} | Get task by ID |
| POST | /api/provisioningtasks | Create task |
| PUT | /api/provisioningtasks/{id} | Update task status |

### Billing APIs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/invoices | Get all invoices |
| GET | /api/invoices/{id} | Get invoice by ID |
| POST | /api/invoices | Create invoice |
| GET | /api/payments | Get all payments |
| GET | /api/payments/{id} | Get payment by ID |
| POST | /api/payments | Process payment |

### Monitoring & Incident APIs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/alarms | Get all alarms |
| GET | /api/alarms/{id} | Get alarm by ID |
| POST | /api/alarms | Create alarm |
| PUT | /api/alarms/{id} | Update alarm |
| DELETE | /api/alarms/{id} | Delete alarm |
| GET | /api/incidents | Get all incidents |
| GET | /api/incidents/{id} | Get incident by ID |
| POST | /api/incidents | Create incident |
| PUT | /api/incidents/{id} | Update incident |

### Analytics & Reporting APIs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/datasets | Get all datasets |
| GET | /api/datasets/{id} | Get dataset by ID |
| POST | /api/datasets | Create dataset |
| PUT | /api/datasets/{id} | Update dataset |
| POST | /api/datasets/{id}/refresh | Refresh dataset |
| GET | /api/reports | Get all KPI reports |
| GET | /api/reports/{id} | Get report by ID |
| GET | /api/reports/scope/{scope} | Get reports by scope |
| POST | /api/reports | Generate report |
| GET | /dashboard | Get dashboard metrics |

### Compliance & Audit APIs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /compliance-reports | Get all reports |
| GET | /compliance-reports/{id} | Get report by ID |
| POST | /compliance-reports | Create report |
| GET | /retention-policies | Get all policies |
| GET | /retention-policies/{id} | Get policy by ID |
| POST | /retention-policies | Create policy |
| PUT | /retention-policies/{id} | Update policy |
| GET | /api/audit-logs | Get all audit logs |
| GET | /api/audit-logs/{id} | Get audit log by ID |
| POST | /api/audit-logs | Create audit log |

### User Management APIs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/roles | Get all roles |
| GET | /api/roles/{id} | Get role by ID |
| POST | /api/roles | Create role |
| PUT | /api/roles/{id} | Update role |
| DELETE | /api/roles/{id} | Delete role |
| GET | /api/notifications | Get all notifications |
| POST | /api/notifications | Create notification |
| PUT | /api/notifications/{id}/read | Mark as read |

### Usage Tracking APIs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/usage | Get all usage records |
| GET | /api/usage/{id} | Get usage record |
| POST | /api/usage | Create usage record |

## Database Schema

### Core Tables

**Users**
- UserID (PK), Email, PasswordHash, RoleID (FK)
- Created, LastLogin

**Roles**
- RoleID (PK), RoleName, Permissions, Status

**Customers**
- CustomerID (PK), Name, Type, KYCStatus, ContactInfo
- Created, Updated

**Subscribers**
- SubscriberID (PK), CustomerID (FK), MSISDN, IMSI
- PlanType, Status, ActivationDate, DeviceId

**Products**
- ProductID (PK), Name, Category, PriceModel, Status

**Orders**
- OrderID (PK), ProductID (FK), SubscriberID (FK)
- OrderDate, Status, TotalAmount

**NetworkResources**
- ResourceID (PK), NetworkResourceType, Location
- Capacity, Status

**ProvisioningTasks**
- TaskID (PK), OrderID (FK), SubscriberID (FK)
- Status, ScheduledDate, CompletedDate

**Invoices**
- InvoiceID (PK), SubscriberID (FK), OrderID (FK)
- Amount, DueDate, IssueDate, Status

**Payments**
- PaymentID (PK), InvoiceID (FK), Amount
- PaymentDate, PaymentMethod, Status

**Alarms**
- AlarmID (PK), AlarmType, Severity, Status
- Timestamp, Description

**Incidents**
- IncidentID (PK), AlarmID (FK), Priority, Status
- AssignedTo, CreatedAt, ResolvedAt

**UsageRecords**
- UsageID (PK), SubscriberID (FK), DataUsed
- VoiceMinutes, SMSCount, UsageDate

**KPIReports**
- ReportID (PK), ReportType, Scope
- Metrics (JSON), GeneratedAt

**AnalyticsDatasets**
- DatasetID (PK), DatasetName, DataSource
- Schema (JSON), LastRefreshed

**ComplianceReports**
- ReportID (PK), ReportType, GeneratedDate
- DataRetention, ComplianceStatus

**RetentionPolicies**
- PolicyID (PK), PolicyName, RetentionPeriodDays
- DataCategory, Status

**AuditLogs**
- LogID (PK), UserID (FK), Action, Timestamp
- EntityType, EntityID

**Notifications**
- NotificationID (PK), SubscriberID (FK), Message
- Status, CreatedAt, SentAt

**Dashboards**
- DashboardID (PK), DashboardName, WidgetConfig
- CreatedBy, LastUpdated

## Project Structure

```
Telecomm360/
├── Controllers/              # 20 API Controllers
│   ├── AuthController.cs
│   ├── CustomerController.cs
│   ├── ProductController.cs
│   ├── OrderController.cs
│   └── ...
├── Service/
│   ├── Interface/           # Service contracts
│   └── Implementation/      # Business logic
├── Repository/
│   ├── Interface/           # Repository contracts
│   └── Implementation/      # Data access
├── Models/                  # 20 Domain entities
├── DTO/                     # Data transfer objects
├── Data/
│   └── AppDbContext.cs      # EF Core context
├── Constants/
│   └── ErrorMessages.cs     # Centralized messages
├── Program.cs               # Application startup
└── appsettings.json         # Configuration

Telecomm360.Tests/          # NUnit test project
├── Service/                 # Service unit tests
├── Controller/              # Controller unit tests
└── Repository/              # Repository unit tests
```

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server LocalDB
- Visual Studio 2022 or VS Code

### Setup Instructions

1. **Clone the repository**
```bash
git clone <repository-url>
cd Telecomm360
```

2. **Update database connection**
Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Telecomm360DB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

3. **Create database**
```bash
dotnet ef database update
```

4. **Seed initial data** (Execute in SQL Server)
```sql
-- Seed Roles
INSERT INTO Roles (RoleName, Permissions, Status) VALUES 
('Admin', 'All', 'Active'),
('Operator', 'Read,Write', 'Active'),
('User', 'Read', 'Active');

-- Seed Admin User
INSERT INTO Users (Email, PasswordHash, RoleID, Created) VALUES
('admin@test.com', '<hashed-password>', 1, GETDATE());

-- Seed Sample Customer
INSERT INTO Customers (Name, Type, KYCStatus, ContactInfo) VALUES
('Acme Corp', 'Enterprise', 'Verified', '{"phone":"123456"}');

-- Seed Products
INSERT INTO Products (Name, Category, PriceModel, Status) VALUES
('5G Plan', 'Mobile', 'Postpaid', 'Active'),
('Fiber Broadband', 'Internet', 'Subscription', 'Active');
```

5. **Run the application**
```bash
dotnet run
```

Application runs at: `http://localhost:5174`

6. **Access Swagger UI**
Navigate to: `http://localhost:5174/swagger`

### Test the API

**Login to get JWT token:**
```bash
curl -X POST http://localhost:5174/api/auth/login ^
  -H "Content-Type: application/json" ^
  -d "{\"email\":\"admin@test.com\",\"password\":\"Admin123!\"}"
```

**Use token in subsequent requests:**
```bash
curl -X GET http://localhost:5174/api/customers ^
  -H "Authorization: Bearer <your-jwt-token>"
```

## Running Tests

```bash
cd Telecomm360.Tests
dotnet test
```

All 208 unit tests should pass with 0 failures.

## Key Features

### 1. Role-Based Access Control (RBAC)
- Users assigned to roles (Admin, Operator, Analyst, Manager, Support, User)
- JWT tokens include role claims
- Permissions enforced at service layer

### 2. Comprehensive Audit Trail
- All critical actions logged to AuditLogs
- Track user, action, entity type, entity ID, timestamp
- Queryable for compliance and forensics

### 3. Flexible Update Operations
- DTOs support partial updates with nullable fields
- Null-coalescing pattern: `entity.Field = dto.Field ?? entity.Field`
- Only provided fields are updated

### 4. Entity Framework Change Tracking
- Repositories fetch entities, modify properties
- EF Core tracks changes automatically
- Prevents tracking conflicts

### 5. Notification System
- Automated notifications for events
- Subscriber-linked notifications
- Status tracking (Pending, Sent, Read)

### 6. Network Resource Management
- Track capacity and availability
- Provision resources for orders
- Monitor utilization

### 7. Incident Management
- Convert alarms to incidents
- Priority-based assignment
- Track resolution lifecycle

### 8. Usage-Based Billing
- Track data, voice, SMS usage
- Generate invoices based on consumption
- Support multiple pricing models

### 9. Analytics & KPI Reporting
- Aggregate operational data
- Calculate business metrics
- Scope-based reports (Network, Finance, Operations)

### 10. Compliance Management
- Data retention policies
- Compliance report generation
- GDPR/regulatory support

## Configuration

### JWT Settings (appsettings.json)
```json
{
  "Jwt": {
    "Key": "ThisIsASecretKeyForJWTTokenGenerationWithAtLeast32Characters",
    "ExpiryInHours": 3
  }
}
```

### Database Connection
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Telecomm360DB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### CORS Policy
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

## Common Patterns

### Controller Pattern
```csharp
[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{
    private readonly IExampleService _service;
    
    public ExampleController(IExampleService service)
    {
        _service = service;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequest(ErrorMessages.INVALID_ID);
            
        var result = await _service.GetByIdAsync(id);
        
        if (result == null)
            return NotFound(ErrorMessages.NOT_FOUND);
            
        return Ok(result);
    }
}
```

### Service Pattern
```csharp
public class ExampleService : IExampleService
{
    private readonly IExampleRepository _repository;
    
    public ExampleService(IExampleRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ExampleDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity?.ToDto();
    }
}
```

### Repository Pattern
```csharp
public class ExampleRepository : IExampleRepository
{
    private readonly AppDbContext _context;
    
    public ExampleRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Example> GetByIdAsync(int id)
    {
        return await _context.Examples.FindAsync(id);
    }
}
```

## Error Handling

All errors return consistent JSON format:
```json
{
  "message": "Error description",
  "statusCode": 400
}
```

Common status codes:
- 200: Success
- 201: Created
- 400: Bad Request (validation errors)
- 404: Not Found
- 500: Internal Server Error

## Security Best Practices

1. **Password Hashing**: Passwords hashed before storage
2. **JWT Tokens**: Stateless authentication with expiry
3. **Input Validation**: DTOs validate all input
4. **SQL Injection Prevention**: EF Core parameterized queries
5. **HTTPS**: Use HTTPS in production
6. **CORS**: Configure appropriate CORS policies

## Performance Considerations

1. **Async/Await**: All I/O operations are asynchronous
2. **EF Core Tracking**: Only track entities being modified
3. **Pagination**: Implement for large result sets
4. **Caching**: Consider Redis for frequently accessed data
5. **Connection Pooling**: Built into EF Core

## Troubleshooting

### Build Errors (MSB3027)
Kill process before rebuild:
```bash
taskkill /F /PID <process-id>
dotnet build
```

### Database Connection Issues
Verify LocalDB is running:
```bash
sqllocaldb info
sqllocaldb start mssqllocaldb
```

### JWT Authentication Failures
- Verify token not expired (3-hour limit)
- Check Authorization header format: `Bearer <token>`
- Ensure JWT key matches appsettings.json

### Foreign Key Violations
Ensure parent entities exist before creating child:
- Customer before Subscriber
- Product before Order
- Order before ProvisioningTask
- Subscriber before Invoice

## Contributing
1. Create feature branch from main
2. Write unit tests for new features
3. Ensure all tests pass (208/208)
4. Update documentation
5. Submit pull request

## License
Proprietary - All rights reserved

## Contact & Support
For technical questions or issues, contact the development team.

---

**Last Updated**: 2024  
**Version**: 1.0.0  
**Status**: Production Ready
