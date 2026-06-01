using Telecom360.Repository.Interface;
using Telecom360.Repository.Implementation;
using Telecom360.Services.Interface;
using Telecom360.Services.Implementation;
using Telecom360.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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


var app = builder.Build();


// ✅ Middleware Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
