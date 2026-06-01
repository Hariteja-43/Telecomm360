using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;

//  Repository + Service
using Telecomm360.Repository;
using Telecomm360.Service.Interface;
using Telecomm360.Service;

using System.Text.Json.Serialization;
using Telecomm360.Repository.Interface;
using Telecomm360.Repository.Implementation;
using Telecomm360.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


// Dependency Injection for Repositories and Services

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();

builder.Services.AddScoped<INetworkResourceRepository, NetworkResourceRepository>();
builder.Services.AddScoped<INetworkResourceService, NetworkResourceService>();

// 
builder.Services.AddScoped<IProvisioningTaskRepository, Telecomm360.Repository.ProvisioningTaskRepository>();
builder.Services.AddScoped<IProvisioningTaskService, ProvisioningTaskService>();


//  Builder is used to configure services and the app is used to configure the HTTP request pipeline.
var app = builder.Build();


// Middleware configuration and HTTP request pipeline setup

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


// ================== RUN ==================
app.Run();