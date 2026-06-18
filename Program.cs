using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Telecom360.Middleware;
using Telecom360.Repository.Implementation;
using Telecom360.Repository.Interface;
using Telecom360.Service.Implementation;
using Telecom360.Service.Interface;
using Telecomm360.Data;
using Telecomm360.Repositories.Implementation;
using Telecomm360.Repositories.Interface;
using Telecomm360.Repository;
using Telecomm360.Repository.Implementation;
using Telecomm360.Repository.Interface;
using Telecomm360.Repository.Interfaces;
using Telecomm360.Service.Implementation;
using Telecomm360.Service.Interface;
using Telecomm360.Service.Interfaces;


// ✅ BUILD
var builder = WebApplication.CreateBuilder(args);


// ================== SERVICES ==================

// ✅ Controllers + Enum support
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });

// ✅ Open API
builder.Services.AddOpenApi();

// ✅ Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// ================== DEPENDENCY INJECTION ==================

// ✅ Billing / Usage
builder.Services.AddScoped<IUsageRecordRepository, UsageRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddScoped<IUsageRecordService, UsageService>();
builder.Services.AddScoped<IInvoiceServices, InvoiceServices>();
builder.Services.AddScoped<IPaymentService, PaymentService>();


// ✅ Analytics
builder.Services.AddScoped<IKPIReportRepository, KPIReportRepository>();
builder.Services.AddScoped<IAnalyticsDatasetRepository, AnalyticsDatasetRepository>();

builder.Services.AddScoped<IKPIReportService, KPIReportService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();


// ✅ Product / Order / Compliance / Retention
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IComplianceReportRepository, ComplianceReportRepository>();
builder.Services.AddScoped<IRetentionPolicyRepository, RetentionPolicyRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IComplianceReportService, ComplianceReportService>();
builder.Services.AddScoped<IRetentionPolicyService, RetentionPolicyService>();


// ✅ Identity / Audit / Notifications
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAlarmRepository, AlarmRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAlarmService, AlarmService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();


// ✅ Customer / Subscriber / Network
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();
builder.Services.AddScoped<INetworkResourceRepository, NetworkResourceRepository>();
builder.Services.AddScoped<INetworkResourceService, NetworkResourceService>();
builder.Services.AddScoped<IProvisioningTaskRepository, ProvisioningTaskRepository>();
builder.Services.AddScoped<IProvisioningTaskService, ProvisioningTaskService>();


// ================== AUTH ==================

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


// ================== SWAGGER ==================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Telecomm360 API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Paste JWT token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(document =>
    {
        return new OpenApiSecurityRequirement
        {
            { new OpenApiSecuritySchemeReference("Bearer", document), new List<string>() }
        };
    });
});


// ================== BUILD APP ==================

var app = builder.Build();


// ================== MIDDLEWARE ==================

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