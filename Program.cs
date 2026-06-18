using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.Repository;
using Telecomm360.Repository.Interfaces;
using Telecomm360.Services;
using Telecomm360.Services.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUsageRecordRepository, UsageRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddScoped<IUsageRecordService, UsageService>();
builder.Services.AddScoped<IInvoiceServices, InvoiceServices>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IKPIReportRepository, KPIReportRepository>();
builder.Services.AddScoped<IAnalyticsDatasetRepository, AnalyticsDatasetRepository>();

builder.Services.AddScoped<IKPIReportService, KPIReportService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();


builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
using Telecomm360.Service.Interface;
using Telecomm360.Service;
using System.Text.Json.Serialization;
using Telecomm360.Repository.Interface;
using Telecomm360.Repository.Implementation;
using Telecomm360.Service.Implementation;
using Telecom360.Repository.Interface;
using Telecom360.Repository.Implementation;
using Telecom360.Services.Interface;
using Telecom360.Services.Implementation;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Telecomm360.Repositories.Implementation;
using Telecomm360.Repositories.Interface;
using Telecomm360.Services.Implementation;
using Telecomm360.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Telecomm360.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });
// ✅ Add Controllers
builder.Services.AddControllers();

// ✅ Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ✅ Dependency Injection - Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IComplianceReportRepository, ComplianceReportRepository>();
builder.Services.AddScoped<IRetentionPolicyRepository, RetentionPolicyRepository>();


// ✅ Dependency Injection - Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IComplianceReportService, ComplianceReportService>();
builder.Services.AddScoped<IRetentionPolicyService, RetentionPolicyService>();

// 3. Dependency Injection: Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAlarmRepository, AlarmRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();

// 4. Dependency Injection: Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAlarmService, AlarmService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();

// Dependency Injection for Repositories and Services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();
builder.Services.AddScoped<INetworkResourceRepository, NetworkResourceRepository>();
builder.Services.AddScoped<INetworkResourceService, NetworkResourceService>();
builder.Services.AddScoped<IProvisioningTaskRepository, ProvisioningTaskRepository>();
builder.Services.AddScoped<IProvisioningTaskService, ProvisioningTaskService>();


var app = builder.Build();


// ✅ Middleware Pipeline

// 1. Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Configure JWT Authentication Rules
var jwtKey = builder.Configuration["Jwt:Key"] ?? "YourSuperSecretBackupKeyThatIsVeryLong123!";
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 5. 🛠️ STRICT .NET 10 SWAGGER CONFIGURATION
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Telecomm360 API", Version = "v1" });

    // Step A: Define the security scheme natively
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\nJust paste your token directly into the box (Swagger will automatically add the word 'Bearer' for you).",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    // Step B: Use the strict .NET 10 Func delegate (document =>) and Reference mapping
    c.AddSecurityRequirement(document => 
    {
        return new OpenApiSecurityRequirement
        {
            { 
                new OpenApiSecuritySchemeReference("Bearer", document), 
                new List<string>() 
            }
        };
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Middleware configuration and HTTP request pipeline setup

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();


// ================== RUN ==================
app.Run();
