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
