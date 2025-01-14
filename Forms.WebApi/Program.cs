using Forms.Repository.Analytics;
using Forms.Repository.Auth;
using Forms.Repository.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Forms.Repository.EmailService;
using System.Reflection;
using Forms.Repository.Survey;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    // Define the Bearer authentication scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Description = "Enter 'Bearer' followed by your token. Example: 'Bearer your_token_here'",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath); 

    // Apply the security scheme globally
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddSingleton<IFirebaseConfig, FirebaseConfig>();
builder.Services.AddScoped<IFirebaseAuthRepository, FirebaseAuthRepository>();
builder.Services.AddTransient<ISurveyRepository, SurveyRepository>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddScoped<IEmailServiceRepository,EmailServiceRepository>();
builder.Services.AddScoped<IQRCodeServiceRepository, QRCodeServiceRepository>();
builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();

// Configure JWT Bearer authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Default scheme
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Default challenge scheme
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; 
    options.SaveToken = true;

    // Firebase token validation parameters
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,  
        ValidateAudience = false, 
        ValidateLifetime = true
    };
});

// CORS config
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();

// HTTPS Redirection and Authentication
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();  

app.MapControllers();

app.Run();
