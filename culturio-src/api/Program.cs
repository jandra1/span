using Culturio.API.Filters;
using Culturio.Application;
using Culturio.Application.Persistence;
using Culturio.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilters>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowCors",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials();
                      });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication(builder.Configuration);






JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
// Adds Microsoft Identity platform (AAD v2.0) support to protect this Api
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options =>
        {
            builder.Configuration.Bind("AzureAdB2C", options);

            options.TokenValidationParameters.NameClaimType = "name";
        },
options => { builder.Configuration.Bind("AzureAdB2C", options); });

// Creating policies that wraps the authorization requirements
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireScope("tasks.read", "tasks.write")
    .Build();
});
builder.Services.AddScoped<MsGraphService>();





builder.Services.AddApplication(builder.Configuration);

builder.Services.AddDbContext<CulturioDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CulturioDb"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowCors");

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var errorResponse = new ProblemDetails
        {
            Title = "Unexpected error has happened",
            Detail = "Unexpected error has happened. Please contact system administrator and provide them with TraceId found in this response",
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };
        errorResponse.Extensions.Add("traceId", Activity.Current?.Id ?? context.TraceIdentifier);

        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();