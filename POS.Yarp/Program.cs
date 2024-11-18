using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services
   .AddReverseProxy()
   .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 3;
    });
});

builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("customPolicy", builder => builder.Expire(TimeSpan.FromSeconds(20)));
});

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

var app = builder.Build();

app.UseCors("MyAllowSpecificOrigins");

app.UseRateLimiter();

app.UseOutputCache();

app.MapReverseProxy();

app.Run();
