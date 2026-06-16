using Dgii.Api.Middleware;
using Dgii.Application.Interfaces;
using Dgii.Application.Services;
using Dgii.Infrastructure.Data;
using Dgii.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters(); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITaxpayerRepository, TaxpayerRepository>();
builder.Services.AddScoped<ITaxReceiptRepository, TaxReceiptRepository>();
builder.Services.AddScoped<ITaxpayerService, TaxpayerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
