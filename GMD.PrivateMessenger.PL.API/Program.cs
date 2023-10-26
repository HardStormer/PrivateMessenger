using AutoMapper.Extensions.ExpressionMapping;
using FluentValidation.AspNetCore;
using GMD.PrivateMessenger.PL.API.Authentication;
using GMD.PrivateMessenger.PL.API.Common.DependencyInjection;
using GMD.PrivateMessenger.PL.API.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using GMD.PrivateMessenger.PL.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.InjectRepositories();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Private Messanger Api"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Authorization",
              In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContextFactory<GMD.PrivateMessenger.DAL.MSSQL.Contexts.BaseDbContext>(
    (DbContextOptionsBuilder options) => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnStr")));

builder.Services.AddFluentValidationAutoValidation(cfg =>
{
    cfg.DisableDataAnnotationsValidation = true;
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddExpressionMapping();
}, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(DefaultApiAuthenticationOptions.DefaultScheme).AddDefaultApiAuthentication();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{ 
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSignalR();
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseReDoc(c =>
	{
		c.DocumentTitle = "REDOC API Documentation";
		c.SpecUrl = "/swagger/v1/swagger.json";
	});
}
else if (app.Environment.IsProduction())
{
	app.UseSwagger();
    app.UseSwaggerUI();
    app.UseReDoc(c =>
    {
        c.DocumentTitle = "REDOC API Documentation";
        c.SpecUrl = "/swagger/v1/swagger.json";
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MessageHub>("/messageHub");
});

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();


app.MapControllers();

app.UseCors("default");


app.Run();
