using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using POS.API.Models;
using System.Net;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

#region Linux Config

OperatingSystem os = Environment.OSVersion;
string osPlatorm = os.Platform.ToString();
if (!osPlatorm.Contains("win", StringComparison.CurrentCultureIgnoreCase))
    _ = builder.WebHost
        .UseKestrel(options =>
        {
            options.Listen(IPAddress.Any, 80);
            options.Listen(IPAddress.Any, 443);
        })
        .UseContentRoot(Directory.GetCurrentDirectory());

#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7019",
                                              "https://localhost:7260")
                                .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                                .AllowAnyHeader();
                      });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// CORS
app.UseCors("AllowedCorsOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
